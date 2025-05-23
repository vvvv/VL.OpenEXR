namespace OpenEXR.Interop
{
    internal enum exr_transcoding_pipeline_buffer_id_t
    {
        EXR_TRANSCODE_BUFFER_PACKED,
        EXR_TRANSCODE_BUFFER_UNPACKED,
        EXR_TRANSCODE_BUFFER_COMPRESSED,
        EXR_TRANSCODE_BUFFER_SCRATCH1,
        EXR_TRANSCODE_BUFFER_SCRATCH2,
        EXR_TRANSCODE_BUFFER_PACKED_SAMPLES,
        EXR_TRANSCODE_BUFFER_SAMPLES,
    }
}
