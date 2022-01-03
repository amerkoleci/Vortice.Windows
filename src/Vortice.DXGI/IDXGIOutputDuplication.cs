// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIOutputDuplication
{
    public DataRectangle MapDesktopSurface()
    {
        MapDesktopSurface(out MappedRect mappedRect).CheckError();
        return new DataRectangle(mappedRect.Bits, mappedRect.Pitch);
    }
}
