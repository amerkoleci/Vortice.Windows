// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

using Vortice.Mathematics;

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12GraphicsCommandList
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
        fixed (ResourceBarrier* pBarriers = barriers)
        {
            ResourceBarrier(barriers.Length, pBarriers);
        }
    }

    public void ClearRenderTargetView(CpuDescriptorHandle renderTargetView, in Color color)
    {
        ClearRenderTargetView(renderTargetView, new Color4(color), 0, (void*)null);
    }

    public void ClearRenderTargetView(CpuDescriptorHandle renderTargetView, in Color color, RawRect[] rects)
    {
        fixed (RawRect* pRects = rects)
        {
            ClearRenderTargetView(renderTargetView, new Color4(color), rects.Length, pRects);
        }
    }

    public void ClearRenderTargetView(CpuDescriptorHandle renderTargetView, in Color color, int rectsCount, RawRect[] rects)
    {
        fixed (RawRect* pRects = rects)
        {
            ClearRenderTargetView(renderTargetView, new Color4(color), rectsCount, pRects);
        }
    }

    public void ClearRenderTargetView(CpuDescriptorHandle renderTargetView, Color4 color)
    {
        ClearRenderTargetView(renderTargetView, color, 0, (void*)null);
    }

    public void ClearRenderTargetView(CpuDescriptorHandle renderTargetView, Color4 color, RawRect[] rects)
    {
        fixed (RawRect* pRects = rects)
        {
            ClearRenderTargetView(renderTargetView, color, rects.Length, pRects);
        }
    }

    public void ClearRenderTargetView(CpuDescriptorHandle renderTargetView, Color4 color, int rectsCount, RawRect[] rects)
    {
        fixed (RawRect* pRects = rects)
        {
            ClearRenderTargetView(renderTargetView, color, rectsCount, pRects);
        }
    }

    public void ClearDepthStencilView(CpuDescriptorHandle depthStencilView, ClearFlags clearFlags, float depth, byte stencil)
    {
        ClearDepthStencilView(depthStencilView, clearFlags, depth, stencil, 0, (void*)null);
    }

    public void ClearDepthStencilView(CpuDescriptorHandle depthStencilView, ClearFlags clearFlags, float depth, byte stencil, RawRect[] rects)
    {
        fixed (RawRect* pRects = rects)
        {
            ClearDepthStencilView(depthStencilView, clearFlags, depth, stencil, rects.Length, pRects);
        }
    }

    public void ClearDepthStencilView(CpuDescriptorHandle depthStencilView, ClearFlags clearFlags, float depth, byte stencil, int rectsCount, RawRect[] rects)
    {
        fixed (RawRect* pRects = rects)
        {
            ClearDepthStencilView(depthStencilView, clearFlags, depth, stencil, rectsCount, pRects);
        }
    }

    public void ClearUnorderedAccessViewFloat(GpuDescriptorHandle viewGpuHandleInCurrentHeap, CpuDescriptorHandle viewCpuHandle, ID3D12Resource resource, Color4 clearValue)
    {
        ClearUnorderedAccessViewFloat(viewGpuHandleInCurrentHeap, viewCpuHandle, resource, &clearValue, 0, null);
    }

    public void ClearUnorderedAccessViewFloat(
        GpuDescriptorHandle viewGpuHandleInCurrentHeap,
        CpuDescriptorHandle viewCpuHandle,
        ID3D12Resource resource,
        Color4 clearValue,
        RawRect[] rects)
    {
        ClearUnorderedAccessViewFloat(viewGpuHandleInCurrentHeap, viewCpuHandle, resource, &clearValue, rects.Length, rects);
    }

    public unsafe void ClearUnorderedAccessViewFloat(
        GpuDescriptorHandle viewGpuHandleInCurrentHeap,
        CpuDescriptorHandle viewCpuHandle,
        ID3D12Resource resource,
        Color4 clearValue,
        int rectCount,
        RawRect[] rects)
    {
        ClearUnorderedAccessViewFloat(viewGpuHandleInCurrentHeap, viewCpuHandle, resource, &clearValue, rectCount, rects);
    }

    public unsafe void ClearUnorderedAccessViewUint(
        GpuDescriptorHandle viewGpuHandleInCurrentHeap,
        CpuDescriptorHandle viewCpuHandle,
        ID3D12Resource resource,
        Int4 clearValue)
    {
        ClearUnorderedAccessViewUint(viewGpuHandleInCurrentHeap, viewCpuHandle, resource, &clearValue, 0, null);
    }

    public unsafe void ClearUnorderedAccessViewUint(
        GpuDescriptorHandle viewGpuHandleInCurrentHeap,
        CpuDescriptorHandle viewCpuHandle,
        ID3D12Resource resource,
        Int4 clearValue,
        RawRect[] rectangles)
    {
        ClearUnorderedAccessViewUint(viewGpuHandleInCurrentHeap, viewCpuHandle, resource, &clearValue, rectangles.Length, rectangles);
    }

    public unsafe void SetComputeRoot32BitConstant(int rootParameterIndex, float srcData, int destOffsetIn32BitValues)
    {
        SetComputeRoot32BitConstant(rootParameterIndex, *(int*)&srcData, destOffsetIn32BitValues);
    }

    public unsafe void SetComputeRoot32BitConstant(int rootParameterIndex, uint srcData, int destOffsetIn32BitValues)
    {
        SetComputeRoot32BitConstant(rootParameterIndex, *(int*)&srcData, destOffsetIn32BitValues);
    }

    public unsafe void SetGraphicsRoot32BitConstant(int rootParameterIndex, float srcData, int destOffsetIn32BitValues)
    {
        SetComputeRoot32BitConstant(rootParameterIndex, *(int*)&srcData, destOffsetIn32BitValues);
    }

    public unsafe void SetGraphicsRoot32BitConstant(int rootParameterIndex, uint srcData, int destOffsetIn32BitValues)
    {
        SetComputeRoot32BitConstant(rootParameterIndex, *(int*)&srcData, destOffsetIn32BitValues);
    }

    public unsafe void SetComputeRoot32BitConstants<T>(int rootParameterIndex, T srcData, int destOffsetIn32BitValues)
        where T : unmanaged
    {
        SetComputeRoot32BitConstants(rootParameterIndex, Unsafe.SizeOf<T>() / 4, new IntPtr(&srcData), destOffsetIn32BitValues);
    }

    public unsafe void SetGraphicsRoot32BitConstants<T>(int rootParameterIndex, T srcData, int destOffsetIn32BitValues)
        where T : unmanaged
    {
        SetGraphicsRoot32BitConstants(rootParameterIndex, Unsafe.SizeOf<T>() / 4, new IntPtr(&srcData), destOffsetIn32BitValues);
    }

    public void OMSetBlendFactor(in Color blendFactor)
    {
        OMSetBlendFactor(new Color4(blendFactor));
    }

    public void OMSetBlendFactor(Color4 blendFactor)
    {
        OMSetBlendFactor(&blendFactor);
    }

    public void OMSetBlendFactor(float red, float green, float blue, float alpha)
    {
        float* colorPtr = stackalloc float[4] { red, green, blue, alpha };
        OMSetBlendFactor(colorPtr);
    }

    public void OMSetBlendFactor(float[] color)
    {
        fixed (float* colorPtr = &color[0])
        {
            OMSetBlendFactor(colorPtr);
        }
    }

    #region Viewport
    public void RSSetViewport(float x, float y, float width, float height, float minDepth = 0.0f, float maxDepth = 1.0f)
    {
        var viewport = new Viewport(x, y, width, height, minDepth, maxDepth);
        RSSetViewports(1, &viewport);
    }

    public void RSSetViewport(Viewport viewport)
    {
        RSSetViewports(1, &viewport);
    }

    public void RSSetViewports(params Viewport[] viewports)
    {
        fixed (Viewport* pViewports = viewports)
        {
            RSSetViewports(viewports.Length, pViewports);
        }
    }

    public unsafe void RSSetViewports(int count, Viewport[] viewports)
    {
        fixed (Viewport* pViewports = viewports)
        {
            RSSetViewports(count, pViewports);
        }
    }

    public void RSSetViewports(Span<Viewport> viewports)
    {
        fixed (Viewport* pViewports = viewports)
        {
            RSSetViewports(viewports.Length, pViewports);
        }
    }

    public void RSSetViewports(int count, Span<Viewport> viewports)
    {
        fixed (Viewport* pViewports = viewports)
        {
            RSSetViewports(count, pViewports);
        }
    }

    public void RSSetViewport<T>(T viewport) where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != sizeof(Viewport))
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(Viewport)}", nameof(viewport));
        }
