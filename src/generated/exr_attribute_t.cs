using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    public unsafe partial struct exr_attribute_t
    {
        [NativeTypeName("const char *")]
        public sbyte* name;

        [NativeTypeName("const char *")]
        public sbyte* type_name;

        [NativeTypeName("uint8_t")]
        public byte name_length;

        [NativeTypeName("uint8_t")]
        public byte type_name_length;

        [NativeTypeName("uint8_t[2]")]
        public _pad_e__FixedBuffer pad;

        public exr_attribute_type_t type;

        [NativeTypeName("__AnonymousRecord_openexr_attr_L465_C5")]
        public _Anonymous_e__Union Anonymous;

        [UnscopedRef]
        public ref byte uc
        {
            get
            {
                return ref Anonymous.uc;
            }
        }

        [UnscopedRef]
        public ref double d
        {
            get
            {
                return ref Anonymous.d;
            }
        }

        [UnscopedRef]
        public ref float f
        {
            get
            {
                return ref Anonymous.f;
            }
        }

        [UnscopedRef]
        public ref int i
        {
            get
            {
                return ref Anonymous.i;
            }
        }

        [UnscopedRef]
        public ref exr_attr_box2i_t* box2i
        {
            get
            {
                return ref Anonymous.box2i;
            }
        }

        [UnscopedRef]
        public ref exr_attr_box2f_t* box2f
        {
            get
            {
                return ref Anonymous.box2f;
            }
        }

        [UnscopedRef]
        public ref exr_attr_chlist_t* chlist
        {
            get
            {
                return ref Anonymous.chlist;
            }
        }

        [UnscopedRef]
        public ref exr_attr_chromaticities_t* chromaticities
        {
            get
            {
                return ref Anonymous.chromaticities;
            }
        }

        [UnscopedRef]
        public ref exr_attr_keycode_t* keycode
        {
            get
            {
                return ref Anonymous.keycode;
            }
        }

        [UnscopedRef]
        public ref exr_attr_float_vector_t* floatvector
        {
            get
            {
                return ref Anonymous.floatvector;
            }
        }

        [UnscopedRef]
        public ref exr_attr_m33f_t* m33f
        {
            get
            {
                return ref Anonymous.m33f;
            }
        }

        [UnscopedRef]
        public ref exr_attr_m33d_t* m33d
        {
            get
            {
                return ref Anonymous.m33d;
            }
        }

        [UnscopedRef]
        public ref exr_attr_m44f_t* m44f
        {
            get
            {
                return ref Anonymous.m44f;
            }
        }

        [UnscopedRef]
        public ref exr_attr_m44d_t* m44d
        {
            get
            {
                return ref Anonymous.m44d;
            }
        }

        [UnscopedRef]
        public ref exr_attr_preview_t* preview
        {
            get
            {
                return ref Anonymous.preview;
            }
        }

        [UnscopedRef]
        public ref exr_attr_rational_t* rational
        {
            get
            {
                return ref Anonymous.rational;
            }
        }

        [UnscopedRef]
        public ref exr_attr_string_t* @string
        {
            get
            {
                return ref Anonymous.@string;
            }
        }

        [UnscopedRef]
        public ref exr_attr_string_vector_t* stringvector
        {
            get
            {
                return ref Anonymous.stringvector;
            }
        }

        [UnscopedRef]
        public ref exr_attr_tiledesc_t* tiledesc
        {
            get
            {
                return ref Anonymous.tiledesc;
            }
        }

        [UnscopedRef]
        public ref exr_attr_timecode_t* timecode
        {
            get
            {
                return ref Anonymous.timecode;
            }
        }

        [UnscopedRef]
        public ref exr_attr_v2i_t* v2i
        {
            get
            {
                return ref Anonymous.v2i;
            }
        }

        [UnscopedRef]
        public ref exr_attr_v2f_t* v2f
        {
            get
            {
                return ref Anonymous.v2f;
            }
        }

        [UnscopedRef]
        public ref exr_attr_v2d_t* v2d
        {
            get
            {
                return ref Anonymous.v2d;
            }
        }

        [UnscopedRef]
        public ref exr_attr_v3i_t* v3i
        {
            get
            {
                return ref Anonymous.v3i;
            }
        }

        [UnscopedRef]
        public ref exr_attr_v3f_t* v3f
        {
            get
            {
                return ref Anonymous.v3f;
            }
        }

        [UnscopedRef]
        public ref exr_attr_v3d_t* v3d
        {
            get
            {
                return ref Anonymous.v3d;
            }
        }

        [UnscopedRef]
        public ref exr_attr_opaquedata_t* opaque
        {
            get
            {
                return ref Anonymous.opaque;
            }
        }

        [UnscopedRef]
        public ref byte* rawptr
        {
            get
            {
                return ref Anonymous.rawptr;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public unsafe partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("uint8_t")]
            public byte uc;

            [FieldOffset(0)]
            public double d;

            [FieldOffset(0)]
            public float f;

            [FieldOffset(0)]
            [NativeTypeName("int32_t")]
            public int i;

            [FieldOffset(0)]
            public exr_attr_box2i_t* box2i;

            [FieldOffset(0)]
            public exr_attr_box2f_t* box2f;

            [FieldOffset(0)]
            public exr_attr_chlist_t* chlist;

            [FieldOffset(0)]
            public exr_attr_chromaticities_t* chromaticities;

            [FieldOffset(0)]
            public exr_attr_keycode_t* keycode;

            [FieldOffset(0)]
            public exr_attr_float_vector_t* floatvector;

            [FieldOffset(0)]
            public exr_attr_m33f_t* m33f;

            [FieldOffset(0)]
            public exr_attr_m33d_t* m33d;

            [FieldOffset(0)]
            public exr_attr_m44f_t* m44f;

            [FieldOffset(0)]
            public exr_attr_m44d_t* m44d;

            [FieldOffset(0)]
            public exr_attr_preview_t* preview;

            [FieldOffset(0)]
            public exr_attr_rational_t* rational;

            [FieldOffset(0)]
            public exr_attr_string_t* @string;

            [FieldOffset(0)]
            public exr_attr_string_vector_t* stringvector;

            [FieldOffset(0)]
            public exr_attr_tiledesc_t* tiledesc;

            [FieldOffset(0)]
            public exr_attr_timecode_t* timecode;

            [FieldOffset(0)]
            public exr_attr_v2i_t* v2i;

            [FieldOffset(0)]
            public exr_attr_v2f_t* v2f;

            [FieldOffset(0)]
            public exr_attr_v2d_t* v2d;

            [FieldOffset(0)]
            public exr_attr_v3i_t* v3i;

            [FieldOffset(0)]
            public exr_attr_v3f_t* v3f;

            [FieldOffset(0)]
            public exr_attr_v3d_t* v3d;

            [FieldOffset(0)]
            public exr_attr_opaquedata_t* opaque;

            [FieldOffset(0)]
            [NativeTypeName("uint8_t *")]
            public byte* rawptr;
        }

        [InlineArray(2)]
        public partial struct _pad_e__FixedBuffer
        {
            public byte e0;
        }
    }
}
