// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIOutput3
{
    /// <summary>
    /// Checks for overlay support.
    /// </summary>
    /// <param name="format">A <see cref="Format"/> value for the color format.</param>
    /// <param name="concernedDevice">Instance of Direct3D device interface.</param>
    /// <returns>Overlay support flags.</returns>
    public OverlaySupportFlags CheckOverlaySupport(Format format, IUnknown concernedDevice)
    {
        return CheckOverlaySupport_(format, concernedDevice);
    }
}
