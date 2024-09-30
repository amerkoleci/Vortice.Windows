// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using Vortice.Mathematics;

namespace Vortice.Direct3D11;

public unsafe partial class ID3D11DeviceContext
{
    /// <unmanaged>D3D11_KEEP_RENDER_TARGETS_AND_DEPTH_STENCIL</unmanaged>
    public const uint KeepRenderTargetsAndDepthStencil = 0xffffffff;

    /// <unmanaged>D3D11_KEEP_UNORDERED_ACCESS_VIEWS</unmanaged>
    public const uint KeepUnorderedAccessViews = 0xffffffff;

    /// <unmanaged>D3D11_DEFAULT_SAMPLE_MASK</unmanaged>
    public const uint DefaultSampleMask = 0xffffffff;

    private static readonly void*[] s_NullBuffers =
    [
        null, null, null, null, null, null, null,
        null, null, null, null, null, null, null
    ];

    private static readonly void*[] s_NullSamplers =
    [
        null, null, null, null, null, null, null, null,
        null, null, null, null, null, null, null, null
    ];

    private static readonly void*[] s_NullUAVs =
    [
        null, null, null,
        null, null, null,
        null, null
    ];

    private static readonly uint[] s_NegativeOnes = new uint[UnorderedAccessViewRegisterCount]
    {
        KeepUnorderedAccessViews, KeepUnorderedAccessViews, KeepUnorderedAccessViews,
        KeepUnorderedAccessViews, KeepUnorderedAccessViews, KeepUnorderedAccessViews,
        KeepUnorderedAccessViews, KeepUnorderedAccessViews
    };

    private bool? _supportsCommandLists;

    public void ClearRenderTargetView(ID3D11RenderTargetView renderTargetView, in Color4 color)
    {
        ClearRenderTargetView(renderTargetView, new Color4(color));
    }

    public void OMSetBlendState(ID3D11BlendState? blendState)
    {
        IntPtr blendStatePtr = blendState?.NativePointer ?? IntPtr.Zero;
        ((delegate* unmanaged[Stdcall]<IntPtr, void*, float*, uint, void>)this[OMSetBlendState__vtbl_index])(NativePointer, (void*)blendStatePtr, null, DefaultSampleMask);
    }

    public void OMSetBlendState(ID3D11BlendState? blendState, float* blendFactor)
    {
        IntPtr blendStatePtr = blendState?.NativePointer ?? IntPtr.Zero;
        ((delegate* unmanaged[Stdcall]<IntPtr, void*, float*, uint, void>)this[OMSetBlendState__vtbl_index])(NativePointer, (void*)blendStatePtr, blendFactor, DefaultSampleMask);
    }

    public void OMSetBlendState(ID3D11BlendState? blendState, float* blendFactor, uint sampleMask)
    {
        IntPtr blendStatePtr = blendState?.NativePointer ?? IntPtr.Zero;
        ((delegate* unmanaged[Stdcall]<IntPtr, void*, float*, uint, void>)this[OMSetBlendState__vtbl_index])(NativePointer, (void*)blendStatePtr, blendFactor, sampleMask);
    }

    public void OMSetBlendState(ID3D11BlendState? blendState, Span<float> blendFactor)
    {
        IntPtr blendStatePtr = blendState?.NativePointer ?? IntPtr.Zero;
        fixed (float* blendFactorPtr = blendFactor)
        {
            ((delegate* unmanaged[Stdcall]<IntPtr, void*, float*, uint, void>)this[OMSetBlendState__vtbl_index])(NativePointer, (void*)blendStatePtr, blendFactorPtr, DefaultSampleMask);
        }
    }

    public void OMSetBlendState(ID3D11BlendState? blendState, ReadOnlySpan<float> blendFactor)
    {
        IntPtr blendStatePtr = blendState?.NativePointer ?? IntPtr.Zero;
        fixed (float* blendFactorPtr = blendFactor)
        {
            ((delegate* unmanaged[Stdcall]<IntPtr, void*, float*, uint, void>)this[OMSetBlendState__vtbl_index])(NativePointer, (void*)blendStatePtr, blendFactorPtr, DefaultSampleMask);
        }
    }

    public void OMSetBlendState(ID3D11BlendState? blendState, Color4 blendFactor)
    {
        OMSetBlendState(blendState, (float*)&blendFactor, DefaultSampleMask);
    }

    public void OMSetBlendState(ID3D11BlendState? blendState, Color4 blendFactor, uint sampleMask = DefaultSampleMask)
    {
        OMSetBlendState(blendState, (float*)&blendFactor, sampleMask);
    }


    /// <summary>
    /// Unsets the render targets.
    /// </summary>
    public void UnsetRenderTargets()
    {
        OMSetRenderTargets(0, (void*)null, null);
    }

    public void OMSetRenderTargets(ID3D11RenderTargetView renderTargetView, ID3D11DepthStencilView? depthStencilView = default)
    {
        IntPtr renderTargetViewPtr = renderTargetView == null ? IntPtr.Zero : renderTargetView.NativePointer;
        OMSetRenderTargets(1, &renderTargetViewPtr, depthStencilView);
    }

    public void OMSetRenderTargets(uint renderTargetViewsCount, ID3D11RenderTargetView[] renderTargetViews, ID3D11DepthStencilView? depthStencilView = default)
    {
        IntPtr* renderTargetViewsPtr = stackalloc IntPtr[(int)renderTargetViewsCount];
        for (int i = 0; i < renderTargetViewsCount; i++)
        {
            renderTargetViewsPtr[i] = (renderTargetViews[i] == null) ? IntPtr.Zero : renderTargetViews[i].NativePointer;
        }

        OMSetRenderTargets(renderTargetViewsCount, renderTargetViewsPtr, depthStencilView);
    }

    public void OMSetRenderTargets(ID3D11RenderTargetView[] renderTargetViews, ID3D11DepthStencilView? depthStencilView = default)
    {
        IntPtr* renderTargetViewsPtr = stackalloc IntPtr[renderTargetViews.Length];
        for (int i = 0; i < renderTargetViews.Length; i++)
        {
            renderTargetViewsPtr[i] = (renderTargetViews[i] == null) ? IntPtr.Zero : renderTargetViews[i].NativePointer;
        }

        OMSetRenderTargets((uint)renderTargetViews.Length, renderTargetViewsPtr, depthStencilView);
    }

    public void OMSetRenderTargets(ReadOnlySpan<ID3D11RenderTargetView> renderTargetViews, ID3D11DepthStencilView? depthStencilView = default)
    {
        IntPtr* renderTargetViewsPtr = stackalloc IntPtr[renderTargetViews.Length];
        for (int i = 0; i < renderTargetViews.Length; i++)
        {
            renderTargetViewsPtr[i] = (renderTargetViews[i] == null) ? IntPtr.Zero : renderTargetViews[i].NativePointer;
        }

        OMSetRenderTargets((uint)renderTargetViews.Length, renderTargetViewsPtr, depthStencilView);
    }

    public void OMSetUnorderedAccessView(uint startSlot, ID3D11UnorderedAccessView unorderedAccessView, uint uavInitialCount = unchecked((uint)-1))
    {
        IntPtr unorderedAccessViewPtr = unorderedAccessView != null ? unorderedAccessView.NativePointer : IntPtr.Zero;

        OMSetRenderTargetsAndUnorderedAccessViews(KeepRenderTargetsAndDepthStencil, null, IntPtr.Zero,
            startSlot,
            1,
            &unorderedAccessViewPtr,
            &uavInitialCount
            );
    }

    public void OMUnsetUnorderedAccessView(uint startSlot, uint uavInitialCount = unchecked((uint)-1))
    {
        void* nullUAV = default;
        OMSetRenderTargetsAndUnorderedAccessViews(
            KeepRenderTargetsAndDepthStencil, null, IntPtr.Zero,
            startSlot, 1,
            &nullUAV,
            &uavInitialCount
            );
    }

    public void OMSetUnorderedAccessViews(uint uavStartSlot, uint unorderedAccessViewCount, ID3D11UnorderedAccessView[] unorderedAccessViews)
    {
        IntPtr* unorderedAccessViewsPtr = stackalloc IntPtr[(int)unorderedAccessViewCount];
        for (int i = 0; i < unorderedAccessViewCount; i++)
        {
            unorderedAccessViewsPtr[i] = unorderedAccessViews[i].NativePointer;
        }

        fixed (uint* negativeOnesPtr = &s_NegativeOnes[0])
        {
            OMSetRenderTargetsAndUnorderedAccessViews(
                KeepRenderTargetsAndDepthStencil, null, IntPtr.Zero,
                uavStartSlot,
                unorderedAccessViewCount,
                unorderedAccessViewsPtr,
                negativeOnesPtr);
        }
    }

    public void OMSetRenderTargetsAndUnorderedAccessViews(
        ID3D11RenderTargetView renderTargetView,
        ID3D11DepthStencilView depthStencilView,
        uint startSlot,
        ID3D11UnorderedAccessView[] unorderedAccessViews)
    {
        IntPtr renderTargetViewPtr = renderTargetView == null ? IntPtr.Zero : renderTargetView.NativePointer;

        IntPtr* unorderedAccessViewsPtr = stackalloc IntPtr[unorderedAccessViews.Length];
        int* uavInitialCounts = stackalloc int[unorderedAccessViews.Length];
        for (int i = 0; i < unorderedAccessViews.Length; i++)
        {
            unorderedAccessViewsPtr[i] = unorderedAccessViews[i].NativePointer;
            uavInitialCounts[i] = -1;
        }

        OMSetRenderTargetsAndUnorderedAccessViews(1,
            &renderTargetViewPtr,
            depthStencilView != null ? depthStencilView.NativePointer : IntPtr.Zero,
            startSlot, (uint)unorderedAccessViews.Length,
            unorderedAccessViewsPtr,
            uavInitialCounts);
    }

    public void OMSetRenderTargetsAndUnorderedAccessViews(
        ID3D11RenderTargetView[] renderTargetViews,
        ID3D11DepthStencilView depthStencilView,
        uint startSlot,
        ID3D11UnorderedAccessView[] unorderedAccessViews)
    {
        IntPtr* renderTargetViewsPtr = stackalloc IntPtr[renderTargetViews.Length];
        for (int i = 0; i < renderTargetViews.Length; i++)
        {
            renderTargetViewsPtr[i] = renderTargetViews[i].NativePointer;
        }

        IntPtr* unorderedAccessViewsPtr = stackalloc IntPtr[unorderedAccessViews.Length];
        int* uavInitialCounts = stackalloc int[unorderedAccessViews.Length];
        for (int i = 0; i < unorderedAccessViews.Length; i++)
        {
            unorderedAccessViewsPtr[i] = unorderedAccessViews[i].NativePointer;
            uavInitialCounts[i] = -1;
        }

        OMSetRenderTargetsAndUnorderedAccessViews((uint)renderTargetViews.Length,
            renderTargetViewsPtr,
            depthStencilView != null ? depthStencilView.NativePointer : IntPtr.Zero,
            startSlot, (uint)unorderedAccessViews.Length,
            unorderedAccessViewsPtr,
            uavInitialCounts);
    }

