using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal partial struct exr_attr_tiledesc_t
    {
        [NativeTypeName("uint32_t")]
        public uint x_size;

        [NativeTypeName("uint32_t")]
        public uint y_size;

        [NativeTypeName("uint8_t")]
        public byte level_and_round;
    }
}
