// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime.Win32;

namespace Vortice.WIC;

public partial class IWICBitmapEncoder
{
    internal IWICImagingFactory _factory;
    private IWICStream? _wicStream;

    /// <summary>
    /// Initializes the encoder with the provided stream.
    /// </summary>
    /// <param name="stream">The stream to use for initialization.</param>
    /// <param name="cacheOption">The <see cref="BitmapEncoderCacheOption"/> used on initialization.</param>
    public void Initialize(IStream stream, BitmapEncoderCacheOption cacheOption = BitmapEncoderCacheOption.NoCache)
    {
        DisposeWICStreamProxy();
        Initialize_(stream, cacheOption);
    }

    /// <summary>
    /// Initializes the encoder with the provided stream.
    /// </summary>
    /// <param name="stream">The stream to use for initialization.</param>
    /// <param name="cacheOption">The <see cref="BitmapEncoderCacheOption"/> used on initialization.</param>
    public void Initialize(Stream stream, BitmapEncoderCacheOption cacheOption = BitmapEncoderCacheOption.NoCache)
    {
        DisposeWICStreamProxy();

        _wicStream = _factory.CreateStream(stream);
        Initialize_(_wicStream, cacheOption);
    }

    protected override void DisposeCore(IntPtr nativePointer, bool disposing)
    {
        base.DisposeCore(nativePointer, disposing);

        DisposeWICStreamProxy();
    }

    private void DisposeWICStreamProxy()
    {
        if (_wicStream != null)
        {
            _wicStream.Dispose();
            _wicStream = null;
        }
    }
}
