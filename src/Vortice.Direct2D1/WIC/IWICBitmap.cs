// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.WIC;

public partial class IWICBitmap
{
    public IWICBitmapLock Lock(BitmapLockFlags flags) => Lock(IntPtr.Zero, flags);

    public unsafe IWICBitmapLock Lock(RectangleI lockRectangle, BitmapLockFlags flags = BitmapLockFlags.None)
    {
        return Lock(new IntPtr(&lockRectangle), flags);
    }
}