    public void OMSetRenderTargetsAndUnorderedAccessViews(
        ID3D11RenderTargetView[] renderTargetViews,
        ID3D11DepthStencilView depthStencilView,
        uint uavStartSlot,
        ID3D11UnorderedAccessView[] unorderedAccessViews,
        uint[] uavInitialCounts)
    {
        OMSetRenderTargetsAndUnorderedAccessViews(
            (uint)renderTargetViews.Length, renderTargetViews, depthStencilView,
            uavStartSlot, (uint)unorderedAccessViews.Length, unorderedAccessViews, uavInitialCounts
            );
    }

    public void OMSetRenderTargetsAndUnorderedAccessViews(
        uint renderTargetViewsCount,
        ID3D11RenderTargetView[] renderTargetViews,
        ID3D11DepthStencilView depthStencilView,
        uint startSlot,
        uint unorderedAccessViewsCount,
        ID3D11UnorderedAccessView[] unorderedAccessViews)
    {
        IntPtr* renderTargetViewsPtr = stackalloc IntPtr[renderTargetViews.Length];
        for (int i = 0; i < renderTargetViews.Length; i++)
        {
            renderTargetViewsPtr[i] = renderTargetViews[i].NativePointer;
        }

        IntPtr* unorderedAccessViewsPtr = stackalloc IntPtr[unorderedAccessViews.Length];
        for (int i = 0; i < unorderedAccessViews.Length; i++)
        {
            unorderedAccessViewsPtr[i] = unorderedAccessViews[i].NativePointer;
        }

        fixed (uint* negativeOnesPtr = &s_NegativeOnes[0])
        {
            OMSetRenderTargetsAndUnorderedAccessViews(renderTargetViewsCount,
                renderTargetViewsPtr,
                depthStencilView != null ? depthStencilView.NativePointer : IntPtr.Zero,
                startSlot,
                unorderedAccessViewsCount,
                unorderedAccessViewsPtr,
                negativeOnesPtr);
        }
    }

    public void OMSetRenderTargetsAndUnorderedAccessViews(
        uint renderTargetViewsCount,
        ID3D11RenderTargetView[] renderTargetViews,
        ID3D11DepthStencilView depthStencilView,
        uint startSlot,
        uint unorderedAccessViewsCount,
        ID3D11UnorderedAccessView[] unorderedAccessViews,
        ReadOnlySpan<uint> uavInitialCounts)
    {
        IntPtr* renderTargetViewsPtr = stackalloc IntPtr[renderTargetViews.Length];
        for (int i = 0; i < renderTargetViews.Length; i++)
        {
            renderTargetViewsPtr[i] = renderTargetViews[i].NativePointer;
        }

        IntPtr* unorderedAccessViewsPtr = stackalloc IntPtr[unorderedAccessViews.Length];
        for (int i = 0; i < unorderedAccessViews.Length; i++)
        {
            unorderedAccessViewsPtr[i] = unorderedAccessViews[i].NativePointer;
        }

        fixed (uint* pUAVInitialCounts = uavInitialCounts)
        {
            OMSetRenderTargetsAndUnorderedAccessViews(renderTargetViewsCount,
                renderTargetViewsPtr,
                depthStencilView != null ? depthStencilView.NativePointer : IntPtr.Zero,
                startSlot,
                unorderedAccessViewsCount,
                unorderedAccessViewsPtr,
                pUAVInitialCounts);
        }
    }

    public ID3D11CommandList FinishCommandList(bool restoreState)
    {
        FinishCommandList(restoreState, out ID3D11CommandList result).CheckError();
        return result;
    }

    public bool IsDataAvailable(ID3D11Asynchronous data)
    {
        return GetData(data, IntPtr.Zero, 0, AsyncGetDataFlags.None) == Result.Ok;
    }

    public bool IsDataAvailable(ID3D11Asynchronous data, AsyncGetDataFlags flags)
    {
        return GetData(data, IntPtr.Zero, 0, flags) == Result.Ok;
    }

    /// <summary>
    ///   Gets data from the GPU asynchronously.
    /// </summary>
    /// <param name = "data">The asynchronous data provider.</param>
    /// <param name = "flags">Flags specifying how the command should operate.</param>
    /// <returns>The data retrieved from the GPU.</returns>
    public DataStream GetData(ID3D11Asynchronous data, AsyncGetDataFlags flags = AsyncGetDataFlags.None)
    {
        var result = new DataStream((int)data.DataSize, true, true);
        GetData(data, result.BasePointer, (uint)result.Length, flags);
        return result;
    }

    public T GetData<T>(ID3D11Asynchronous data, AsyncGetDataFlags flags) where T : unmanaged
    {
        GetData(data, flags, out T result);
        return result;
    }

    public bool GetData<T>(ID3D11Asynchronous data, AsyncGetDataFlags flags, out T result) where T : unmanaged
    {
        result = default;
        fixed (void* resultPtr = &result)
        {
            return GetData(data, (IntPtr)resultPtr, (uint)sizeof(T), flags) == Result.Ok;
        }
    }

    public bool GetData<T>(ID3D11Asynchronous data, out T result) where T : unmanaged
    {
        result = default;
        fixed (void* resultPtr = &result)
        {
            return GetData(data, (IntPtr)resultPtr, (uint)sizeof(T), AsyncGetDataFlags.None) == Result.Ok;
        }
    }

    public void OMGetBlendState(out ID3D11BlendState blendState, float* blendFactor, out uint sampleMask)
    {
        IntPtr blendStatePtr = IntPtr.Zero;
        ((delegate* unmanaged[Stdcall]<IntPtr, void*, float*, out uint, void>)this[OMGetBlendState__vtbl_index])(
            NativePointer, &blendStatePtr, blendFactor, out sampleMask);
        blendState = new ID3D11BlendState(blendStatePtr);
    }

    public ID3D11BlendState OMGetBlendState()
    {
        OMGetBlendState(out ID3D11BlendState blendState, default, out _);
        return blendState;
    }

    public ID3D11BlendState OMGetBlendState(out Color4 blendFactor)
    {
        Color4 blendFactorResult = default;
        OMGetBlendState(out ID3D11BlendState blendState, (float*)&blendFactorResult, out _);
        blendFactor = blendFactorResult;
        return blendState;
    }

    public ID3D11BlendState OMGetBlendState(out Color4 blendFactor, out uint sampleMask)
    {
        Color4 blendFactorResult = default;
        OMGetBlendState(out ID3D11BlendState blendState, (float*)&blendFactorResult, out sampleMask);
        blendFactor = blendFactorResult;
        return blendState;
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

    public void RSSetViewports(Viewport[] viewports)
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

        fixed (T* viewportsPtr = &viewports[0])
        {
            RSSetViewports((uint)viewports.Length, viewportsPtr);
        }
    }

    public void RSSetViewports<T>(Span<T> viewports) where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != sizeof(Viewport))
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(Viewport)}", nameof(viewports));
        }
#endif

        fixed (T* viewportsPtr = viewports)
        {
            RSSetViewports((uint)viewports.Length, viewportsPtr);
        }
    }

    /// <summary>
    /// Get the number of bound viewports.
    /// </summary>
    /// <returns></returns>
    public uint RSGetViewports()
    {
        uint numViewports = 0;
        RSGetViewports(ref numViewports, (void*)null);
        return numViewports;
    }

    public Viewport RSGetViewport()
    {
        uint numViewports = 1;
        Viewport viewport = default;
        RSGetViewports(ref numViewports, (void*)&viewport);
        return viewport;
    }

    public void RSGetViewport(ref Viewport viewport)
    {
        uint numViewports = 1;
        fixed (Viewport* viewportPtr = &viewport)
        {
            RSGetViewports(ref numViewports, viewportPtr);
        }
    }

    public void RSGetViewports(Span<Viewport> viewports)
    {
        fixed (Viewport* viewportsPtr = &MemoryMarshal.GetReference(viewports))
        {
            uint numViewports = (uint)viewports.Length;
            RSGetViewports(ref numViewports, (void*)viewportsPtr);
        }
    }

    public void RSGetViewports<T>(ref uint count, Span<T> viewports) where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != sizeof(Viewport))
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(Viewport)}", nameof(viewports));
        }
#endif

        fixed (T* viewportsPtr = viewports)
        {
            RSGetViewports(ref count, viewportsPtr);
        }
    }

    public void RSGetViewports<T>(Span<T> viewports) where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != sizeof(Viewport))
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(Viewport)}", nameof(viewports));
        }
#endif

        fixed (T* viewportsPtr = viewports)
        {
            uint numViewports = (uint)viewports.Length;
            RSGetViewports(ref numViewports, viewportsPtr);
        }
    }

    /// <summary>	
    /// Get the array of viewports bound  to the rasterizer stage.	
    /// </summary>	
    /// <returns>An array of viewports, must be size of <see cref="Viewport"/></returns>
    public ReadOnlySpan<T> RSGetViewports<T>() where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != sizeof(Viewport))
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(Viewport)}");
        }
