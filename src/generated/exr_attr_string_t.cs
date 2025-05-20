namespace OpenEXR.Interop
{
    internal unsafe partial struct exr_attr_string_t
    {
        [NativeTypeName("int32_t")]
        public int length;

        [NativeTypeName("int32_t")]
        public int alloc_size;

        [NativeTypeName("const char *")]
        public sbyte* str;
    }
}
