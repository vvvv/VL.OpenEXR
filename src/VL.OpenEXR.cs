﻿using System;
using System.Runtime.InteropServices;
using Stride.Graphics;

namespace OpenEXR
{
    enum ExrPixelFormat
    {
        Unknown = -1,
        U32 = 0,
        F16 = 1,
        F32 = 2,
        RGBF32 = 3
    }

    public enum ExrEncoding {
        Uncompressed = 0,
        RLE = 1,
        ZIP1 = 2,
        ZIP16 = 3,
        PIZ = 4,
    }

    public static class ExrLoader
    {
        #pragma warning disable CA5393
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]

        [DllImport("VL.OpenEXR.Native.dll")]
        static extern IntPtr load_from_path(string path, out int width, out int height, out ExrPixelFormat format);

        public static Texture LoadFromPath(string path, GraphicsDevice device)
        {
            ExrPixelFormat exrFormat;
            PixelFormat format;
            IntPtr ptr = load_from_path(path, out var width, out var height, out exrFormat);

            if(exrFormat == ExrPixelFormat.Unknown || ptr == IntPtr.Zero)
            {
                format = PixelFormat.None;
                return null;
            }

            int sizeInBytes = 0;
            bool hasAlpha = true;
            (format, sizeInBytes, hasAlpha) = exrFormat switch
            {
                ExrPixelFormat.F16 => (PixelFormat.R16G16B16A16_Float, 2, true),
                ExrPixelFormat.F32 => (PixelFormat.R32G32B32A32_Float, 4, true),
                ExrPixelFormat.U32 => (PixelFormat.R32G32B32A32_UInt , 4, true),
                ExrPixelFormat.RGBF32 => (PixelFormat.R32G32B32_Float, 4, false),
                _ => (PixelFormat.None, 0, false),
            };

            var rowPitch = width * (hasAlpha ? 4 : 3) * sizeInBytes;

            var texture = Texture.New(
                device,
                TextureDescription.New2D(width, height, format, usage: GraphicsResourceUsage.Immutable),
                new DataBox(ptr, rowPitch, rowPitch * height));

            Marshal.FreeCoTaskMem(ptr);

            return texture;
        }
    }

    public static unsafe class ExrWriter
    {
        #pragma warning disable CA5393
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]

        [DllImport("VL.OpenEXR.Native.dll")]
        static extern int write_texture(string path, int width, int height, ExrPixelFormat format, ExrEncoding encoding, IntPtr data);

        public static int WriteTexture(byte[] data, string path, int width, int height, PixelFormat format, ExrEncoding encoding)
        {
            return WriteTexture((ReadOnlySpan<byte>)data, path, width, height, format, encoding);
        }

        public static int WriteTexture(ReadOnlySpan<byte> data, string path, int width, int height, PixelFormat format, ExrEncoding encoding)
        {
            ExrPixelFormat exrFormat = format switch
            {
                PixelFormat.R32G32B32A32_UInt  => ExrPixelFormat.U32,
                PixelFormat.R16G16B16A16_Float => ExrPixelFormat.F16,
                PixelFormat.R32G32B32A32_Float => ExrPixelFormat.F32,
                _ => ExrPixelFormat.Unknown
            };

            if(exrFormat == ExrPixelFormat.Unknown) return 1; //return with error

            fixed (byte* pointer = data)
            {
                return write_texture(path, width, height, exrFormat, encoding, new IntPtr(pointer));
            }
        }
    }
}