#endif
        uint numViewports = 0;
        RSGetViewports(ref numViewports, (void*)null);
        T[] viewports = new T[numViewports];

        fixed (T* viewportsPtr = viewports)
        {
            RSGetViewports(ref numViewports, viewportsPtr);
        }
        return viewports;
    }

    public void RSGetViewports(ref uint count, Span<Viewport> viewports)
    {
        fixed (Viewport* viewportsPtr = &MemoryMarshal.GetReference(viewports))
        {
            RSGetViewports(ref count, (void*)viewportsPtr);
        }
    }

    public void RSGetViewports(ref uint count, Viewport* viewports)
    {
        RSGetViewports(ref count, (void*)viewports);
    }
    #endregion

    #region ScissorRect
    /// <summary>
    /// Get the number of bound scissor rectangles.
    /// </summary>
    /// <returns></returns>
    public uint RSGetScissorRects()
    {
        uint numRects = 0;
        RSGetScissorRects(ref numRects, IntPtr.Zero);
        return numRects;
    }

    public void RSSetScissorRect(int x, int y, int width, int height)
    {
        RawRect rawRect = new(x, y, x + width, y + height);
        RSSetScissorRects(1, &rawRect);
    }

    public void RSSetScissorRect(int width, int height)
    {
        RawRect rect = new(0, 0, width, height);
        RSSetScissorRects(1, &rect);
    }

    public void RSSetScissorRect(in RectI rectangle)
    {
        RawRect rawRect = rectangle;
        RSSetScissorRects(1, &rawRect);
    }

    public void RSSetScissorRect(RawRect rectangle)
    {
        RSSetScissorRects(1, &rectangle);
    }

    public void RSSetScissorRects(RawRect[] rectangles)
    {
        fixed (RawRect* pRects = rectangles)
        {
            RSSetScissorRects((uint)rectangles.Length, pRects);
        }
    }

    public void RSSetScissorRects(uint count, RawRect[] rectangles)
    {
        fixed (RawRect* pRects = rectangles)
        {
            RSSetScissorRects(count, pRects);
        }
    }

    public void RSSetScissorRects(Span<RawRect> rectangles)
    {
        fixed (RawRect* pRects = rectangles)
        {
            RSSetScissorRects((uint)rectangles.Length, pRects);
        }
    }

    public void RSSetScissorRects(uint count, Span<RawRect> rectangles)
    {
        fixed (RawRect* pRects = rectangles)
        {
            RSSetScissorRects(count, pRects);
        }
    }

    public void RSSetScissorRect<T>(T rect) where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != Unsafe.SizeOf<RawRect>())
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(RawRect)}", nameof(rect));
        }
#endif

        RSSetScissorRects(1, &rect);
    }

    public void RSSetScissorRects<T>(T[] rects) where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != Unsafe.SizeOf<RawRect>())
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(RawRect)}", nameof(rects));
        }
#endif

        fixed (void* rectPtr = &rects[0])
        {
            RSSetScissorRects((uint)rects.Length, rectPtr);
        }
    }

    public void RSSetScissorRects<T>(uint numRects, T[] rects) where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != Unsafe.SizeOf<RawRect>())
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(RawRect)}", nameof(rects));
        }
#endif

        fixed (void* rectPtr = &rects[0])
        {
            RSSetScissorRects(numRects, rectPtr);
        }
    }

    public void RSSetScissorRects<T>(Span<T> rects) where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != Unsafe.SizeOf<RawRect>())
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(RawRect)}", nameof(rects));
        }
