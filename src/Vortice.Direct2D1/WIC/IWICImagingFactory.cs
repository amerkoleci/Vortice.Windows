// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.IO;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;
using Vortice.Win32;

namespace Vortice.WIC
{
    public partial class IWICImagingFactory
    {
        public IWICImagingFactory()
        {
            ComUtilities.CreateComInstance(
                WICImagingFactoryClsid,
                ComContext.InprocServer,
                typeof(IWICImagingFactory).GUID,
                this);
        }

        /// <summary>
        /// Create new unitialized <see cref="IWICStream"/> instance.
        /// </summary>
        /// <returns>New instance of <see cref="IWICStream"/>.</returns>
        public IWICStream CreateStream() => CreateStream_();

        /// <summary>
        /// Create a <see cref="IWICStream"/> from file name.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="access">The <see cref="FileAccess"/> mode.</param>
        /// <returns>New instance of <see cref="IWICStream"/> or throws exception.</returns>
        public IWICStream CreateStream(string fileName, FileAccess access)
        {
            var stream = CreateStream_();
            stream.Initialize(fileName, access);
            return stream;
        }

        /// <summary>
        /// Create a <see cref="IWICStream"/> from another stream. Access rights are inherited from the underlying stream
        /// </summary>
        /// <param name="comStream">The initialize stream.</param>
        /// <returns>New instance of <see cref="IWICStream"/> or throws exception.</returns>
        public IWICStream CreateStream(IStream comStream)
        {
            var stream = CreateStream_();
            stream.Initialize(comStream);
            return stream;
        }

        /// <summary>
        /// Create a <see cref="IWICStream"/> from another stream. Access rights are inherited from the underlying stream
        /// </summary>
        /// <param name="stream">The initialize stream.</param>
        /// <returns>New instance of <see cref="IWICStream"/> or throws exception.</returns>
        public IWICStream CreateStream(Stream stream)
        {
            var wicStream = CreateStream_();
            wicStream.Initialize(stream);
            return wicStream;
        }

        /// <summary>
        /// Create a <see cref="IWICStream"/> from given data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Data to initialize with.</param>
        /// <returns>New instance of <see cref="IWICStream"/> or throws exception.</returns>
        public IWICStream CreateStream<T>(T[] data) where T : unmanaged
        {
            var wicStream = CreateStream_();
            wicStream.Initialize(data);
            return wicStream;
        }

        public IWICBitmap CreateBitmap(int width, int height, Guid pixelFormatGuid, BitmapCreateCacheOption option = BitmapCreateCacheOption.CacheOnLoad)
        {
            return CreateBitmap_(width, height, pixelFormatGuid, option);
        }

        public IWICBitmapEncoder CreateEncoder(Guid guidContainerFormat)
        {
            return CreateEncoder_(guidContainerFormat, null);
        }

        public IWICBitmapEncoder CreateEncoder(ContainerFormat format, Guid? guidVendor = null)
        {
            switch (format)
            {
                case ContainerFormat.Bmp:
                    return CreateEncoder_(ContainerFormatGuids.Bmp, guidVendor);
                case ContainerFormat.Png:
                    return CreateEncoder_(ContainerFormatGuids.Png, guidVendor);
                case ContainerFormat.Ico:
                    return CreateEncoder_(ContainerFormatGuids.Ico, guidVendor);
                case ContainerFormat.Jpeg:
                    return CreateEncoder_(ContainerFormatGuids.Jpeg, guidVendor);
                case ContainerFormat.Tiff:
                    return CreateEncoder_(ContainerFormatGuids.Tiff, guidVendor);
                case ContainerFormat.Gif:
                    return CreateEncoder_(ContainerFormatGuids.Gif, guidVendor);
                case ContainerFormat.Wmp:
                    return CreateEncoder_(ContainerFormatGuids.Wmp, guidVendor);
                case ContainerFormat.Dds:
                    return CreateEncoder_(ContainerFormatGuids.Dds, guidVendor);
                case ContainerFormat.Adng:
                    return CreateEncoder_(ContainerFormatGuids.Adng, guidVendor);
                case ContainerFormat.Heif:
                    return CreateEncoder_(ContainerFormatGuids.Heif, guidVendor);
                case ContainerFormat.Webp:
                    return CreateEncoder_(ContainerFormatGuids.Webp, guidVendor);
                default:
                    return null;
            }
        }

        public IWICBitmapEncoder CreateEncoder(Guid guidContainerFormat, IStream stream, BitmapEncoderCacheOption cacheOption = BitmapEncoderCacheOption.NoCache)
        {
            var encoder = CreateEncoder_(guidContainerFormat, null);
            encoder.Initialize(stream, cacheOption);
            return encoder;
        }

        public IWICBitmapEncoder CreateEncoder(ContainerFormat format, IStream stream, BitmapEncoderCacheOption cacheOption = BitmapEncoderCacheOption.NoCache)
        {
            var encoder = CreateEncoder(format, null);
            encoder.Initialize(stream, cacheOption);
            return encoder;
        }

        public IWICBitmapEncoder CreateEncoder(ContainerFormat format, Guid guidVendor, IStream stream, BitmapEncoderCacheOption cacheOption = BitmapEncoderCacheOption.NoCache)
        {
            var encoder = CreateEncoder(format, guidVendor);
            encoder.Initialize(stream, cacheOption);
            return encoder;
        }

        public IWICBitmapEncoder CreateEncoder(Guid guidContainerFormat, Stream stream, BitmapEncoderCacheOption cacheOption = BitmapEncoderCacheOption.NoCache)
        {
            var encoder = CreateEncoder_(guidContainerFormat, null);
            encoder.Initialize(stream, cacheOption);
            return encoder;
        }

        public IWICBitmapEncoder CreateEncoder(ContainerFormat format, Stream stream, BitmapEncoderCacheOption cacheOption = BitmapEncoderCacheOption.NoCache)
        {
            var encoder = CreateEncoder(format, null);
            encoder.Initialize(stream, cacheOption);
            return encoder;
        }

        public IWICBitmapEncoder CreateEncoder(ContainerFormat format, Guid guidVendor, Stream stream, BitmapEncoderCacheOption cacheOption = BitmapEncoderCacheOption.NoCache)
        {
            var encoder = CreateEncoder(format, guidVendor);
            encoder.Initialize(stream, cacheOption);
            return encoder;
        }

        public IWICBitmapDecoder CreateDecoderFromFilename(string fileName, DecodeOptions metadataOptions)
        {
            return CreateDecoderFromFilename(fileName, null, FileAccess.Read, metadataOptions);
        }

        public IWICBitmapDecoder CreateDecoderFromFilename(
            string fileName,
            FileAccess desiredAccess,
            DecodeOptions metadataOptions)
        {
            return CreateDecoderFromFilename(fileName, null, desiredAccess, metadataOptions);
        }

        public IWICBitmapDecoder CreateDecoderFromFilename(string fileName, Guid? guidVendor, FileAccess desiredAccess, DecodeOptions metadataOptions)
        {
            var nativeAccess = desiredAccess.ToNative();
            return CreateDecoderFromFilename_(fileName, guidVendor, (int)nativeAccess, metadataOptions);
        }
    }
}
