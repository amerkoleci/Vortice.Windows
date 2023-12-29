// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;

namespace Vortice.WIC;

public unsafe partial class IWICBitmap
{
    public IWICBitmapLock Lock(BitmapLockFlags flags) => Lock(null, flags);

    public IWICBitmapLock Lock(Rectangle lockRectangle, BitmapLockFlags flags = BitmapLockFlags.None)
    {
        return Lock(&lockRectangle, flags);
    }
}
