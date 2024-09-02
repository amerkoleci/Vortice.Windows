// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;
using static Vortice.MediaFoundation.MediaFactory;

namespace Vortice.MediaFoundation;

public unsafe partial class MFByteStream
{
    private Stream? _sourceStream;
    private readonly bool _disposeStream;
    private ComStream? _comStream;
    private ComStreamProxy? _streamProxy;

    /// <summary>
    /// Instantiates a new instance <see cref="MFByteStream"/> from a <see cref="Stream"/>.
    /// </summary>
    public MFByteStream(Stream sourceStream, bool disposeStream = false)
    {
        _sourceStream = sourceStream;
        _disposeStream = disposeStream;

        if (PlatformDetection.IsAppContainerProcess)
        {
            //var randomAccessStream = sourceStream.AsRandomAccessStream();
            //MFCreateMFByteStreamOnStreamEx(new ComObject(Marshal.GetIUnknownForObject(randomAccessStream)), this);
        }
        else
        {
            _streamProxy = new ComStreamProxy(sourceStream);
            MFCreateMFByteStreamOnStream(_streamProxy, this);
        }
    }

    /// <summary>
    /// Instantiates a new instance <see cref="MFByteStream"/> from a <see cref="Stream"/>.
    /// </summary>
    public MFByteStream(byte[] sourceStream)
        : this(new MemoryStream(sourceStream))
    {
    }

    /// <summary>
    /// Instantiates a new instance <see cref="MFByteStream"/> from a <see cref="Stream"/>.
    /// </summary>
    public MFByteStream(string fileName)
        : this(File.OpenRead(fileName), true)
    {
    }

    /// <summary>
    /// Instantiates a new instance <see cref="MFByteStream"/> from a <see cref="ComStream"/>.
    /// </summary>
    /// <msdn-id>hh162754</msdn-id>	
    /// <unmanaged>HRESULT MFCreateMFByteStreamOnStreamEx([In] IUnknown* punkStream,[Out] IMFByteStream** ppByteStream)</unmanaged>	
    /// <unmanaged-short>MFCreateMFByteStreamOnStreamEx</unmanaged-short>	
    public MFByteStream(ComStream sourceStream)
    {
        _comStream = sourceStream;
        MFCreateMFByteStreamOnStream(sourceStream, this);
    }

    protected override unsafe void DisposeCore(IntPtr nativePointer, bool disposing)
    {
        base.DisposeCore(nativePointer, disposing);

        if (_streamProxy != null)
        {
            _streamProxy.Dispose();
            _streamProxy = default;
        }

        if (_disposeStream)
        {
            _sourceStream?.Dispose();
            _sourceStream = default;
        }
    }

    public uint Read(Span<byte> bRef, int offset, uint count)
    {
        fixed (void* ptr = &bRef[offset])
        {
            return Read((IntPtr)ptr, count);
        }
    }

    public uint Read(byte[] bRef, int offset, uint count)
    {
        fixed (void* ptr = &bRef[offset])
        {
            return Read((IntPtr)ptr, count);
        }
    }

    public void BeginRead(Span<byte> bRef, int offset, uint count, IMFAsyncCallback callback, object? context = default)
    {
        fixed (void* ptr = &bRef[offset])
        {
            BeginRead((IntPtr)ptr, count, callback, context != null ? Marshal.GetIUnknownForObject(context) : IntPtr.Zero);
        }
    }

    public void BeginRead(byte[] bRef, int offset, uint count, IMFAsyncCallback callback, object? context = default)
    {
        fixed (void* ptr = &bRef[offset])
        {
            BeginRead((IntPtr)ptr, count, callback, context != null ? Marshal.GetIUnknownForObject(context) : IntPtr.Zero);
        }
    }

    public uint Write(Span<byte> bRef, int offset, uint count)
    {
        fixed (void* ptr = &bRef[offset])
        {
            return Write((IntPtr)ptr, count);
        }
    }

    public uint Write(byte[] bRef, int offset, uint count)
    {
        fixed (void* ptr = &bRef[offset])
        {
            return Write((IntPtr)ptr, count);
        }
    }

    public void BeginWrite(Span<byte> bRef, int offset, uint count, IMFAsyncCallback callback, object? context = default)
    {
        fixed (void* ptr = &bRef[offset])
        {
            BeginWrite((IntPtr)ptr, count, callback, context != null ? Marshal.GetIUnknownForObject(context) : IntPtr.Zero);
        }
    }

    public void BeginWrite(byte[] bRef, int offset, uint count, IMFAsyncCallback callback, object? context = default)
    {
        fixed (void* ptr = &bRef[offset])
        {
            BeginWrite((IntPtr)ptr, count, callback, context != null ? Marshal.GetIUnknownForObject(context) : IntPtr.Zero);
        }
    }
}
