// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.Direct2D1
{
    public partial class ID2D1BitmapRenderTarget
    {
        /// <inheritdoc/>
        protected override unsafe void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReleaseBitmap();
            }

            base.Dispose(disposing);
        }

        private void ReleaseBitmap()
        {
            if (Bitmap__ != null)
            {
                // Don't use Dispose() in order to avoid circular references
                ((IUnknown)Bitmap__).Release();
                Bitmap__ = null;
            }
        }
    }
}
