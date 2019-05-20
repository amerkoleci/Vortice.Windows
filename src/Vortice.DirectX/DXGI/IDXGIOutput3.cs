// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.DirectX.DXGI
{
    public partial class IDXGIOutput3
    {
#if !WINDOWS_UWP
        /// <summary>
        /// Checks for overlay support.
        /// </summary>
        /// <param name="format">A <see cref="Format"/> value for the color format.</param>
        /// <param name="concernedDevice">Instance of Direct3D device interface.</param>
        /// <returns>Overlay support flags.</returns>
        public OverlaySupportFlags CheckOverlaySupport(Format format, IUnknown concernedDevice)
        {
            return CheckOverlaySupportPrivate(format, concernedDevice);
        }
#endif
    }
}
