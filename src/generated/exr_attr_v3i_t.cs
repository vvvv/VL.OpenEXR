using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct exr_attr_v3i_t
    {
        [NativeTypeName("int32_t")]
        public int x;

        [NativeTypeName("int32_t")]
        public int y;

        [NativeTypeName("int32_t")]
        public int z;
    }
}
