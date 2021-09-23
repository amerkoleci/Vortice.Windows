// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.Mathematics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12.Video
{
    /// <summary>
    /// Encapsulates a list of graphics commands for video decoding.
    /// </summary>
    public partial class ID3D12VideoDecodeCommandList
    {
        public unsafe void ResourceBarrierTransition(
            ID3D12Resource resource,
            ResourceStates stateBefore,
            ResourceStates stateAfter,
            int subresource = D3D12.ResourceBarrierAllSubResources,
            ResourceBarrierFlags flags = ResourceBarrierFlags.None)
        {
            var barrier = new ResourceBarrier(
                new ResourceTransitionBarrier(resource, stateBefore, stateAfter, subresource),
                flags);
            ResourceBarrier(1, new IntPtr(&barrier));
        }

        public unsafe void ResourceBarrierAliasing(ID3D12Resource resourceBefore, ID3D12Resource resourceAfter)
        {
            var barrier = new ResourceBarrier(new ResourceAliasingBarrier(resourceBefore, resourceAfter));
            ResourceBarrier(1, new IntPtr(&barrier));
        }

        public unsafe void ResourceBarrierUnorderedAccessView(ID3D12Resource resource)
        {
            var barrier = new ResourceBarrier(new ResourceUnorderedAccessViewBarrier(resource));
            ResourceBarrier(1, new IntPtr(&barrier));
        }

        public unsafe void ResourceBarrier(ResourceBarrier barrier)
        {
            ResourceBarrier(1, new IntPtr(&barrier));
        }

        public unsafe void ResourceBarrier(ResourceBarrier[] barriers)
        {
            fixed (void* pBarriers = barriers)
            {
                ResourceBarrier(barriers.Length, new IntPtr(pBarriers));
            }
        }

        public unsafe void ResourceBarrier(int barriersCount, ResourceBarrier[] barriers)
        {
            fixed (void* pBarriers = barriers)
            {
                ResourceBarrier(barriersCount, new IntPtr(pBarriers));
            }
        }

        public unsafe void ResourceBarrier(Span<ResourceBarrier> barriers)
        {
            fixed (ResourceBarrier* barriersPtr = barriers)
            {
                ResourceBarrier(barriers.Length, (IntPtr)barriersPtr);
            }
        }

        public unsafe void BeginEvent(string name)
        {
            int bufferSize = PixHelpers.CalculateNoArgsEventSize(name);
            void* buffer = stackalloc byte[bufferSize];
            PixHelpers.FormatNoArgsEventToBuffer(buffer, PixHelpers.PixEventType.PIXEvent_BeginEvent_NoArgs, 0, name);
            BeginEvent(PixHelpers.WinPIXEventPIX3BlobVersion, new IntPtr(buffer), bufferSize);
        }

        public unsafe void SetMarker(string name)
        {
            int bufferSize = PixHelpers.CalculateNoArgsEventSize(name);
            void* buffer = stackalloc byte[bufferSize];
            PixHelpers.FormatNoArgsEventToBuffer(buffer, PixHelpers.PixEventType.PIXEvent_SetMarker_NoArgs, 0, name);
            SetMarker(PixHelpers.WinPIXEventPIX3BlobVersion, new IntPtr(buffer), bufferSize);
        }


        /// <summary>
        /// Discards a resource.
        /// </summary>
        /// <param name="resource">The resource to discard.</param>
        /// <param name="firstSubresource">Index of the first subresource in the resource to discard.</param>
        /// <param name="numSubresources">The number of subresources in the resource to discard.</param>
        public void DiscardResource(ID3D12Resource resource, int firstSubresource, int numSubresources)
        {
            DiscardResource(resource, new DiscardRegion
            {
                NumRects = 0,
                Rects = IntPtr.Zero,
                FirstSubresource = firstSubresource,
                NumSubresources = numSubresources
            });
        }

        /// <summary>
        /// Discards a resource.
        /// </summary>
        /// <param name="resource">The resource to discard.</param>
        /// <param name="rectCount">The number of rects to discard in rects.</param>
        /// <param name="rects">An array of  rectangles in the resource to discard. If null, DiscardResource discards the entire resource.</param>
        /// <param name="firstSubresource">Index of the first subresource in the resource to discard.</param>
        /// <param name="numSubresources">The number of subresources in the resource to discard.</param>
        public unsafe void DiscardResource(ID3D12Resource resource, int rectCount, RawRect[] rects, int firstSubresource, int numSubresources)
        {
            fixed (void* rectsPtr = &rects[0])
            {
                DiscardResource(resource, new DiscardRegion
                {
                    NumRects = rectCount,
                    Rects = (IntPtr)rectsPtr,
                    FirstSubresource = firstSubresource,
                    NumSubresources = numSubresources
                });
            }
        }
    }
}
