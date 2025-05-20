using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal partial struct exr_attr_v2i_t
    {
        [NativeTypeName("int32_t")]
        public int x;

        [NativeTypeName("int32_t")]
        public int y;
    }
}
