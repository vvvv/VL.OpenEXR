using System.Runtime.CompilerServices;

namespace OpenEXR.Interop
{
    public partial struct exr_attr_chlist_entry_t
    {
        public exr_attr_string_t name;

        public exr_pixel_type_t pixel_type;

        [NativeTypeName("uint8_t")]
        public byte p_linear;

        [NativeTypeName("uint8_t[3]")]
        public _reserved_e__FixedBuffer reserved;

        [NativeTypeName("int32_t")]
        public int x_sampling;

        [NativeTypeName("int32_t")]
        public int y_sampling;

        [InlineArray(3)]
        public partial struct _reserved_e__FixedBuffer
        {
            public byte e0;
        }
    }
}
