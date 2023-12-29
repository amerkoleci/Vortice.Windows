// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Win32;
using SharpGen.Runtime.Win32;

namespace Vortice.WIC;

public unsafe partial class IWICStream
{
    private ComStreamProxy? _streamProxy;

    /// <summary>
    /// Initialize stream from file name.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="access">The <see cref="FileAccess"/> mode.</param>
    /// <returns>The <see cref="Result"/> of the operation.</returns>
    public Result Initialize(string fileName, FileAccess access)
    {
        DisposeStreamProxy();

        NativeFileAccess desiredAccess = access.ToNative();
        return InitializeFromFilename(fileName, (int)desiredAccess);
    }

    /// <summary>
    /// Initialize the <see cref="IWICStream"/> from another stream. Access rights are inherited from the underlying stream
    /// </summary>
    /// <param name="comStream"></param>
    /// <returns>The <see cref="Result"/> of the operation.</returns>
    public Result Initialize(IStream comStream)
    {
        DisposeStreamProxy();
        return InitializeFromIStream(comStream);
    }

    /// <summary>
    /// Initialize the <see cref="IWICStream"/> from another stream. Access rights are inherited from the underlying stream
    /// </summary>
    /// <param name="stream">The initialize stream.</param>
    /// <returns>The <see cref="Result"/> of the operation.</returns>
    public Result Initialize(Stream stream)
    {
        DisposeStreamProxy();
        _streamProxy = new ComStreamProxy(stream);
        return InitializeFromIStream(_streamProxy);
    }

    /// <summary>
    /// Initialize the stream from given data.
    /// </summary>
    /// <param name="data">Data to initialize with.</param>
    /// <returns>The <see cref="Result"/> of the operation.</returns>
    public Result Initialize(byte[] data)
    {
        DisposeStreamProxy();
        fixed (void* dataPtr = &data[0])
        {
            return InitializeFromMemory(dataPtr, data.Length);
        }
    }

    /// <summary>
    /// Initialize the stream from given data.
    /// </summary>
    /// <param name="data">Data to initialize with.</param>
    /// <returns>The <see cref="Result"/> of the operation.</returns>
    public Result Initialize<T>(ReadOnlySpan<T> data) where T : unmanaged
    {
        DisposeStreamProxy();

        fixed (void* dataPtr = data)
        {
            return InitializeFromMemory(dataPtr, data.Length * sizeof(T));
        }
    }

    /// <summary>
    /// Initialize the stream from given data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">Data to initialize with.</param>
    /// <returns>The <see cref="Result"/> of the operation.</returns>
    public Result Initialize<T>(T[] data) where T : unmanaged
    {
        DisposeStreamProxy();

        fixed (void* dataPtr = data)
        {
            return InitializeFromMemory(dataPtr, data.Length * sizeof(T));
        }
    }


    protected override void DisposeCore(IntPtr nativePointer, bool disposing)
    {
        base.DisposeCore(nativePointer, disposing);

        DisposeStreamProxy();
    }

    private void DisposeStreamProxy()
    {
        if (_streamProxy != null)
        {
            _streamProxy.Dispose();
            _streamProxy = null;
        }
    }
}