#endif
        fixed (void* rectsPtr = rects)
        {
            RSSetScissorRects((uint)rects.Length, rectsPtr);
        }
    }

    public RawRect RSGetScissorRect()
    {
        uint numRects = 1;
        var rect = new RawRect();
        RSGetScissorRects(ref numRects, new IntPtr(&rect));
        return rect;
    }

    public void RSGetScissorRect(ref RawRect rect)
    {
        uint numRects = 1;
        fixed (void* rectPtr = &rect)
        {
            RSGetScissorRects(ref numRects, (IntPtr)rectPtr);
        }
    }

    public void RSGetScissorRects(RawRect[] rects)
    {
        uint numRects = (uint)rects.Length;
        fixed (void* rectsPtr = &rects[0])
        {
            RSGetScissorRects(ref numRects, (IntPtr)rectsPtr);
        }
    }

    public void RSGetScissorRects(Span<RawRect> rects)
    {
        fixed (RawRect* rectsPtr = &MemoryMarshal.GetReference(rects))
        {
            uint numRects = (uint)rects.Length;
            RSGetScissorRects(ref numRects, (IntPtr)rectsPtr);
        }
    }

    public void RSGetScissorRects(ref uint count, RawRect[] rects)
    {
        fixed (void* rectsPtr = rects)
        {
            RSGetScissorRects(ref count, (IntPtr)rectsPtr);
        }
    }

    public void RSGetScissorRects(ref uint count, RawRect* rects)
    {
        RSGetScissorRects(ref count, (IntPtr)rects);
    }
    #endregion

    /// <summary>
    /// Set the target output buffers for the stream-output stage of the pipeline.
    /// </summary>
    /// <param name="targets">The array of output buffers <see cref="ID3D11Buffer"/> to bind to the device. The buffers must have been created with the <see cref="BindFlags.StreamOutput"/> flag.</param>
    /// <param name="offsets">Array of offsets to the output buffers from targets, one offset for each buffer. The offset values must be in bytes.</param>
    public void SOSetTargets(ID3D11Buffer[] targets, uint[]? offsets = null)
    {
        SOSetTargets((uint)targets.Length, targets, offsets);
    }

    /// <summary>
    /// Set the target output buffers for the stream-output stage of the pipeline.
    /// </summary>
    /// <param name="buffersCount">The number of buffer to bind to the device. A maximum of four output buffers can be set. If less than four are defined by the call, the remaining buffer slots are set to null.</param>
    /// <param name="targets">The array of output buffers <see cref="ID3D11Buffer"/> to bind to the device. The buffers must have been created with the <see cref="BindFlags.StreamOutput"/> flag.</param>
    /// <param name="offsets">Array of offsets to the output buffers from targets, one offset for each buffer. The offset values must be in bytes.</param>
    public void SOSetTargets(uint buffersCount, ID3D11Buffer[] targets, uint[]? offsets = null)
    {
        IntPtr* targetsPtr = stackalloc IntPtr[(int)buffersCount];
        for (int i = 0; i < buffersCount; i++)
        {
            targetsPtr[i] = targets[i] != null ? targets[i].NativePointer : IntPtr.Zero;
        }

        if (offsets != null && offsets.Length > 0)
        {
            fixed (uint* offsetsPtr = offsets)
            {
                SOSetTargets(buffersCount, targetsPtr, offsetsPtr);
            }
        }
        else
        {
            SOSetTargets(buffersCount, targetsPtr, null);
        }
    }


    /// <summary>
    /// Unsets the render targets.
    /// </summary>
    public void UnsetSOTargets()
    {
        SOSetTargets(0, (void*)null, (void*)null);
    }

    public void IASetVertexBuffer(uint slot, ID3D11Buffer buffer, uint stride, uint offset = 0)
    {
        IntPtr pVertexBuffers = buffer == null ? IntPtr.Zero : buffer.NativePointer;
        IASetVertexBuffers(slot, 1, &pVertexBuffers, &stride, &offset);
    }
    public void IASetVertexBuffers(uint firstSlot, ID3D11Buffer[] vertexBuffers, ReadOnlySpan<uint> strides, ReadOnlySpan<uint> offsets)
    {
        IASetVertexBuffers(firstSlot, (uint)vertexBuffers.Length, vertexBuffers, strides, offsets);
    }

    public void IASetVertexBuffers(uint firstSlot, uint vertexBufferViewsCount, ID3D11Buffer[] vertexBuffers, ReadOnlySpan<uint> strides, ReadOnlySpan<uint> offsets)
    {
        IntPtr* vertexBuffersPtr = stackalloc IntPtr[(int)vertexBufferViewsCount];
        for (int i = 0; i < vertexBufferViewsCount; i++)
        {
            vertexBuffersPtr[i] = (vertexBuffers[i] == null) ? IntPtr.Zero : vertexBuffers[i].NativePointer;
        }

        fixed (uint* pStrides = strides)
        fixed (uint* pOffsets = offsets)
            IASetVertexBuffers(firstSlot, vertexBufferViewsCount, vertexBuffersPtr, pStrides, pOffsets);
    }

    public void IASetVertexBuffers(uint firstSlot, uint vertexBufferViewsCount, ID3D11Buffer[] vertexBuffers, Span<uint> strides, Span<uint> offsets)
    {
        IntPtr* vertexBuffersPtr = stackalloc IntPtr[(int)vertexBufferViewsCount];
        for (int i = 0; i < vertexBufferViewsCount; i++)
        {
            vertexBuffersPtr[i] = (vertexBuffers[i] == null) ? IntPtr.Zero : vertexBuffers[i].NativePointer;
        }

        fixed (uint* pStrides = strides)
        fixed (uint* pOffsets = offsets)
            IASetVertexBuffers(firstSlot, vertexBufferViewsCount, vertexBuffersPtr, pStrides, pOffsets);
    }

    #region VertexShader
    public void VSSetShader(ID3D11VertexShader? vertexShader)
    {
        IntPtr shaderPtr = vertexShader?.NativePointer ?? IntPtr.Zero;
        VSSetShader(shaderPtr, IntPtr.Zero, 0);
    }

    public void VSSetShader(ID3D11VertexShader? vertexShader, ID3D11ClassInstance[] classInstances)
    {
        VSSetShader(vertexShader, classInstances, (uint)classInstances.Length);
    }

    public void VSUnsetConstantBuffer(uint slot)
    {
        void* nullBuffer = default;
        VSSetConstantBuffers(slot, 1, &nullBuffer);
    }

    public void VSUnsetConstantBuffers(uint startSlot, uint count)
    {
        fixed (void* nullBuffersPtr = s_NullBuffers)
        {
            VSSetConstantBuffers(startSlot, count, nullBuffersPtr);
        }
    }

    public void VSSetConstantBuffer(uint slot, ID3D11Buffer? constantBuffer)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        VSSetConstantBuffers(slot, 1, &nativePtr);
    }

    public void VSSetConstantBuffers(uint startSlot, ID3D11Buffer[] constantBuffers)
    {
        VSSetConstantBuffers(startSlot, (uint)constantBuffers.Length, constantBuffers);
    }

    public void VSSetConstantBuffers(uint startSlot, uint count, ID3D11Buffer[] constantBuffers)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        VSSetConstantBuffers(startSlot, count, ppConstantBuffers);
    }

    public void VSUnsetSampler(uint slot)
    {
        void* nullSampler = default;
        VSSetSamplers(slot, 1, &nullSampler);
    }

    public void VSUnsetSamplers(uint startSlot, uint count)
    {
        fixed (void* nullSamplersPtr = s_NullSamplers)
        {
            VSSetSamplers(startSlot, count, nullSamplersPtr);
        }
    }

    public void VSSetSampler(uint slot, ID3D11SamplerState? sampler)
    {
        IntPtr nativePtr = sampler == null ? IntPtr.Zero : sampler.NativePointer;
        VSSetSamplers(slot, 1, &nativePtr);
    }

    public void VSSetSamplers(uint startSlot, ID3D11SamplerState[] samplers)
    {
        VSSetSamplers(startSlot, (uint)samplers.Length, samplers);
    }

    public void VSSetSamplers(uint startSlot, uint count, ID3D11SamplerState[] samplers)
    {
        IntPtr* ppSamplers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppSamplers[i] = (samplers[i] == null) ? IntPtr.Zero : samplers[i].NativePointer;
        }

        VSSetSamplers(startSlot, count, ppSamplers);
    }

    public void VSUnsetShaderResource(uint slot)
    {
        void* nullResource = default;
        VSSetShaderResources(slot, 1, &nullResource);
    }

    public void VSUnsetShaderResources(uint startSlot, uint count)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = IntPtr.Zero;
        }

        VSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public void VSSetShaderResource(uint slot, ID3D11ShaderResourceView? shaderResourceView)
    {
        IntPtr nativePtr = shaderResourceView == null ? IntPtr.Zero : shaderResourceView.NativePointer;
        VSSetShaderResources(slot, 1, &nativePtr);
    }

    public void VSSetShaderResources(uint startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        VSSetShaderResources(startSlot, (uint)shaderResourceViews.Length, shaderResourceViews);
    }

    public void VSSetShaderResources(uint startSlot, uint count, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = (shaderResourceViews[i] == null) ? IntPtr.Zero : shaderResourceViews[i].NativePointer;
        }

        VSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public ID3D11VertexShader VSGetShader()
    {
        uint count = 0;
        VSGetShader(out ID3D11VertexShader shader, null, ref count);
        return shader;
    }

    public ID3D11VertexShader VSGetShader(ID3D11ClassInstance[] classInstances)
    {
        uint count = (uint)classInstances.Length;
        VSGetShader(out ID3D11VertexShader shader, classInstances, ref count);
        return shader;
    }

    public ID3D11VertexShader VSGetShader(ref uint classInstancesCount, ID3D11ClassInstance[] classInstances)
    {
        VSGetShader(out ID3D11VertexShader shader, classInstances, ref classInstancesCount);
        return shader;
    }

    public void VSGetConstantBuffers(uint startSlot, ID3D11Buffer[] constantBuffers)
    {
        VSGetConstantBuffers(startSlot, (uint)constantBuffers.Length, constantBuffers);
    }

    public void VSGetSamplers(uint startSlot, ID3D11SamplerState[] samplers)
    {
        VSGetSamplers(startSlot, (uint)samplers.Length, samplers);
    }

    public void VSGetShaderResources(uint startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        VSGetShaderResources(startSlot, (uint)shaderResourceViews.Length, shaderResourceViews);
    }
    #endregion

    #region PixelShader
    public void PSSetShader(ID3D11PixelShader? pixelShader)
    {
        IntPtr shaderPtr = pixelShader?.NativePointer ?? IntPtr.Zero;
        PSSetShader(shaderPtr, IntPtr.Zero, 0);
    }

    public void PSSetShader(ID3D11PixelShader? pixelShader, ID3D11ClassInstance[] classInstances)
    {
        PSSetShader(pixelShader, classInstances, (uint)classInstances.Length);
    }

    public void PSUnsetConstantBuffer(uint slot)
    {
        void* nullBuffer = default;
        PSSetConstantBuffers(slot, 1, &nullBuffer);
    }

    public void PSUnsetConstantBuffers(uint startSlot, uint count)
    {
        fixed (void* nullBuffersPtr = s_NullBuffers)
        {
            PSSetConstantBuffers(startSlot, count, nullBuffersPtr);
        }
    }

    public void PSSetConstantBuffer(uint slot, ID3D11Buffer? constantBuffer)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        PSSetConstantBuffers(slot, 1, &nativePtr);
    }

    public void PSSetConstantBuffers(uint startSlot, ID3D11Buffer[] constantBuffers)
    {
        PSSetConstantBuffers(startSlot, (uint)constantBuffers.Length, constantBuffers);
    }

    public void PSSetConstantBuffers(uint startSlot, uint count, ID3D11Buffer[] constantBuffers)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        PSSetConstantBuffers(startSlot, count, ppConstantBuffers);
    }

    public void PSUnsetSampler(uint slot)
    {
        void* nullSampler = default;
        PSSetSamplers(slot, 1, &nullSampler);
    }

    public void PSUnsetSamplers(uint startSlot, uint count)
    {
        fixed (void* nullSamplersPtr = s_NullSamplers)
        {
            PSSetSamplers(startSlot, count, nullSamplersPtr);
        }
    }

    public void PSSetSampler(uint slot, ID3D11SamplerState? sampler)
    {
        IntPtr nativePtr = sampler == null ? IntPtr.Zero : sampler.NativePointer;
        PSSetSamplers(slot, 1, &nativePtr);
    }

    public void PSSetSamplers(uint startSlot, ID3D11SamplerState[] samplers)
    {
        PSSetSamplers(startSlot, (uint)samplers.Length, samplers);
    }

    public void PSSetSamplers(uint startSlot, uint count, ID3D11SamplerState[] samplers)
    {
        IntPtr* ppSamplers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppSamplers[i] = (samplers[i] == null) ? IntPtr.Zero : samplers[i].NativePointer;
        }

        PSSetSamplers(startSlot, count, ppSamplers);
    }


    public void PSUnsetShaderResource(uint slot)
    {
        void* nullResource = default;
        PSSetShaderResources(slot, 1, &nullResource);
    }

    public void PSUnsetShaderResources(uint startSlot, uint count)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = IntPtr.Zero;
        }

        PSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public void PSSetShaderResource(uint slot, ID3D11ShaderResourceView shaderResourceView)
    {
        IntPtr nativePtr = shaderResourceView == null ? IntPtr.Zero : shaderResourceView.NativePointer;
        PSSetShaderResources(slot, 1, &nativePtr);
    }

    public void PSSetShaderResources(uint startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        PSSetShaderResources(startSlot, (uint)shaderResourceViews.Length, shaderResourceViews);
    }

    public void PSSetShaderResources(uint startSlot, uint count, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = (shaderResourceViews[i] == null) ? IntPtr.Zero : shaderResourceViews[i].NativePointer;
        }

        PSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public ID3D11PixelShader PSGetShader()
    {
        uint count = 0;
        PSGetShader(out ID3D11PixelShader shader, null, ref count);
        return shader;
    }

    public ID3D11PixelShader PSGetShader(ID3D11ClassInstance[] classInstances)
    {
        uint count = (uint)classInstances.Length;
        PSGetShader(out ID3D11PixelShader shader, classInstances, ref count);
        return shader;
    }

    public ID3D11PixelShader PSGetShader(ref uint classInstancesCount, ID3D11ClassInstance[] classInstances)
    {
        PSGetShader(out ID3D11PixelShader shader, classInstances, ref classInstancesCount);
        return shader;
    }

    public void PSGetConstantBuffers(uint startSlot, ID3D11Buffer[] constantBuffers)
    {
        PSGetConstantBuffers(startSlot, (uint)constantBuffers.Length, constantBuffers);
    }

    public void PSGetSamplers(uint startSlot, ID3D11SamplerState[] samplers)
    {
        PSGetSamplers(startSlot, (uint)samplers.Length, samplers);
    }

    public void PSGetShaderResources(uint startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        PSGetShaderResources(startSlot, (uint)shaderResourceViews.Length, shaderResourceViews);
    }
    #endregion

    #region DomainShader
    public void DSSetShader(ID3D11DomainShader? domainShader)
    {
        IntPtr shaderPtr = domainShader?.NativePointer ?? IntPtr.Zero;
        DSSetShader(shaderPtr, IntPtr.Zero, 0);
    }

    public void DSSetShader(ID3D11DomainShader? domainShader, ID3D11ClassInstance[] classInstances)
    {
        DSSetShader(domainShader, classInstances, (uint)classInstances.Length);
    }

    public void DSUnsetConstantBuffer(uint slot)
    {
        void* nullBuffer = default;
        DSSetConstantBuffers(slot, 1, &nullBuffer);
    }

    public void DSUnsetConstantBuffers(uint startSlot, uint count)
    {
        fixed (void* nullBuffersPtr = s_NullBuffers)
        {
            DSSetConstantBuffers(startSlot, count, nullBuffersPtr);
        }
    }

    public void DSSetConstantBuffer(uint slot, ID3D11Buffer? constantBuffer)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        DSSetConstantBuffers(slot, 1, &nativePtr);
    }

    public void DSSetConstantBuffers(uint startSlot, ID3D11Buffer[] constantBuffers)
    {
        DSSetConstantBuffers(startSlot, (uint)constantBuffers.Length, constantBuffers);
    }

    public void DSSetConstantBuffers(uint startSlot, uint count, ID3D11Buffer[] constantBuffers)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        DSSetConstantBuffers(startSlot, count, ppConstantBuffers);
    }

    public void DSUnsetSampler(uint slot)
    {
        void* nullSampler = default;
        DSSetSamplers(slot, 1, &nullSampler);
    }

    public void DSUnsetSamplers(uint startSlot, uint count)
    {
        fixed (void* nullSamplersPtr = s_NullSamplers)
        {
            DSSetSamplers(startSlot, count, nullSamplersPtr);
        }
    }

    public void DSSetSampler(uint slot, ID3D11SamplerState? sampler)
    {
        IntPtr nativePtr = sampler == null ? IntPtr.Zero : sampler.NativePointer;
        DSSetSamplers(slot, 1, &nativePtr);
    }

    public void DSSetSamplers(uint startSlot, ID3D11SamplerState[] samplers)
    {
        DSSetSamplers(startSlot, (uint)samplers.Length, samplers);
    }

    public void DSSetSamplers(uint startSlot, uint count, ID3D11SamplerState[] samplers)
    {
        IntPtr* ppSamplers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppSamplers[i] = (samplers[i] == null) ? IntPtr.Zero : samplers[i].NativePointer;
        }

        DSSetSamplers(startSlot, count, ppSamplers);
    }

    public void DSUnsetShaderResource(uint slot)
    {
        void* nullResource = default;
        DSSetShaderResources(slot, 1, &nullResource);
    }

    public void DSUnsetShaderResources(uint startSlot, uint count)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = IntPtr.Zero;
        }

        DSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public void DSSetShaderResource(uint slot, ID3D11ShaderResourceView? shaderResourceView)
    {
        IntPtr nativePtr = shaderResourceView == null ? IntPtr.Zero : shaderResourceView.NativePointer;
        DSSetShaderResources(slot, 1, &nativePtr);
    }

    public void DSSetShaderResources(uint startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        DSSetShaderResources(startSlot, (uint)shaderResourceViews.Length, shaderResourceViews);
    }

    public void DSSetShaderResources(uint startSlot, uint count, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = (shaderResourceViews[i] == null) ? IntPtr.Zero : shaderResourceViews[i].NativePointer;
        }

        DSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public ID3D11DomainShader DSGetShader()
    {
        uint count = 0;
        DSGetShader(out ID3D11DomainShader shader, null, ref count);
        return shader;
    }

    public ID3D11DomainShader DSGetShader(ID3D11ClassInstance[] classInstances)
    {
        uint count = (uint)classInstances.Length;
        DSGetShader(out ID3D11DomainShader shader, classInstances, ref count);
        return shader;
    }

    public ID3D11DomainShader DSGetShader(ref uint classInstancesCount, ID3D11ClassInstance[] classInstances)
    {
        DSGetShader(out ID3D11DomainShader shader, classInstances, ref classInstancesCount);
        return shader;
    }

    public void DSGetConstantBuffers(uint startSlot, ID3D11Buffer[] constantBuffers)
    {
        DSGetConstantBuffers(startSlot, (uint)constantBuffers.Length, constantBuffers);
    }

    public void DSGetSamplers(uint startSlot, ID3D11SamplerState[] samplers)
    {
        DSGetSamplers(startSlot, (uint)samplers.Length, samplers);
    }

    public void DSGetShaderResources(uint startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        DSGetShaderResources(startSlot, (uint)shaderResourceViews.Length, shaderResourceViews);
    }
    #endregion

    #region HullShader
    public void HSSetShader(ID3D11HullShader? hullShader)
    {
        IntPtr shaderPtr = hullShader?.NativePointer ?? IntPtr.Zero;
        HSSetShader(shaderPtr, IntPtr.Zero, 0);
    }

    public void HSSetShader(ID3D11HullShader? hullShader, ID3D11ClassInstance[] classInstances)
    {
        HSSetShader(hullShader, classInstances, (uint)classInstances.Length);
    }

    public void HSUnsetConstantBuffer(uint slot)
    {
        void* nullBuffer = default;
        HSSetConstantBuffers(slot, 1, &nullBuffer);
    }

    public void HSUnsetConstantBuffers(uint startSlot, uint count)
    {
        fixed (void* nullBuffersPtr = s_NullBuffers)
        {
            HSSetConstantBuffers(startSlot, count, nullBuffersPtr);
        }
    }

    public void HSSetConstantBuffer(uint slot, ID3D11Buffer? constantBuffer)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        HSSetConstantBuffers(slot, 1, &nativePtr);
    }

    public void HSSetConstantBuffers(uint startSlot, ID3D11Buffer[] constantBuffers)
    {
        HSSetConstantBuffers(startSlot, (uint)constantBuffers.Length, constantBuffers);
    }

    public void HSSetConstantBuffers(uint startSlot, uint count, ID3D11Buffer[] constantBuffers)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        HSSetConstantBuffers(startSlot, count, ppConstantBuffers);
    }

    public void HSUnsetSampler(uint slot)
    {
        void* nullSampler = default;
        HSSetSamplers(slot, 1, &nullSampler);
    }

    public void HSUnsetSamplers(uint startSlot, uint count)
    {
        fixed (void* nullSamplersPtr = s_NullSamplers)
        {
            HSSetSamplers(startSlot, count, nullSamplersPtr);
        }
    }

    public void HSSetSampler(uint slot, ID3D11SamplerState? sampler)
    {
        IntPtr nativePtr = sampler == null ? IntPtr.Zero : sampler.NativePointer;
        HSSetSamplers(slot, 1, &nativePtr);
    }

    public void HSSetSamplers(uint startSlot, ID3D11SamplerState[] samplers)
    {
        HSSetSamplers(startSlot, (uint)samplers.Length, samplers);
    }

    public void HSSetSamplers(uint startSlot, uint count, ID3D11SamplerState[] samplers)
    {
        IntPtr* ppSamplers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppSamplers[i] = (samplers[i] == null) ? IntPtr.Zero : samplers[i].NativePointer;
        }

        HSSetSamplers(startSlot, count, ppSamplers);
    }

    public void HSUnsetShaderResource(uint slot)
    {
        void* nullResource = default;
        HSSetShaderResources(slot, 1, &nullResource);
    }

    public void HSUnsetShaderResources(uint startSlot, uint count)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = IntPtr.Zero;
        }

        HSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public void HSSetShaderResource(uint slot, ID3D11ShaderResourceView? shaderResourceView)
    {
        IntPtr nativePtr = shaderResourceView == null ? IntPtr.Zero : shaderResourceView.NativePointer;
        HSSetShaderResources(slot, 1, &nativePtr);
    }

    public void HSSetShaderResources(uint startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        HSSetShaderResources(startSlot, (uint)shaderResourceViews.Length, shaderResourceViews);
    }

    public void HSSetShaderResources(uint startSlot, uint count, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = (shaderResourceViews[i] == null) ? IntPtr.Zero : shaderResourceViews[i].NativePointer;
        }

        HSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public ID3D11HullShader HSGetShader()
    {
        uint count = 0;
        HSGetShader(out ID3D11HullShader shader, null, ref count);
        return shader;
    }

    public ID3D11HullShader HSGetShader(ID3D11ClassInstance[] classInstances)
    {
        uint count = (uint)classInstances.Length;
        HSGetShader(out ID3D11HullShader shader, classInstances, ref count);
        return shader;
    }

    public ID3D11HullShader HSGetShader(ref uint classInstancesCount, ID3D11ClassInstance[] classInstances)
    {
        HSGetShader(out ID3D11HullShader shader, classInstances, ref classInstancesCount);
        return shader;
    }

    public void HSGetConstantBuffers(uint startSlot, ID3D11Buffer[] constantBuffers)
    {
        HSGetConstantBuffers(startSlot, (uint)constantBuffers.Length, constantBuffers);
    }

    public void HSGetSamplers(uint startSlot, ID3D11SamplerState[] samplers)
    {
        HSGetSamplers(startSlot, (uint)samplers.Length, samplers);
    }

    public void HSGetShaderResources(uint startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        HSGetShaderResources(startSlot, (uint)shaderResourceViews.Length, shaderResourceViews);
    }
    #endregion

    #region GeometryShader
    public void GSSetShader(ID3D11GeometryShader? geometryShader)
    {
        IntPtr shaderPtr = geometryShader?.NativePointer ?? IntPtr.Zero;
        GSSetShader(shaderPtr, IntPtr.Zero, 0);
    }

    public void GSSetShader(ID3D11GeometryShader? geometryShader, ID3D11ClassInstance[] classInstances)
    {
        GSSetShader(geometryShader, classInstances, (uint)classInstances.Length);
    }

    public void GSUnsetConstantBuffer(uint slot)
    {
        void* nullBuffer = default;
        GSSetConstantBuffers(slot, 1, &nullBuffer);
    }

    public void GSUnsetConstantBuffers(uint startSlot, uint count)
    {
        fixed (void* nullBuffersPtr = s_NullBuffers)
        {
            GSSetConstantBuffers(startSlot, count, nullBuffersPtr);
        }
    }

    public void GSSetConstantBuffer(uint slot, ID3D11Buffer? constantBuffer)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        GSSetConstantBuffers(slot, 1, &nativePtr);
    }

    public void GSSetConstantBuffers(uint startSlot, ID3D11Buffer[] constantBuffers)
    {
        GSSetConstantBuffers(startSlot, (uint)constantBuffers.Length, constantBuffers);
    }

    public void GSSetConstantBuffers(uint startSlot, uint count, ID3D11Buffer[] constantBuffers)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        GSSetConstantBuffers(startSlot, count, ppConstantBuffers);
    }

    public void GSUnsetSampler(uint slot)
    {
        void* nullSampler = default;
        GSSetSamplers(slot, 1, &nullSampler);
    }

    public void GSUnsetSamplers(uint startSlot, uint count)
    {
        fixed (void* nullSamplersPtr = s_NullSamplers)
        {
            GSSetSamplers(startSlot, count, nullSamplersPtr);
        }
    }

    public void GSSetSampler(uint slot, ID3D11SamplerState? sampler)
    {
        IntPtr nativePtr = sampler == null ? IntPtr.Zero : sampler.NativePointer;
        GSSetSamplers(slot, 1, &nativePtr);
    }

    public void GSSetSamplers(uint startSlot, ID3D11SamplerState[] samplers)
    {
        GSSetSamplers(startSlot, (uint)samplers.Length, samplers);
    }

    public void GSSetSamplers(uint startSlot, uint count, ID3D11SamplerState[] samplers)
    {
        IntPtr* ppSamplers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppSamplers[i] = (samplers[i] == null) ? IntPtr.Zero : samplers[i].NativePointer;
        }

        GSSetSamplers(startSlot, count, ppSamplers);
    }

    public void GSUnsetShaderResource(uint slot)
    {
        void* nullResource = default;
        GSSetShaderResources(slot, 1, &nullResource);
    }

    public void GSUnsetShaderResources(uint startSlot, uint count)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = IntPtr.Zero;
        }

        GSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public void GSSetShaderResource(uint slot, ID3D11ShaderResourceView? shaderResourceView)
    {
        IntPtr nativePtr = shaderResourceView == null ? IntPtr.Zero : shaderResourceView.NativePointer;
        GSSetShaderResources(slot, 1, &nativePtr);
    }

    public void GSSetShaderResources(uint startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        GSSetShaderResources(startSlot, (uint)shaderResourceViews.Length, shaderResourceViews);
    }

    public void GSSetShaderResources(uint startSlot, uint count, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = (shaderResourceViews[i] == null) ? IntPtr.Zero : shaderResourceViews[i].NativePointer;
        }

        GSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public ID3D11GeometryShader GSGetShader()
    {
        uint count = 0;
        GSGetShader(out ID3D11GeometryShader shader, null, ref count);
        return shader;
    }

    public ID3D11GeometryShader GSGetShader(ID3D11ClassInstance[] classInstances)
    {
        uint count = (uint)classInstances.Length;
        GSGetShader(out ID3D11GeometryShader shader, classInstances, ref count);
        return shader;
    }

    public ID3D11GeometryShader GSGetShader(ref uint classInstancesCount, ID3D11ClassInstance[] classInstances)
    {
        GSGetShader(out ID3D11GeometryShader shader, classInstances, ref classInstancesCount);
        return shader;
    }

    public void GSGetConstantBuffers(uint startSlot, ID3D11Buffer[] constantBuffers)
    {
        GSGetConstantBuffers(startSlot, (uint)constantBuffers.Length, constantBuffers);
    }

    public void GSGetSamplers(uint startSlot, ID3D11SamplerState[] samplers)
    {
        GSGetSamplers(startSlot, (uint)samplers.Length, samplers);
    }

    public void GSGetShaderResources(uint startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        GSGetShaderResources(startSlot, (uint)shaderResourceViews.Length, shaderResourceViews);
    }
    #endregion

    #region ComputeShader
    public void CSSetShader(ID3D11ComputeShader? computeShader)
    {
        IntPtr shaderPtr = computeShader?.NativePointer ?? IntPtr.Zero;
        CSSetShader(shaderPtr, IntPtr.Zero, 0);
    }

    public void CSSetShader(ID3D11ComputeShader? computeShader, ID3D11ClassInstance[] classInstances)
    {
        CSSetShader(computeShader, classInstances, (uint)classInstances.Length);
    }

    public void CSUnsetConstantBuffer(uint slot)
    {
        void* nullBuffer = default;
        CSSetConstantBuffers(slot, 1, &nullBuffer);
    }

    public void CSUnsetConstantBuffers(uint startSlot, uint count)
    {
        fixed (void* nullBuffersPtr = s_NullBuffers)
        {
            CSSetConstantBuffers(startSlot, count, nullBuffersPtr);
        }
    }

    public void CSSetConstantBuffer(uint slot, ID3D11Buffer? constantBuffer)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        CSSetConstantBuffers(slot, 1, &nativePtr);
    }

    public void CSSetConstantBuffers(uint startSlot, Span<ID3D11Buffer> constantBuffers)
    {
        CSSetConstantBuffers(startSlot, (uint)constantBuffers.Length, constantBuffers);
    }

    public void CSSetConstantBuffers(uint startSlot, uint count, Span<ID3D11Buffer> constantBuffers)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        CSSetConstantBuffers(startSlot, count, ppConstantBuffers);
    }

    public void CSUnsetSampler(uint slot)
    {
        void* nullSampler = default;
        CSSetSamplers(slot, 1, &nullSampler);
    }

    public void CSUnsetSamplers(uint startSlot, uint count)
    {
        fixed (void* nullSamplersPtr = s_NullSamplers)
        {
            CSSetSamplers(startSlot, count, nullSamplersPtr);
        }
    }

    public void CSSetSampler(uint slot, ID3D11SamplerState? sampler)
    {
        IntPtr nativePtr = sampler == null ? IntPtr.Zero : sampler.NativePointer;
        CSSetSamplers(slot, 1, &nativePtr);
    }

    public void CSSetSamplers(uint startSlot, Span<ID3D11SamplerState> samplers)
    {
        CSSetSamplers(startSlot, (uint)samplers.Length, samplers);
    }

    public void CSSetSamplers(uint startSlot, uint count, Span<ID3D11SamplerState> samplers)
    {
        IntPtr* ppSamplers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppSamplers[i] = (samplers[i] == null) ? IntPtr.Zero : samplers[i].NativePointer;
        }

        CSSetSamplers(startSlot, count, ppSamplers);
    }

    public void CSUnsetShaderResource(uint slot)
    {
        void* nullResource = default;
        CSSetShaderResources(slot, 1, &nullResource);
    }

    public void CSUnsetShaderResources(uint startSlot, uint count)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = IntPtr.Zero;
        }

        CSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public void CSSetShaderResource(uint slot, ID3D11ShaderResourceView? shaderResourceView)
    {
        IntPtr nativePtr = shaderResourceView == null ? IntPtr.Zero : shaderResourceView.NativePointer;
        CSSetShaderResources(slot, 1, &nativePtr);
    }

    public void CSSetShaderResources(uint startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        CSSetShaderResources(startSlot, (uint)shaderResourceViews.Length, shaderResourceViews);
    }

    public void CSSetShaderResources(uint startSlot, uint count, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = (shaderResourceViews[i] == null) ? IntPtr.Zero : shaderResourceViews[i].NativePointer;
        }

        CSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public ID3D11ComputeShader CSGetShader()
    {
        uint count = 0;
        CSGetShader(out ID3D11ComputeShader shader, null, ref count);
        return shader;
    }

    public ID3D11ComputeShader CSGetShader(ID3D11ClassInstance[] classInstances)
    {
        uint count = (uint)classInstances.Length;
        CSGetShader(out ID3D11ComputeShader shader, classInstances, ref count);
        return shader;
    }

    public ID3D11ComputeShader CSGetShader(ref uint classInstancesCount, ID3D11ClassInstance[] classInstances)
    {
        CSGetShader(out ID3D11ComputeShader shader, classInstances, ref classInstancesCount);
        return shader;
    }

    public void CSGetConstantBuffers(uint startSlot, ID3D11Buffer[] constantBuffers)
    {
        CSGetConstantBuffers(startSlot, (uint)constantBuffers.Length, constantBuffers);
    }

    public void CSGetSamplers(uint startSlot, ID3D11SamplerState[] samplers)
    {
        CSGetSamplers(startSlot, (uint)samplers.Length, samplers);
    }

    public void CSGetShaderResources(uint startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        CSGetShaderResources(startSlot, (uint)shaderResourceViews.Length, shaderResourceViews);
    }

    public void CSSetUnorderedAccessView(uint slot, ID3D11UnorderedAccessView? unorderedAccessView, uint uavInitialCount = KeepUnorderedAccessViews)
    {
        IntPtr nativePtr = unorderedAccessView == null ? IntPtr.Zero : unorderedAccessView.NativePointer;
        CSSetUnorderedAccessViews(slot, 1, &nativePtr, &uavInitialCount);
    }

    public void CSSetUnorderedAccessViews(uint startSlot, ID3D11UnorderedAccessView[] unorderedAccessViews)
    {
        int numUAVs = unorderedAccessViews.Length;
        IntPtr* ppUnorderedAccessViews = stackalloc IntPtr[numUAVs];
        uint* pUAVInitialCounts = stackalloc uint[numUAVs];

        for (uint i = 0; i < numUAVs; i++)
        {
            ppUnorderedAccessViews[i] = (unorderedAccessViews[i] == null) ? IntPtr.Zero : unorderedAccessViews[i].NativePointer;
            pUAVInitialCounts[i] = KeepUnorderedAccessViews;
        }

        CSSetUnorderedAccessViews(startSlot, (uint)numUAVs, ppUnorderedAccessViews, pUAVInitialCounts);
    }

    public void CSSetUnorderedAccessViews(uint startSlot, uint numUAVs, ID3D11UnorderedAccessView[] unorderedAccessViews)
    {
        IntPtr* ppUnorderedAccessViews = stackalloc IntPtr[(int)numUAVs];
        uint* pUAVInitialCounts = stackalloc uint[(int)numUAVs];

        for (int i = 0; i < numUAVs; i++)
        {
            ppUnorderedAccessViews[i] = (unorderedAccessViews[i] == null) ? IntPtr.Zero : unorderedAccessViews[i].NativePointer;
            pUAVInitialCounts[i] = unchecked((uint)-1);
        }

        CSSetUnorderedAccessViews(startSlot, numUAVs, ppUnorderedAccessViews, pUAVInitialCounts);
    }

    public void CSSetUnorderedAccessViews(uint startSlot, ID3D11UnorderedAccessView[] unorderedAccessViews, uint[] uavInitialCounts)
    {
        int numUAVs = unorderedAccessViews.Length;
        IntPtr* ppUnorderedAccessViews = stackalloc IntPtr[numUAVs];
        for (int i = 0; i < numUAVs; i++)
        {
            ppUnorderedAccessViews[i] = (unorderedAccessViews[i] == null) ? IntPtr.Zero : unorderedAccessViews[i].NativePointer;
        }

        fixed (uint* pUAVInitialCounts = uavInitialCounts)
        {
            CSSetUnorderedAccessViews(startSlot, (uint)numUAVs, ppUnorderedAccessViews, pUAVInitialCounts);
        }
    }

    public void CSSetUnorderedAccessViews(uint startSlot, uint numUAVs, ID3D11UnorderedAccessView[] unorderedAccessViews, uint[] uavInitialCounts)
    {
        IntPtr* ppUnorderedAccessViews = stackalloc IntPtr[(int)numUAVs];
        for (int i = 0; i < numUAVs; i++)
        {
            ppUnorderedAccessViews[i] = (unorderedAccessViews[i] == null) ? IntPtr.Zero : unorderedAccessViews[i].NativePointer;
        }

        fixed (uint* pUAVInitialCounts = uavInitialCounts)
        {
            CSSetUnorderedAccessViews(startSlot, numUAVs, ppUnorderedAccessViews, pUAVInitialCounts);
        }
    }

    public void CSUnsetUnorderedAccessView(uint slot, uint uavInitialCount = unchecked((uint)-1))
    {
        void* nullUAV = default;
        CSSetUnorderedAccessViews(slot, 1, &nullUAV, &uavInitialCount);
    }

    public void CSUnsetUnorderedAccessViews(uint startSlot, uint count, uint uavInitialCount = unchecked((uint)-1))
    {
        fixed (void* nullUAVsPtr = s_NullUAVs)
        {
            CSSetUnorderedAccessViews(startSlot, count, nullUAVsPtr, &uavInitialCount);
        }
    }

    #endregion

    #region Map/Unmap
    /// <summary>
    /// Maps the data contained in a subresource to a memory pointer, and denies the GPU access to that subresource.
    /// </summary>
    /// <param name="resource">The resource.</param>
    /// <param name="mode">The mode.</param>
    /// <param name="flags">The flags.</param>
    public MappedSubresource Map(ID3D11Buffer resource, MapMode mode, MapFlags flags = MapFlags.None)
    {
        Map(resource, 0, mode, flags, out MappedSubresource mappedSubresource).CheckError();
        return mappedSubresource;
    }

    public MappedSubresource Map(ID3D11Resource resource, uint subresource, MapMode mode = MapMode.Read, MapFlags flags = MapFlags.None)
    {
        Map(resource, subresource, mode, flags, out MappedSubresource mappedSubresource).CheckError();
        return mappedSubresource;
    }

    /// <summary>
    /// Maps the data contained in a subresource to a memory pointer, and denies the GPU access to that subresource.
    /// </summary>
    /// <param name="resource">The resource.</param>
    /// <param name="mipSlice">The mip slice.</param>
    /// <param name="arraySlice">The array slice.</param>
    /// <param name="mode">The mode.</param>
    /// <param name="flags">The flags.</param>
    /// <param name="subresource">The mapped subresource index.</param>
    /// <param name="mipSize">Size of the selected miplevel.</param>
    public MappedSubresource Map(ID3D11Resource resource, uint mipSlice, uint arraySlice, MapMode mode, MapFlags flags, out uint subresource, out uint mipSize)
    {
        subresource = resource.CalculateSubResourceIndex(mipSlice, arraySlice, out mipSize);
        Map(resource, subresource, mode, flags, out MappedSubresource mappedSubresource).CheckError();
        return mappedSubresource;
    }

    /// <summary>
    /// Maps the data contained in a subresource to a memory pointer, and denies the GPU access to that subresource.
    /// </summary>
    /// <param name="resource">The resource.</param>
    /// <param name="mipSlice">The mip slice.</param>
    /// <param name="arraySlice">The array slice.</param>
    /// <param name="mode">The mode.</param>
    /// <param name="flags">The flags.</param>
    /// <param name="mipSize">Size of the selected miplevel.</param>
    /// <param name="mappedSubresource"></param>
    public Result Map(ID3D11Resource resource, uint mipSlice, uint arraySlice, MapMode mode, MapFlags flags, out uint mipSize, out MappedSubresource mappedSubresource)
    {
        uint subresource = resource.CalculateSubResourceIndex(mipSlice, arraySlice, out mipSize);
        return Map(resource, subresource, mode, flags, out mappedSubresource);
    }

    public Span<T> Map<T>(ID3D11Texture1D resource, uint mipSlice, uint arraySlice, MapMode mode = MapMode.Read, MapFlags flags = MapFlags.None) where T : unmanaged
    {
        Texture1DDescription description = resource.GetDescription();
        uint subresource = D3D11.CalculateSubResourceIndex(mipSlice, arraySlice, description.MipLevels);
        Map(resource, subresource, mode, flags, out MappedSubresource mappedSubresource).CheckError();
        Span<byte> source = new(mappedSubresource.DataPointer.ToPointer(), (int)mappedSubresource.RowPitch);
        return MemoryMarshal.Cast<byte, T>(source);
    }

    public Span<T> Map<T>(ID3D11Texture2D resource, uint mipSlice, uint arraySlice, MapMode mode = MapMode.Read, MapFlags flags = MapFlags.None) where T : unmanaged
    {
        uint subresource = resource.CalculateSubResourceIndex(mipSlice, arraySlice, out uint mipSize);
        Map(resource, subresource, mode, flags, out MappedSubresource mappedSubresource).CheckError();
        Span<byte> source = new(mappedSubresource.DataPointer.ToPointer(), (int)(mipSize * mappedSubresource.RowPitch));
        return MemoryMarshal.Cast<byte, T>(source);
    }

    public Span<T> Map<T>(ID3D11Texture3D resource, uint mipSlice, uint arraySlice, MapMode mode = MapMode.Read, MapFlags flags = MapFlags.None) where T : unmanaged
    {
        uint subresource = resource.CalculateSubResourceIndex(mipSlice, arraySlice, out uint mipSize);
        Map(resource, subresource, mode, flags, out MappedSubresource mappedSubresource).CheckError();
        Span<byte> source = new(mappedSubresource.DataPointer.ToPointer(), (int)(mipSize * mappedSubresource.DepthPitch));
        return MemoryMarshal.Cast<byte, T>(source);
    }

    public ReadOnlySpan<T> MapReadOnly<T>(ID3D11Resource resource, uint mipSlice = 0, uint arraySlice = 0, MapFlags flags = MapFlags.None) where T : unmanaged
    {
        uint subresource = resource.CalculateSubResourceIndex(mipSlice, arraySlice, out uint mipSize);
        Map(resource, subresource, MapMode.Read, flags, out MappedSubresource mappedSubresource).CheckError();
        ReadOnlySpan<byte> source = new(mappedSubresource.DataPointer.ToPointer(), (int)(mipSize * mappedSubresource.RowPitch));
        return MemoryMarshal.Cast<byte, T>(source);
    }

    public void Unmap(ID3D11Buffer buffer) => Unmap(buffer, 0);

    public void Unmap(ID3D11Resource resource, uint mipSlice, uint arraySlice)
    {
        uint subresource = resource.CalculateSubResourceIndex(mipSlice, arraySlice, out _);
        Unmap(resource, subresource);
    }
    #endregion

    #region UpdateSubresource
    /// <summary>
    /// Copies data from the CPU to to a non-mappable subresource region.
    /// </summary>
    /// <typeparam name="T">Type of the data to upload</typeparam>
    /// <param name="value">A reference to the data to upload.</param>
    /// <param name="resource">The destination resource.</param>
    /// <param name="subresource">The destination subresource.</param>
    /// <param name="rowPitch">The row pitch.</param>
    /// <param name="depthPitch">The depth pitch.</param>
    /// <param name="region">The region</param>
    public void UpdateSubresource<T>(in T value, ID3D11Resource resource, uint subresource = 0, uint rowPitch = 0, uint depthPitch = 0, Box? region = null) where T : unmanaged
    {
        fixed (T* valuePtr = &value)
        {
            UpdateSubresource(resource, subresource, region, (IntPtr)valuePtr, rowPitch, depthPitch);
        }
    }

    /// <summary>
    /// Copies data from the CPU to to a non-mappable subresource region.
    /// </summary>
    /// <typeparam name="T">Type of the data to upload</typeparam>
    /// <param name="data">A reference to the data to upload.</param>
    /// <param name="resource">The destination resource.</param>
    /// <param name="subresource">The destination subresource.</param>
    /// <param name="rowPitch">The row pitch.</param>
    /// <param name="depthPitch">The depth pitch.</param>
    /// <param name="region">A region that defines the portion of the destination subresource to copy the resource data into. Coordinates are in bytes for buffers and in texels for textures.</param>
    public void UpdateSubresource<T>(T[] data, ID3D11Resource resource, uint subresource = 0, uint rowPitch = 0, uint depthPitch = 0, Box? region = null) where T : unmanaged
    {
        fixed (T* dataPtr = data)
        {
            UpdateSubresource(resource, subresource, region, (IntPtr)dataPtr, rowPitch, depthPitch);
        }
    }

    /// <summary>
    /// Copies data from the CPU to to a non-mappable subresource region.
    /// </summary>
    /// <typeparam name="T">Type of the data to upload</typeparam>
    /// <param name="data">A reference to the data to upload.</param>
    /// <param name="resource">The destination resource.</param>
    /// <param name="subresource">The destination subresource.</param>
    /// <param name="rowPitch">The row pitch.</param>
    /// <param name="depthPitch">The depth pitch.</param>
    /// <param name="region">A region that defines the portion of the destination subresource to copy the resource data into. Coordinates are in bytes for buffers and in texels for textures.</param>
    public void UpdateSubresource<T>(Span<T> data, ID3D11Resource resource, uint subresource = 0, uint rowPitch = 0, uint depthPitch = 0, Box? region = null) where T : unmanaged
    {
        fixed (T* dataPtr = data)
        {
            UpdateSubresource(resource, subresource, region, (IntPtr)dataPtr, rowPitch, depthPitch);
        }
    }

    /// <summary>
    /// Copies data from the CPU to to a non-mappable subresource region.
    /// </summary>
    /// <typeparam name="T">Type of the data to upload</typeparam>
    /// <param name="data">A reference to the data to upload.</param>
    /// <param name="resource">The destination resource.</param>
    /// <param name="subresource">The destination subresource.</param>
    /// <param name="rowPitch">The row pitch.</param>
    /// <param name="depthPitch">The depth pitch.</param>
    /// <param name="region">A region that defines the portion of the destination subresource to copy the resource data into. Coordinates are in bytes for buffers and in texels for textures.</param>
    public void UpdateSubresource<T>(ReadOnlySpan<T> data, ID3D11Resource resource, uint subresource = 0, uint rowPitch = 0, uint depthPitch = 0, Box? region = null) where T : unmanaged
    {
        fixed (T* dataPtr = data)
        {
            UpdateSubresource(resource, subresource, region, (IntPtr)dataPtr, rowPitch, depthPitch);
        }
    }

    public void WriteTexture<T>(ID3D11Texture1D resource, uint arraySlice, uint mipLevel, T[] data) where T : unmanaged
    {
        ReadOnlySpan<T> span = data.AsSpan();
        WriteTexture(resource, arraySlice, mipLevel, span);
    }

    public void WriteTexture<T>(ID3D11Texture1D resource, uint arraySlice, uint mipLevel, Span<T> data) where T : unmanaged
    {
        Texture1DDescription description = resource.Description;
        fixed (T* dataPtr = data)
        {
            uint subresource = D3D11.CalculateSubResourceIndex(mipLevel, arraySlice, description.MipLevels);
            DXGI.FormatHelper.GetSurfaceInfo(
                description.Format,
                description.GetWidth(mipLevel),
                1,
                out uint rowPitch,
                out uint slicePitch);

            UpdateSubresource(resource, subresource, null, (IntPtr)dataPtr, rowPitch, slicePitch);
        }
    }

    public void WriteTexture<T>(ID3D11Texture1D resource, uint arraySlice, uint mipLevel, ReadOnlySpan<T> data) where T : unmanaged
    {
        Texture1DDescription description = resource.Description;
        fixed (T* dataPtr = data)
        {
            uint subresource = D3D11.CalculateSubResourceIndex(mipLevel, arraySlice, description.MipLevels);
            DXGI.FormatHelper.GetSurfaceInfo(
                description.Format,
                description.GetWidth(mipLevel),
                1,
                out uint rowPitch,
                out uint slicePitch);

            UpdateSubresource(resource, subresource, null, (IntPtr)dataPtr, rowPitch, slicePitch);
        }
    }

    public void WriteTexture<T>(ID3D11Texture2D resource, uint arraySlice, uint mipLevel, T[] data) where T : unmanaged
    {
        ReadOnlySpan<T> span = data.AsSpan();
        WriteTexture(resource, arraySlice, mipLevel, span);
    }

    public void WriteTexture<T>(ID3D11Texture2D resource, uint arraySlice, uint mipLevel, Span<T> data) where T : unmanaged
    {
        Texture2DDescription description = resource.Description;
        fixed (T* dataPtr = data)
        {
            uint subresource = D3D11.CalculateSubResourceIndex(mipLevel, arraySlice, description.MipLevels);
            DXGI.FormatHelper.GetSurfaceInfo(
                description.Format,
                description.GetWidth(mipLevel),
                description.GetHeight(mipLevel),
                out uint rowPitch,
                out uint slicePitch);

            UpdateSubresource(resource, subresource, null, (IntPtr)dataPtr, rowPitch, slicePitch);
        }
    }

    public void WriteTexture<T>(ID3D11Texture2D resource, uint arraySlice, uint mipLevel, ReadOnlySpan<T> data) where T : unmanaged
    {
        Texture2DDescription description = resource.Description;
        fixed (T* dataPtr = data)
        {
            uint subresource = D3D11.CalculateSubResourceIndex(mipLevel, arraySlice, description.MipLevels);
            DXGI.FormatHelper.GetSurfaceInfo(
                description.Format,
                description.GetWidth(mipLevel),
                description.GetHeight(mipLevel),
                out uint rowPitch,
                out uint slicePitch);

            UpdateSubresource(resource, subresource, null, (IntPtr)dataPtr, rowPitch, slicePitch);
        }
    }

    public unsafe void WriteTexture<T>(ID3D11Texture2D resource, uint arraySlice, uint mipLevel, ref T data) where T : unmanaged
    {
        Texture2DDescription description = resource.Description;
        fixed (T* dataPtr = &data)
        {
            uint subresource = D3D11.CalculateSubResourceIndex(mipLevel, arraySlice, description.MipLevels);
            DXGI.FormatHelper.GetSurfaceInfo(
                description.Format,
                description.GetWidth(mipLevel),
                description.GetHeight(mipLevel),
                out uint rowPitch,
                out uint slicePitch);

            UpdateSubresource(resource, subresource, null, (IntPtr)dataPtr, rowPitch, slicePitch);
        }
    }

    /// <summary>
    /// Copies data from the CPU to to a non-mappable subresource region.
    /// </summary>
    /// <param name="source">The source data.</param>
    /// <param name="resource">The destination resource.</param>
    /// <param name="subresource">The destination subresource.</param>
    public void UpdateSubresource(MappedSubresource source, ID3D11Resource resource, uint subresource = 0)
    {
        UpdateSubresource(resource, subresource, null, source.DataPointer, source.RowPitch, source.DepthPitch);
    }

    /// <summary>
    /// Copies data from the CPU to to a non-mappable subresource region.
    /// </summary>
    /// <param name="source">The source data.</param>
    /// <param name="resource">The destination resource.</param>
    /// <param name="subresource">The destination subresource.</param>
    /// <param name="region">The destination region within the resource.</param>
    public void UpdateSubresource(MappedSubresource source, ID3D11Resource resource, uint subresource, Box region)
    {
        UpdateSubresource(resource, subresource, region, source.DataPointer, source.RowPitch, source.DepthPitch);
    }

    /// <summary>
    /// Copies data from the CPU to to a non-mappable subresource region.
    /// </summary>
    /// <typeparam name="T">Type of the data to upload</typeparam>
    /// <param name="value">A reference to the data to upload.</param>
    /// <param name="resource">The destination resource.</param>
    /// <param name="srcBytesPerElement">The size in bytes per pixel/block element.</param>
    /// <param name="subresource">The destination subresource.</param>
    /// <param name="rowPitch">The row pitch.</param>
    /// <param name="depthPitch">The depth pitch.</param>
    /// <param name="isCompressedResource">if set to <c>true</c> the resource is a block/compressed resource</param>
    /// <remarks>
    /// This method is implementing the <a href="http://blogs.msdn.com/b/chuckw/archive/2010/07/28/known-issue-direct3d-11-updatesubresource-and-deferred-contexts.aspx">workaround for deferred context</a>.
    /// </remarks>
    public void UpdateSubresourceSafe<T>(ref T value, ID3D11Resource resource, uint srcBytesPerElement, uint subresource = 0, uint rowPitch = 0, uint depthPitch = 0, bool isCompressedResource = false) where T : unmanaged
    {
        fixed (T* valuePtr = &value)
        {
            UpdateSubresourceSafe(resource, subresource, null, (IntPtr)valuePtr, rowPitch, depthPitch, srcBytesPerElement, isCompressedResource);
        }
    }

    /// <summary>
    /// Copies data from the CPU to to a non-mappable subresource region.
    /// </summary>
    /// <typeparam name="T">Type of the data to upload</typeparam>
    /// <param name="data">A reference to the data to upload.</param>
    /// <param name="resource">The destination resource.</param>
    /// <param name="srcBytesPerElement">The size in bytes per pixel/block element.</param>
    /// <param name="subresource">The destination subresource.</param>
    /// <param name="rowPitch">The row pitch.</param>
    /// <param name="depthPitch">The depth pitch.</param>
    /// <param name="isCompressedResource">if set to <c>true</c> the resource is a block/compressed resource</param>
    /// <remarks>
    /// This method is implementing the <a href="http://blogs.msdn.com/b/chuckw/archive/2010/07/28/known-issue-direct3d-11-updatesubresource-and-deferred-contexts.aspx">workaround for deferred context</a>.
    /// </remarks>
    public void UpdateSubresourceSafe<T>(T[] data, ID3D11Resource resource, uint srcBytesPerElement, uint subresource = 0, uint rowPitch = 0, uint depthPitch = 0, bool isCompressedResource = false) where T : unmanaged
    {
        unsafe
        {
            fixed (void* dataPtr = &data[0])
            {
                UpdateSubresourceSafe(resource, subresource, null, (IntPtr)dataPtr, rowPitch, depthPitch, srcBytesPerElement, isCompressedResource);
            }
        }
    }

    /// <summary>
    /// Copies data from the CPU to to a non-mappable subresource region.
    /// </summary>
    /// <typeparam name="T">Type of the data to upload</typeparam>
    /// <param name="data">A reference to the data to upload.</param>
    /// <param name="resource">The destination resource.</param>
    /// <param name="srcBytesPerElement">The size in bytes per pixel/block element.</param>
    /// <param name="subresource">The destination subresource.</param>
    /// <param name="rowPitch">The row pitch.</param>
    /// <param name="depthPitch">The depth pitch.</param>
    /// <param name="isCompressedResource">if set to <c>true</c> the resource is a block/compressed resource</param>
    /// <remarks>
    /// This method is implementing the <a href="http://blogs.msdn.com/b/chuckw/archive/2010/07/28/known-issue-direct3d-11-updatesubresource-and-deferred-contexts.aspx">workaround for deferred context</a>.
    /// </remarks>
    public unsafe void UpdateSubresourceSafe<T>(Span<T> data, ID3D11Resource resource, uint srcBytesPerElement, uint subresource = 0, uint rowPitch = 0, uint depthPitch = 0, bool isCompressedResource = false) where T : unmanaged
    {
        fixed (void* dataPtr = data)
        {
            UpdateSubresourceSafe(resource, subresource, null, (IntPtr)dataPtr, rowPitch, depthPitch, srcBytesPerElement, isCompressedResource);
        }
    }

    /// <summary>
    ///   Copies data from the CPU to to a non-mappable subresource region.
    /// </summary>
    /// <param name = "source">The source data.</param>
    /// <param name = "resource">The destination resource.</param>
    /// <param name="srcBytesPerElement">The size in bytes per pixel/block element.</param>
    /// <param name = "subresource">The destination subresource.</param>
    /// <param name="isCompressedResource">if set to <c>true</c> the resource is a block/compressed resource</param>
    /// <remarks>
    /// This method is implementing the <a href="http://blogs.msdn.com/b/chuckw/archive/2010/07/28/known-issue-direct3d-11-updatesubresource-and-deferred-contexts.aspx">workaround for deferred context</a>.
    /// </remarks>
    public void UpdateSubresourceSafe(MappedSubresource source, ID3D11Resource resource, uint srcBytesPerElement, uint subresource = 0, bool isCompressedResource = false)
    {
        UpdateSubresourceSafe(resource, subresource, null, source.DataPointer, source.RowPitch, source.DepthPitch, srcBytesPerElement, isCompressedResource);
    }

    /// <summary>
    /// Copies data from the CPU to to a non-mappable subresource region.
    /// </summary>
    /// <param name = "source">The source data.</param>
    /// <param name = "resource">The destination resource.</param>
    /// <param name="srcBytesPerElement">The size in bytes per pixel/block element.</param>
    /// <param name = "subresource">The destination subresource.</param>
    /// <param name = "region">The destination region within the resource.</param>
    /// <param name="isCompressedResource">if set to <c>true</c> the resource is a block/compressed resource</param>
    /// <remarks>
    /// This method is implementing the <a href="http://blogs.msdn.com/b/chuckw/archive/2010/07/28/known-issue-direct3d-11-updatesubresource-and-deferred-contexts.aspx">workaround for deferred context</a>.
    /// </remarks>
    public void UpdateSubresourceSafe(MappedSubresource source, ID3D11Resource resource, uint srcBytesPerElement, uint subresource, Box region, bool isCompressedResource = false)
    {
        UpdateSubresourceSafe(resource, subresource, region, source.DataPointer, source.RowPitch, source.DepthPitch, srcBytesPerElement, isCompressedResource);
    }

    internal unsafe bool UpdateSubresourceSafe(
        ID3D11Resource dstResource, uint dstSubresource, Box? dstBox,
        IntPtr pSrcData, uint srcRowPitch, uint srcDepthPitch, uint srcBytesPerElement, bool isCompressedResource)
    {

        bool needWorkaround = false;

        // Check thread support just once as it won't change during the life of this instance.
        if (!_supportsCommandLists.HasValue)
        {
            Device.CheckThreadingSupport(out var supportsConcurrentResources, out var supportsCommandLists);
            _supportsCommandLists = supportsCommandLists;
        }

        if (dstBox.HasValue)
        {
            if (ContextType == DeviceContextType.Deferred)
            {
                // If this deferred context doesn't support command list, we need to perform the workaround
                needWorkaround = !_supportsCommandLists.Value;
            }
        }

        // Adjust the pSrcData pointer if needed
        IntPtr pAdjustedSrcData = pSrcData;
        if (needWorkaround)
        {
            Box alignedBox = dstBox!.Value;

            // convert from pixels to blocks
            if (isCompressedResource)
            {
                alignedBox = new Box(alignedBox.Left / 4, alignedBox.Top / 4, alignedBox.Front, alignedBox.Right / 4, alignedBox.Bottom / 4, alignedBox.Back);
            }

            pAdjustedSrcData = (IntPtr)(((byte*)pSrcData) - (alignedBox.Front * srcDepthPitch) - (alignedBox.Top * srcRowPitch) - (alignedBox.Left * srcBytesPerElement));
        }

        UpdateSubresource(dstResource, dstSubresource, dstBox, pAdjustedSrcData, srcRowPitch, srcDepthPitch);

        return needWorkaround;
    }
    #endregion
}
