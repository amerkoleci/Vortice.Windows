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
        public IWICStream? CreateStream(Stream stream)
        {
            var wicStream = CreateStream_();
            if (wicStream.Initialize(stream).Failure)
            {
                wicStream.Dispose();
                return null;
            }

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
            IWICBitmapEncoder encoder = CreateEncoder_(guidContainerFormat, null);
            encoder._factory = this;
            return encoder;
        }

        public IWICBitmapEncoder CreateEncoder(ContainerFormat format, Guid? guidVendor = null)
        {
            IWICBitmapEncoder encoder = CreateEncoder_(WIC.GetGuid(format), guidVendor);
            encoder._factory = this;
            return encoder;
        }

        public IWICBitmapEncoder CreateEncoder(Guid guidContainerFormat, IStream stream, BitmapEncoderCacheOption cacheOption = BitmapEncoderCacheOption.NoCache)
        {
            var encoder = CreateEncoder_(guidContainerFormat, null);
            encoder._factory = this;
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
            encoder._factory = this;
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

        public IWICBitmapDecoder CreateDecoderFromStream(IStream stream, DecodeOptions metadataOptions = DecodeOptions.CacheOnDemand)
        {
            return CreateDecoderFromStream_(stream, null, metadataOptions);
        }

        public IWICBitmapDecoder CreateDecoderFromStream(IStream stream, Guid vendor, DecodeOptions metadataOptions = DecodeOptions.CacheOnDemand)
        {
            return CreateDecoderFromStream_(stream, vendor, metadataOptions);
        }

        public IWICBitmapDecoder CreateDecoderFromStream(Stream stream, DecodeOptions metadataOptions = DecodeOptions.CacheOnDemand)
        {
            var wicStream = CreateStream(stream);
            var decoder = CreateDecoderFromStream_(wicStream, null, metadataOptions);
            decoder._wicStream = wicStream;
            return decoder;
        }

        public IWICBitmapDecoder CreateDecoderFromStream(Stream stream, Guid vendor, DecodeOptions metadataOptions = DecodeOptions.CacheOnDemand)
        {
            var wicStream = CreateStream(stream);
            var decoder = CreateDecoderFromStream_(wicStream, vendor, metadataOptions);
            decoder._wicStream = wicStream;
            return decoder;
        }

        public IWICBitmapDecoder CreateDecoderFromFileName(string fileName, FileAccess desiredAccess = FileAccess.Read, DecodeOptions metadataOptions = DecodeOptions.CacheOnDemand)
        {
            NativeFileAccess nativeAccess = desiredAccess.ToNative();
            return CreateDecoderFromFilename_(fileName, null, (int)nativeAccess, metadataOptions);
        }

        public IWICBitmapDecoder CreateDecoderFromFileName(string fileName, Guid? guidVendor, FileAccess desiredAccess = FileAccess.Read, DecodeOptions metadataOptions = DecodeOptions.CacheOnDemand)
        {
            NativeFileAccess nativeAccess = desiredAccess.ToNative();
            return CreateDecoderFromFilename_(fileName, guidVendor, (int)nativeAccess, metadataOptions);
        }

        public unsafe IWICBitmap CreateBitmapFromMemory<T>(int width, int height, Guid pixelFormat, T[] pixelData, int stride = 0) where T : unmanaged
        {
            if (stride == 0)
            {
                stride = width * sizeof(T);
            }

            int sizeInBytes = height * stride;
            fixed (void* pixelDataPtr = &pixelData[0])
            {
                return CreateBitmapFromMemory(width, height, pixelFormat, stride, sizeInBytes, pixelDataPtr);
            }
        }

        public unsafe IWICBitmap CreateBitmapFromMemory<T>(int width, int height, Guid pixelFormat, Span<T> pixelData, int stride = 0) where T : unmanaged
        {
            if (stride == 0)
            {
                stride = width * sizeof(T);
            }

            int sizeInBytes = height * stride;
            fixed (void* pixelDataPtr = &pixelData[0])
            {
                return CreateBitmapFromMemory(width, height, pixelFormat, stride, sizeInBytes, pixelDataPtr);
            }
        }
    }
}
