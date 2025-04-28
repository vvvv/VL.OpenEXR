using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct exr_attr_v3d_t
    {
        public double x;

        public double y;

        public double z;
    }
}
