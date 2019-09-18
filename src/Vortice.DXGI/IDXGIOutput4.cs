// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
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
            if (PlatformDetection.IsUAP)
            {
                throw new NotSupportedException("IDXGIOutput4.CheckOverlayColorSpaceSupport is not supported on UAP platform");
            }

            return CheckOverlayColorSpaceSupport_(format, colorSpace, concernedDevice);
        }
    }
}
