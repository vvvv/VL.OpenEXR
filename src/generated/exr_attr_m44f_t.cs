using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal partial struct exr_attr_m44f_t
    {
        [NativeTypeName("float[16]")]
        public _m_e__FixedBuffer m;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(16)]
        public partial struct _m_e__FixedBuffer
        {
            public float e0;
        }
    }
}
