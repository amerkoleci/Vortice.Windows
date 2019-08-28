// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.DirectX.DXGI;

namespace Vortice.DirectX.Direct2D
{
    public partial struct PixelFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PixelFormat"/> struct.
        /// </summary>
        /// <param name="format">The <see cref="Format"/></param>
        /// <param name="alphaMode">A value that specifies whether the alpha channel is using pre-multiplied alpha, straight alpha, whether it should be ignored and considered opaque, or whether it is unknown.</param>
        public PixelFormat(Format format, AlphaMode alphaMode)
        {
            Format = format;
            AlphaMode = alphaMode;
        }
    }
}
