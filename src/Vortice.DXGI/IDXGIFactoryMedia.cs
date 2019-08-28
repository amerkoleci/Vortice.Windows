// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactoryMedia
    {
        public IDXGIDecodeSwapChain CreateDecodeSwapChainForCompositionSurfaceHandle(
            IUnknown device,
            IntPtr surface,
            IDXGIResource yuvDecodeBuffers)
        {
            // Reserved for future use (https://docs.microsoft.com/it-it/windows/desktop/api/dxgi1_3/ns-dxgi1_3-dxgi_decode_swap_chain_desc)
            var description = new DecodeSwapChainDescription
            {
                Flags = 0
            };

            return CreateDecodeSwapChainForCompositionSurfaceHandle(device, surface, description, yuvDecodeBuffers, null);
        }

        public IDXGIDecodeSwapChain CreateDecodeSwapChainForCompositionSurfaceHandle(
            IUnknown device,
            IntPtr surface,
            IDXGIResource yuvDecodeBuffers,
            IDXGIOutput restrictToOutput)
        {
            // Reserved for future use (https://docs.microsoft.com/it-it/windows/desktop/api/dxgi1_3/ns-dxgi1_3-dxgi_decode_swap_chain_desc)
            var description = new DecodeSwapChainDescription
            {
                Flags = 0
            };

            return CreateDecodeSwapChainForCompositionSurfaceHandle(device, surface, description, yuvDecodeBuffers, restrictToOutput);
        }

        public IDXGISwapChain1 CreateSwapChainForCompositionSurfaceHandle(IUnknown device, IntPtr surface, SwapChainDescription1 description)
        {
            return CreateSwapChainForCompositionSurfaceHandle(device, surface, ref description, null);
        }

        public IDXGISwapChain1 CreateSwapChainForCompositionSurfaceHandle(IUnknown device, IntPtr surface, SwapChainDescription1 description, IDXGIOutput restrictToOutput)
        {
            return CreateSwapChainForCompositionSurfaceHandle(device, surface, ref description, restrictToOutput);
        }
    }
}
