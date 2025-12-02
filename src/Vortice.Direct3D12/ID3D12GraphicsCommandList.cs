// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using Vortice.Mathematics;
using Vortice.DXGI;

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12GraphicsCommandList
{
    public void ResourceBarrierTransition(
        ID3D12Resource resource,
        ResourceStates stateBefore,
        ResourceStates stateAfter,
        uint subresource = D3D12.ResourceBarrierAllSubResources,
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
            ResourceBarrier((uint)barriers.Length, pBarriers);
        }
    }

    public void ResourceBarrier(uint barriersCount, ResourceBarrier[] barriers)
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
            ResourceBarrier((uint)barriers.Length, pBarriers);
        }
    }

    public void UnsetDescriptorHeaps()
    {
        SetDescriptorHeaps(0, (void*)null);
    }

    public void SetDescriptorHeaps(ID3D12DescriptorHeap? heap)
    {
        if (heap is not null)
        {
            IntPtr heapPtr = heap.NativePointer;
            SetDescriptorHeaps(1, &heapPtr);
        }
        else
        {
            SetDescriptorHeaps(0, (void*)null);
        }
    }

    public void SetDescriptorHeaps(ID3D12DescriptorHeap[] descriptorHeaps)
    {
        SetDescriptorHeaps(descriptorHeaps.Length, descriptorHeaps);
    }

    public void SetDescriptorHeaps(int numDescriptorHeaps, ID3D12DescriptorHeap[] descriptorHeaps)
    {
        if (numDescriptorHeaps == 0)
        {
            SetDescriptorHeaps(0, (void*)null);
            return;
        }

        IntPtr* descriptorHeapsPtr = stackalloc IntPtr[numDescriptorHeaps];
        for (int i = 0; i < numDescriptorHeaps; i++)
        {
            descriptorHeapsPtr[i] = (descriptorHeaps[i] == null) ? IntPtr.Zero : descriptorHeaps[i].NativePointer;
        }
        SetDescriptorHeaps((uint)numDescriptorHeaps, descriptorHeapsPtr);
    }

    public void SetDescriptorHeaps(ReadOnlySpan<ID3D12DescriptorHeap> descriptorHeaps)
    {
        SetDescriptorHeaps(descriptorHeaps.Length, descriptorHeaps);
    }

    public void SetDescriptorHeaps(int numDescriptorHeaps, ReadOnlySpan<ID3D12DescriptorHeap> descriptorHeaps)
    {
        if (numDescriptorHeaps == 0)
        {
            SetDescriptorHeaps(0, (void*)null);
            return;
        }

        IntPtr* descriptorHeapsPtr = stackalloc IntPtr[(int)numDescriptorHeaps];
        for (int i = 0; i < numDescriptorHeaps; i++)
        {
            descriptorHeapsPtr[i] = (descriptorHeaps[i] == null) ? IntPtr.Zero : descriptorHeaps[i].NativePointer;
        }
        SetDescriptorHeaps((uint)numDescriptorHeaps, descriptorHeapsPtr);
    }

    public void ClearRenderTargetView(CpuDescriptorHandle renderTargetView, Color4 color)
    {
        ClearRenderTargetView(renderTargetView, color, 0, (void*)null);
    }

    public void ClearRenderTargetView(CpuDescriptorHandle renderTargetView, Color4 color, RawRect[] rects)
    {
        fixed (RawRect* pRects = rects)
        {
            ClearRenderTargetView(renderTargetView, color, (uint)rects.Length, pRects);
        }
    }

    public void ClearRenderTargetView(CpuDescriptorHandle renderTargetView, Color4 color, uint rectsCount, RawRect[] rects)
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
            ClearDepthStencilView(depthStencilView, clearFlags, depth, stencil, (uint)rects.Length, pRects);
        }
    }

    public void ClearDepthStencilView(CpuDescriptorHandle depthStencilView, ClearFlags clearFlags, float depth, byte stencil, uint rectsCount, RawRect[] rects)
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
        ClearUnorderedAccessViewFloat(viewGpuHandleInCurrentHeap, viewCpuHandle, resource, &clearValue, (uint)rects.Length, rects);
    }

    public void ClearUnorderedAccessViewFloat(
        GpuDescriptorHandle viewGpuHandleInCurrentHeap,
        CpuDescriptorHandle viewCpuHandle,
        ID3D12Resource resource,
        Color4 clearValue,
        uint rectCount,
        RawRect[] rects)
    {
        ClearUnorderedAccessViewFloat(viewGpuHandleInCurrentHeap, viewCpuHandle, resource, &clearValue, rectCount, rects);
    }

    public void ClearUnorderedAccessViewUint(
        GpuDescriptorHandle viewGpuHandleInCurrentHeap,
        CpuDescriptorHandle viewCpuHandle,
        ID3D12Resource resource,
        Int4 clearValue)
    {
        ClearUnorderedAccessViewUint(viewGpuHandleInCurrentHeap, viewCpuHandle, resource, &clearValue, 0, null);
    }

    public void ClearUnorderedAccessViewUint(
        GpuDescriptorHandle viewGpuHandleInCurrentHeap,
        CpuDescriptorHandle viewCpuHandle,
        ID3D12Resource resource,
        Int4 clearValue,
        RawRect[] rectangles)
    {
        ClearUnorderedAccessViewUint(viewGpuHandleInCurrentHeap, viewCpuHandle, resource, &clearValue, (uint)rectangles.Length, rectangles);
    }

    public void SetComputeRoot32BitConstant(uint rootParameterIndex, float srcData, uint destOffsetIn32BitValues)
    {
        SetComputeRoot32BitConstant(rootParameterIndex, *(uint*)&srcData, destOffsetIn32BitValues);
    }

    public void SetComputeRoot32BitConstant(uint rootParameterIndex, int srcData, uint destOffsetIn32BitValues)
    {
        SetComputeRoot32BitConstant(rootParameterIndex, *(uint*)&srcData, destOffsetIn32BitValues);
    }
    public void SetComputeRoot32BitConstants(uint rootParameterIndex, uint num32BitValuesToSet, IntPtr srcData, uint destOffsetIn32BitValues)
    {
        SetComputeRoot32BitConstants(rootParameterIndex, num32BitValuesToSet, srcData.ToPointer(), destOffsetIn32BitValues);
    }
    public void SetComputeRoot32BitConstants<T>(uint rootParameterIndex, T[] srcData, uint destOffset = 0)
        where T : unmanaged
    {
        ReadOnlySpan<T> span = srcData.AsSpan();

        SetComputeRoot32BitConstants(rootParameterIndex, span, destOffset);
    }

    public void SetComputeRoot32BitConstants<T>(uint rootParameterIndex, ReadOnlySpan<T> source, uint destOffset = 0) where T : unmanaged
    {
        fixed (T* pSrcData = source)
        {
            SetComputeRoot32BitConstants(rootParameterIndex, (uint)(source.Length * sizeof(T) / 4), pSrcData, destOffset / 4);
        }
    }

    public void SetComputeRoot32BitConstants<T>(uint rootParameterIndex, ref T srcData, uint destOffset = 0)
        where T : unmanaged
    {
        fixed (void* pSrcData = &srcData)
        {
            SetComputeRoot32BitConstants(rootParameterIndex, (uint)sizeof(T) / 4, pSrcData, destOffset / 4);
        }
    }

    public void SetComputeRoot32BitConstants<T>(uint rootParameterIndex, T srcData, uint destOffsetIn32BitValues)
        where T : unmanaged
    {
        SetComputeRoot32BitConstants(rootParameterIndex, (uint)sizeof(T) / 4, &srcData, destOffsetIn32BitValues);
    }

    public void SetGraphicsRoot32BitConstant(uint rootParameterIndex, float srcData, uint destOffsetIn32BitValues)
    {
        SetGraphicsRoot32BitConstant(rootParameterIndex, *(uint*)&srcData, destOffsetIn32BitValues);
    }

    public void SetGraphicsRoot32BitConstants(uint rootParameterIndex, uint num32BitValuesToSet, IntPtr srcData, uint destOffsetIn32BitValues)
    {
        SetGraphicsRoot32BitConstants(rootParameterIndex, num32BitValuesToSet, srcData.ToPointer(), destOffsetIn32BitValues);
    }

    public void SetGraphicsRoot32BitConstants<T>(uint rootParameterIndex, T[] srcData, uint destOffset = 0)
        where T : unmanaged
    {
        ReadOnlySpan<T> span = srcData.AsSpan();

        SetGraphicsRoot32BitConstants(rootParameterIndex, span, destOffset);
    }

    public void SetGraphicsRoot32BitConstants<T>(uint rootParameterIndex, ReadOnlySpan<T> source, uint destOffset = 0) where T : unmanaged
    {
        fixed (T* pSrcData = source)
        {
            SetGraphicsRoot32BitConstants(rootParameterIndex, (uint)(source.Length * sizeof(T) / 4), pSrcData, destOffset / 4);
        }
    }

    public void SetGraphicsRoot32BitConstants<T>(uint rootParameterIndex, ref T srcData, uint destOffset = 0)
        where T : unmanaged
    {
        fixed (void* pSrcData = &srcData)
        {
            SetGraphicsRoot32BitConstants(rootParameterIndex, (uint)sizeof(T) / 4, pSrcData, destOffset / 4);
        }
    }

    public void SetGraphicsRoot32BitConstants<T>(uint rootParameterIndex, T srcData, uint destOffsetIn32BitValues)
        where T : unmanaged
    {
        SetGraphicsRoot32BitConstants(rootParameterIndex, (uint)sizeof(T) / 4, &srcData, destOffsetIn32BitValues);
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

    public void OMSetBlendFactor(ReadOnlySpan<float> color)
    {
        fixed (float* colorPtr = color)
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
            RSSetViewports((uint)viewports.Length, pViewports);
        }
    }

    public void RSSetViewports(uint count, Viewport[] viewports)
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
            RSSetViewports((uint)viewports.Length, pViewports);
        }
    }

    public void RSSetViewports(uint count, Span<Viewport> viewports)
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
                RSSetViewports((uint)viewports.Length, viewportsPtr);
            }
        }
    }

    public void RSSetViewports<T>(uint count, T[] viewports) where T : unmanaged
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

    public void RSSetViewports<T>(uint count, Span<T> viewports) where T : unmanaged
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

    public void RSSetScissorRect(in RectI rectangle)
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
            RSSetScissorRects((uint)rects.Length, pRects);
        }
    }

    public void RSSetScissorRects(uint count, RawRect[] rects)
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
            RSSetScissorRects((uint)rects.Length, pRects);
        }
    }

    public void RSSetScissorRects(uint count, Span<RawRect> rects)
    {
        fixed (RawRect* pRects = rects)
        {
            RSSetScissorRects(count, pRects);
        }
    }
    #endregion

    /// <summary>
    /// Unsets the render targets.
    /// </summary>
    public void UnsetRenderTargets()
    {
        OMSetRenderTargets(0, (void*)null, false, null);
    }

    /// <summary>
    /// Sets the graphics render target and/or the depth/stencil target.
    /// </summary>
    /// <param name="renderTargetDescriptor"> A descriptor handle that points to the render target. This should have
    /// been created using <see cref="ID3D12Device.CreateRenderTargetView"/>. Can be <c>null</c> to unbind the render
    /// target. </param>
    /// <param name="depthStencilDescriptor"> A descriptor handle that points to the depth/stencil target. This should
    /// have been created using <see cref="ID3D12Device.CreateDepthStencilView"/>. Can be <c>null</c> to unbind the
    /// depth/stencil target. </param>
    public void OMSetRenderTargets(CpuDescriptorHandle renderTargetDescriptor, CpuDescriptorHandle? depthStencilDescriptor = null)
    {
        if (renderTargetDescriptor.Ptr == 0)
        {
            OMSetRenderTargets(0, (void*)null, false, depthStencilDescriptor);
        }
        else
        {
            OMSetRenderTargets(1, (void*)&renderTargetDescriptor, false, depthStencilDescriptor);
        }
    }

    /// <summary>
    /// Sets an array of graphics render targets and/or the depth/stencil target.
    /// </summary>
    /// <param name="renderTargetDescriptors"> An array of descriptor handles that point to the render targets. Each
    /// handle should have been created using <see cref="ID3D12Device.CreateRenderTargetView"/>. </param>
    /// <param name="depthStencilDescriptor"> A descriptor handle that points to the depth/stencil target. This should
    /// have been created using <see cref="ID3D12Device.CreateDepthStencilView"/>. Can be <c>null</c> to unbind the
    /// depth/stencil target. </param>
    public void OMSetRenderTargets(CpuDescriptorHandle[] renderTargetDescriptors, CpuDescriptorHandle? depthStencilDescriptor = null)
    {
        ReadOnlySpan<CpuDescriptorHandle> renderTargetDescriptorsSpan = renderTargetDescriptors.AsSpan();
        OMSetRenderTargets(renderTargetDescriptorsSpan, depthStencilDescriptor);
    }

    /// <summary>
    /// Sets an array of graphics render targets and/or the depth/stencil target.
    /// </summary>
    /// <param name="renderTargetDescriptors"> A read-only list of descriptor handles that point to the render targets.
    /// Each handle should have been created using <see cref="ID3D12Device.CreateRenderTargetView"/>. </param>
    /// <param name="depthStencilDescriptor"> A descriptor handle that points to the depth/stencil target. This should
    /// have been created using <see cref="ID3D12Device.CreateDepthStencilView"/>. Can be <c>null</c> to unbind the
    /// depth/stencil target. </param>
    public void OMSetRenderTargets(ReadOnlySpan<CpuDescriptorHandle> renderTargetDescriptors, CpuDescriptorHandle? depthStencilDescriptor = null)
    {
        fixed (CpuDescriptorHandle* renderTargetDescriptorsPtr = renderTargetDescriptors)
        {
            OMSetRenderTargets((uint)renderTargetDescriptors.Length, (void*)renderTargetDescriptorsPtr, false, depthStencilDescriptor);
        }
    }

    /// <summary>
    /// Sets an array of graphics render targets and/or the depth/stencil target.
    /// </summary>
    /// <param name="numRenderTargetDescriptors"> The number of render targets to set. </param>
    /// <param name="firstRenderTargetDescriptor"> A descriptor handle that points to the first render target. This
    /// handle should be the first in a contiguous set of descriptors. </param>
    /// <param name="depthStencilDescriptor"> A descriptor handle that points to the depth/stencil target. This should
    /// have been created using <see cref="ID3D12Device.CreateDepthStencilView"/>. Can be <c>null</c> to unbind the
    /// depth/stencil target. </param>
    public void OMSetRenderTargets(uint numRenderTargetDescriptors, CpuDescriptorHandle firstRenderTargetDescriptor, CpuDescriptorHandle? depthStencilDescriptor = null)
    {
        OMSetRenderTargets(numRenderTargetDescriptors, (void*)&firstRenderTargetDescriptor, true, depthStencilDescriptor);
    }

    public void OMSetRenderTargets(
        uint numRenderTargetDescriptors,
        ReadOnlySpan<CpuDescriptorHandle> renderTargetDescriptors,
        bool RTsSingleHandleToDescriptorRange = false,
        CpuDescriptorHandle? depthStencilDescriptor = null)
    {
        fixed (CpuDescriptorHandle* renderTargetDescriptorsPtr = renderTargetDescriptors)
        {
            OMSetRenderTargets(numRenderTargetDescriptors, (void*)renderTargetDescriptorsPtr, RTsSingleHandleToDescriptorRange, depthStencilDescriptor);
        }
    }

    public void OMSetRenderTargets(
        uint numRenderTargetDescriptors,
        CpuDescriptorHandle* renderTargetDescriptors,
        bool RTsSingleHandleToDescriptorRange = false,
        CpuDescriptorHandle? depthStencilDescriptor = null)
    {
        OMSetRenderTargets(numRenderTargetDescriptors, (void*)renderTargetDescriptors, RTsSingleHandleToDescriptorRange, depthStencilDescriptor);
    }

    #region IASetVertexBuffers
    public void IASetVertexBuffers(uint slot, VertexBufferView vertexBufferView)
    {
        IASetVertexBuffers(slot, 1, &vertexBufferView);
    }

    public void IASetVertexBuffers(uint startSlot, Span<VertexBufferView> vertexBufferViews)
    {
        fixed (VertexBufferView* vertexBufferViewsPtr = vertexBufferViews)
        {
            IASetVertexBuffers(startSlot, (uint)vertexBufferViews.Length, vertexBufferViewsPtr);
        }
    }

    public unsafe void IASetVertexBuffers(uint startSlot, params VertexBufferView[] vertexBufferViews)
    {
        fixed (VertexBufferView* vertexBufferViewsPtr = vertexBufferViews)
        {
            IASetVertexBuffers(startSlot, (uint)vertexBufferViews.Length, vertexBufferViewsPtr);
        }
    }

    public void IASetVertexBuffers(uint startSlot, uint viewsCount, VertexBufferView[] vertexBufferViews)
    {
        fixed (VertexBufferView* vertexBufferViewsPtr = vertexBufferViews)
        {
            IASetVertexBuffers(startSlot, viewsCount, vertexBufferViewsPtr);
        }
    }

    public void IASetVertexBuffers(uint startSlot, uint viewsCount, VertexBufferView* vertexBufferViews)
    {
        IASetVertexBuffers(startSlot, viewsCount, (void*)vertexBufferViews);
    }
    #endregion

    public void IASetIndexBuffer(IndexBufferView* view)
    {
        IASetIndexBuffer((void*)view);
    }

    public void IASetIndexBuffer(ulong bufferLocation, uint sizeInBytes, Format format)
    {
        IndexBufferView view = new(bufferLocation, sizeInBytes, format);
        IASetIndexBuffer((void*)&view);
    }

    public void IASetIndexBuffer(ulong bufferLocation, uint sizeInBytes, bool is32Bit = false)
    {
        IndexBufferView view = new(bufferLocation, sizeInBytes, is32Bit ? Format.R32_UInt : Format.R16_UInt);
        IASetIndexBuffer((void*)&view);
    }

    public void IASetIndexBuffer(IndexBufferView? view)
    {
        if (view.HasValue)
        {
            IndexBufferView viewCall = view.Value;
            IASetIndexBuffer((void*)&viewCall);
        }
        else
        {
            IASetIndexBuffer((void*)null);
        }
    }

    public void BeginEvent(string name)
    {
        int bufferSize = PixHelpers.CalculateNoArgsEventSize(name);
        void* buffer = stackalloc byte[bufferSize];
        PixHelpers.FormatNoArgsEventToBuffer(buffer, PixHelpers.PixEventType.PIXEvent_BeginEvent_NoArgs, 0, name);
        BeginEvent(PixHelpers.WinPIXEventPIX3BlobVersion, new IntPtr(buffer), (uint)bufferSize);
    }

    public void SetMarker(string name)
    {
        int bufferSize = PixHelpers.CalculateNoArgsEventSize(name);
        void* buffer = stackalloc byte[bufferSize];
        PixHelpers.FormatNoArgsEventToBuffer(buffer, PixHelpers.PixEventType.PIXEvent_SetMarker_NoArgs, 0, name);
        SetMarker(PixHelpers.WinPIXEventPIX3BlobVersion, new IntPtr(buffer), (uint)bufferSize);
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
        uint destinationX, uint destinationY, uint destinationZ,
        TextureCopyLocation source, Box? sourceBox = null)
    {
        CopyTextureRegion_(destination, destinationX, destinationY, destinationZ, source, sourceBox);
    }

    /// <summary>
    /// This method uses the GPU to copy texture data between two locations.
    /// Both the source and the destination may reference texture data located within either a buffer resource or a texture resource.
    /// </summary>
    /// <param name="destination">Specifies the destination <see cref="TextureCopyLocation"/>. The subresource referred to must be in the <see cref="ResourceStates.CopyDest"/> state.</param>
    /// <param name="destinationCoordinate">
    /// The xyz-coordinate of the upper left corner of the destination region.
    /// For a 1D subresource, Y must be zero.
    /// For a 1D or 2D subresource, Z must be zero.
    /// </param>
    /// <param name="source">Specifies the source D3D12_TEXTURE_COPY_LOCATION. The subresource referred to must be in the D3D12_RESOURCE_STATE_COPY_SOURCE state.</param>
    /// <param name="sourceBox">Specifies an optional <see cref="Box"/> that sets the size of the source texture to copy.</param>
    public void CopyTextureRegion(
        TextureCopyLocation destination,
        Int3 destinationCoordinate,
        TextureCopyLocation source, Box? sourceBox = null)
    {
        CopyTextureRegion_(destination,
            (uint)destinationCoordinate.X, (uint)destinationCoordinate.Y, (uint)destinationCoordinate.Z,
            source, sourceBox);
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
    public void DiscardResource(ID3D12Resource resource, uint firstSubresource, uint numSubresources)
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
    public void DiscardResource(ID3D12Resource resource,
        uint rectCount, RawRect[] rects,
        uint firstSubresource, uint numSubresources)
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
    public void DiscardResource(ID3D12Resource resource, RawRect[] rects, uint firstSubresource, uint numSubresources)
    {
        fixed (RawRect* rectsPtr = &rects[0])
        {
            DiscardResource(resource, new DiscardRegion
            {
                NumRects = (uint)rects.Length,
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

    private static unsafe void MemcpySubresource(
        MemCpyDest* pDest,
        SubresourceData* pSrc,
        nuint RowSizeInBytes,
        uint NumRows,
        uint NumSlices)
    {
        for (uint z = 0; z < NumSlices; ++z)
        {
            byte* pDestSlice = (byte*)(pDest->pData) + pDest->SlicePitch * (nuint)(z);
            byte* pSrcSlice = unchecked((byte*)(pSrc->pData) + pSrc->SlicePitch * z);

            for (uint y = 0; y < NumRows; ++y)
            {
                NativeMemory.Copy(unchecked(pSrcSlice + pSrc->RowPitch * y), pDestSlice + pDest->RowPitch * (nuint)y, RowSizeInBytes);
            }
        }
    }

    private static unsafe void MemcpySubresource(MemCpyDest* pDest,
        void* pResourceData, SubresourceInfo* pSrc,
        nuint RowSizeInBytes,
        uint NumRows,
        uint NumSlices)
    {
        for (uint z = 0; z < NumSlices; ++z)
        {
            byte* pDestSlice = (byte*)(pDest->pData) + pDest->SlicePitch * (nuint)(z);
            byte* pSrcSlice = ((byte*)(pResourceData) + pSrc->Offset) + pSrc->DepthPitch * z;

            for (uint y = 0; y < NumRows; ++y)
            {
                NativeMemory.Copy(pSrcSlice + pSrc->RowPitch * y, pDestSlice + pDest->RowPitch * y, RowSizeInBytes);
            }
        }
    }

    public ulong UpdateSubresources(ID3D12Resource destinationResource, ID3D12Resource intermediate,
        uint firstSubresource, uint numSubresources,
        ulong requiredSize,
        PlacedSubresourceFootPrint* pLayouts,
        uint* pNumRows,
        ulong* pRowSizesInBytes,
        SubresourceData* pSrcData)
    {
        ResourceDescription IntermediateDesc = intermediate.Description;
        ResourceDescription DestinationDesc = destinationResource.Description;

        if ((IntermediateDesc.Dimension != ResourceDimension.Buffer)
            || (IntermediateDesc.Width < (requiredSize + pLayouts[0].Offset))
            || (requiredSize > nuint.MaxValue)
            || ((DestinationDesc.Dimension == ResourceDimension.Buffer) && ((firstSubresource != 0) || (numSubresources != 1))))
        {
            return 0;
        }

        byte* pData;
        Result hr = intermediate.Map(0, (void**)&pData);
        if (hr.Failure)
        {
            return 0;
        }

        for (uint i = 0; i < numSubresources; ++i)
        {
            if (pRowSizesInBytes[i] > nuint.MaxValue)
            {
                return 0;
            }

            MemCpyDest DestData = new()
            {
                pData = pData + pLayouts[i].Offset,
                RowPitch = pLayouts[i].Footprint.RowPitch,
                SlicePitch = unchecked(pLayouts[i].Footprint.RowPitch * (nuint)(pNumRows[i])),
            };

            MemcpySubresource(&DestData, &pSrcData[i], unchecked((nuint)(pRowSizesInBytes[i])), pNumRows[i], pLayouts[i].Footprint.Depth);
        }
        intermediate.Unmap(0, null);

        if (DestinationDesc.Dimension == ResourceDimension.Buffer)
        {
            CopyBufferRegion(destinationResource, 0, intermediate, pLayouts[0].Offset, (ulong)pLayouts[0].Footprint.Width);
        }
        else
        {
            for (uint i = 0; i < numSubresources; ++i)
            {
                TextureCopyLocation dst = new(destinationResource, i + firstSubresource);
                TextureCopyLocation src = new(intermediate, pLayouts[i]);
                CopyTextureRegion_(dst, 0, 0, 0, src, null);
            }
        }

        return requiredSize;
    }

    public ulong UpdateSubresources(ID3D12Resource destinationResource, ID3D12Resource intermediate,
        uint firstSubresource, uint numSubresources,
        ulong requiredSize,
        PlacedSubresourceFootPrint* pLayouts,
        uint* pNumRows,
        ulong* pRowSizesInBytes,
        void* pResourceData,
        SubresourceInfo* pSrcData)
    {
        ResourceDescription IntermediateDesc = intermediate.Description;
        ResourceDescription DestinationDesc = destinationResource.Description;

        if ((IntermediateDesc.Dimension != ResourceDimension.Buffer)
            || (IntermediateDesc.Width < (requiredSize + pLayouts[0].Offset))
            || (requiredSize > nuint.MaxValue)
            || ((DestinationDesc.Dimension == ResourceDimension.Buffer) && ((firstSubresource != 0) || (numSubresources != 1))))
        {
            return 0;
        }

        byte* pData;
        Result hr = intermediate.Map(0, (void**)&pData);

        if (hr.Failure)
        {
            return 0;
        }

        for (uint i = 0; i < numSubresources; ++i)
        {
            if (pRowSizesInBytes[i] > nuint.MaxValue)
            {
                return 0;
            }

            MemCpyDest DestData = new()
            {
                pData = pData + pLayouts[i].Offset,
                RowPitch = pLayouts[i].Footprint.RowPitch,
                SlicePitch = unchecked(pLayouts[i].Footprint.RowPitch * (nuint)pNumRows[i]),
            };

            MemcpySubresource(&DestData, pResourceData, &pSrcData[i], unchecked((nuint)(pRowSizesInBytes[i])), pNumRows[i], pLayouts[i].Footprint.Depth);
        }
        intermediate.Unmap(0, null);

        if (DestinationDesc.Dimension == ResourceDimension.Buffer)
        {
            CopyBufferRegion(destinationResource, 0, intermediate, pLayouts[0].Offset, pLayouts[0].Footprint.Width);
        }
        else
        {
            for (uint i = 0; i < numSubresources; ++i)
            {
                TextureCopyLocation dst = new(destinationResource, i + firstSubresource);
                TextureCopyLocation src = new(intermediate, pLayouts[i]);
                CopyTextureRegion_(dst, 0, 0, 0, src, null);
            }
        }

        return requiredSize;
    }

    public ulong UpdateSubresources(ID3D12Resource destinationResource,
        ID3D12Resource intermediate,
        ulong intermediateOffset,
        uint firstSubresource,
        uint numSubresources,
        SubresourceData* pSrcData)
    {
        PlacedSubresourceFootPrint* layouts = stackalloc PlacedSubresourceFootPrint[(int)numSubresources];
        uint* numRows = stackalloc uint[(int)numSubresources];
        ulong* rowSizesInBytes = stackalloc ulong[(int)numSubresources];
        ulong requiredSize = 0;

        ResourceDescription resourceDesc = destinationResource.Description;

        using (ID3D12Device device = destinationResource.GetDevice<ID3D12Device>())
        {
            device.GetCopyableFootprints(resourceDesc,
                firstSubresource,
                numSubresources,
                intermediateOffset,
                layouts,
                numRows,
                rowSizesInBytes,
                out requiredSize);
        }

        ulong result = UpdateSubresources(
            destinationResource, intermediate,
            firstSubresource, numSubresources,
            requiredSize,
            layouts,
            numRows,
            rowSizesInBytes,
            pSrcData);
        return result;
    }

    public ulong UpdateSubresources(ID3D12Resource destinationResource, ID3D12Resource intermediate,
        uint firstSubresource, uint numSubresources,
        ulong requiredSize,
        ReadOnlySpan<PlacedSubresourceFootPrint> layouts,
        ReadOnlySpan<uint> numRows,
        ReadOnlySpan<ulong> rowSizesInBytes,
        SubresourceData* pSrcData)
    {
        fixed (PlacedSubresourceFootPrint* pLayouts = layouts)
        fixed (uint* pNumRows = numRows)
        fixed (ulong* pRowSizesInBytes = rowSizesInBytes)
        {
            return UpdateSubresources(
                destinationResource, intermediate,
                firstSubresource, numSubresources,
                requiredSize,
                pLayouts, pNumRows, pRowSizesInBytes, pSrcData);
        }
    }

    public ulong UpdateSubresources(ID3D12Resource destinationResource, ID3D12Resource intermediate,
        uint firstSubresource, uint numSubresources,
        ulong requiredSize,
        ReadOnlySpan<PlacedSubresourceFootPrint> layouts,
        ReadOnlySpan<uint> numRows,
        ReadOnlySpan<ulong> rowSizesInBytes,
        void* resourceData,
        SubresourceInfo* pSrcData)
    {
        fixed (PlacedSubresourceFootPrint* pLayouts = layouts)
        fixed (uint* pNumRows = numRows)
        fixed (ulong* pRowSizesInBytes = rowSizesInBytes)
        {
            return UpdateSubresources(
                destinationResource, intermediate,
                firstSubresource, numSubresources,
                requiredSize,
                pLayouts, pNumRows, pRowSizesInBytes,
                resourceData, pSrcData);
        }
    }

    struct MemCpyDest
    {
        public unsafe void* pData;

        public nuint RowPitch;
        public nuint SlicePitch;
    }
}
