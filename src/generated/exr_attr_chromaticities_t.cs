using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct exr_attr_chromaticities_t
    {
        public float red_x;

        public float red_y;

        public float green_x;

        public float green_y;

        public float blue_x;

        public float blue_y;

        public float white_x;

        public float white_y;
    }
}
