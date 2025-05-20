namespace OpenEXR.Interop
{
    internal enum exr_deep_image_state_t
    {
        EXR_DIS_MESSY = 0,
        EXR_DIS_SORTED = 1,
        EXR_DIS_NON_OVERLAPPING = 2,
        EXR_DIS_TIDY = 3,
        EXR_DIS_LAST_TYPE,
    }
}
