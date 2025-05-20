using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal partial struct exr_attr_v2d_t
    {
        public double x;

        public double y;
    }
}
