using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct exr_attr_v2f_t
    {
        public float x;

        public float y;
    }
}
