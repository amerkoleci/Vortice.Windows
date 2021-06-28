// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1
{
    public partial class ID2D1Bitmap1
    {
        /// <inheritdoc/>
        protected override void DisposeCore(IntPtr nativePointer, bool disposing)
        {
            if (disposing)
            {
                DisposeColorContext();
            }

            base.DisposeCore(nativePointer, disposing);
        }

        private void DisposeColorContext()
        {
            if (ColorContext__ != null)
            {
                ColorContext__.Dispose();
                ColorContext__ = null;
            }
        }
    }
}
