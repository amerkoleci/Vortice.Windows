// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory2
    {
        public IDXGISwapChain1 CreateSwapChainForComposition(IUnknown deviceOrCommandQueue, SwapChainDescription1 description, IDXGIOutput? restrictToOutput = default)
        {
            if (deviceOrCommandQueue == null)
                throw new ArgumentNullException(nameof(deviceOrCommandQueue), $"Null not allowed for {nameof(deviceOrCommandQueue)}");

            return CreateSwapChainForComposition(deviceOrCommandQueue, ref description, restrictToOutput);
        }

        public IDXGISwapChain1 CreateSwapChainForCoreWindow(
            IUnknown deviceOrCommandQueue, 
            IUnknown window, 
            SwapChainDescription1 description, 
            IDXGIOutput? restrictToOutput = default)
        {
            if (deviceOrCommandQueue == null)
                throw new ArgumentNullException(nameof(deviceOrCommandQueue), $"Null not allowed for {nameof(deviceOrCommandQueue)}");

            if (window == null)
                throw new ArgumentNullException(nameof(window), $"Null not allowed for {nameof(window)}");

            return CreateSwapChainForCoreWindow(deviceOrCommandQueue, window, ref description, restrictToOutput);
        }

        public IDXGISwapChain1 CreateSwapChainForHwnd(
            IUnknown deviceOrCommandQueue,
            IntPtr hwnd,
            SwapChainDescription1 description,
            SwapChainFullscreenDescription? fullscreenDescription = null,
            IDXGIOutput? restrictToOutput = default)
        {
            if (deviceOrCommandQueue == null)
                throw new ArgumentNullException(nameof(deviceOrCommandQueue), $"Null not allowed for {nameof(deviceOrCommandQueue)}");

            if (hwnd == IntPtr.Zero)
                throw new ArgumentNullException(nameof(hwnd), "Invalid window handle");

            return CreateSwapChainForHwnd(deviceOrCommandQueue, hwnd, ref description, fullscreenDescription, restrictToOutput);
        }

        public int RegisterOcclusionStatusEvent(WaitHandle waitHandle)
        {
            return RegisterOcclusionStatusEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
        }

        public int RegisterStereoStatusEvent(WaitHandle waitHandle)
        {
            return RegisterStereoStatusEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
        }
    }
}
