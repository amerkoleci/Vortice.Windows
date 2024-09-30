// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WIC;

public partial class IWICBitmapDecoder
{
    internal IWICStream? _wicStream;

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
