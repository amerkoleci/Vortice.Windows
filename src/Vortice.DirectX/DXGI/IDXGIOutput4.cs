// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.DirectX.DXGI
{
    public partial class IDXGIOutput4
    {
#if !WINDOWS_UWP
        /// <summary>
        /// Checks for overlay color space support.
        /// </summary>
        /// <param name="format">A <see cref="Format"/> value for the color format.</param>
        /// <param name="colorSpace">A <see cref="ColorSpaceType"/> value that specifies color space type to check overlay support for.</param>
        /// <param name="concernedDevice">Instance of Direct3D device interface.</param>
        /// <returns>Overlay color space support flags.</returns>
        public OverlayColorSpaceSupportFlags CheckOverlayColorSpaceSupport(Format format, ColorSpaceType colorSpace, IUnknown concernedDevice)
        {
            return CheckOverlayColorSpaceSupportPrivate(format, colorSpace, concernedDevice);
        }
#endif
    }
}
