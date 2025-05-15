using System;
using System.Buffers;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;
using OpenEXR;
using OpenEXR.Interop;
using Stride.Core.Mathematics;
using VL.Core;
using VL.Core.Utils;
using static OpenEXR.Interop.exr;

namespace VL.OpenEXR;

public unsafe class ExrContext : SafeHandle
{
    public static ExrContext OpenRead(string path)
    {
        using var marshaledPath = new MarshaledString(path);
        exr_context_t* handle;
        exr_start_read(&handle, marshaledPath.Value, null).ThrowIfError();
        return new ExrContext(handle, true);
    }

    public static ExrContext OpenWrite(string path, exr_default_write_mode_t mode)
    {
        using var marshaledPath = new MarshaledString(path);
        exr_context_t* handle;
        exr_start_write(&handle, marshaledPath.Value, mode, null).ThrowIfError();
        return new ExrContext(handle, true);
    }

    private ExrContext(exr_context_t* handle, bool ownsHandle) : base(0, ownsHandle)
    {
        SetHandle((nint)handle);
    }

    public override bool IsInvalid => handle == default;

    protected override bool ReleaseHandle()
    {
        var handle = this.handle;
        return exr_finish(&handle) == 0;
    }

    internal new exr_context_t* handle => (exr_context_t*)base.handle;

    public int Count
    {
        get
        {
            int result;
            exr_get_count(handle, &result).ThrowIfError();
            return result;
        }
    }

    public ExrPart GetPart(int index)
    {
        if (index < 0 || index >= Count)
            throw new ArgumentOutOfRangeException(nameof(index), $"Part index {index} is out of range [0, {Count})");
        return new ExrPart(this, index);
    }

    public ExrPart AddPart(string name, exr_storage_t storageType)
    {
        int partIndex;
        using var marshaledName = new MarshaledString(name);
        exr_add_part(handle, marshaledName.Value, storageType, &partIndex).ThrowIfError();
        return new ExrPart(this, partIndex);
    }
}

public unsafe class ExrPart(ExrContext context, int partIndex)
{
    exr_context_t* Handle => context.handle;
    int PartIndex => partIndex;

    public Rectangle GetDataWindow()
    {
        exr_attr_box2i_t box;
        exr_get_data_window(Handle, PartIndex, &box).ThrowIfError();
        return box.ToRectangle();
    }

    public Rectangle GetDisplayWindow()
    {
        exr_attr_box2i_t box;
        exr_get_display_window(Handle, PartIndex, &box).ThrowIfError();
        return box.ToRectangle();
    }

    public exr_storage_t GetStorage()
    {
        exr_storage_t result;
        exr_get_storage(Handle, PartIndex, &result).ThrowIfError();
        return result;
    }

    public int GetChannelCount()
    {
        exr_attr_chlist_t* channels;
        exr_get_channels(Handle, PartIndex, &channels).ThrowIfError();
        return channels->num_channels;
    }

    public unsafe exr_pixel_type_t GetPixelType(int channelIndex)
    {
        exr_attr_chlist_t* channels;
        exr_get_channels(Handle, PartIndex, &channels).ThrowIfError();
        if (channelIndex < 0 || channelIndex >= channels->num_channels)
            throw new ArgumentOutOfRangeException(nameof(channelIndex), $"Channel index {channelIndex} is out of range [0, {channels->num_channels})");
        var channel = channels->entries[channelIndex];
        return channel.pixel_type;
    }

    public int GetScanlinesPerChunk()
    {
        int result;
        exr_get_scanlines_per_chunk(Handle, PartIndex, &result).ThrowIfError();
        return result;
    }

    public Int2 GetTileCount(Int2 level)
    {
        Int2 counts;
        exr_get_tile_counts(Handle, PartIndex, level.X, level.Y, &counts.X, &counts.Y).ThrowIfError();
        return counts;
    }

    public Size2 GetTileSizes(Int2 level)
    {
        Size2 size;
        exr_get_tile_sizes(Handle, PartIndex, level.X, level.Y, &size.Width, &size.Height).ThrowIfError();
        return size;
    }

    public exr_chunk_info_t ReadScanlineChunkInfo(int y)
    {
        exr_chunk_info_t result;
        exr_read_scanline_chunk_info(Handle, PartIndex, y, &result).ThrowIfError();
        return result;
    }

