// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.DirectComposition;

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
