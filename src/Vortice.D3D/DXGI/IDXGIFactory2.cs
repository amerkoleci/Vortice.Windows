// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory2
    {
        /// <summary>
        /// Try to create new instance of <see cref="IDXGIFactory2"/>.
        /// </summary>
        /// <param name="debug">Whether to enable debug callback.</param>
        /// <param name="factory">The <see cref="IDXGIFactory2"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result TryCreate(bool debug, out IDXGIFactory2 factory)
        {
            int flags = debug ? DXGIInternal.CreateFactoryDebug : 0x00;
            var result = DXGIInternal.CreateDXGIFactory2(flags, typeof(IDXGIFactory2).GUID, out var nativePtr);
            if (result.Success)
            {
                factory = new IDXGIFactory2(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }

        public IDXGISwapChain1 CreateSwapChainForComposition(IUnknown device, SwapChainDescription1 description)
        {
            Guard.NotNull(device, nameof(device));
            return CreateSwapChainForComposition(device, ref description, null);
        }

        public IDXGISwapChain1 CreateSwapChainForComposition(IUnknown device, SwapChainDescription1 description, IDXGIOutput restrictToOutput)
        {
            Guard.NotNull(device, nameof(device));
            Guard.NotNull(restrictToOutput, nameof(restrictToOutput));
            return CreateSwapChainForComposition(device, ref description, restrictToOutput);
        }

        public IDXGISwapChain1 CreateSwapChainForCoreWindow(IUnknown device, IUnknown window, SwapChainDescription1 description)
        {
            Guard.NotNull(device, nameof(device));
            Guard.NotNull(window, nameof(window));

            return CreateSwapChainForCoreWindow(device, window, ref description, null);
        }

        public IDXGISwapChain1 CreateSwapChainForCoreWindow(IUnknown device, IUnknown window, SwapChainDescription1 description, IDXGIOutput restrictToOutput)
        {
            Guard.NotNull(device, nameof(device));
            Guard.NotNull(window, nameof(window));
            Guard.NotNull(restrictToOutput, nameof(restrictToOutput));

            return CreateSwapChainForCoreWindow(device, window, ref description, restrictToOutput);
        }

        public IDXGISwapChain1 CreateSwapChainForHwnd(IUnknown device, IntPtr hwnd, SwapChainDescription1 description)
        {
            Guard.NotNull(device, nameof(device));
            Guard.IsTrue(hwnd != IntPtr.Zero, nameof(hwnd), "Invalid hwnd handle");
            return CreateSwapChainForHwnd(device, hwnd, ref description, null, null);
        }

        public IDXGISwapChain1 CreateSwapChainForHwnd(
            IUnknown device,
            IntPtr hwnd,
            SwapChainDescription1 description,
            SwapChainFullscreenDescription fullscreenDescription)
        {
            Guard.NotNull(device, nameof(device));
            Guard.IsTrue(hwnd != IntPtr.Zero, nameof(hwnd), "Invalid hwnd handle");
            return CreateSwapChainForHwnd(device, hwnd, ref description, fullscreenDescription, null);
        }

        public IDXGISwapChain1 CreateSwapChainForHwnd(
            IUnknown device,
            IntPtr hwnd,
            SwapChainDescription1 description,
            SwapChainFullscreenDescription fullscreenDescription,
            IDXGIOutput restrictToOutput)
        {
            Guard.NotNull(device, nameof(device));
            Guard.IsTrue(hwnd != IntPtr.Zero, nameof(hwnd), "Invalid hwnd handle");
            Guard.NotNull(restrictToOutput, nameof(restrictToOutput));

            return CreateSwapChainForHwnd(device, hwnd, ref description, fullscreenDescription, restrictToOutput);
        }
    }
}
