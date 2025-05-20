namespace OpenEXR.Interop
{
    internal partial struct exr_chunk_info_t
    {
        [NativeTypeName("int32_t")]
        public int idx;

        [NativeTypeName("int32_t")]
        public int start_x;

        [NativeTypeName("int32_t")]
        public int start_y;

        [NativeTypeName("int32_t")]
        public int height;

        [NativeTypeName("int32_t")]
        public int width;

        [NativeTypeName("uint8_t")]
        public byte level_x;

        [NativeTypeName("uint8_t")]
        public byte level_y;

        [NativeTypeName("uint8_t")]
        public byte type;

        [NativeTypeName("uint8_t")]
        public byte compression;

        [NativeTypeName("uint64_t")]
        public ulong data_offset;

        [NativeTypeName("uint64_t")]
        public ulong packed_size;

        [NativeTypeName("uint64_t")]
        public ulong unpacked_size;

        [NativeTypeName("uint64_t")]
        public ulong sample_count_data_offset;

        [NativeTypeName("uint64_t")]
        public ulong sample_count_table_size;
    }
}
