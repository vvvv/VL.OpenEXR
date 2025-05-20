using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal partial struct exr_attr_m33f_t
    {
        [NativeTypeName("float[9]")]
        public _m_e__FixedBuffer m;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(9)]
        public partial struct _m_e__FixedBuffer
        {
            public float e0;
        }
    }
}
