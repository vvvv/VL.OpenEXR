using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct exr_attr_m33d_t
    {
        [NativeTypeName("double[9]")]
        public _m_e__FixedBuffer m;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(9)]
        public partial struct _m_e__FixedBuffer
        {
            public double e0;
        }
    }
}
