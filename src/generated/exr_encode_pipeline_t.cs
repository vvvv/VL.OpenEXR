using System.Runtime.CompilerServices;

namespace OpenEXR.Interop
{
    public unsafe partial struct exr_encode_pipeline_t
    {
        [NativeTypeName("size_t")]
        public nuint pipe_size;

        public exr_coding_channel_info_t* channels;

        [NativeTypeName("int16_t")]
        public short channel_count;

        [NativeTypeName("uint16_t")]
        public ushort encode_flags;

        public int part_index;

        [NativeTypeName("exr_const_context_t")]
        public exr_context_t* context;

        public exr_chunk_info_t chunk;

        public void* encoding_user_data;

        public void* packed_buffer;

        [NativeTypeName("uint64_t")]
        public ulong packed_bytes;

        [NativeTypeName("size_t")]
        public nuint packed_alloc_size;

        [NativeTypeName("int32_t *")]
        public int* sample_count_table;

        [NativeTypeName("size_t")]
        public nuint sample_count_alloc_size;

        public void* packed_sample_count_table;

        [NativeTypeName("size_t")]
        public nuint packed_sample_count_bytes;

        [NativeTypeName("size_t")]
        public nuint packed_sample_count_alloc_size;

        public void* compressed_buffer;

        [NativeTypeName("size_t")]
        public nuint compressed_bytes;

        [NativeTypeName("size_t")]
        public nuint compressed_alloc_size;

        public void* scratch_buffer_1;

        [NativeTypeName("size_t")]
        public nuint scratch_alloc_size_1;

        public void* scratch_buffer_2;

        [NativeTypeName("size_t")]
        public nuint scratch_alloc_size_2;

        [NativeTypeName("void *(*)(exr_transcoding_pipeline_buffer_id_t, size_t)")]
        public delegate* unmanaged[Cdecl]<exr_transcoding_pipeline_buffer_id_t, nuint, void*> alloc_fn;

        [NativeTypeName("void (*)(exr_transcoding_pipeline_buffer_id_t, void *)")]
        public delegate* unmanaged[Cdecl]<exr_transcoding_pipeline_buffer_id_t, void*, void> free_fn;

        [NativeTypeName("exr_result_t (*)(struct _exr_encode_pipeline *)")]
        public delegate* unmanaged[Cdecl]<exr_encode_pipeline_t*, int> convert_and_pack_fn;

        [NativeTypeName("exr_result_t (*)(struct _exr_encode_pipeline *)")]
        public delegate* unmanaged[Cdecl]<exr_encode_pipeline_t*, int> compress_fn;

        [NativeTypeName("exr_result_t (*)(struct _exr_encode_pipeline *)")]
        public delegate* unmanaged[Cdecl]<exr_encode_pipeline_t*, int> yield_until_ready_fn;

        [NativeTypeName("exr_result_t (*)(struct _exr_encode_pipeline *)")]
        public delegate* unmanaged[Cdecl]<exr_encode_pipeline_t*, int> write_fn;

        [NativeTypeName("exr_coding_channel_info_t[5]")]
        public __quick_chan_store_e__FixedBuffer _quick_chan_store;

        [InlineArray(5)]
        public partial struct __quick_chan_store_e__FixedBuffer
        {
            public exr_coding_channel_info_t e0;
        }
    }
}
