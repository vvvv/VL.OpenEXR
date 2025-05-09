using System.Runtime.InteropServices;

namespace OpenEXR.Interop
{
    public static unsafe partial class exr
    {
        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void exr_get_library_version(int* maj, int* min, int* patch, [NativeTypeName("const char **")] sbyte** extra);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void exr_set_default_maximum_image_size(int w, int h);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void exr_get_default_maximum_image_size(int* w, int* h);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void exr_set_default_maximum_tile_size(int w, int h);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void exr_get_default_maximum_tile_size(int* w, int* h);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void exr_set_default_zip_compression_level(int l);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void exr_get_default_zip_compression_level(int* l);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void exr_set_default_dwa_compression_quality(float q);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void exr_get_default_dwa_compression_quality(float* q);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void exr_set_default_memory_routines([NativeTypeName("exr_memory_allocation_func_t")] delegate* unmanaged[Cdecl]<nuint, void*> alloc_func, [NativeTypeName("exr_memory_free_func_t")] delegate* unmanaged[Cdecl]<void*, void> free_func);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* exr_get_default_error_message([NativeTypeName("exr_result_t")] int code);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* exr_get_error_code_as_string([NativeTypeName("exr_result_t")] int code);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_test_file_header([NativeTypeName("const char *")] sbyte* filename, [NativeTypeName("const exr_context_initializer_t *")] exr_context_initializer_t* ctxtdata);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_finish([NativeTypeName("exr_context_t *")] exr_context_t** ctxt);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_start_read([NativeTypeName("exr_context_t *")] exr_context_t** ctxt, [NativeTypeName("const char *")] sbyte* filename, [NativeTypeName("const exr_context_initializer_t *")] exr_context_initializer_t* ctxtdata);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_start_write([NativeTypeName("exr_context_t *")] exr_context_t** ctxt, [NativeTypeName("const char *")] sbyte* filename, exr_default_write_mode_t default_mode, [NativeTypeName("const exr_context_initializer_t *")] exr_context_initializer_t* ctxtdata);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_start_inplace_header_update([NativeTypeName("exr_context_t *")] exr_context_t** ctxt, [NativeTypeName("const char *")] sbyte* filename, [NativeTypeName("const exr_context_initializer_t *")] exr_context_initializer_t* ctxtdata);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_start_temporary_context([NativeTypeName("exr_context_t *")] exr_context_t** ctxt, [NativeTypeName("const char *")] sbyte* context_name, [NativeTypeName("const exr_context_initializer_t *")] exr_context_initializer_t* ctxtdata);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_file_name([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, [NativeTypeName("const char **")] sbyte** name);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_file_version_and_flags([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, [NativeTypeName("uint32_t *")] uint* ver);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_user_data([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, void** userdata);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_register_attr_type_handler([NativeTypeName("exr_context_t")] exr_context_t* ctxt, [NativeTypeName("const char *")] sbyte* type, [NativeTypeName("exr_result_t (*)(exr_context_t, const void *, int32_t, int32_t *, void **)")] delegate* unmanaged[Cdecl]<exr_context_t*, void*, int, int*, void**, int> unpack_func_ptr, [NativeTypeName("exr_result_t (*)(exr_context_t, const void *, int32_t, int32_t *, void *)")] delegate* unmanaged[Cdecl]<exr_context_t*, void*, int, int*, void*, int> pack_func_ptr, [NativeTypeName("void (*)(exr_context_t, void *, int32_t)")] delegate* unmanaged[Cdecl]<exr_context_t*, void*, int, void> destroy_unpacked_func_ptr);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_set_longname_support([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int onoff);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_write_header([NativeTypeName("exr_context_t")] exr_context_t* ctxt);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_count([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int* count);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_name([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char **")] sbyte** @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_storage([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, exr_storage_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_add_part([NativeTypeName("exr_context_t")] exr_context_t* ctxt, [NativeTypeName("const char *")] sbyte* partname, exr_storage_t type, int* new_index);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_tile_levels([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("int32_t *")] int* levelsx, [NativeTypeName("int32_t *")] int* levelsy);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_tile_sizes([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, int levelx, int levely, [NativeTypeName("int32_t *")] int* tilew, [NativeTypeName("int32_t *")] int* tileh);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_tile_counts([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, int levelx, int levely, [NativeTypeName("int32_t *")] int* countx, [NativeTypeName("int32_t *")] int* county);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_level_sizes([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, int levelx, int levely, [NativeTypeName("int32_t *")] int* levw, [NativeTypeName("int32_t *")] int* levh);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_chunk_count([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("int32_t *")] int* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_chunk_table([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("uint64_t **")] ulong** table, [NativeTypeName("int32_t *")] int* count);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_validate_chunk_table([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_scanlines_per_chunk([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("int32_t *")] int* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_chunk_unpacked_size([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("uint64_t *")] ulong* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_zip_compression_level([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, int* level);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_set_zip_compression_level([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, int level);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_dwa_compression_level([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, float* level);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_set_dwa_compression_level([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, float level);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_attribute_count([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("int32_t *")] int* count);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_attribute_by_index([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, exr_attr_list_access_mode_t mode, [NativeTypeName("int32_t")] int idx, [NativeTypeName("const exr_attribute_t **")] exr_attribute_t** outattr);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_attribute_by_name([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attribute_t **")] exr_attribute_t** outattr);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_attribute_list([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, exr_attr_list_access_mode_t mode, [NativeTypeName("int32_t *")] int* count, [NativeTypeName("const exr_attribute_t **")] exr_attribute_t** outlist);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_declare_by_type([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* type, exr_attribute_t** newattr);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_declare([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attribute_type_t type, exr_attribute_t** newattr);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_initialize_required_attr([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const exr_attr_box2i_t *")] exr_attr_box2i_t* displayWindow, [NativeTypeName("const exr_attr_box2i_t *")] exr_attr_box2i_t* dataWindow, float pixelaspectratio, [NativeTypeName("const exr_attr_v2f_t *")] exr_attr_v2f_t* screenWindowCenter, float screenWindowWidth, exr_lineorder_t lineorder, exr_compression_t ctype);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_initialize_required_attr_simple([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("int32_t")] int width, [NativeTypeName("int32_t")] int height, exr_compression_t ctype);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_copy_unset_attributes([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("exr_const_context_t")] exr_context_t* source, int src_part_index);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_channels([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const exr_attr_chlist_t **")] exr_attr_chlist_t** chlist);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_add_channel([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_pixel_type_t ptype, exr_perceptual_treatment_t percept, [NativeTypeName("int32_t")] int xsamp, [NativeTypeName("int32_t")] int ysamp);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_set_channels([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const exr_attr_chlist_t *")] exr_attr_chlist_t* channels);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_compression([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, exr_compression_t* compression);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_set_compression([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, exr_compression_t ctype);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_data_window([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, exr_attr_box2i_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int exr_set_data_window([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const exr_attr_box2i_t *")] exr_attr_box2i_t* dw);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_display_window([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, exr_attr_box2i_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int exr_set_display_window([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const exr_attr_box2i_t *")] exr_attr_box2i_t* dw);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_lineorder([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, exr_lineorder_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_set_lineorder([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, exr_lineorder_t lo);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_pixel_aspect_ratio([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, float* par);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_set_pixel_aspect_ratio([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, float par);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_screen_window_center([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, exr_attr_v2f_t* wc);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int exr_set_screen_window_center([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const exr_attr_v2f_t *")] exr_attr_v2f_t* wc);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_screen_window_width([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, float* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_set_screen_window_width([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, float ssw);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_tile_descriptor([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("uint32_t *")] uint* xsize, [NativeTypeName("uint32_t *")] uint* ysize, exr_tile_level_mode_t* level, exr_tile_round_mode_t* round);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_set_tile_descriptor([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("uint32_t")] uint x_size, [NativeTypeName("uint32_t")] uint y_size, exr_tile_level_mode_t level_mode, exr_tile_round_mode_t round_mode);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_set_name([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* val);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_version([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("int32_t *")] int* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_set_version([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("int32_t")] int val);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_set_chunk_count([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("int32_t")] int val);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_box2i([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_box2i_t* outval);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_box2i([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_box2i_t *")] exr_attr_box2i_t* val);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_box2f([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_box2f_t* outval);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_box2f([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_box2f_t *")] exr_attr_box2f_t* val);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_channels([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_chlist_t **")] exr_attr_chlist_t** chlist);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_channels([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_chlist_t *")] exr_attr_chlist_t* channels);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_chromaticities([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_chromaticities_t* chroma);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_chromaticities([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_chromaticities_t *")] exr_attr_chromaticities_t* chroma);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_compression([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_compression_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_compression([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_compression_t comp);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_double([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, double* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_double([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, double val);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_envmap([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_envmap_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_envmap([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_envmap_t emap);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_float([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, float* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_float([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, float val);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_float_vector([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("int32_t *")] int* sz, [NativeTypeName("const float **")] float** @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_float_vector([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("int32_t")] int sz, [NativeTypeName("const float *")] float* vals);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_int([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("int32_t *")] int* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_int([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("int32_t")] int val);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_keycode([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_keycode_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_keycode([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_keycode_t *")] exr_attr_keycode_t* kc);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_lineorder([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_lineorder_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_lineorder([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_lineorder_t lo);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_m33f([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_m33f_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_m33f([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_m33f_t *")] exr_attr_m33f_t* m);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_m33d([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_m33d_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_m33d([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_m33d_t *")] exr_attr_m33d_t* m);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_m44f([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_m44f_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_m44f([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_m44f_t *")] exr_attr_m44f_t* m);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_m44d([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_m44d_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_m44d([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_m44d_t *")] exr_attr_m44d_t* m);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_preview([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_preview_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_preview([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_preview_t *")] exr_attr_preview_t* p);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_rational([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_rational_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_rational([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_rational_t *")] exr_attr_rational_t* r);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_string([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("int32_t *")] int* length, [NativeTypeName("const char **")] sbyte** @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_string([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* s);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_string_vector([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("int32_t *")] int* size, [NativeTypeName("const char **")] sbyte** @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_string_vector([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("int32_t")] int size, [NativeTypeName("const char **")] sbyte** sv);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_tiledesc([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_tiledesc_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_tiledesc([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_tiledesc_t *")] exr_attr_tiledesc_t* td);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_timecode([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_timecode_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_timecode([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_timecode_t *")] exr_attr_timecode_t* tc);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_v2i([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_v2i_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_v2i([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_v2i_t *")] exr_attr_v2i_t* v);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_v2f([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_v2f_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_v2f([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_v2f_t *")] exr_attr_v2f_t* v);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_v2d([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_v2d_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_v2d([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_v2d_t *")] exr_attr_v2d_t* v);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_v3i([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_v3i_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_v3i([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_v3i_t *")] exr_attr_v3i_t* v);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_v3f([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_v3f_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_v3f([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_v3f_t *")] exr_attr_v3f_t* v);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_v3d([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, exr_attr_v3d_t* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_v3d([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const exr_attr_v3d_t *")] exr_attr_v3d_t* v);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_get_user([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char **")] sbyte** type, [NativeTypeName("int32_t *")] int* size, [NativeTypeName("const void **")] void** @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_attr_set_user([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* type, [NativeTypeName("int32_t")] int size, [NativeTypeName("const void *")] void* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_get_chunk_table_offset([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("uint64_t *")] ulong* chunk_offset_out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_chunk_default_initialize([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const exr_attr_box2i_t *")] exr_attr_box2i_t* box, int levelx, int levely, exr_chunk_info_t* cinfo);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_read_scanline_chunk_info([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, int y, exr_chunk_info_t* cinfo);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_read_tile_chunk_info([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, int tilex, int tiley, int levelx, int levely, exr_chunk_info_t* cinfo);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_read_chunk([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const exr_chunk_info_t *")] exr_chunk_info_t* cinfo, void* packed_data);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_read_deep_chunk([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const exr_chunk_info_t *")] exr_chunk_info_t* cinfo, void* packed_data, void* sample_data);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_write_scanline_chunk_info([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, int y, exr_chunk_info_t* cinfo);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_write_tile_chunk_info([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, int tilex, int tiley, int levelx, int levely, exr_chunk_info_t* cinfo);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_write_scanline_chunk([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, int y, [NativeTypeName("const void *")] void* packed_data, [NativeTypeName("uint64_t")] ulong packed_size);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_write_deep_scanline_chunk([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, int y, [NativeTypeName("const void *")] void* packed_data, [NativeTypeName("uint64_t")] ulong packed_size, [NativeTypeName("uint64_t")] ulong unpacked_size, [NativeTypeName("const void *")] void* sample_data, [NativeTypeName("uint64_t")] ulong sample_data_size);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_write_tile_chunk([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, int tilex, int tiley, int levelx, int levely, [NativeTypeName("const void *")] void* packed_data, [NativeTypeName("uint64_t")] ulong packed_size);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_write_deep_tile_chunk([NativeTypeName("exr_context_t")] exr_context_t* ctxt, int part_index, int tilex, int tiley, int levelx, int levely, [NativeTypeName("const void *")] void* packed_data, [NativeTypeName("uint64_t")] ulong packed_size, [NativeTypeName("uint64_t")] ulong unpacked_size, [NativeTypeName("const void *")] void* sample_data, [NativeTypeName("uint64_t")] ulong sample_data_size);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_encoding_initialize([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const exr_chunk_info_t *")] exr_chunk_info_t* cinfo, exr_encode_pipeline_t* encode_pipe);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_encoding_choose_default_routines([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, exr_encode_pipeline_t* encode_pipe);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_encoding_update([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const exr_chunk_info_t *")] exr_chunk_info_t* cinfo, exr_encode_pipeline_t* encode_pipe);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_encoding_run([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, exr_encode_pipeline_t* encode_pipe);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_encoding_destroy([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, exr_encode_pipeline_t* encode_pipe);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_decoding_initialize([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const exr_chunk_info_t *")] exr_chunk_info_t* cinfo, exr_decode_pipeline_t* decode);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_decoding_choose_default_routines([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, exr_decode_pipeline_t* decode);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_decoding_update([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, [NativeTypeName("const exr_chunk_info_t *")] exr_chunk_info_t* cinfo, exr_decode_pipeline_t* decode);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_decoding_run([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int part_index, exr_decode_pipeline_t* decode);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_decoding_destroy([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, exr_decode_pipeline_t* decode);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint exr_compress_max_buffer_size([NativeTypeName("size_t")] nuint in_bytes);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_compress_buffer([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, int level, [NativeTypeName("const void *")] void* @in, [NativeTypeName("size_t")] nuint in_bytes, void* @out, [NativeTypeName("size_t")] nuint out_bytes_avail, [NativeTypeName("size_t *")] nuint* actual_out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_uncompress_buffer([NativeTypeName("exr_const_context_t")] exr_context_t* ctxt, [NativeTypeName("const void *")] void* @in, [NativeTypeName("size_t")] nuint in_bytes, void* @out, [NativeTypeName("size_t")] nuint out_bytes_avail, [NativeTypeName("size_t *")] nuint* actual_out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint exr_rle_compress_buffer([NativeTypeName("size_t")] nuint in_bytes, [NativeTypeName("const void *")] void* @in, void* @out, [NativeTypeName("size_t")] nuint out_bytes_avail);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint exr_rle_uncompress_buffer([NativeTypeName("size_t")] nuint in_bytes, [NativeTypeName("size_t")] nuint max_len, [NativeTypeName("const void *")] void* @in, void* @out);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int exr_compression_lines_per_chunk(exr_compression_t comptype);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_compress_chunk(exr_encode_pipeline_t* encode_state);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_uncompress_chunk(exr_decode_pipeline_t* decode_state);

        [DllImport("OpenEXRCore-3_3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("exr_result_t")]
        public static extern int exr_print_context_info([NativeTypeName("exr_const_context_t")] exr_context_t* c, int verbose);

        [NativeTypeName("#define EXR_CONTEXT_FLAG_STRICT_HEADER (1 << 0)")]
        public const int EXR_CONTEXT_FLAG_STRICT_HEADER = (1 << 0);

        [NativeTypeName("#define EXR_CONTEXT_FLAG_SILENT_HEADER_PARSE (1 << 1)")]
        public const int EXR_CONTEXT_FLAG_SILENT_HEADER_PARSE = (1 << 1);

        [NativeTypeName("#define EXR_CONTEXT_FLAG_DISABLE_CHUNK_RECONSTRUCTION (1 << 2)")]
        public const int EXR_CONTEXT_FLAG_DISABLE_CHUNK_RECONSTRUCTION = (1 << 2);

        [NativeTypeName("#define EXR_CONTEXT_FLAG_WRITE_LEGACY_HEADER (1 << 3)")]
        public const int EXR_CONTEXT_FLAG_WRITE_LEGACY_HEADER = (1 << 3);

        [NativeTypeName("#define EXR_ENCODE_DATA_SAMPLE_COUNTS_ARE_INDIVIDUAL ((uint16_t) (1 << 0))")]
        public const ushort EXR_ENCODE_DATA_SAMPLE_COUNTS_ARE_INDIVIDUAL = ((ushort)(1 << 0));

        [NativeTypeName("#define EXR_ENCODE_NON_IMAGE_DATA_AS_POINTERS ((uint16_t) (1 << 1))")]
        public const ushort EXR_ENCODE_NON_IMAGE_DATA_AS_POINTERS = ((ushort)(1 << 1));

        [NativeTypeName("#define EXR_DECODE_SAMPLE_COUNTS_AS_INDIVIDUAL ((uint16_t) (1 << 0))")]
        public const ushort EXR_DECODE_SAMPLE_COUNTS_AS_INDIVIDUAL = ((ushort)(1 << 0));

        [NativeTypeName("#define EXR_DECODE_NON_IMAGE_DATA_AS_POINTERS ((uint16_t) (1 << 1))")]
        public const ushort EXR_DECODE_NON_IMAGE_DATA_AS_POINTERS = ((ushort)(1 << 1));

        [NativeTypeName("#define EXR_DECODE_SAMPLE_DATA_ONLY ((uint16_t) (1 << 2))")]
        public const ushort EXR_DECODE_SAMPLE_DATA_ONLY = ((ushort)(1 << 2));
    }
}
