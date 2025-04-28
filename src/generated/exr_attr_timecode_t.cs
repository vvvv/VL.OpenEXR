using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct exr_attr_timecode_t
    {
        [NativeTypeName("uint32_t")]
        public uint time_and_flags;

        [NativeTypeName("uint32_t")]
        public uint user_data;
    }
}