    public exr_chunk_info_t ReadTileChunkInfo(Int2 tile, Int2 level = default)
    {
        exr_chunk_info_t result;
        exr_read_tile_chunk_info(Handle, PartIndex, tile.X, tile.Y, level.X, level.Y, &result).ThrowIfError();
        return result;
    }

    internal IMemoryOwner<T> Decode<T>(ExrChannel[] selectedChannels, Rectangle displayWindow) where T : unmanaged, IElement<T>
    {
        MemoryOwner<T> chunkMemory;
        Rectangle chunkWindow;

        var dataWindow = GetDataWindow();
        var storage = GetStorage();
        if (storage == exr_storage_t.EXR_STORAGE_SCANLINE)
        {
            var c0 = ReadScanlineChunkInfo(displayWindow.Top);
            var c1 = ReadScanlineChunkInfo(Math.Max(displayWindow.Top, displayWindow.Bottom - 1));
            chunkWindow = new Rectangle(c0.start_x, c0.start_y, c0.width, (c1.idx - c0.idx) * c0.height + c1.height);

            chunkMemory = MemoryOwner<T>.Allocate(chunkWindow.Width * chunkWindow.Height * selectedChannels.Length);
            fixed (T* data = chunkMemory.Span)
            {
                DecodeScanlines(data, chunkWindow, selectedChannels);
            }
        }
        else if (storage == exr_storage_t.EXR_STORAGE_TILED)
        {
            var tileLevel = new Int2(0, 0);
            var tileCount = GetTileCount(tileLevel);
            var tileSize = GetTileSizes(tileLevel);

            using var tiles = Pooled.GetList<Int2>();
            chunkWindow = Rectangle.Empty;
            for (int y = 0; y < tileCount.Y; y++)
            {
                for (int x = 0; x < tileCount.X; x++)
                {
                    var tileRect = new Rectangle(x * tileSize.Width + dataWindow.X, y * tileSize.Height + dataWindow.Y, tileSize.Width, tileSize.Height);
                    if (displayWindow.Intersects(tileRect))
                    {
                        tiles.Add(new Int2(x, y));
                        chunkWindow = Rectangle.Union(chunkWindow, tileRect);
                    }
                }
            }

            chunkWindow = Rectangle.Intersect(chunkWindow, dataWindow);
            chunkMemory = MemoryOwner<T>.Allocate(chunkWindow.Width * chunkWindow.Height * selectedChannels.Length);
            fixed (T* data = chunkMemory.Span)
            {
                DecodeTiles(data, tiles.Value, chunkWindow, tileLevel, selectedChannels);
            }
        }
        else
        {
            throw new NotImplementedException($"Storage type {storage} is not supported.");
        }

        if (displayWindow != dataWindow)
        {
            // Slice it accordingly
            using (chunkMemory)
            {
                var displayMemory = MemoryOwner<T>.Allocate(displayWindow.Width * displayWindow.Height * selectedChannels.Length);
                chunkMemory.Span
                    .AsSpan2D(
                        height: chunkWindow.Height, 
                        width: chunkWindow.Width * selectedChannels.Length)
                    .Slice(
                        row: displayWindow.Y - chunkWindow.Y,
                        column: (displayWindow.X - chunkWindow.X) * selectedChannels.Length,
                        height: displayWindow.Height,
                        width: displayWindow.Width * selectedChannels.Length)
                    .CopyTo(displayMemory.Span);
                return displayMemory;
            }
        }

        return chunkMemory;
    }

