// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.WIC
{
    public static partial class WIC
    {
        public static Guid GetGuid(ContainerFormat format)
        {
            switch (format)
            {
                case ContainerFormat.Bmp:
                    return ContainerFormatGuids.Bmp;
                case ContainerFormat.Png:
                    return ContainerFormatGuids.Png;
                case ContainerFormat.Ico:
                    return ContainerFormatGuids.Ico;
                case ContainerFormat.Jpeg:
                    return ContainerFormatGuids.Jpeg;
                case ContainerFormat.Tiff:
                    return ContainerFormatGuids.Tiff;
                case ContainerFormat.Gif:
                    return ContainerFormatGuids.Gif;
                case ContainerFormat.Wmp:
                    return ContainerFormatGuids.Wmp;
                case ContainerFormat.Dds:
                    return ContainerFormatGuids.Dds;
                case ContainerFormat.Adng:
                    return ContainerFormatGuids.Adng;
                case ContainerFormat.Heif:
                    return ContainerFormatGuids.Heif;
                case ContainerFormat.Webp:
                    return ContainerFormatGuids.Webp;
                case ContainerFormat.Raw:
                    return ContainerFormatGuids.Raw;
                default:
                    throw new ArgumentException(nameof(format));
            }
        }

        public static IWICBitmapSource WICConvertBitmapSource(Guid dstFormat, IWICBitmapSource source)
        {
            WICConvertBitmapSource_(dstFormat, source, out IWICBitmapSource destination).CheckError();
            return destination;
        }

        public static Result WICConvertBitmapSource(Guid dstFormat, IWICBitmapSource source, out IWICBitmapSource destination)
        {
            return WICConvertBitmapSource_(dstFormat, source, out destination);
        }

        public static IWICBitmap WICCreateBitmapFromSection(int width, int height, Guid pixelFormat, IntPtr section, int stride, int offset)
        {
            WICCreateBitmapFromSection_(width, height, pixelFormat, section, stride, offset, out IWICBitmap result).CheckError();
            return result;
        }

        public static Result WICCreateBitmapFromSection(int width, int height, Guid pixelFormat, IntPtr section, int stride, int offset, out IWICBitmap result)
        {
            return WICCreateBitmapFromSection_(width, height, pixelFormat, section, stride, offset, out result);
        }
    }
}
