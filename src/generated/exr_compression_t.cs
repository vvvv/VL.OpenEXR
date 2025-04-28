namespace OpenEXR.Interop
{
    public enum exr_compression_t
    {
        EXR_COMPRESSION_NONE = 0,
        EXR_COMPRESSION_RLE = 1,
        EXR_COMPRESSION_ZIPS = 2,
        EXR_COMPRESSION_ZIP = 3,
        EXR_COMPRESSION_PIZ = 4,
        EXR_COMPRESSION_PXR24 = 5,
        EXR_COMPRESSION_B44 = 6,
        EXR_COMPRESSION_B44A = 7,
        EXR_COMPRESSION_DWAA = 8,
        EXR_COMPRESSION_DWAB = 9,
        EXR_COMPRESSION_LAST_TYPE,
    }
}
