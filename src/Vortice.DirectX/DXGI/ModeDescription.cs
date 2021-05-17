// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DXGI
{
    /// <summary>
    /// Describes a display mode.
    /// </summary>
    public partial struct ModeDescription
    {
        /// <summary>
        /// Initialize instance of <see cref="ModeDescription"/> struct.
        /// </summary>
        /// <param name="width">
        /// A value that describes the resolution width. If you specify the width as zero when you call the IDXGIFactory.CreateSwapChain method to create a swap chain, the runtime obtains the width from the output window and assigns this width value to the swap-chain description. 
        /// </param>
        /// <param name="height">
        /// A value that describes the resolution height. If you specify the width as zero when you call the IDXGIFactory.CreateSwapChain method to create a swap chain, the runtime obtains the height from the output window and assigns this height value to the swap-chain description. 
        /// </param>
        /// <param name="format">A <see cref="Vortice.DXGI.Format"/> describing the display format.</param>
        public ModeDescription(
            int width,
            int height,
            Format format = Format.B8G8R8A8_UNorm)
        {
            Width = width;
            Height = height;
            Format = format;
            RefreshRate = new Rational(60, 1);
            ScanlineOrdering = ModeScanlineOrder.Unspecified;
            Scaling = ModeScaling.Unspecified;
        }

        /// <summary>
        /// Initialize instance of <see cref="ModeDescription"/> struct.
        /// </summary>
        /// <param name="width">
        /// A value that describes the resolution width. If you specify the width as zero when you call the IDXGIFactory.CreateSwapChain method to create a swap chain, the runtime obtains the width from the output window and assigns this width value to the swap-chain description. 
        /// </param>
        /// <param name="height">
        /// A value that describes the resolution height. If you specify the width as zero when you call the IDXGIFactory.CreateSwapChain method to create a swap chain, the runtime obtains the height from the output window and assigns this height value to the swap-chain description. 
        /// </param>
        /// <param name="refreshRate">A <see cref="Rational"/> describing the refresh rate in hertz</param>
        /// <param name="format">A <see cref="Vortice.DXGI.Format"/> describing the display format.</param>
        public ModeDescription(
            int width,
            int height,
            Rational refreshRate,
            Format format)
        {
            Width = width;
            Height = height;
            Format = format;
            RefreshRate = refreshRate;
            ScanlineOrdering = ModeScanlineOrder.Unspecified;
            Scaling = ModeScaling.Unspecified;
        }
    }
}