    private void DecodeScanlines<T>(T* data, Rectangle window, ExrChannel[] selectedChannels)
        where T : unmanaged, IElement<T>
    {
        var scanlines = GetScanlinesPerChunk();
        var elementCount = selectedChannels.Length;

        Parallel.ForEach(
            source: Partitioner.Create(window.Y, window.Y + window.Height, scanlines),
            localInit: () =>
            {
                var decoder = (exr_decode_pipeline_t*)Marshal.AllocHGlobal(sizeof(exr_decode_pipeline_t));
                decoder->channels = null;
                return new nint(decoder);
            },
            body: (t, s, d) =>
            {
                var decoder = (exr_decode_pipeline_t*)d;
                var (y0, y1) = t;
                var dstY0 = y0 - window.Y;
                var dstY1 = y1 - window.Y;
                var dstData0 = data + dstY0 * window.Width * elementCount;
                var dstData1 = data + dstY1 * window.Width * elementCount;

                var chunkInfo = ReadScanlineChunkInfo(y0);
                if (decoder->channels is null)
                {
                    exr_decoding_initialize(Handle, PartIndex, &chunkInfo, decoder).ThrowIfError();

                    for (int i = 0; i < selectedChannels.Length; i++)
                    {
                        var c = selectedChannels[i];
                        if (c.Index < 0)
                            continue;

                        ref var channel = ref decoder->channels[c.Index];
                        channel.user_bytes_per_element = (short)sizeof(T);
                        channel.user_data_type = (ushort)T.PixelType;
                        channel.user_pixel_stride = elementCount * sizeof(T);
                        channel.user_line_stride = window.Width * channel.user_pixel_stride;
                    }

                    exr_decoding_choose_default_routines(Handle, PartIndex, decoder).ThrowIfError();
                }
                else
                {
                    exr_decoding_update(Handle, PartIndex, &chunkInfo, decoder).ThrowIfError();
                }

                for (int i = 0; i < selectedChannels.Length; i++)
                {
                    var c = selectedChannels[i];
                    if (c.Index >= 0)
                    {
                        ref var channel = ref decoder->channels[c.Index];
                        channel.decode_to_ptr = (byte*)(dstData0 + i);
                    }
                    else
                    {
                        // Fill channels not present in file with ones
                        for (var dst = dstData0 + i; dst < dstData1; dst += elementCount)
                            *dst = T.One;
                    }
                }

                exr_decoding_run(Handle, PartIndex, decoder).ThrowIfError();

                return d;
            },
            localFinally: d =>
            {
                var decoder = (exr_decode_pipeline_t*)d;
                if (decoder->channels != null)
                    exr_decoding_destroy(Handle, decoder).ThrowIfError();
                Marshal.FreeHGlobal(d);
            });
    }

    private void DecodeTiles<T>(T* data, List<Int2> tiles, Rectangle window, Int2 level, ExrChannel[] selectedChannels)
        where T : unmanaged, IElement<T>
    {
        var elementCount = selectedChannels.Length;
        var tileSize = GetTileSizes(level);

        Parallel.ForEach(
            source: tiles,
            localInit: () =>
            {
                var decoder = (exr_decode_pipeline_t*)Marshal.AllocHGlobal(sizeof(exr_decode_pipeline_t));
                decoder->channels = null;
                return new nint(decoder);
            },
            body: (tile, s, d) =>
            {
                var decoder = (exr_decode_pipeline_t*)d;
                var location = new Int2(tile.X * tileSize.Width, tile.Y * tileSize.Height);
                var lineStride = window.Width * elementCount;
                var dstData0 = data + location.Y * lineStride + location.X * elementCount;

                var chunkInfo = ReadTileChunkInfo(tile, level);
                if (decoder->channels is null)
                {
                    exr_decoding_initialize(Handle, PartIndex, &chunkInfo, decoder).ThrowIfError();

                    for (int i = 0; i < selectedChannels.Length; i++)
                    {
                        var c = selectedChannels[i];
                        if (c.Index < 0)
                            continue;

                        ref var channel = ref decoder->channels[c.Index];
                        channel.user_bytes_per_element = (short)sizeof(T);
                        channel.user_data_type = (ushort)T.PixelType;
                        channel.user_pixel_stride = elementCount * sizeof(T);
                        channel.user_line_stride = window.Width * channel.user_pixel_stride;
                    }

                    exr_decoding_choose_default_routines(Handle, PartIndex, decoder).ThrowIfError();
                }
                else
                {
                    exr_decoding_update(Handle, PartIndex, &chunkInfo, decoder).ThrowIfError();
                }

                for (int i = 0; i < selectedChannels.Length; i++)
                {
                    var c = selectedChannels[i];
                    if (c.Index >= 0)
                    {
                        ref var channel = ref decoder->channels[c.Index];
                        channel.decode_to_ptr = (byte*)(dstData0 + i);
                    }
                    else
                    {
                        // Fill channels not present in file with ones
                        for (int y = 0; y < tileSize.Height; y++)
                        {
                            var rowStart = dstData0 + y * lineStride;
                            var rowEnd = rowStart + tileSize.Width * elementCount;
                            for (var dst = rowStart + i; dst < rowEnd; dst += elementCount)
                                *dst = T.One;
                        }
                    }
                }

                exr_decoding_run(Handle, PartIndex, decoder).ThrowIfError();

                return d;
            },
            localFinally: d =>
            {
                var decoder = (exr_decode_pipeline_t*)d;
                if (decoder->channels != null)
                    exr_decoding_destroy(Handle, decoder).ThrowIfError();
                Marshal.FreeHGlobal(d);
            });
    }

