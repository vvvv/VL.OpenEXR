namespace OpenEXR.Interop
{
    internal unsafe partial struct exr_attr_string_vector_t
    {
        [NativeTypeName("int32_t")]
        public int n_strings;

        [NativeTypeName("int32_t")]
        public int alloc_size;

        [NativeTypeName("const exr_attr_string_t *")]
        public exr_attr_string_t* strings;
    }
}
