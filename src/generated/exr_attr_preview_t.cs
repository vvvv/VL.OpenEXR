namespace OpenEXR.Interop
{
    public unsafe partial struct exr_attr_preview_t
    {
        [NativeTypeName("uint32_t")]
        public uint width;

        [NativeTypeName("uint32_t")]
        public uint height;

        [NativeTypeName("size_t")]
        public nuint alloc_size;

        [NativeTypeName("const uint8_t *")]
        public byte* rgba;
    }
}