    internal void Encode<T>(ReadOnlySpan2D<T> data, ReadOnlySpan<ExrChannel> channels, exr_compression_t compression, exr_storage_t storage)
         where T : unmanaged, IElement<T>
    {
        foreach (var channel in channels)
        {
            if (channel.Index < 0)
                continue;

            using var marshalledName = new MarshaledString(channel.Name);
            exr_add_channel(Handle, PartIndex, marshalledName.Value, T.PixelType, exr_perceptual_treatment_t.EXR_PERCEPTUALLY_LINEAR, 1, 1).ThrowIfError();
        }
        exr_set_compression(Handle, PartIndex, compression).ThrowIfError();

        var dataWindow = new Rectangle(0, 0, data.Width / channels.Length, data.Height);
        var dataWindowBox = exr_attr_box2i_t.FromRectangle(dataWindow);
        exr_set_data_window(Handle, PartIndex, &dataWindowBox).ThrowIfError();

        var displayWindow = new Rectangle(0, 0, data.Width / channels.Length, data.Height);
        var displayWindowBox = exr_attr_box2i_t.FromRectangle(displayWindow);
        exr_set_display_window(Handle, PartIndex, &displayWindowBox).ThrowIfError();

        exr_set_lineorder(Handle, PartIndex, exr_lineorder_t.EXR_LINEORDER_INCREASING_Y).ThrowIfError();
        exr_set_pixel_aspect_ratio(Handle, PartIndex, 1.0f).ThrowIfError();
        var screenWindowCenter = new exr_attr_v2f_t() { x = 0.0f, y = 0.0f };
        exr_set_screen_window_center(Handle, PartIndex, &screenWindowCenter).ThrowIfError();
        exr_set_screen_window_width(Handle, PartIndex, 1.0f).ThrowIfError();

        if (storage == exr_storage_t.EXR_STORAGE_SCANLINE)
            EncodeScanlines(data, channels, compression);
        else if (storage == exr_storage_t.EXR_STORAGE_TILED)
            EncodeTiles(data, channels, compression);
        else
            throw new NotImplementedException($"Storage type {storage} is not supported.");
    }

    private void EncodeScanlines<T>(ReadOnlySpan2D<T> data, ReadOnlySpan<ExrChannel> channels, exr_compression_t compression)
        where T : unmanaged, IElement<T>
    {
        var scanlines = GetScanlinesPerChunk();
        var chunkCount = (int)Math.Ceiling((float)data.Height / scanlines);
        exr_set_chunk_count(Handle, PartIndex, chunkCount).ThrowIfError();

        // TODO: What if we want to write multiple parts?
        exr_write_header(Handle).ThrowIfError();

        for (int y = 0; y < data.Height; y += scanlines)
        {
            exr_chunk_info_t ci;
            exr_write_scanline_chunk_info(Handle, PartIndex, y, &ci).ThrowIfError();

            exr_encode_pipeline_t encoder;
            exr_encoding_initialize(Handle, PartIndex, &ci, &encoder).ThrowIfError();

            for (int i = 0; i < channels.Length; i++)
            {
                var c = channels[i];
                if (c.Index < 0)
                    continue;

                ref var channel = ref encoder.channels[c.Index];
                channel.user_bytes_per_element = (short)sizeof(T);
                channel.user_data_type = (ushort)T.PixelType;
                channel.user_pixel_stride = channels.Length * sizeof(T);
                channel.user_line_stride = data.Width * sizeof(T);
                channel.encode_from_ptr = (byte*)Unsafe.AsPointer(ref Unsafe.AsRef(in data[y, i]));
            }

            exr_encoding_choose_default_routines(Handle, PartIndex, &encoder).ThrowIfError();
            exr_encoding_run(Handle, PartIndex, &encoder);
            exr_encoding_destroy(Handle, &encoder);
        }
    }

