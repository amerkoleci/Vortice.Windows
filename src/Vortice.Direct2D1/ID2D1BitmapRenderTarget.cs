// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1
{
    public partial class ID2D1BitmapRenderTarget
    {
        /// <inheritdoc/>
        protected override void DisposeCore(IntPtr nativePointer, bool disposing)
        {
            if (disposing)
            {
                DisposeBitmap();
            }

            base.DisposeCore(nativePointer, disposing);
        }

        private void DisposeBitmap()
        {
            if (Bitmap__ != null)
            {
                Bitmap__.Dispose();
                Bitmap__ = null;
            }
        }
    }
}
