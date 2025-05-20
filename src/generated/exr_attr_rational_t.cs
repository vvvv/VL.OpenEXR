using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal partial struct exr_attr_rational_t
    {
        [NativeTypeName("int32_t")]
        public int num;

        [NativeTypeName("uint32_t")]
        public uint denom;
    }
}
