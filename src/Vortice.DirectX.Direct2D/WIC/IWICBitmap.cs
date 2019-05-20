// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;

namespace Vortice.DirectX.Direct2D.WIC
{
    public partial class IWICBitmap
    {
        public IWICBitmapLock Lock(BitmapLockFlags flags)
        {
            return Lock(IntPtr.Zero, flags);
        }

        public unsafe IWICBitmapLock Lock(Rectangle lockRectangle, BitmapLockFlags flags = BitmapLockFlags.None)
        {
            return Lock(new IntPtr(&lockRectangle), flags);
        }
    }
}
