// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory2
    {
        public IDXGISwapChain1 CreateSwapChainForComposition(IUnknown device, SwapChainDescription1 description)
        {
            return CreateSwapChainForComposition(device, ref description, null);
        }

        public IDXGISwapChain1 CreateSwapChainForComposition(IUnknown device, SwapChainDescription1 description, IDXGIOutput restrictToOutput)
        {
            return CreateSwapChainForComposition(device, ref description, restrictToOutput);
        }

        public IDXGISwapChain1 CreateSwapChainForCoreWindow(IUnknown device, IUnknown window, SwapChainDescription1 description)
        {
            return CreateSwapChainForCoreWindow(device, window, ref description, null);
        }

        public IDXGISwapChain1 CreateSwapChainForCoreWindow(IUnknown device, IUnknown window, SwapChainDescription1 description, IDXGIOutput restrictToOutput)
        {
            return CreateSwapChainForCoreWindow(device, window, ref description, restrictToOutput);
        }

        public IDXGISwapChain1 CreateSwapChainForHwnd(IUnknown device, IntPtr hwnd, SwapChainDescription1 description)
        {
            return CreateSwapChainForHwnd(device, hwnd, ref description, null, null);
        }

        public IDXGISwapChain1 CreateSwapChainForHwnd(
            IUnknown device,
            IntPtr hwnd,
            SwapChainDescription1 description,
            SwapChainFullscreenDescription fullscreenDescription)
        {
            return CreateSwapChainForHwnd(device, hwnd, ref description, fullscreenDescription, null);
        }

        public IDXGISwapChain1 CreateSwapChainForHwnd(
            IUnknown device,
            IntPtr hwnd,
            SwapChainDescription1 description,
            SwapChainFullscreenDescription fullscreenDescription,
            IDXGIOutput restrictToOutput)
        {
            return CreateSwapChainForHwnd(device, hwnd, ref description, fullscreenDescription, restrictToOutput);
        }

        public int RegisterOcclusionStatusEvent(EventWaitHandle waitHandle)
        {
            return RegisterOcclusionStatusEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
        }

        public int RegisterStereoStatusEvent(EventWaitHandle waitHandle)
        {
            return RegisterStereoStatusEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
        }
    }
}
