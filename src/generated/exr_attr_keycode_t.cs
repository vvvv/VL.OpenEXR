using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct exr_attr_keycode_t
    {
        [NativeTypeName("int32_t")]
        public int film_mfc_code;

        [NativeTypeName("int32_t")]
        public int film_type;

        [NativeTypeName("int32_t")]
        public int prefix;

        [NativeTypeName("int32_t")]
        public int count;

        [NativeTypeName("int32_t")]
        public int perf_offset;

        [NativeTypeName("int32_t")]
        public int perfs_per_frame;

        [NativeTypeName("int32_t")]
        public int perfs_per_count;
    }
}
