// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime.Win32;
using Vortice.Win32;

namespace Vortice.WIC;

public unsafe partial class IWICImagingFactory
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
        IWICStream stream = CreateStream_();
        stream.Initialize(fileName, access).CheckError();
        return stream;
    }

    /// <summary>
    /// Create a <see cref="IWICStream"/> from another stream. Access rights are inherited from the underlying stream
    /// </summary>
    /// <param name="comStream">The initialize stream.</param>
    /// <returns>New instance of <see cref="IWICStream"/> or throws exception.</returns>
    public IWICStream CreateStream(IStream comStream)
    {
        IWICStream stream = CreateStream_();
        stream.Initialize(comStream).CheckError();
        return stream;
    }

    /// <summary>
    /// Create a <see cref="IWICStream"/> from another stream. Access rights are inherited from the underlying stream
    /// </summary>
    /// <param name="stream">The initialize stream.</param>
    /// <returns>New instance of <see cref="IWICStream"/> or throws exception.</returns>
    public IWICStream CreateStream(Stream stream)
    {
        IWICStream wicStream = CreateStream_();
        wicStream.Initialize(stream).CheckError();
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
        IWICStream wicStream = CreateStream_();
        wicStream.Initialize(data).CheckError();
        return wicStream;
    }

    public IWICBitmap CreateBitmap(uint width, uint height, Guid pixelFormatGuid, BitmapCreateCacheOption option = BitmapCreateCacheOption.CacheOnLoad)
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
        IWICBitmapEncoder encoder = CreateEncoder_(guidContainerFormat, null);
        encoder._factory = this;
        encoder.Initialize(stream, cacheOption);
        return encoder;
    }

    public IWICBitmapEncoder CreateEncoder(ContainerFormat format, IStream stream, BitmapEncoderCacheOption cacheOption = BitmapEncoderCacheOption.NoCache)
    {
        IWICBitmapEncoder encoder = CreateEncoder(format, null);
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

    public IWICBitmapDecoder CreateDecoder(Guid guidContainerFormat)
    {
        return CreateDecoder(guidContainerFormat, null);
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
        IWICStream wicStream = CreateStream(stream);
        IWICBitmapDecoder decoder = CreateDecoderFromStream_(wicStream, null, metadataOptions);
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

    public IWICBitmap CreateBitmapFromMemory<T>(uint width, uint height, Guid pixelFormat, T[] source, uint stride = 0) where T : unmanaged
    {
        ReadOnlySpan<T> span = source.AsSpan();

        return CreateBitmapFromMemory(width, height, pixelFormat, span, stride);
    }

    public IWICBitmap CreateBitmapFromMemory<T>(uint width, uint height, Guid pixelFormat, ReadOnlySpan<T> source, uint stride = 0) where T : unmanaged
    {
        return CreateBitmapFromMemory(width, height, pixelFormat, ref MemoryMarshal.GetReference(source), stride);
    }

    public IWICBitmap CreateBitmapFromMemory<T>(uint width, uint height, Guid pixelFormat, ref T source, uint stride = 0) where T : unmanaged
    {
        if (stride == 0)
        {
            stride = PixelFormat.GetStride(pixelFormat, width);
        }

        uint sizeInBytes = height * stride;
        fixed (void* sourcePointer = &source)
        {
            return CreateBitmapFromMemory(width, height, pixelFormat, stride, sizeInBytes, sourcePointer);
        }
    }

    public IWICBitmap CreateBitmapFromMemory(uint width, uint height, Guid pixelFormat, IntPtr buffer, uint stride = 0, uint bufferSize = 0)
    {
        if (stride == 0)
        {
            stride = PixelFormat.GetStride(pixelFormat, width);
        }

        if (bufferSize == 0)
        {
            bufferSize = height * stride;
        }

        return CreateBitmapFromMemory(width, height, pixelFormat, stride, bufferSize, buffer.ToPointer());
    }

    public IWICBitmap CreateBitmapFromMemory(uint width, uint height, Guid pixelFormat, uint stride, uint bufferSize, IntPtr buffer)
    {
        return CreateBitmapFromMemory(width, height, pixelFormat, stride, bufferSize, buffer.ToPointer());
    }
}
