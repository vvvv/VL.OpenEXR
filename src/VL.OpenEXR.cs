using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Stride.Graphics;
using Stride.Core.Mathematics;
using VL.OpenEXR;
using VL.Core;
using CommunityToolkit.HighPerformance;
using System.Threading;

namespace OpenEXR
{
    enum ExrPixelFormat
    {
        Unknown = -1,
        U32 = 0,
        F16 = 1,
        F32 = 2,
    }

    public enum ExrEncoding 
    {
        Uncompressed = 0,
        RLE = 1,
        ZIP1 = 2,
        ZIP16 = 3,
        PIZ = 4,
        PXR24 = 5,
        B44 = 6,
        B44A = 7,
        DWAA = 8,
        DWAB = 9,
    }

    public enum ExrOutputChannels 
    {
        Rgb = 0,
        Rgba = 1,
    }

    public enum ExrStorage
    {
        Scanline,
        Tiled
    }

    public static class ExrLoader
    {
        #pragma warning disable CA5393
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]

        [DllImport("VL.OpenEXR.Native.dll")]
        static extern Int32 load_from_path(string path, out int width, out int height, out int num_channels, out ExrPixelFormat format, out IntPtr data);

        public static Texture LoadFromPath(string path, GraphicsDevice device, int partIndex = 0, Optional<RectangleF> window = default)
        {
            if (Path.GetExtension(path) == ".hdr")
                return LoadHDR(path, device);
            else
                return LoadEXR(path, device, partIndex, window);
        }

        private static unsafe Texture LoadEXR(string path, GraphicsDevice device, int partIndex = 0, Optional<RectangleF> window = default)
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
                var dataWindow = part.GetDataWindow();
                if (window.HasValue)
                    dataWindow = Rectangle.Intersect(dataWindow, (Rectangle)window.Value);
                if (dataWindow.IsEmpty)
                    return null;

                using var memory = part.Decode<T>(selectedChannels, dataWindow);
                using var handle = memory.Memory.Pin();

                return CreateTexture((nint)handle.Pointer, dataWindow.Width, dataWindow.Height, pixelFormat, device);
            }
        }

        private static unsafe Texture LoadHDR(string path, GraphicsDevice device)
        {
            var result = load_from_path(path, out var width, out var height, out var numChannels, out var exrFormat, out var ptr);

            if (result != 0)
                return null;

            if (exrFormat == ExrPixelFormat.Unknown || ptr == IntPtr.Zero)
                return null;

            var format = GetPixelFormat(exrFormat, numChannels);
            var texture = CreateTexture(ptr, width, height, format, device);

            Marshal.FreeCoTaskMem(ptr);

            return texture;
        }

        private static Texture CreateTexture(nint data, int width, int height, PixelFormat format, GraphicsDevice device)
        {
            var rowPitch = width * format.SizeInBytes();
            return Texture.New(
                device,
                TextureDescription.New2D(width, height, format, usage: GraphicsResourceUsage.Immutable),
                new DataBox(data, rowPitch, rowPitch * height));
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
    }

    public static unsafe class ExrWriter
    {
        #pragma warning disable CA5393
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]

        [DllImport("VL.OpenEXR.Native.dll")]
        static extern int write_texture(string path, int width, int height, ExrPixelFormat format, ExrEncoding encoding, ExrOutputChannels outputChannels, IntPtr data);

        public static void WriteTexture(byte[] data, string path, int width, int height, PixelFormat format, ExrEncoding encoding, ExrOutputChannels outputChannels, ExrStorage storage)
        {
            WriteTexture((ReadOnlySpan<byte>)data, path, width, height, format, encoding, outputChannels, storage);
        }

        public static void WriteTexture(ReadOnlySpan<byte> data, string path, int width, int height, PixelFormat format, ExrEncoding encoding, ExrOutputChannels outputChannels, ExrStorage storage)
        {
            switch (format)
            {
                case PixelFormat.R32G32B32A32_UInt:
                    Encode<UIntElement>(data);
                    break;
                case PixelFormat.R16G16B16A16_Float:
                    Encode<HalfElement>(data);
                    break;
                case PixelFormat.R32G32B32A32_Float:
                    Encode<FloatElement>(data);
                    break;
                default:
                    throw new ArgumentException($"Incompatible pixel format {format}");
            }

            void Encode<T>(ReadOnlySpan<byte> data) where T : unmanaged, IElement<T>
            {
                using var context = ExrContext.OpenWrite(path, Interop.exr_default_write_mode_t.EXR_WRITE_FILE_DIRECTLY);
                var part = context.AddPart(Path.GetFileNameWithoutExtension(path), (Interop.exr_storage_t)storage);

                ReadOnlySpan<ExrChannel> channels = outputChannels switch
                {
                    ExrOutputChannels.Rgba =>
                    [
                        new(3, "R", T.PixelType),
                        new(2, "G", T.PixelType),
                        new(1, "B", T.PixelType),
                        new(0, "A", T.PixelType)
                    ],
                    ExrOutputChannels.Rgb =>
                    [
                        new(2, "R", T.PixelType),
                        new(1, "G", T.PixelType),
                        new(0, "B", T.PixelType),
                        new(-1, "A", T.PixelType),
                    ],
                    _ => throw new NotImplementedException()
                };

                var halfData = MemoryMarshal.Cast<byte, T>(data).AsSpan2D(height, width * channels.Length);
                part.Encode(halfData, channels, (Interop.exr_compression_t)encoding, (Interop.exr_storage_t)storage);
            }
        }
    }
}