    private void EncodeTiles<T>(ReadOnlySpan2D<T> data, ReadOnlySpan<ExrChannel> channels, exr_compression_t compression)
        where T : unmanaged, IElement<T>
    {
        var width = data.Width / channels.Length;
        var tileSize = new Size2(64, 64);
        var tileCount = new Int2((int)Math.Ceiling((float)width / tileSize.Width), (int)Math.Ceiling((float)data.Height / tileSize.Height));
        var level = default(Int2);
        exr_set_tile_descriptor(Handle, PartIndex, (uint)tileSize.Width, (uint)tileSize.Height, exr_tile_level_mode_t.EXR_TILE_ONE_LEVEL, exr_tile_round_mode_t.EXR_TILE_ROUND_DOWN);
        //var chunkCount = (int)Math.Ceiling((float)data.Height / scanlines);
        //exr_set_chunk_count(Handle, PartIndex, chunkCount).ThrowIfError();

        // TODO: What if we want to write multiple parts?
        exr_write_header(Handle).ThrowIfError();

        for (int tileY = 0; tileY < tileCount.Y; tileY++)
        {
            for (int tileX = 0; tileX < tileCount.X; tileX++)
            {
                exr_chunk_info_t ci;
                exr_write_tile_chunk_info(Handle, PartIndex, tileX, tileY, level.X, level.Y, &ci).ThrowIfError();

                exr_encode_pipeline_t encoder;
                exr_encoding_initialize(Handle, PartIndex, &ci, &encoder).ThrowIfError();

                var x = tileX * tileSize.Width;
                var y = tileY * tileSize.Height;
                for (int i = 0; i < channels.Length; i++)
                {
                    var c = channels[i];
                    if (c.Index < 0)
                        continue;

                    ref var channel = ref encoder.channels[c.Index];
                    channel.user_bytes_per_element = (short)sizeof(T);
                    channel.user_data_type = (ushort)T.PixelType;
                    channel.user_pixel_stride = channels.Length * sizeof(T);
                    channel.user_line_stride = data.Width * sizeof(T);
                    channel.encode_from_ptr = (byte*)Unsafe.AsPointer(ref Unsafe.AsRef(in data[y, x + i]));
                }

                exr_encoding_choose_default_routines(Handle, PartIndex, &encoder).ThrowIfError();
                exr_encoding_run(Handle, PartIndex, &encoder);
                exr_encoding_destroy(Handle, &encoder);
            }
        }
    }

    public HashSet<string> GetLayers()
    {
        var layers = new HashSet<string>();
        var channels = GetChannels();
        foreach (var c in channels)
        {
            var l = c.Layer;
            if (l != string.Empty)
                layers.Add(l);
        }
        return layers;
    }

    public unsafe ExrChannel[] GetChannels()
    {
        exr_attr_chlist_t* channels;
        exr_get_channels(Handle, partIndex, &channels).ThrowIfError();
        var result = new ExrChannel[channels->num_channels];
        for (int i = 0; i < channels->num_channels; i++)
        {
            var channel = channels->entries[i];
            var name = new string(channel.name.str, 0, channel.name.length);
            result[i] = new ExrChannel(i, name, channel.pixel_type);
        }
        return result;
    }

    public ExrChannel[] GetChannels(string layer)
    {
        var channels = GetChannels();
        var result = new List<ExrChannel>();
        foreach (var c in channels)
        {
            if (c.Name.StartsWith(layer))
                result.Add(c);
        }
        return result.ToArray();
    }
}

interface IElement<T> where T : unmanaged, IElement<T>
{
    static abstract exr_pixel_type_t PixelType { get; }
    static abstract T One { get; }
}

record struct FloatElement(float Value) : IElement<FloatElement>
{
    public static exr_pixel_type_t PixelType => exr_pixel_type_t.EXR_PIXEL_FLOAT;
    public static FloatElement One => new FloatElement(1.0f);
}

record struct HalfElement(ushort Value) : IElement<HalfElement>
{
    public static exr_pixel_type_t PixelType => exr_pixel_type_t.EXR_PIXEL_HALF;
    public static HalfElement One => new HalfElement(0x3C00);
}

record struct UIntElement(uint Value) : IElement<UIntElement>
{
    public static exr_pixel_type_t PixelType => exr_pixel_type_t.EXR_PIXEL_UINT;
    public static UIntElement One => new UIntElement(1);
}

public record struct ExrChannel(int Index, string Name, exr_pixel_type_t PixelType)
{
    public string Layer
    {
        get
        {
            var index = Name.LastIndexOf('.');
            if (index > 0)
                return Name.Substring(0, index);
            return string.Empty;
        }
    }

    public string Suffix
    {
        get
        {
            var index = Name.LastIndexOf('.');
            if (index >= 0 && index < Name.Length)
                return Name.Substring(index + 1);
            return Name;
        }
    }

    public int Order
    {
        get
        {
            return Suffix switch
            {
                "R" => 0,
                "G" => 1,
                "B" => 2,
                "A" => 3,
                _ => int.MaxValue,
            };
        }
    }
}