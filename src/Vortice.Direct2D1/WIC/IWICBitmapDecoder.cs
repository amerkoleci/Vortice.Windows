// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.WIC
{
    public partial class IWICBitmapDecoder
    {
        internal IWICStream? _wicStream;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

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
}
