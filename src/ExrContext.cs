using System;
using System.Buffers;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using OpenEXR;
using OpenEXR.Interop;
using Stride.Core.Mathematics;
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
}

public unsafe class ExrPart(ExrContext context, int partIndex)
{
    exr_context_t* Handle => context.handle;
    int PartIndex => partIndex;

    public Rectangle GetDataWindow()
    {
        exr_attr_box2i_t box;
        exr_get_data_window(Handle, PartIndex, &box).ThrowIfError();
        return new Rectangle(box.min.x, box.min.y, box.max.x - box.min.x + 1, box.max.y - box.min.y + 1);
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

    public exr_chunk_info_t ReadScanlineChunkInfo(int y)
    {
        exr_chunk_info_t result;
        exr_read_scanline_chunk_info(Handle, PartIndex, y, &result).ThrowIfError();
        return result;
    }

    public IMemoryOwner<T> Decode<T>(ExrChannel[] selectedChannels) where T : unmanaged, IElement<T>
    {
        var storage = GetStorage();
        if (storage != exr_storage_t.EXR_STORAGE_SCANLINE)
            throw new NotSupportedException($"Unsupported storage type: {storage}");

        var window = GetDataWindow();
        var memoryOwner = MemoryPool<T>.Shared.Rent(window.Width * window.Height * selectedChannels.Length);
        fixed (T* data = memoryOwner.Memory.Span)
        {
            Decode(data, window, selectedChannels);
        }
        return memoryOwner;
    }

    private void Decode<T>(T* data, Rectangle window, ExrChannel[] selectedChannels)
        where T : unmanaged, IElement<T>
    {
        var scanlines = GetScanlinesPerChunk();
        var elementCount = selectedChannels.Length;

        var chunkInfo = ReadScanlineChunkInfo(window.Y);

        Parallel.ForEach(
            source: Partitioner.Create(window.Y, window.Y + window.Height, scanlines),
            localInit: () =>
            {
                var d = Marshal.AllocHGlobal(sizeof(exr_decode_pipeline_t));
                var ci = chunkInfo;
                var decoder = (exr_decode_pipeline_t*)d;
                exr_decoding_initialize(Handle, PartIndex, &ci, decoder).ThrowIfError();
                exr_decoding_choose_default_routines(Handle, PartIndex, decoder).ThrowIfError();
                return d;
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

                exr_decoding_update(Handle, PartIndex, &chunkInfo, decoder).ThrowIfError();

                for (int c = 0; c < decoder->channel_count; c++)
                {
                    ref var channel = ref decoder->channels[c];
                    var i = GetElementIndex(selectedChannels, c);
                    if (i < 0)
                    {
                        channel.decode_to_ptr = null;
                        continue;
                    }

                    channel.user_bytes_per_element = (short)sizeof(T);
                    channel.user_data_type = (ushort)T.PixelType;
                    channel.user_pixel_stride = elementCount * sizeof(T);
                    channel.user_line_stride = window.Width * channel.user_pixel_stride;
                    channel.decode_to_ptr = (byte*)(dstData0 + i);
                }

                // Fill remaining channels with ones
                for (int c = 0; c < selectedChannels.Length; c++)
                {
                    if (selectedChannels[c].Index >= 0)
                        continue;

                    for (var dst = dstData0 + c; dst < dstData1; dst += elementCount)
                        *dst = T.One;
                }

                exr_decoding_run(Handle, PartIndex, decoder).ThrowIfError();

                return d;
            },
            localFinally: d =>
            {
                var decoder = (exr_decode_pipeline_t*)d;
                exr_decoding_destroy(Handle, decoder).ThrowIfError();
                Marshal.FreeHGlobal(d);
            });

        static int GetElementIndex(ExrChannel[] channels, int index)
        {
            for (int i = 0; i < channels.Length; i++)
            {
                if (channels[i].Index == index)
                    return i;
            }
            return -1;
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

public interface IElement<T> where T : unmanaged, IElement<T>
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