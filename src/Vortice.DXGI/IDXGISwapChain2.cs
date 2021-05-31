// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Drawing;
using Vortice.Mathematics;

namespace Vortice.DXGI
{
    public partial class IDXGISwapChain2
    {
        public Size SourceSize
        {
            get
            {
                GetSourceSize(out int width, out int height);
                return new Size(width, height);
            }
            set
            {
                SetSourceSize(value.Width, value.Height);
            }
        }
    }
}
