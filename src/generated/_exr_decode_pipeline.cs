using System.Runtime.CompilerServices;

namespace OpenEXR.Interop
{
    public unsafe partial struct _exr_decode_pipeline
    {
        [NativeTypeName("size_t")]
        public nuint pipe_size;

        public exr_coding_channel_info_t* channels;

        [NativeTypeName("int16_t")]
        public short channel_count;

        [NativeTypeName("uint16_t")]
        public ushort decode_flags;

        public int part_index;

        [NativeTypeName("exr_const_context_t")]
        public _priv_exr_context_t* context;

        public exr_chunk_info_t chunk;

        [NativeTypeName("int32_t")]
        public int user_line_begin_skip;

        [NativeTypeName("int32_t")]
        public int user_line_end_ignore;

        [NativeTypeName("uint64_t")]
        public ulong bytes_decompressed;

        public void* decoding_user_data;

        public void* packed_buffer;

        [NativeTypeName("size_t")]
        public nuint packed_alloc_size;

        public void* unpacked_buffer;

        [NativeTypeName("size_t")]
        public nuint unpacked_alloc_size;

        public void* packed_sample_count_table;

        [NativeTypeName("size_t")]
        public nuint packed_sample_count_alloc_size;

        [NativeTypeName("int32_t *")]
        public int* sample_count_table;

        [NativeTypeName("size_t")]
        public nuint sample_count_alloc_size;

        public void* scratch_buffer_1;

        [NativeTypeName("size_t")]
        public nuint scratch_alloc_size_1;

        public void* scratch_buffer_2;

        [NativeTypeName("size_t")]
        public nuint scratch_alloc_size_2;

        [NativeTypeName("void *(*)(exr_transcoding_pipeline_buffer_id_t, size_t)")]
        public delegate* unmanaged[Cdecl]<exr_transcoding_pipeline_buffer_id, nuint, void*> alloc_fn;

        [NativeTypeName("void (*)(exr_transcoding_pipeline_buffer_id_t, void *)")]
        public delegate* unmanaged[Cdecl]<exr_transcoding_pipeline_buffer_id, void*, void> free_fn;

        [NativeTypeName("exr_result_t (*)(struct _exr_decode_pipeline *)")]
        public delegate* unmanaged[Cdecl]<_exr_decode_pipeline*, int> read_fn;

        [NativeTypeName("exr_result_t (*)(struct _exr_decode_pipeline *)")]
        public delegate* unmanaged[Cdecl]<_exr_decode_pipeline*, int> decompress_fn;

        [NativeTypeName("exr_result_t (*)(struct _exr_decode_pipeline *)")]
        public delegate* unmanaged[Cdecl]<_exr_decode_pipeline*, int> realloc_nonimage_data_fn;

        [NativeTypeName("exr_result_t (*)(struct _exr_decode_pipeline *)")]
        public delegate* unmanaged[Cdecl]<_exr_decode_pipeline*, int> unpack_and_convert_fn;

        [NativeTypeName("exr_coding_channel_info_t[5]")]
        public __quick_chan_store_e__FixedBuffer _quick_chan_store;

        [InlineArray(5)]
        public partial struct __quick_chan_store_e__FixedBuffer
        {
            public exr_coding_channel_info_t e0;
        }
    }
}
