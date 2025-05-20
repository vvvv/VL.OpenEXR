using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal partial struct exr_attr_v3f_t
    {
        public float x;

        public float y;

        public float z;
    }
}
