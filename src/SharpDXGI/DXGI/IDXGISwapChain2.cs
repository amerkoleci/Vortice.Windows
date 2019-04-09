// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.Mathematics;

namespace SharpDXGI
{
    public partial class IDXGISwapChain2
    {
        public Size SourceSize
        {
            get
            {
                GetSourceSize(out var width, out var height);
                return new Size(width, height);
            }
            set
            {
                SetSourceSize(value.Width, value.Height);
            }
        }
    }
}
