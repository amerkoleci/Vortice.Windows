// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Video;

/// <summary>
/// Encapsulates a list of graphics commands for video encoding, including motion estimation.
/// </summary>
public unsafe partial class ID3D12VideoEncodeCommandList
{
    public void ResourceBarrierTransition(
        ID3D12Resource resource,
        ResourceStates stateBefore,
        ResourceStates stateAfter,
        int subresource = D3D12.ResourceBarrierAllSubResources,
        ResourceBarrierFlags flags = ResourceBarrierFlags.None)
    {
        var barrier = new ResourceBarrier(
            new ResourceTransitionBarrier(resource, stateBefore, stateAfter, subresource),
            flags);

        ResourceBarrier(1, &barrier);
    }

    public void ResourceBarrierAliasing(ID3D12Resource resourceBefore, ID3D12Resource resourceAfter)
    {
        var barrier = new ResourceBarrier(new ResourceAliasingBarrier(resourceBefore, resourceAfter));
        ResourceBarrier(1, &barrier);
    }

    public void ResourceBarrierUnorderedAccessView(ID3D12Resource resource)
    {
        var barrier = new ResourceBarrier(new ResourceUnorderedAccessViewBarrier(resource));
        ResourceBarrier(1, &barrier);
    }

    public void ResourceBarrier(ResourceBarrier barrier)
    {
        ResourceBarrier(1, &barrier);
    }

    public void ResourceBarrier(ResourceBarrier[] barriers)
    {
        fixed (ResourceBarrier* pBarriers = barriers)
        {
            ResourceBarrier(barriers.Length, pBarriers);
        }
    }

    public void ResourceBarrier(int barriersCount, ResourceBarrier[] barriers)
    {
        fixed (ResourceBarrier* pBarriers = barriers)
        {
            ResourceBarrier(barriersCount, pBarriers);
        }
    }

    public void ResourceBarrier(Span<ResourceBarrier> barriers)
    {
        fixed (ResourceBarrier* barriersPtr = barriers)
        {
            ResourceBarrier(barriers.Length, barriersPtr);
        }
    }

    public void BeginEvent(string name)
    {
        int bufferSize = PixHelpers.CalculateNoArgsEventSize(name);
        void* buffer = stackalloc byte[bufferSize];
        PixHelpers.FormatNoArgsEventToBuffer(buffer, PixHelpers.PixEventType.PIXEvent_BeginEvent_NoArgs, 0, name);
        BeginEvent(PixHelpers.WinPIXEventPIX3BlobVersion, new IntPtr(buffer), bufferSize);
    }

    public void SetMarker(string name)
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
