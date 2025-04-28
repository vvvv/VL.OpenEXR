using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    public unsafe partial struct exr_coding_channel_info_t
    {
        [NativeTypeName("const char *")]
        public sbyte* channel_name;

        [NativeTypeName("int32_t")]
        public int height;

        [NativeTypeName("int32_t")]
        public int width;

        [NativeTypeName("int32_t")]
        public int x_samples;

        [NativeTypeName("int32_t")]
        public int y_samples;

        [NativeTypeName("uint8_t")]
        public byte p_linear;

        [NativeTypeName("int8_t")]
        public sbyte bytes_per_element;

        [NativeTypeName("uint16_t")]
        public ushort data_type;

        [NativeTypeName("int16_t")]
        public short user_bytes_per_element;

        [NativeTypeName("uint16_t")]
        public ushort user_data_type;

        [NativeTypeName("int32_t")]
        public int user_pixel_stride;

        [NativeTypeName("int32_t")]
        public int user_line_stride;

        [NativeTypeName("__AnonymousRecord_openexr_coding_L130_C5")]
        public _Anonymous_e__Union Anonymous;

        [UnscopedRef]
        public ref byte* decode_to_ptr
        {
            get
            {
                return ref Anonymous.decode_to_ptr;
            }
        }

        [UnscopedRef]
        public ref byte* encode_from_ptr
        {
            get
            {
                return ref Anonymous.encode_from_ptr;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public unsafe partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("uint8_t *")]
            public byte* decode_to_ptr;

            [FieldOffset(0)]
            [NativeTypeName("const uint8_t *")]
            public byte* encode_from_ptr;
        }
    }
}
