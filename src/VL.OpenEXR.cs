using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Stride.Core.Mathematics;
using Stride.Graphics;
using VL.OpenEXR;

namespace OpenEXR
{
    enum ExrPixelFormat
    {
        Unknown = -1,
        U32 = 0,
        F16 = 1,
        F32 = 2,
    }

    public enum ExrEncoding {
        Uncompressed = 0,
        RLE = 1,
        ZIP1 = 2,
        ZIP16 = 3,
        PIZ = 4,
    }

    public enum ExrOutputChannels {
        Rgb = 0,
        Rgba = 1,
    }

    public static class ExrLoader
    {
        #pragma warning disable CA5393
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]

        [DllImport("VL.OpenEXR.Native.dll")]
        static extern Int32 load_from_path(string path, out int width, out int height, out int num_channels, out ExrPixelFormat format, out IntPtr data);

        public static Texture LoadFromPath(string path, GraphicsDevice device, bool useOpenEXRCore = true)
        {
            if (Path.GetExtension(path) == ".hdr")
                useOpenEXRCore = false;

            if (useOpenEXRCore)
                return LoadFromPathOpenEXRCore(path, device);

            var result = load_from_path(path, out var width, out var height, out var numChannels, out var exrFormat, out var ptr);

            if (result != 0) 
                return null;

            if (exrFormat == ExrPixelFormat.Unknown || ptr == IntPtr.Zero)
                return null;

            var format = GetPixelFormat(exrFormat, numChannels);
            var rowPitch = width * format.SizeInBytes();

            var texture = Texture.New(
                device,
                TextureDescription.New2D(width, height, format, usage: GraphicsResourceUsage.Immutable),
                new DataBox(ptr, rowPitch, rowPitch * height));

            Marshal.FreeCoTaskMem(ptr);

            return texture;
        }

        private static PixelFormat GetPixelFormat(ExrPixelFormat exrFormat, int numChannels)
        {
            return (exrFormat, numChannels) switch
            {
                (ExrPixelFormat.F16, 4) => PixelFormat.R16G16B16A16_Float,
                (ExrPixelFormat.F32, 4) => PixelFormat.R32G32B32A32_Float,
                (ExrPixelFormat.U32, 4) => PixelFormat.R32G32B32A32_UInt,
                (ExrPixelFormat.F16, 3) => PixelFormat.R16G16B16A16_Float,
                (ExrPixelFormat.F32, 3) => PixelFormat.R32G32B32_Float,
                (ExrPixelFormat.U32, 3) => PixelFormat.R32G32B32_UInt,
                (ExrPixelFormat.F16, 2) => PixelFormat.R16G16_Float,
                (ExrPixelFormat.F32, 2) => PixelFormat.R32G32_Float,
                (ExrPixelFormat.U32, 2) => PixelFormat.R32G32_UInt,
                (ExrPixelFormat.F16, 1) => PixelFormat.R16_Float,
                (ExrPixelFormat.F32, 1) => PixelFormat.R32_Float,
                (ExrPixelFormat.U32, 1) => PixelFormat.R32_UInt,
                _ => PixelFormat.None,
            };
        }

        private static unsafe Texture LoadFromPathOpenEXRCore(string path, GraphicsDevice device, int partIndex = 0)
        {
            using var context = ExrContext.OpenRead(path);
            var part = context.GetPart(partIndex);
            var selectedChannels = part.GetChannels()
                .OrderBy(c => c.Order)
                .Take(4)
                .ToArray();

            var exrFormat = (ExrPixelFormat)selectedChannels[0].PixelType;
            var pixelFormat = GetPixelFormat(exrFormat, selectedChannels.Length);
            if (pixelFormat == PixelFormat.R16G16B16A16_Float && selectedChannels.Length == 3)
            {
                // We use PixelFormat.R16G16B16A16_Float for 3 channels, so we need to add an alpha channel
                selectedChannels = selectedChannels.Append(new ExrChannel(-1, "A", Interop.exr_pixel_type_t.EXR_PIXEL_HALF)).ToArray();
            }

            return exrFormat switch
            { 
                ExrPixelFormat.U32 => Decode<UIntElement>(),
                ExrPixelFormat.F16 => Decode<HalfElement>(),
                ExrPixelFormat.F32 => Decode<FloatElement>(),
                _ => throw new NotImplementedException()
            };

            Texture Decode<T>() where T : unmanaged, IElement<T>
            {
                using var memory = part.Decode<T>(selectedChannels);
                using var handle = memory.Memory.Pin();

                var dataWindow = part.GetDataWindow();
                var rowPitch = dataWindow.Width * pixelFormat.SizeInBytes();
                return Texture.New(
                    device, 
                    TextureDescription.New2D(dataWindow.Width, dataWindow.Height, pixelFormat, usage: GraphicsResourceUsage.Immutable),
                    new DataBox((nint)handle.Pointer, rowPitch, rowPitch * dataWindow.Height));
            }
        }
    }

    public static unsafe class ExrWriter
    {
        #pragma warning disable CA5393
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]

        [DllImport("VL.OpenEXR.Native.dll")]
        static extern int write_texture(string path, int width, int height, ExrPixelFormat format, ExrEncoding encoding, ExrOutputChannels outputChannels, IntPtr data);

        public static int WriteTexture(byte[] data, string path, int width, int height, PixelFormat format, ExrEncoding encoding, ExrOutputChannels outputChannels)
        {
            return WriteTexture((ReadOnlySpan<byte>)data, path, width, height, format, encoding, outputChannels);
        }

        public static int WriteTexture(ReadOnlySpan<byte> data, string path, int width, int height, PixelFormat format, ExrEncoding encoding, ExrOutputChannels outputChannels)
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
                return write_texture(path, width, height, exrFormat, encoding, outputChannels, new IntPtr(pointer));
            }
        }
    }
}
