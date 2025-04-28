namespace OpenEXR.Interop
{
    public unsafe partial struct exr_attr_float_vector_t
    {
        [NativeTypeName("int32_t")]
        public int length;

        [NativeTypeName("int32_t")]
        public int alloc_size;

        [NativeTypeName("const float *")]
        public float* arr;
    }
}
