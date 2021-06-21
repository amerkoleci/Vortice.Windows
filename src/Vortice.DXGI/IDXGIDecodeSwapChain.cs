// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Drawing;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIDecodeSwapChain
    {
        public Size DestSize
        {
            get
            {
                GetDestSize(out int width, out int height);
                return new Size(width, height);
            }
            set
            {
                SetDestSize(value.Width, value.Height);
            }
        }

        public Result PresentBuffer(int bufferToPresent, int syncInterval)
        {
            return PresentBuffer(bufferToPresent, syncInterval, PresentFlags.None);
        }
    }
}
