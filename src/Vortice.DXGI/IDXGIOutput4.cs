// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIOutput4
{
    /// <summary>
    /// Checks for overlay color space support.
    /// </summary>
    /// <param name="format">A <see cref="Format"/> value for the color format.</param>
    /// <param name="colorSpace">A <see cref="ColorSpaceType"/> value that specifies color space type to check overlay support for.</param>
    /// <param name="concernedDevice">Instance of Direct3D device interface.</param>
    /// <returns>Overlay color space support flags.</returns>
    public OverlayColorSpaceSupportFlags CheckOverlayColorSpaceSupport(Format format, ColorSpaceType colorSpace, IUnknown concernedDevice)
    {
        return CheckOverlayColorSpaceSupport_(format, colorSpace, concernedDevice);
    }
}