#endif

        RSSetViewports(1, &viewport);
    }

    public void RSSetViewports<T>(T[] viewports) where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != sizeof(Viewport))
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(Viewport)}", nameof(viewports));
        }
#endif

        unsafe
        {
            fixed (void* viewportsPtr = &viewports[0])
            {
                RSSetViewports(viewports.Length, viewportsPtr);
            }
        }
    }

    public void RSSetViewports<T>(int count, T[] viewports) where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != sizeof(Viewport))
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(Viewport)}", nameof(viewports));
        }
#endif

        unsafe
        {
            fixed (void* viewportsPtr = &viewports[0])
            {
                RSSetViewports(count, viewportsPtr);
            }
        }
    }

    public void RSSetViewports<T>(int count, Span<T> viewports) where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != sizeof(Viewport))
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(Viewport)}", nameof(viewports));
        }
#endif
        unsafe
        {
            fixed (void* pViewports = viewports)
            {
                RSSetViewports(count, pViewports);
            }
        }
    }
    #endregion

    #region ScissorRect
    public void RSSetScissorRect(int width, int height)
    {
        RawRect rect = new(0, 0, width, height);
        RSSetScissorRects(1, &rect);
    }

    public void RSSetScissorRect(RectI rectangle)
    {
        RawRect rect = rectangle;
        RSSetScissorRects(1, &rect);
    }

    public void RSSetScissorRect(RawRect rectangle)
    {
        RSSetScissorRects(1, &rectangle);
    }

    public void RSSetScissorRects(params RawRect[] rects)
    {
        fixed (void* pRects = rects)
        {
            RSSetScissorRects(rects.Length, pRects);
        }
    }

    public void RSSetScissorRects(int count, RawRect[] rects)
    {
        fixed (void* pRects = rects)
        {
            RSSetScissorRects(count, pRects);
        }
    }

    public void RSSetScissorRects(Span<RawRect> rects)
    {
        fixed (RawRect* pRects = rects)
        {
            RSSetScissorRects(rects.Length, pRects);
        }
    }

    public void RSSetScissorRects(int count, Span<RawRect> rects)
    {
        fixed (RawRect* pRects = rects)
        {
            RSSetScissorRects(count, pRects);
        }
    }
    #endregion

    public void OMSetRenderTargets(CpuDescriptorHandle renderTargetDescriptor, CpuDescriptorHandle? depthStencilDescriptor = null)
    {
        OMSetRenderTargets(1, &renderTargetDescriptor, false, depthStencilDescriptor);
    }

    public void OMSetRenderTargets(CpuDescriptorHandle renderTargetDescriptor, bool RTsSingleHandleToDescriptorRange, CpuDescriptorHandle? depthStencilDescriptor = null)
    {
        OMSetRenderTargets(1, &renderTargetDescriptor, RTsSingleHandleToDescriptorRange, depthStencilDescriptor);
    }

    public void OMSetRenderTargets(CpuDescriptorHandle[] renderTargetDescriptors, CpuDescriptorHandle? depthStencilDescriptor = null)
    {
        fixed (CpuDescriptorHandle* renderTargetDescriptorsPtr = renderTargetDescriptors)
        {
            OMSetRenderTargets(renderTargetDescriptors.Length, renderTargetDescriptorsPtr, false, depthStencilDescriptor);
        }
    }

    public void OMSetRenderTargets(CpuDescriptorHandle[] renderTargetDescriptors, bool RTsSingleHandleToDescriptorRange, CpuDescriptorHandle? depthStencilDescriptor = null)
    {
        fixed (CpuDescriptorHandle* renderTargetDescriptorsPtr = renderTargetDescriptors)
        {
            OMSetRenderTargets(renderTargetDescriptors.Length, renderTargetDescriptorsPtr, RTsSingleHandleToDescriptorRange, depthStencilDescriptor);
        }
    }

    public void OMSetRenderTargets(int renderTargetDescriptorsCount, CpuDescriptorHandle[] renderTargetDescriptors, CpuDescriptorHandle? depthStencilDescriptor = null)
    {
        fixed (CpuDescriptorHandle* renderTargetDescriptorsPtr = renderTargetDescriptors)
        {
            OMSetRenderTargets(renderTargetDescriptorsCount, renderTargetDescriptorsPtr, false, depthStencilDescriptor);
        }
    }

    public void OMSetRenderTargets(int renderTargetDescriptorsCount, CpuDescriptorHandle[] renderTargetDescriptors, bool RTsSingleHandleToDescriptorRange, CpuDescriptorHandle? depthStencilDescriptor = null)
    {
        fixed (CpuDescriptorHandle* renderTargetDescriptorsPtr = renderTargetDescriptors)
        {
            OMSetRenderTargets(renderTargetDescriptorsCount, renderTargetDescriptorsPtr, RTsSingleHandleToDescriptorRange, depthStencilDescriptor);
        }
    }

    #region IASetVertexBuffers
    public void IASetVertexBuffers(int slot, VertexBufferView vertexBufferView)
    {
        IASetVertexBuffers(slot, 1, &vertexBufferView);
    }

    public void IASetVertexBuffers(int startSlot, Span<VertexBufferView> vertexBufferViews)
    {
        fixed (VertexBufferView* vertexBufferViewsPtr = vertexBufferViews)
        {
            IASetVertexBuffers(startSlot, vertexBufferViews.Length, vertexBufferViewsPtr);
        }
    }

    public unsafe void IASetVertexBuffers(int startSlot, params VertexBufferView[] vertexBufferViews)
    {
        fixed (VertexBufferView* vertexBufferViewsPtr = vertexBufferViews)
        {
            IASetVertexBuffers(startSlot, vertexBufferViews.Length, vertexBufferViewsPtr);
        }
    }

    public void IASetVertexBuffers(int startSlot, int viewsCount, VertexBufferView[] vertexBufferViews)
    {
        fixed (VertexBufferView* vertexBufferViewsPtr = vertexBufferViews)
        {
            IASetVertexBuffers(startSlot, viewsCount, vertexBufferViewsPtr);
        }
    }
    #endregion

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
    /// This method uses the GPU to copy texture data between two locations.
    /// Both the source and the destination may reference texture data located within either a buffer resource or a texture resource.
    /// </summary>
    /// <param name="destination">Specifies the destination <see cref="TextureCopyLocation"/>. The subresource referred to must be in the <see cref="ResourceStates.CopyDest"/> state.</param>
    /// <param name="destinationX">The x-coordinate of the upper left corner of the destination region.</param>
    /// <param name="destinationY">The y-coordinate of the upper left corner of the destination region. For a 1D subresource, this must be zero.</param>
    /// <param name="destinationZ">The z-coordinate of the upper left corner of the destination region. For a 1D or 2D subresource, this must be zero.</param>
    /// <param name="source">Specifies the source D3D12_TEXTURE_COPY_LOCATION. The subresource referred to must be in the D3D12_RESOURCE_STATE_COPY_SOURCE state.</param>
    /// <param name="sourceBox">Specifies an optional <see cref="Box"/> that sets the size of the source texture to copy.</param>
    public void CopyTextureRegion(
        TextureCopyLocation destination,
        int destinationX, int destinationY, int destinationZ,
        TextureCopyLocation source, Box? sourceBox = null)
    {
        CopyTextureRegion_(destination, destinationX, destinationY, destinationZ, source, sourceBox);
    }

    /// <summary>
    /// Discards an entire resource.
    /// </summary>
    /// <param name="resource">The resource to discard.</param>
    public void DiscardResource(ID3D12Resource resource)
    {
        DiscardResource(resource, null);
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
    public void DiscardResource(ID3D12Resource resource, int rectCount, RawRect[] rects, int firstSubresource, int numSubresources)
    {
        fixed (RawRect* rectsPtr = &rects[0])
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

    /// <summary>
    /// Discards a resource.
    /// </summary>
    /// <param name="resource">The resource to discard.</param>
    /// <param name="rects">An array of  rectangles in the resource to discard. If null, DiscardResource discards the entire resource.</param>
    /// <param name="firstSubresource">Index of the first subresource in the resource to discard.</param>
    /// <param name="numSubresources">The number of subresources in the resource to discard.</param>
    public void DiscardResource(ID3D12Resource resource, RawRect[] rects, int firstSubresource, int numSubresources)
    {
        fixed (RawRect* rectsPtr = &rects[0])
        {
            DiscardResource(resource, new DiscardRegion
            {
                NumRects = rects.Length,
                Rects = (IntPtr)rectsPtr,
                FirstSubresource = firstSubresource,
                NumSubresources = numSubresources
            });
        }
    }

    public void Reset(ID3D12CommandAllocator commandAllocator)
    {
        Reset(commandAllocator, null);
    }

    internal static unsafe void MemcpySubresource(MemCpyDest* pDest,
        void* pResourceData, SubresourceInfo* pSrc,
        nuint RowSizeInBytes, uint NumRows, uint NumSlices)
    {
        for (uint z = 0u; z < NumSlices; ++z)
        {
            byte* pDestSlice = (byte*)pDest->pData + pDest->SlicePitch * z;
            byte* pSrcSlice = ((byte*)pResourceData + pSrc->Offset) + pSrc->DepthPitch * (nint)z;

            for (uint y = 0u; y < NumRows; ++y)
            {
                Buffer.MemoryCopy(
                    pSrcSlice + pSrc->RowPitch * (nint)y,
                    pDestSlice + pDest->RowPitch * y,
                    (ulong)RowSizeInBytes,
                    (ulong)RowSizeInBytes
                );
            }
        }
    }

    public ulong UpdateSubresources(ID3D12Resource destinationResource, ID3D12Resource intermediate,
        int firstSubresource, int numSubresources,
        ulong RequiredSize,
        PlacedSubresourceFootPrint[] pLayouts,
        uint[] numRows,
        ulong[] rowSizesInBytes,
        void* resourceData,
        SubresourceInfo* pSrcData)
    {
        ResourceDescription IntermediateDesc = intermediate.Description;
        ResourceDescription DestinationDesc = destinationResource.Description;

        if (IntermediateDesc.Dimension != ResourceDimension.Buffer ||
            IntermediateDesc.Width < RequiredSize + pLayouts[0].Offset ||
            RequiredSize > unchecked((nuint)(-1)) ||
            (DestinationDesc.Dimension == ResourceDimension.Buffer && (firstSubresource != 0 || numSubresources != 1)))
        {
            return 0;
        }

        byte* pData;
        Result hr = intermediate.Map(0, (void**)&pData);

        if (hr.Failure)
        {
            return 0;
        }

        for (int i = 0; i < numSubresources; ++i)
        {
            if (rowSizesInBytes[i] > unchecked((nuint)(-1)))
            {
                return 0;
            }

            MemCpyDest DestData = new()
            {
                pData = pData + pLayouts[i].Offset,
                RowPitch = (nuint)pLayouts[i].Footprint.RowPitch,
                SlicePitch = (nuint)(pLayouts[i].Footprint.RowPitch * numRows[i])
            };

            MemcpySubresource(&DestData, resourceData, &pSrcData[i], (nuint)rowSizesInBytes[i], numRows[i], (uint)pLayouts[i].Footprint.Depth);
        }
        intermediate.Unmap(0, null);

        if (DestinationDesc.Dimension == ResourceDimension.Buffer)
        {
            CopyBufferRegion(destinationResource, 0, intermediate, pLayouts[0].Offset, (ulong)pLayouts[0].Footprint.Width);
        }
        else
        {
            for (int i = 0; i < numSubresources; ++i)
            {
                TextureCopyLocation dst = new(destinationResource, i + firstSubresource);
                TextureCopyLocation src = new(intermediate, pLayouts[i]);
                CopyTextureRegion(dst, 0, 0, 0, src, (Box?)null);
            }
        }
        return RequiredSize;
    }
}

internal unsafe struct MemCpyDest
{
    public void* pData;

    public nuint RowPitch;
    public nuint SlicePitch;
}
