using System.Runtime.CompilerServices;

namespace OpenEXR.Interop
{
    public unsafe partial struct _exr_context_initializer_v3
    {
        [NativeTypeName("size_t")]
        public nuint size;

        [NativeTypeName("exr_error_handler_cb_t")]
        public delegate* unmanaged[Cdecl]<_priv_exr_context_t*, int, sbyte*, void> error_handler_fn;

        [NativeTypeName("exr_memory_allocation_func_t")]
        public delegate* unmanaged[Cdecl]<nuint, void*> alloc_fn;

        [NativeTypeName("exr_memory_free_func_t")]
        public delegate* unmanaged[Cdecl]<void*, void> free_fn;

        public void* user_data;

        [NativeTypeName("exr_read_func_ptr_t")]
        public delegate* unmanaged[Cdecl]<_priv_exr_context_t*, void*, void*, ulong, ulong, delegate* unmanaged[Cdecl]<_priv_exr_context_t*, int, sbyte*, int>, long> read_fn;

        [NativeTypeName("exr_query_size_func_ptr_t")]
        public delegate* unmanaged[Cdecl]<_priv_exr_context_t*, void*, long> size_fn;

        [NativeTypeName("exr_write_func_ptr_t")]
        public delegate* unmanaged[Cdecl]<_priv_exr_context_t*, void*, void*, ulong, ulong, delegate* unmanaged[Cdecl]<_priv_exr_context_t*, int, sbyte*, int>, long> write_fn;

        [NativeTypeName("exr_destroy_stream_func_ptr_t")]
        public delegate* unmanaged[Cdecl]<_priv_exr_context_t*, void*, int, void> destroy_fn;

        public int max_image_width;

        public int max_image_height;

        public int max_tile_width;

        public int max_tile_height;

        public int zip_level;

        public float dwa_quality;

        public int flags;

        [NativeTypeName("uint8_t[4]")]
        public _pad_e__FixedBuffer pad;

        [InlineArray(4)]
        public partial struct _pad_e__FixedBuffer
        {
            public byte e0;
        }
    }
}
