// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.DXGI;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionSurfaceFactory
    {
        public IDCompositionSurface CreateSurface(int width, int height, Format pixelFormat, AlphaMode alphaMode)
        {
            CreateSurface(width, height, pixelFormat, alphaMode, out IDCompositionSurface surface).CheckError();
            return surface;
        }

        public IDCompositionVirtualSurface CreateVirtualSurface(int initialWidth, int initialHeight, Format pixelFormat, AlphaMode alphaMode)
        {
            CreateVirtualSurface(initialWidth, initialHeight, pixelFormat, alphaMode, out IDCompositionVirtualSurface virtualSurface).CheckError();
            return virtualSurface;
        }
    }
}
