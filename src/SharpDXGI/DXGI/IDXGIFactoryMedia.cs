// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace SharpDXGI
{
    public partial class IDXGIFactoryMedia
    {
        public IDXGIDecodeSwapChain CreateDecodeSwapChainForCompositionSurfaceHandle(
            IUnknown device,
            IntPtr surface,
            IDXGIResource yuvDecodeBuffers)
        {
            Guard.NotNull(device, nameof(device));
            Guard.IsTrue(surface != IntPtr.Zero, nameof(surface), "Invalid surface handle");
            Guard.NotNull(yuvDecodeBuffers, nameof(yuvDecodeBuffers));

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
            Guard.NotNull(device, nameof(device));
            Guard.IsTrue(surface != IntPtr.Zero, nameof(surface), "Invalid surface handle");
            Guard.NotNull(device, nameof(device));
            Guard.NotNull(yuvDecodeBuffers, nameof(yuvDecodeBuffers));
            Guard.NotNull(restrictToOutput, nameof(restrictToOutput));

            // Reserved for future use (https://docs.microsoft.com/it-it/windows/desktop/api/dxgi1_3/ns-dxgi1_3-dxgi_decode_swap_chain_desc)
            var description = new DecodeSwapChainDescription
            {
                Flags = 0
            };

            return CreateDecodeSwapChainForCompositionSurfaceHandle(device, surface, description, yuvDecodeBuffers, restrictToOutput);
        }

        public IDXGISwapChain1 CreateSwapChainForCompositionSurfaceHandle(IUnknown device, IntPtr surface, SwapChainDescription1 description)
        {
            Guard.NotNull(device, nameof(device));
            Guard.IsTrue(surface != IntPtr.Zero, nameof(surface), "Invalid surface handle");

            return CreateSwapChainForCompositionSurfaceHandle(device, surface, ref description, null);
        }

        public IDXGISwapChain1 CreateSwapChainForCompositionSurfaceHandle(IUnknown device, IntPtr surface, SwapChainDescription1 description, IDXGIOutput restrictToOutput)
        {
            Guard.NotNull(device, nameof(device));
            Guard.IsTrue(surface != IntPtr.Zero, nameof(surface), "Invalid surface handle");
            Guard.NotNull(restrictToOutput, nameof(restrictToOutput));

            return CreateSwapChainForCompositionSurfaceHandle(device, surface, ref description, restrictToOutput);
        }
    }
}
