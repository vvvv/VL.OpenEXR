using System.Runtime.CompilerServices;

namespace OpenEXR.Interop
{
    public unsafe partial struct exr_attr_opaquedata_t
    {
        [NativeTypeName("int32_t")]
        public int size;

        [NativeTypeName("int32_t")]
        public int unpacked_size;

        [NativeTypeName("int32_t")]
        public int packed_alloc_size;

        [NativeTypeName("uint8_t[4]")]
        public _pad_e__FixedBuffer pad;

        public void* packed_data;

        public void* unpacked_data;

        [NativeTypeName("exr_result_t (*)(exr_context_t, const void *, int32_t, int32_t *, void **)")]
        public delegate* unmanaged[Cdecl]<exr_context_t*, void*, int, int*, void**, int> unpack_func_ptr;

        [NativeTypeName("exr_result_t (*)(exr_context_t, const void *, int32_t, int32_t *, void *)")]
        public delegate* unmanaged[Cdecl]<exr_context_t*, void*, int, int*, void*, int> pack_func_ptr;

        [NativeTypeName("void (*)(exr_context_t, void *, int32_t)")]
        public delegate* unmanaged[Cdecl]<exr_context_t*, void*, int, void> destroy_unpacked_func_ptr;

        [InlineArray(4)]
        public partial struct _pad_e__FixedBuffer
        {
            public byte e0;
        }
    }
}
