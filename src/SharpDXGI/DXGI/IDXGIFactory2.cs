// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using SharpGen.Runtime;

namespace SharpDXGI
{
    public partial class IDXGIFactory2
    {
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

        public int RegisterOcclusionStatusEvent(EventWaitHandle waitHandle)
        {
            Guard.NotNull(waitHandle, nameof(waitHandle));
            return RegisterOcclusionStatusEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
        }

        public int RegisterStereoStatusEvent(EventWaitHandle waitHandle)
        {
            Guard.NotNull(waitHandle, nameof(waitHandle));

            return RegisterStereoStatusEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
        }
    }
}
