// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpDXGI;

namespace SharpDirect2D.WIC
{
    public partial class IWICImagingFactory
    {
        public IWICImagingFactory()
        {
            ComUtilities.CreateComInstance(
                WICImagingFactoryClsid,
                ComUtilities.CLSCTX.ClsctxInprocServer,
                typeof(IWICImagingFactory).GUID,
                this);
        }

        public IWICBitmap CreateBitmap(int width, int height, Guid pixelFormatGuid, BitmapCreateCacheOption option = BitmapCreateCacheOption.CacheOnLoad)
        {
            return CreateBitmapPrivate(width, height, pixelFormatGuid, option);
        }

        public IWICBitmapEncoder CreateEncoder(Guid guidContainerFormat)
        {
            return CreateEncoder(guidContainerFormat, null);
        }

        public IWICBitmapEncoder CreateEncoder(ContainerFormat format, Guid? guidVendor = null)
        {
            switch (format)
            {
                case ContainerFormat.Bmp:
                    return CreateEncoder(ContainerFormatGuids.Bmp, guidVendor);
                case ContainerFormat.Png:
                    return CreateEncoder(ContainerFormatGuids.Png, guidVendor);
                case ContainerFormat.Ico:
                    return CreateEncoder(ContainerFormatGuids.Ico, guidVendor);
                case ContainerFormat.Jpeg:
                    return CreateEncoder(ContainerFormatGuids.Jpeg, guidVendor);
                case ContainerFormat.Tiff:
                    return CreateEncoder(ContainerFormatGuids.Tiff, guidVendor);
                case ContainerFormat.Gif:
                    return CreateEncoder(ContainerFormatGuids.Gif, guidVendor);
                case ContainerFormat.Wmp:
                    return CreateEncoder(ContainerFormatGuids.Wmp, guidVendor);
                case ContainerFormat.Dds:
                    return CreateEncoder(ContainerFormatGuids.Dds, guidVendor);
                case ContainerFormat.Adng:
                    return CreateEncoder(ContainerFormatGuids.Adng, guidVendor);
                case ContainerFormat.Heif:
                    return CreateEncoder(ContainerFormatGuids.Heif, guidVendor);
                case ContainerFormat.Webp:
                    return CreateEncoder(ContainerFormatGuids.Webp, guidVendor);
                default:
                    return null;
            }
        }
    }
}
