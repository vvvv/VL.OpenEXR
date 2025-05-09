using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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

    public Rectangle GetDataWindow(int partIndex)
    {
        exr_attr_box2i_t box;
        exr_get_data_window(handle, partIndex, &box).ThrowIfError();
        return new Rectangle(box.min.x, box.min.y, box.max.x - box.min.x + 1, box.max.y - box.min.y + 1);
    }

    public exr_storage_t GetStorage(int partIndex)
    {
        exr_storage_t result;
        exr_get_storage(handle, partIndex, &result).ThrowIfError();
        return result;
    }

    public int GetChannelCount(int partIndex)
    {
        exr_attr_chlist_t* channels;
        exr_get_channels(handle, partIndex, &channels).ThrowIfError();
        return channels->num_channels;
    }

    public int GetScanlinesPerChunk(int partIndex)
    {
        int result;
        exr_get_scanlines_per_chunk(handle, partIndex, &result).ThrowIfError();
        return result;
    }

    public exr_chunk_info_t ReadScanlineChunkInfo(int partIndex, int y)
    {
        exr_chunk_info_t result;
        exr_read_scanline_chunk_info(handle, partIndex, y, &result).ThrowIfError();
        return result;
    }

    public IMemoryOwner<Color4> GetData(int partIndex)
    {
        Debug.Assert(GetStorage(partIndex) == exr_storage_t.EXR_STORAGE_SCANLINE);

        var window = GetDataWindow(partIndex);
        var scanlines = GetScanlinesPerChunk(partIndex);
        var channelCount = GetChannelCount(partIndex);
        var memoryOwner = MemoryPool<Color4>.Shared.Rent(window.Width * window.Height);
        var memory = memoryOwner.Memory;

        Parallel.ForEach(Partitioner.Create(window.Y, window.Y + window.Height, scanlines), t =>
        {
            var (y, r) = t;
            var dstY = y - window.Y;

            var chunkInfo = ReadScanlineChunkInfo(partIndex, y);

            exr_decode_pipeline_t decoder;
            exr_decoding_initialize(handle, partIndex, &chunkInfo, &decoder).ThrowIfError();

            fixed (Color4* data = memory.Span)
            {
                for (int c = 0; c < decoder.channel_count; c++)
                {
                    ref var channel = ref decoder.channels[c];
                    var dstC = GetDstChannel(ref channel, c);
                    channel.user_bytes_per_element = sizeof(float);
                    channel.user_data_type = (ushort)exr_pixel_type_t.EXR_PIXEL_FLOAT;
                    channel.user_pixel_stride = sizeof(Color4);
                    channel.user_line_stride = window.Width * sizeof(Color4);
                    channel.decode_to_ptr = (byte*)(data + dstY * window.Width) + (dstC * sizeof(float));
                }
            }

            exr_decoding_choose_default_routines(handle, partIndex, &decoder).ThrowIfError();
            exr_decoding_run(handle, partIndex, &decoder).ThrowIfError();

            exr_decoding_destroy(handle, &decoder).ThrowIfError();
        });
        //var dstY = 0;
        //for (int y = window.Y; y < window.Y + window.Height; y += scanlines, dstY += scanlines)
        //{
        //    var chunkInfo = ReadScanlineChunkInfo(partIndex, y);

        //    exr_decode_pipeline_t decoder;
        //    exr_decoding_initialize(handle, partIndex, &chunkInfo, &decoder).ThrowIfError();

        //    fixed (Color4* data = memory.Span)
        //    {
        //        for (int c = 0; c < decoder.channel_count; c++)
        //        {
        //            ref var channel = ref decoder.channels[c];
        //            var dstC = GetDstChannel(ref channel, c);
        //            channel.user_bytes_per_element = sizeof(float);
        //            channel.user_data_type = (ushort)exr_pixel_type_t.EXR_PIXEL_FLOAT;
        //            channel.user_pixel_stride = sizeof(Color4);
        //            channel.user_line_stride = window.Width * sizeof(Color4);
        //            channel.decode_to_ptr = (byte*)(data + dstY * window.Width) + (dstC * sizeof(float));
        //        }
        //    }

        //    exr_decoding_choose_default_routines(handle, partIndex, &decoder).ThrowIfError();
        //    exr_decoding_run(handle, partIndex, &decoder).ThrowIfError();

        //    exr_decoding_destroy(handle, &decoder).ThrowIfError();
        //}

        return memoryOwner;

        static int GetDstChannel(ref exr_coding_channel_info_t info, int i)
        {
            var c = new ReadOnlySpan<char>(info.channel_name, 1);
            return c switch
            {
                "R" => 0,
                "G" => 1,
                "B" => 2,
                "A" => 3,
                _ => i
            };
        }
    }
}

public unsafe struct ExrDecodePipeline : IDisposable
{
    private readonly exr_context_t* context;
    private readonly exr_decode_pipeline_t native;

    public ExrDecodePipeline(exr_context_t* context, exr_decode_pipeline_t native)
    {
        this.context = context;
        this.native = native;
    }

    public void Dispose()
    {
        var native = this.native;
        exr_decoding_destroy(context, &native).ThrowIfError();
    }
}