namespace OpenEXR.Interop
{
    internal unsafe partial struct exr_attr_chlist_t
    {
        public int num_channels;

        public int num_alloced;

        [NativeTypeName("const exr_attr_chlist_entry_t *")]
        public exr_attr_chlist_entry_t* entries;
    }
}
