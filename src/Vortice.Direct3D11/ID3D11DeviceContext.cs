// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using Vortice.Mathematics;

namespace Vortice.Direct3D11;

public unsafe partial class ID3D11DeviceContext
{
    /// <summary>
    /// D3D11_KEEP_RENDER_TARGETS_AND_DEPTH_STENCIL
    /// </summary>
    public const int KeepRenderTargetsAndDepthStencil = -1;

    /// <summary>
    /// D3D11_KEEP_UNORDERED_ACCESS_VIEWS
    /// </summary>
    public const int KeepUnorderedAccessViews = -1;

    public const uint DefaultSampleMask = 0xffffffff;

    private static readonly void*[] s_NullBuffers = new void*[CommonShaderConstantBufferSlotCount]
    {
        null, null, null, null, null, null, null,
        null, null, null, null, null, null, null
    };

    private static readonly void*[] s_NullSamplers = new void*[CommonShaderSamplerSlotCount]
    {
        null, null, null, null, null, null, null, null,
        null, null, null, null, null, null, null, null
    };

    private static readonly void*[] s_NullUAVs = new void*[UnorderedAccessViewRegisterCount]
    {
        null, null, null,
        null, null, null,
        null, null
    };

    private static readonly int[] s_NegativeOnes = new int[UnorderedAccessViewRegisterCount]
    {
        KeepUnorderedAccessViews, KeepUnorderedAccessViews, KeepUnorderedAccessViews,
        KeepUnorderedAccessViews, KeepUnorderedAccessViews, KeepUnorderedAccessViews,
        KeepUnorderedAccessViews, KeepUnorderedAccessViews
    };

    private bool? _supportsCommandLists;

    public void ClearRenderTargetView(ID3D11RenderTargetView renderTargetView, in Color color)
    {
        ClearRenderTargetView(renderTargetView, new Color4(color));
    }

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

    public void OMSetBlendState(ID3D11BlendState? blendState, ReadOnlySpan<float> blendFactor)
    {
        IntPtr blendStatePtr = blendState?.NativePointer ?? IntPtr.Zero;
        fixed (float* blendFactorPtr = blendFactor)
        {
            ((delegate* unmanaged[Stdcall]<IntPtr, void*, float*, uint, void>)this[OMSetBlendState__vtbl_index])(NativePointer, (void*)blendStatePtr, blendFactorPtr, DefaultSampleMask);
        }
    }

    public void OMSetBlendState(ID3D11BlendState? blendState, in Color blendFactor)
    {
        OMSetBlendState(blendState, new Color4(blendFactor));
    }

    public void OMSetBlendState(ID3D11BlendState? blendState, Color4 blendFactor)
    {
        OMSetBlendState(blendState, (float*)&blendFactor, DefaultSampleMask);
    }

    public void OMSetBlendState(ID3D11BlendState? blendState, in Color blendFactor, uint sampleMask = DefaultSampleMask)
    {
        OMSetBlendState(blendState, new Color4(blendFactor), sampleMask);
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

    public void OMSetRenderTargets(int renderTargetViewsCount, ID3D11RenderTargetView[] renderTargetViews, ID3D11DepthStencilView? depthStencilView = default)
    {
        IntPtr* renderTargetViewsPtr = stackalloc IntPtr[renderTargetViewsCount];
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

        OMSetRenderTargets(renderTargetViews.Length, renderTargetViewsPtr, depthStencilView);
    }

    public void OMSetRenderTargets(ReadOnlySpan<ID3D11RenderTargetView> renderTargetViews, ID3D11DepthStencilView? depthStencilView = default)
    {
        IntPtr* renderTargetViewsPtr = stackalloc IntPtr[renderTargetViews.Length];
        for (int i = 0; i < renderTargetViews.Length; i++)
        {
            renderTargetViewsPtr[i] = (renderTargetViews[i] == null) ? IntPtr.Zero : renderTargetViews[i].NativePointer;
        }

        OMSetRenderTargets(renderTargetViews.Length, renderTargetViewsPtr, depthStencilView);
    }

    public void OMSetUnorderedAccessView(int startSlot, ID3D11UnorderedAccessView unorderedAccessView, int uavInitialCount = -1)
    {
        IntPtr unorderedAccessViewPtr = unorderedAccessView != null ? unorderedAccessView.NativePointer : IntPtr.Zero;

        OMSetRenderTargetsAndUnorderedAccessViews(KeepRenderTargetsAndDepthStencil, null, IntPtr.Zero,
            startSlot,
            1,
            &unorderedAccessViewPtr,
            &uavInitialCount
            );
    }

    public void OMUnsetUnorderedAccessView(int startSlot, int uavInitialCount = -1)
    {
        void* nullUAV = default;
        OMSetRenderTargetsAndUnorderedAccessViews(
            KeepRenderTargetsAndDepthStencil, null, IntPtr.Zero,
            startSlot, 1,
            &nullUAV,
            &uavInitialCount
            );
    }

    public void OMSetUnorderedAccessViews(int uavStartSlot, int unorderedAccessViewCount, ID3D11UnorderedAccessView[] unorderedAccessViews)
    {
        IntPtr* unorderedAccessViewsPtr = stackalloc IntPtr[unorderedAccessViewCount];
        for (int i = 0; i < unorderedAccessViewCount; i++)
        {
            unorderedAccessViewsPtr[i] = unorderedAccessViews[i].NativePointer;
        }

        fixed (int* negativeOnesPtr = &s_NegativeOnes[0])
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
        int startSlot,
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
            startSlot, unorderedAccessViews.Length,
            unorderedAccessViewsPtr,
            uavInitialCounts);
    }

    public void OMSetRenderTargetsAndUnorderedAccessViews(
        ID3D11RenderTargetView[] renderTargetViews,
        ID3D11DepthStencilView depthStencilView,
        int startSlot,
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

        OMSetRenderTargetsAndUnorderedAccessViews(renderTargetViews.Length,
            renderTargetViewsPtr,
            depthStencilView != null ? depthStencilView.NativePointer : IntPtr.Zero,
            startSlot, unorderedAccessViews.Length,
            unorderedAccessViewsPtr,
            uavInitialCounts);
    }

    public void OMSetRenderTargetsAndUnorderedAccessViews(
        ID3D11RenderTargetView[] renderTargetViews,
        ID3D11DepthStencilView depthStencilView,
        int uavStartSlot,
        ID3D11UnorderedAccessView[] unorderedAccessViews,
        int[] uavInitialCounts)
    {
        OMSetRenderTargetsAndUnorderedAccessViews(
            renderTargetViews.Length, renderTargetViews, depthStencilView,
            uavStartSlot, unorderedAccessViews.Length, unorderedAccessViews, uavInitialCounts
            );
    }

    public void OMSetRenderTargetsAndUnorderedAccessViews(
        int renderTargetViewsCount,
        ID3D11RenderTargetView[] renderTargetViews,
        ID3D11DepthStencilView depthStencilView,
        int startSlot,
        int unorderedAccessViewsCount,
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

        fixed (int* negativeOnesPtr = &s_NegativeOnes[0])
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
        int renderTargetViewsCount,
        ID3D11RenderTargetView[] renderTargetViews,
        ID3D11DepthStencilView depthStencilView,
        int startSlot,
        int unorderedAccessViewsCount,
        ID3D11UnorderedAccessView[] unorderedAccessViews,
        int[] uavInitialCounts)
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

        fixed (int* pUAVInitialCounts = &uavInitialCounts[0])
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
        var result = new DataStream(data.DataSize, true, true);
        GetData(data, result.BasePointer, (int)result.Length, flags);
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
            return GetData(data, (IntPtr)resultPtr, sizeof(T), flags) == Result.Ok;
        }
    }

    public bool GetData<T>(ID3D11Asynchronous data, out T result) where T : unmanaged
    {
        result = default;
        fixed (void* resultPtr = &result)
        {
            return GetData(data, (IntPtr)resultPtr, sizeof(T), AsyncGetDataFlags.None) == Result.Ok;
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
            RSSetViewports(viewports.Length, pViewports);
        }
    }

    public void RSSetViewports(int count, Viewport[] viewports)
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
            RSSetViewports(viewports.Length, viewportsPtr);
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
            RSSetViewports(viewports.Length, viewportsPtr);
        }
    }

    /// <summary>
    /// Get the number of bound viewports.
    /// </summary>
    /// <returns></returns>
    public int RSGetViewports()
    {
        int numViewports = 0;
        RSGetViewports(ref numViewports, (void*)null);
        return numViewports;
    }

    public Viewport RSGetViewport()
    {
        int numViewports = 1;
        Viewport viewport = default;
        RSGetViewports(ref numViewports, (void*)&viewport);
        return viewport;
    }

    public void RSGetViewport(ref Viewport viewport)
    {
        int numViewports = 1;
        fixed (Viewport* viewportPtr = &viewport)
        {
            RSGetViewports(ref numViewports, viewportPtr);
        }
    }

    public void RSGetViewports(Viewport[] viewports)
    {
        int numViewports = viewports.Length;
        fixed (Viewport* viewportsPtr = &viewports[0])
        {
            RSGetViewports(ref numViewports, (void*)viewportsPtr);
        }
    }

    public void RSGetViewports(Span<Viewport> viewports)
    {
        fixed (Viewport* viewportsPtr = &MemoryMarshal.GetReference(viewports))
        {
            int numViewports = viewports.Length;
            RSGetViewports(ref numViewports, (void*)viewportsPtr);
        }
    }

    public void RSGetViewports<T>(ref int count, T[] viewports) where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != sizeof(Viewport))
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(Viewport)}", nameof(viewports));
        }
#endif

        fixed (T* viewportsPtr = &viewports[0])
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
            int numViewports = viewports.Length;
            RSGetViewports(ref numViewports, viewportsPtr);
        }
    }

    /// <summary>
    /// Get the array of viewports bound  to the rasterizer stage.
    /// </summary>
    /// <typeparam name="T">An array of viewports,  must be size of <see cref="Viewport"/>.</typeparam>
    /// <param name="viewports"></param>
    public void RSGetViewports<T>(T[] viewports) where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != sizeof(Viewport))
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(Viewport)}", nameof(viewports));
        }
#endif

        int numViewports = viewports.Length;
        fixed (T* viewportsPtr = &viewports[0])
        {
            RSGetViewports(ref numViewports, viewportsPtr);
        }
    }

    /// <summary>	
    /// Get the array of viewports bound  to the rasterizer stage.	
    /// </summary>	
    /// <returns>An array of viewports, must be size of <see cref="Viewport"/></returns>
    public T[] RSGetViewports<T>() where T : unmanaged
    {
#if DEBUG
        if (sizeof(T) != sizeof(Viewport))
        {
            throw new ArgumentException($"Type T must have same size and layout as {nameof(Viewport)}");
        }
#endif
        int numViewports = 0;
        RSGetViewports(ref numViewports, (void*)null);
        T[] viewports = new T[numViewports];
        RSGetViewports(viewports);
        return viewports;
    }

    public void RSGetViewports(ref int count, Viewport[] viewports)
    {
        fixed (Viewport* viewportsPtr = &viewports[0])
        {
            RSGetViewports(ref count, (void*)viewportsPtr);
        }
    }

    public void RSGetViewports(ref int count, Span<Viewport> viewports)
    {
        fixed (Viewport* viewportsPtr = &MemoryMarshal.GetReference(viewports))
        {
            RSGetViewports(ref count, (void*)viewportsPtr);
        }
    }

    public void RSGetViewports(ref int count, Viewport* viewports)
    {
        RSGetViewports(ref count, (void*)viewports);
    }
    #endregion

    #region ScissorRect
    /// <summary>
    /// Get the number of bound scissor rectangles.
    /// </summary>
    /// <returns></returns>
    public int RSGetScissorRects()
    {
        int numRects = 0;
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
            RSSetScissorRects(rectangles.Length, pRects);
        }
    }

    public void RSSetScissorRects(int count, RawRect[] rectangles)
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
            RSSetScissorRects(rectangles.Length, pRects);
        }
    }

    public void RSSetScissorRects(int count, Span<RawRect> rectangles)
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
            RSSetScissorRects(rects.Length, rectPtr);
        }
    }

    public void RSSetScissorRects<T>(int numRects, T[] rects) where T : unmanaged
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
            RSSetScissorRects(rects.Length, rectsPtr);
        }
    }

    public RawRect RSGetScissorRect()
    {
        int numRects = 1;
        var rect = new RawRect();
        RSGetScissorRects(ref numRects, new IntPtr(&rect));
        return rect;
    }

    public void RSGetScissorRect(ref RawRect rect)
    {
        int numRects = 1;
        fixed (void* rectPtr = &rect)
        {
            RSGetScissorRects(ref numRects, (IntPtr)rectPtr);
        }
    }

    public void RSGetScissorRects(RawRect[] rects)
    {
        int numRects = rects.Length;
        fixed (void* rectsPtr = &rects[0])
        {
            RSGetScissorRects(ref numRects, (IntPtr)rectsPtr);
        }
    }

    public void RSGetScissorRects(Span<RawRect> rects)
    {
        fixed (RawRect* rectsPtr = &MemoryMarshal.GetReference(rects))
        {
            int numRects = rects.Length;
            RSGetScissorRects(ref numRects, (IntPtr)rectsPtr);
        }
    }

    public void RSGetScissorRects(ref int count, RawRect[] rects)
    {
        fixed (void* rectsPtr = &rects[0])
        {
            RSGetScissorRects(ref count, (IntPtr)rectsPtr);
        }
    }

    public void RSGetScissorRects(ref int count, RawRect* rects)
    {
        RSGetScissorRects(ref count, (IntPtr)rects);
    }
    #endregion

    /// <summary>
    /// Set the target output buffers for the stream-output stage of the pipeline.
    /// </summary>
    /// <param name="targets">The array of output buffers <see cref="ID3D11Buffer"/> to bind to the device. The buffers must have been created with the <see cref="BindFlags.StreamOutput"/> flag.</param>
    /// <param name="offsets">Array of offsets to the output buffers from targets, one offset for each buffer. The offset values must be in bytes.</param>
    public void SOSetTargets(ID3D11Buffer[] targets, int[]? offsets = null)
    {
        SOSetTargets(targets.Length, targets, offsets);
    }

    /// <summary>
    /// Set the target output buffers for the stream-output stage of the pipeline.
    /// </summary>
    /// <param name="buffersCount">The number of buffer to bind to the device. A maximum of four output buffers can be set. If less than four are defined by the call, the remaining buffer slots are set to null.</param>
    /// <param name="targets">The array of output buffers <see cref="ID3D11Buffer"/> to bind to the device. The buffers must have been created with the <see cref="BindFlags.StreamOutput"/> flag.</param>
    /// <param name="offsets">Array of offsets to the output buffers from targets, one offset for each buffer. The offset values must be in bytes.</param>
    public void SOSetTargets(int buffersCount, ID3D11Buffer[] targets, int[]? offsets = null)
    {
        IntPtr* targetsPtr = stackalloc IntPtr[buffersCount];
        for (int i = 0; i < buffersCount; i++)
        {
            targetsPtr[i] = targets[i] != null ? targets[i].NativePointer : IntPtr.Zero;
        }

        if (offsets != null && offsets.Length > 0)
        {
            fixed (int* offsetsPtr = &offsets[0])
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

    public void IASetVertexBuffer(int slot, ID3D11Buffer buffer, int stride, int offset = 0)
    {
        IntPtr pVertexBuffers = buffer == null ? IntPtr.Zero : buffer.NativePointer;
        IASetVertexBuffers(slot, 1, &pVertexBuffers, &stride, &offset);
    }
    public void IASetVertexBuffers(int firstSlot, ID3D11Buffer[] vertexBuffers, int[] strides, int[] offsets)
    {
        IASetVertexBuffers(firstSlot, vertexBuffers.Length, vertexBuffers, strides, offsets);
    }

    public void IASetVertexBuffers(int firstSlot, int vertexBufferViewsCount, ID3D11Buffer[] vertexBuffers, int[] strides, int[] offsets)
    {
        IntPtr* vertexBuffersPtr = stackalloc IntPtr[vertexBufferViewsCount];
        for (int i = 0; i < vertexBufferViewsCount; i++)
        {
            vertexBuffersPtr[i] = (vertexBuffers[i] == null) ? IntPtr.Zero : vertexBuffers[i].NativePointer;
        }

        fixed (int* pStrides = strides)
        {
            fixed (int* pOffsets = offsets)
            {
                IASetVertexBuffers(firstSlot, vertexBufferViewsCount, vertexBuffersPtr, pStrides, pOffsets);
            }
        }
    }

    public void IASetVertexBuffers(int firstSlot, int vertexBufferViewsCount, ID3D11Buffer[] vertexBuffers, Span<int> strides, Span<int> offsets)
    {
        IntPtr* vertexBuffersPtr = stackalloc IntPtr[vertexBufferViewsCount];
        for (int i = 0; i < vertexBufferViewsCount; i++)
        {
            vertexBuffersPtr[i] = (vertexBuffers[i] == null) ? IntPtr.Zero : vertexBuffers[i].NativePointer;
        }

        fixed (int* pStrides = strides)
        {
            fixed (int* pOffsets = offsets)
            {
                IASetVertexBuffers(firstSlot, vertexBufferViewsCount, vertexBuffersPtr, pStrides, pOffsets);
            }
        }
    }

    #region VertexShader
    public void VSSetShader(ID3D11VertexShader? vertexShader)
    {
        IntPtr shaderPtr = vertexShader?.NativePointer ?? IntPtr.Zero;
        VSSetShader(shaderPtr, IntPtr.Zero, 0);
    }

    public void VSSetShader(ID3D11VertexShader? vertexShader, ID3D11ClassInstance[] classInstances)
    {
        VSSetShader(vertexShader, classInstances, classInstances.Length);
    }

    public void VSUnsetConstantBuffer(int slot)
    {
        void* nullBuffer = default;
        VSSetConstantBuffers(slot, 1, &nullBuffer);
    }

    public void VSUnsetConstantBuffers(int startSlot, int count)
    {
        fixed (void* nullBuffersPtr = s_NullBuffers)
        {
            VSSetConstantBuffers(startSlot, count, nullBuffersPtr);
        }
    }

    public void VSSetConstantBuffer(int slot, ID3D11Buffer? constantBuffer)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        VSSetConstantBuffers(slot, 1, &nativePtr);
    }

    public void VSSetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
    {
        VSSetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
    }

    public void VSSetConstantBuffers(int startSlot, int count, ID3D11Buffer[] constantBuffers)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        VSSetConstantBuffers(startSlot, count, ppConstantBuffers);
    }

    public void VSUnsetSampler(int slot)
    {
        void* nullSampler = default;
        VSSetSamplers(slot, 1, &nullSampler);
    }

    public void VSUnsetSamplers(int startSlot, int count)
    {
        fixed (void* nullSamplersPtr = s_NullSamplers)
        {
            VSSetSamplers(startSlot, count, nullSamplersPtr);
        }
    }

    public void VSSetSampler(int slot, ID3D11SamplerState? sampler)
    {
        IntPtr nativePtr = sampler == null ? IntPtr.Zero : sampler.NativePointer;
        VSSetSamplers(slot, 1, &nativePtr);
    }

    public void VSSetSamplers(int startSlot, ID3D11SamplerState[] samplers)
    {
        VSSetSamplers(startSlot, samplers.Length, samplers);
    }

    public void VSSetSamplers(int startSlot, int count, ID3D11SamplerState[] samplers)
    {
        IntPtr* ppSamplers = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppSamplers[i] = (samplers[i] == null) ? IntPtr.Zero : samplers[i].NativePointer;
        }

        VSSetSamplers(startSlot, count, ppSamplers);
    }

    public void VSUnsetShaderResource(int slot)
    {
        void* nullResource = default;
        VSSetShaderResources(slot, 1, &nullResource);
    }

    public void VSUnsetShaderResources(int startSlot, int count)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = IntPtr.Zero;
        }

        VSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public void VSSetShaderResource(int slot, ID3D11ShaderResourceView? shaderResourceView)
    {
        IntPtr nativePtr = shaderResourceView == null ? IntPtr.Zero : shaderResourceView.NativePointer;
        VSSetShaderResources(slot, 1, &nativePtr);
    }

    public void VSSetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        VSSetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
    }

    public void VSSetShaderResources(int startSlot, int count, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = (shaderResourceViews[i] == null) ? IntPtr.Zero : shaderResourceViews[i].NativePointer;
        }

        VSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public ID3D11VertexShader VSGetShader()
    {
        int count = 0;
        VSGetShader(out ID3D11VertexShader shader, null, ref count);
        return shader;
    }

    public ID3D11VertexShader VSGetShader(ID3D11ClassInstance[] classInstances)
    {
        int count = classInstances.Length;
        VSGetShader(out ID3D11VertexShader shader, classInstances, ref count);
        return shader;
    }

    public ID3D11VertexShader VSGetShader(ref int classInstancesCount, ID3D11ClassInstance[] classInstances)
    {
        VSGetShader(out ID3D11VertexShader shader, classInstances, ref classInstancesCount);
        return shader;
    }

    public void VSGetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
    {
        VSGetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
    }

    public void VSGetSamplers(int startSlot, ID3D11SamplerState[] samplers)
    {
        VSGetSamplers(startSlot, samplers.Length, samplers);
    }

    public void VSGetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        VSGetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
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
        PSSetShader(pixelShader, classInstances, classInstances.Length);
    }

    public void PSUnsetConstantBuffer(int slot)
    {
        void* nullBuffer = default;
        PSSetConstantBuffers(slot, 1, &nullBuffer);
    }

    public void PSUnsetConstantBuffers(int startSlot, int count)
    {
        fixed (void* nullBuffersPtr = s_NullBuffers)
        {
            PSSetConstantBuffers(startSlot, count, nullBuffersPtr);
        }
    }

    public void PSSetConstantBuffer(int slot, ID3D11Buffer? constantBuffer)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        PSSetConstantBuffers(slot, 1, &nativePtr);
    }

    public void PSSetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
    {
        PSSetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
    }

    public void PSSetConstantBuffers(int startSlot, int count, ID3D11Buffer[] constantBuffers)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        PSSetConstantBuffers(startSlot, count, ppConstantBuffers);
    }

    public void PSUnsetSampler(int slot)
    {
        void* nullSampler = default;
        PSSetSamplers(slot, 1, &nullSampler);
    }

    public void PSUnsetSamplers(int startSlot, int count)
    {
        fixed (void* nullSamplersPtr = s_NullSamplers)
        {
            PSSetSamplers(startSlot, count, nullSamplersPtr);
        }
    }

    public void PSSetSampler(int slot, ID3D11SamplerState? sampler)
    {
        IntPtr nativePtr = sampler == null ? IntPtr.Zero : sampler.NativePointer;
        PSSetSamplers(slot, 1, &nativePtr);
    }

    public void PSSetSamplers(int startSlot, ID3D11SamplerState[] samplers)
    {
        PSSetSamplers(startSlot, samplers.Length, samplers);
    }

    public void PSSetSamplers(int startSlot, int count, ID3D11SamplerState[] samplers)
    {
        IntPtr* ppSamplers = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppSamplers[i] = (samplers[i] == null) ? IntPtr.Zero : samplers[i].NativePointer;
        }

        PSSetSamplers(startSlot, count, ppSamplers);
    }


    public void PSUnsetShaderResource(int slot)
    {
        void* nullResource = default;
        PSSetShaderResources(slot, 1, &nullResource);
    }

    public void PSUnsetShaderResources(int startSlot, int count)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = IntPtr.Zero;
        }

        PSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public void PSSetShaderResource(int slot, ID3D11ShaderResourceView shaderResourceView)
    {
        IntPtr nativePtr = shaderResourceView == null ? IntPtr.Zero : shaderResourceView.NativePointer;
        PSSetShaderResources(slot, 1, &nativePtr);
    }

    public void PSSetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        PSSetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
    }

    public void PSSetShaderResources(int startSlot, int count, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = (shaderResourceViews[i] == null) ? IntPtr.Zero : shaderResourceViews[i].NativePointer;
        }

        PSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public ID3D11PixelShader PSGetShader()
    {
        int count = 0;
        PSGetShader(out ID3D11PixelShader shader, null, ref count);
        return shader;
    }

    public ID3D11PixelShader PSGetShader(ID3D11ClassInstance[] classInstances)
    {
        int count = classInstances.Length;
        PSGetShader(out ID3D11PixelShader shader, classInstances, ref count);
        return shader;
    }

    public ID3D11PixelShader PSGetShader(ref int classInstancesCount, ID3D11ClassInstance[] classInstances)
    {
        PSGetShader(out ID3D11PixelShader shader, classInstances, ref classInstancesCount);
        return shader;
    }

    public void PSGetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
    {
        PSGetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
    }

    public void PSGetSamplers(int startSlot, ID3D11SamplerState[] samplers)
    {
        PSGetSamplers(startSlot, samplers.Length, samplers);
    }

    public void PSGetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        PSGetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
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
        DSSetShader(domainShader, classInstances, classInstances.Length);
    }

    public void DSUnsetConstantBuffer(int slot)
    {
        void* nullBuffer = default;
        DSSetConstantBuffers(slot, 1, &nullBuffer);
    }

    public void DSUnsetConstantBuffers(int startSlot, int count)
    {
        fixed (void* nullBuffersPtr = s_NullBuffers)
        {
            DSSetConstantBuffers(startSlot, count, nullBuffersPtr);
        }
    }

    public void DSSetConstantBuffer(int slot, ID3D11Buffer? constantBuffer)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        DSSetConstantBuffers(slot, 1, &nativePtr);
    }

    public void DSSetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
    {
        DSSetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
    }

    public void DSSetConstantBuffers(int startSlot, int count, ID3D11Buffer[] constantBuffers)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        DSSetConstantBuffers(startSlot, count, ppConstantBuffers);
    }

    public void DSUnsetSampler(int slot)
    {
        void* nullSampler = default;
        DSSetSamplers(slot, 1, &nullSampler);
    }

    public void DSUnsetSamplers(int startSlot, int count)
    {
        fixed (void* nullSamplersPtr = s_NullSamplers)
        {
            DSSetSamplers(startSlot, count, nullSamplersPtr);
        }
    }

    public void DSSetSampler(int slot, ID3D11SamplerState? sampler)
    {
        IntPtr nativePtr = sampler == null ? IntPtr.Zero : sampler.NativePointer;
        DSSetSamplers(slot, 1, &nativePtr);
    }

    public void DSSetSamplers(int startSlot, ID3D11SamplerState[] samplers)
    {
        DSSetSamplers(startSlot, samplers.Length, samplers);
    }

    public void DSSetSamplers(int startSlot, int count, ID3D11SamplerState[] samplers)
    {
        IntPtr* ppSamplers = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppSamplers[i] = (samplers[i] == null) ? IntPtr.Zero : samplers[i].NativePointer;
        }

        DSSetSamplers(startSlot, count, ppSamplers);
    }

    public void DSUnsetShaderResource(int slot)
    {
        void* nullResource = default;
        DSSetShaderResources(slot, 1, &nullResource);
    }

    public void DSUnsetShaderResources(int startSlot, int count)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = IntPtr.Zero;
        }

        DSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public void DSSetShaderResource(int slot, ID3D11ShaderResourceView? shaderResourceView)
    {
        IntPtr nativePtr = shaderResourceView == null ? IntPtr.Zero : shaderResourceView.NativePointer;
        DSSetShaderResources(slot, 1, &nativePtr);
    }

    public void DSSetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        DSSetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
    }

    public void DSSetShaderResources(int startSlot, int count, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = (shaderResourceViews[i] == null) ? IntPtr.Zero : shaderResourceViews[i].NativePointer;
        }

        DSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public ID3D11DomainShader DSGetShader()
    {
        int count = 0;
        DSGetShader(out ID3D11DomainShader shader, null, ref count);
        return shader;
    }

    public ID3D11DomainShader DSGetShader(ID3D11ClassInstance[] classInstances)
    {
        int count = classInstances.Length;
        DSGetShader(out ID3D11DomainShader shader, classInstances, ref count);
        return shader;
    }

    public ID3D11DomainShader DSGetShader(ref int classInstancesCount, ID3D11ClassInstance[] classInstances)
    {
        DSGetShader(out ID3D11DomainShader shader, classInstances, ref classInstancesCount);
        return shader;
    }

    public void DSGetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
    {
        DSGetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
    }

    public void DSGetSamplers(int startSlot, ID3D11SamplerState[] samplers)
    {
        DSGetSamplers(startSlot, samplers.Length, samplers);
    }

    public void DSGetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        DSGetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
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
        HSSetShader(hullShader, classInstances, classInstances.Length);
    }

    public void HSUnsetConstantBuffer(int slot)
    {
        void* nullBuffer = default;
        HSSetConstantBuffers(slot, 1, &nullBuffer);
    }

    public void HSUnsetConstantBuffers(int startSlot, int count)
    {
        fixed (void* nullBuffersPtr = s_NullBuffers)
        {
            HSSetConstantBuffers(startSlot, count, nullBuffersPtr);
        }
    }

    public void HSSetConstantBuffer(int slot, ID3D11Buffer? constantBuffer)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        HSSetConstantBuffers(slot, 1, &nativePtr);
    }

    public void HSSetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
    {
        HSSetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
    }

    public void HSSetConstantBuffers(int startSlot, int count, ID3D11Buffer[] constantBuffers)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        HSSetConstantBuffers(startSlot, count, ppConstantBuffers);
    }

    public void HSUnsetSampler(int slot)
    {
        void* nullSampler = default;
        HSSetSamplers(slot, 1, &nullSampler);
    }

    public void HSUnsetSamplers(int startSlot, int count)
    {
        fixed (void* nullSamplersPtr = s_NullSamplers)
        {
            HSSetSamplers(startSlot, count, nullSamplersPtr);
        }
    }

    public void HSSetSampler(int slot, ID3D11SamplerState? sampler)
    {
        IntPtr nativePtr = sampler == null ? IntPtr.Zero : sampler.NativePointer;
        HSSetSamplers(slot, 1, &nativePtr);
    }

    public void HSSetSamplers(int startSlot, ID3D11SamplerState[] samplers)
    {
        HSSetSamplers(startSlot, samplers.Length, samplers);
    }

    public void HSSetSamplers(int startSlot, int count, ID3D11SamplerState[] samplers)
    {
        IntPtr* ppSamplers = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppSamplers[i] = (samplers[i] == null) ? IntPtr.Zero : samplers[i].NativePointer;
        }

        HSSetSamplers(startSlot, count, ppSamplers);
    }

    public void HSUnsetShaderResource(int slot)
    {
        void* nullResource = default;
        HSSetShaderResources(slot, 1, &nullResource);
    }

    public void HSUnsetShaderResources(int startSlot, int count)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = IntPtr.Zero;
        }

        HSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public void HSSetShaderResource(int slot, ID3D11ShaderResourceView? shaderResourceView)
    {
        IntPtr nativePtr = shaderResourceView == null ? IntPtr.Zero : shaderResourceView.NativePointer;
        HSSetShaderResources(slot, 1, &nativePtr);
    }

    public void HSSetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        HSSetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
    }

    public void HSSetShaderResources(int startSlot, int count, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = (shaderResourceViews[i] == null) ? IntPtr.Zero : shaderResourceViews[i].NativePointer;
        }

        HSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public ID3D11HullShader HSGetShader()
    {
        int count = 0;
        HSGetShader(out ID3D11HullShader shader, null, ref count);
        return shader;
    }

    public ID3D11HullShader HSGetShader(ID3D11ClassInstance[] classInstances)
    {
        int count = classInstances.Length;
        HSGetShader(out ID3D11HullShader shader, classInstances, ref count);
        return shader;
    }

    public ID3D11HullShader HSGetShader(ref int classInstancesCount, ID3D11ClassInstance[] classInstances)
    {
        HSGetShader(out ID3D11HullShader shader, classInstances, ref classInstancesCount);
        return shader;
    }

    public void HSGetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
    {
        HSGetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
    }

    public void HSGetSamplers(int startSlot, ID3D11SamplerState[] samplers)
    {
        HSGetSamplers(startSlot, samplers.Length, samplers);
    }

    public void HSGetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        HSGetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
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
        GSSetShader(geometryShader, classInstances, classInstances.Length);
    }

    public void GSUnsetConstantBuffer(int slot)
    {
        void* nullBuffer = default;
        GSSetConstantBuffers(slot, 1, &nullBuffer);
    }

    public void GSUnsetConstantBuffers(int startSlot, int count)
    {
        fixed (void* nullBuffersPtr = s_NullBuffers)
        {
            GSSetConstantBuffers(startSlot, count, nullBuffersPtr);
        }
    }

    public void GSSetConstantBuffer(int slot, ID3D11Buffer? constantBuffer)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        GSSetConstantBuffers(slot, 1, &nativePtr);
    }

    public void GSSetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
    {
        GSSetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
    }

    public void GSSetConstantBuffers(int startSlot, int count, ID3D11Buffer[] constantBuffers)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        GSSetConstantBuffers(startSlot, count, ppConstantBuffers);
    }

    public void GSUnsetSampler(int slot)
    {
        void* nullSampler = default;
        GSSetSamplers(slot, 1, &nullSampler);
    }

    public void GSUnsetSamplers(int startSlot, int count)
    {
        fixed (void* nullSamplersPtr = s_NullSamplers)
        {
            GSSetSamplers(startSlot, count, nullSamplersPtr);
        }
    }

    public void GSSetSampler(int slot, ID3D11SamplerState? sampler)
    {
        IntPtr nativePtr = sampler == null ? IntPtr.Zero : sampler.NativePointer;
        GSSetSamplers(slot, 1, &nativePtr);
    }

    public void GSSetSamplers(int startSlot, ID3D11SamplerState[] samplers)
    {
        GSSetSamplers(startSlot, samplers.Length, samplers);
    }

    public void GSSetSamplers(int startSlot, int count, ID3D11SamplerState[] samplers)
    {
        IntPtr* ppSamplers = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppSamplers[i] = (samplers[i] == null) ? IntPtr.Zero : samplers[i].NativePointer;
        }

        GSSetSamplers(startSlot, count, ppSamplers);
    }

    public void GSUnsetShaderResource(int slot)
    {
        void* nullResource = default;
        GSSetShaderResources(slot, 1, &nullResource);
    }

    public void GSUnsetShaderResources(int startSlot, int count)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = IntPtr.Zero;
        }

        GSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public void GSSetShaderResource(int slot, ID3D11ShaderResourceView? shaderResourceView)
    {
        IntPtr nativePtr = shaderResourceView == null ? IntPtr.Zero : shaderResourceView.NativePointer;
        GSSetShaderResources(slot, 1, &nativePtr);
    }

    public void GSSetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        GSSetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
    }

    public void GSSetShaderResources(int startSlot, int count, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = (shaderResourceViews[i] == null) ? IntPtr.Zero : shaderResourceViews[i].NativePointer;
        }

        GSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public ID3D11GeometryShader GSGetShader()
    {
        int count = 0;
        GSGetShader(out ID3D11GeometryShader shader, null, ref count);
        return shader;
    }

    public ID3D11GeometryShader GSGetShader(ID3D11ClassInstance[] classInstances)
    {
        int count = classInstances.Length;
        GSGetShader(out ID3D11GeometryShader shader, classInstances, ref count);
        return shader;
    }

    public ID3D11GeometryShader GSGetShader(ref int classInstancesCount, ID3D11ClassInstance[] classInstances)
    {
        GSGetShader(out ID3D11GeometryShader shader, classInstances, ref classInstancesCount);
        return shader;
    }

    public void GSGetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
    {
        GSGetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
    }

    public void GSGetSamplers(int startSlot, ID3D11SamplerState[] samplers)
    {
        GSGetSamplers(startSlot, samplers.Length, samplers);
    }

    public void GSGetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        GSGetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
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
        CSSetShader(computeShader, classInstances, classInstances.Length);
    }

    public void CSUnsetConstantBuffer(int slot)
    {
        void* nullBuffer = default;
        CSSetConstantBuffers(slot, 1, &nullBuffer);
    }

    public void CSUnsetConstantBuffers(int startSlot, int count)
    {
        fixed (void* nullBuffersPtr = s_NullBuffers)
        {
            CSSetConstantBuffers(startSlot, count, nullBuffersPtr);
        }
    }

    public void CSSetConstantBuffer(int slot, ID3D11Buffer? constantBuffer)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        CSSetConstantBuffers(slot, 1, &nativePtr);
    }

    public void CSSetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
    {
        CSSetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
    }

    public void CSSetConstantBuffers(int startSlot, int count, ID3D11Buffer[] constantBuffers)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        CSSetConstantBuffers(startSlot, count, ppConstantBuffers);
    }

    public void CSUnsetSampler(int slot)
    {
        void* nullSampler = default;
        CSSetSamplers(slot, 1, &nullSampler);
    }

    public void CSUnsetSamplers(int startSlot, int count)
    {
        fixed (void* nullSamplersPtr = s_NullSamplers)
        {
            CSSetSamplers(startSlot, count, nullSamplersPtr);
        }
    }

    public void CSSetSampler(int slot, ID3D11SamplerState? sampler)
    {
        IntPtr nativePtr = sampler == null ? IntPtr.Zero : sampler.NativePointer;
        CSSetSamplers(slot, 1, &nativePtr);
    }

    public void CSSetSamplers(int startSlot, ID3D11SamplerState[] samplers)
    {
        CSSetSamplers(startSlot, samplers.Length, samplers);
    }

    public void CSSetSamplers(int startSlot, int count, ID3D11SamplerState[] samplers)
    {
        IntPtr* ppSamplers = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppSamplers[i] = (samplers[i] == null) ? IntPtr.Zero : samplers[i].NativePointer;
        }

        CSSetSamplers(startSlot, count, ppSamplers);
    }

    public void CSUnsetShaderResource(int slot)
    {
        void* nullResource = default;
        CSSetShaderResources(slot, 1, &nullResource);
    }

    public void CSUnsetShaderResources(int startSlot, int count)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = IntPtr.Zero;
        }

        CSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public void CSSetShaderResource(int slot, ID3D11ShaderResourceView? shaderResourceView)
    {
        IntPtr nativePtr = shaderResourceView == null ? IntPtr.Zero : shaderResourceView.NativePointer;
        CSSetShaderResources(slot, 1, &nativePtr);
    }

    public void CSSetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        CSSetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
    }

    public void CSSetShaderResources(int startSlot, int count, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        IntPtr* ppShaderResourceViews = stackalloc IntPtr[count];

        for (int i = 0; i < count; i++)
        {
            ppShaderResourceViews[i] = (shaderResourceViews[i] == null) ? IntPtr.Zero : shaderResourceViews[i].NativePointer;
        }

        CSSetShaderResources(startSlot, count, ppShaderResourceViews);
    }

    public ID3D11ComputeShader CSGetShader()
    {
        int count = 0;
        CSGetShader(out ID3D11ComputeShader shader, null, ref count);
        return shader;
    }

    public ID3D11ComputeShader CSGetShader(ID3D11ClassInstance[] classInstances)
    {
        int count = classInstances.Length;
        CSGetShader(out ID3D11ComputeShader shader, classInstances, ref count);
        return shader;
    }

    public ID3D11ComputeShader CSGetShader(ref int classInstancesCount, ID3D11ClassInstance[] classInstances)
    {
        CSGetShader(out ID3D11ComputeShader shader, classInstances, ref classInstancesCount);
        return shader;
    }

    public void CSGetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
    {
        CSGetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
    }

    public void CSGetSamplers(int startSlot, ID3D11SamplerState[] samplers)
    {
        CSGetSamplers(startSlot, samplers.Length, samplers);
    }

    public void CSGetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
    {
        CSGetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
    }

    public void CSSetUnorderedAccessView(int slot, ID3D11UnorderedAccessView? unorderedAccessView, int uavInitialCount = KeepUnorderedAccessViews)
    {
        IntPtr nativePtr = unorderedAccessView == null ? IntPtr.Zero : unorderedAccessView.NativePointer;
        CSSetUnorderedAccessViews(slot, 1, &nativePtr, &uavInitialCount);
    }

    public void CSSetUnorderedAccessViews(int startSlot, ID3D11UnorderedAccessView[] unorderedAccessViews)
    {
        int numUAVs = unorderedAccessViews.Length;
        IntPtr* ppUnorderedAccessViews = stackalloc IntPtr[numUAVs];
        int* pUAVInitialCounts = stackalloc int[numUAVs];

        for (int i = 0; i < numUAVs; i++)
        {
            ppUnorderedAccessViews[i] = (unorderedAccessViews[i] == null) ? IntPtr.Zero : unorderedAccessViews[i].NativePointer;
            pUAVInitialCounts[i] = KeepUnorderedAccessViews;
        }

        CSSetUnorderedAccessViews(startSlot, numUAVs, ppUnorderedAccessViews, pUAVInitialCounts);
    }

    public void CSSetUnorderedAccessViews(int startSlot, int numUAVs, ID3D11UnorderedAccessView[] unorderedAccessViews)
    {
        IntPtr* ppUnorderedAccessViews = stackalloc IntPtr[numUAVs];
        int* pUAVInitialCounts = stackalloc int[numUAVs];

        for (int i = 0; i < numUAVs; i++)
        {
            ppUnorderedAccessViews[i] = (unorderedAccessViews[i] == null) ? IntPtr.Zero : unorderedAccessViews[i].NativePointer;
            pUAVInitialCounts[i] = -1;
        }

        CSSetUnorderedAccessViews(startSlot, numUAVs, ppUnorderedAccessViews, pUAVInitialCounts);
    }

    public void CSSetUnorderedAccessViews(int startSlot, ID3D11UnorderedAccessView[] unorderedAccessViews, int[] uavInitialCounts)
    {
        int numUAVs = unorderedAccessViews.Length;
        IntPtr* ppUnorderedAccessViews = stackalloc IntPtr[numUAVs];
        for (int i = 0; i < numUAVs; i++)
        {
            ppUnorderedAccessViews[i] = (unorderedAccessViews[i] == null) ? IntPtr.Zero : unorderedAccessViews[i].NativePointer;
        }

        fixed (int* pUAVInitialCounts = uavInitialCounts)
        {
            CSSetUnorderedAccessViews(startSlot, numUAVs, ppUnorderedAccessViews, pUAVInitialCounts);
        }
    }

    public void CSSetUnorderedAccessViews(int startSlot, int numUAVs, ID3D11UnorderedAccessView[] unorderedAccessViews, int[] uavInitialCounts)
    {
        IntPtr* ppUnorderedAccessViews = stackalloc IntPtr[numUAVs];
        for (int i = 0; i < numUAVs; i++)
        {
            ppUnorderedAccessViews[i] = (unorderedAccessViews[i] == null) ? IntPtr.Zero : unorderedAccessViews[i].NativePointer;
        }

        fixed (int* pUAVInitialCounts = &uavInitialCounts[0])
        {
            CSSetUnorderedAccessViews(startSlot, numUAVs, ppUnorderedAccessViews, pUAVInitialCounts);
        }
    }

    public void CSUnsetUnorderedAccessView(int slot, int uavInitialCount = -1)
    {
        void* nullUAV = default;
        CSSetUnorderedAccessViews(slot, 1, &nullUAV, &uavInitialCount);
    }

    public void CSUnsetUnorderedAccessViews(int startSlot, int count, int uavInitialCount = -1)
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

    public MappedSubresource Map(ID3D11Resource resource, int subresource, MapMode mode = MapMode.Read, MapFlags flags = MapFlags.None)
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
    public MappedSubresource Map(ID3D11Resource resource, int mipSlice, int arraySlice, MapMode mode, MapFlags flags, out int subresource, out int mipSize)
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
    public Result Map(ID3D11Resource resource, int mipSlice, int arraySlice, MapMode mode, MapFlags flags, out int mipSize, out MappedSubresource mappedSubresource)
    {
        int subresource = resource.CalculateSubResourceIndex(mipSlice, arraySlice, out mipSize);
        return Map(resource, subresource, mode, flags, out mappedSubresource);
    }

    public Span<T> Map<T>(ID3D11Texture1D resource, int mipSlice, int arraySlice, MapMode mode = MapMode.Read, MapFlags flags = MapFlags.None) where T : unmanaged
    {
        Texture1DDescription description = resource.GetDescription();
        int subresource = D3D11.CalculateSubResourceIndex(mipSlice, arraySlice, description.MipLevels);
        Map(resource, subresource, mode, flags, out MappedSubresource mappedSubresource).CheckError();
        Span<byte> source = new(mappedSubresource.DataPointer.ToPointer(), mappedSubresource.RowPitch);
        return MemoryMarshal.Cast<byte, T>(source);
    }

    public Span<T> Map<T>(ID3D11Texture2D resource, int mipSlice, int arraySlice, MapMode mode = MapMode.Read, MapFlags flags = MapFlags.None) where T : unmanaged
    {
        int subresource = resource.CalculateSubResourceIndex(mipSlice, arraySlice, out int mipSize);
        Map(resource, subresource, mode, flags, out MappedSubresource mappedSubresource).CheckError();
        Span<byte> source = new(mappedSubresource.DataPointer.ToPointer(), mipSize * mappedSubresource.RowPitch);
        return MemoryMarshal.Cast<byte, T>(source);
    }

    public Span<T> Map<T>(ID3D11Texture3D resource, int mipSlice, int arraySlice, MapMode mode = MapMode.Read, MapFlags flags = MapFlags.None) where T : unmanaged
    {
        int subresource = resource.CalculateSubResourceIndex(mipSlice, arraySlice, out int mipSize);
        Map(resource, subresource, mode, flags, out MappedSubresource mappedSubresource).CheckError();
        Span<byte> source = new(mappedSubresource.DataPointer.ToPointer(), mipSize * mappedSubresource.DepthPitch);
        return MemoryMarshal.Cast<byte, T>(source);
    }

    public ReadOnlySpan<T> MapReadOnly<T>(ID3D11Resource resource, int mipSlice = 0, int arraySlice = 0, MapFlags flags = MapFlags.None) where T : unmanaged
    {
        int subresource = resource.CalculateSubResourceIndex(mipSlice, arraySlice, out int mipSize);
        Map(resource, subresource, MapMode.Read, flags, out MappedSubresource mappedSubresource).CheckError();
        ReadOnlySpan<byte> source = new(mappedSubresource.DataPointer.ToPointer(), mipSize * mappedSubresource.RowPitch);
        return MemoryMarshal.Cast<byte, T>(source);
    }

    public void Unmap(ID3D11Buffer buffer) => Unmap(buffer, 0);

    public void Unmap(ID3D11Resource resource, int mipSlice, int arraySlice)
    {
        int subresource = resource.CalculateSubResourceIndex(mipSlice, arraySlice, out _);
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
    public void UpdateSubresource<T>(in T value, ID3D11Resource resource, int subresource = 0, int rowPitch = 0, int depthPitch = 0, Box? region = null) where T : unmanaged
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
    public void UpdateSubresource<T>(T[] data, ID3D11Resource resource, int subresource = 0, int rowPitch = 0, int depthPitch = 0, Box? region = null) where T : unmanaged
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
    public void UpdateSubresource<T>(ReadOnlySpan<T> data, ID3D11Resource resource, int subresource = 0, int rowPitch = 0, int depthPitch = 0, Box? region = null) where T : unmanaged
    {
        fixed (T* dataPtr = data)
        {
            UpdateSubresource(resource, subresource, region, (IntPtr)dataPtr, rowPitch, depthPitch);
        }
    }

    public void WriteTexture<T>(ID3D11Texture1D resource, int arraySlice, int mipLevel, T[] data) where T : unmanaged
    {
        ReadOnlySpan<T> span = data.AsSpan();
        WriteTexture(resource, arraySlice, mipLevel, span);
    }

    public void WriteTexture<T>(ID3D11Texture1D resource, int arraySlice, int mipLevel, ReadOnlySpan<T> data) where T : unmanaged
    {
        Texture1DDescription description = resource.Description;
        fixed (T* dataPtr = data)
        {
            int subresource = D3D11.CalculateSubResourceIndex(mipLevel, arraySlice, description.MipLevels);
            DXGI.FormatHelper.GetSurfaceInfo(
                description.Format,
                description.GetWidth(mipLevel),
                1,
                out int rowPitch,
                out int slicePitch);

            UpdateSubresource(resource, subresource, null, (IntPtr)dataPtr, rowPitch, slicePitch);
        }
    }

    public void WriteTexture<T>(ID3D11Texture2D resource, int arraySlice, int mipLevel, T[] data) where T : unmanaged
    {
        ReadOnlySpan<T> span = data.AsSpan();
        WriteTexture(resource, arraySlice, mipLevel, span);
    }

    public void WriteTexture<T>(ID3D11Texture2D resource, int arraySlice, int mipLevel, ReadOnlySpan<T> data) where T : unmanaged
    {
        Texture2DDescription description = resource.Description;
        fixed (T* dataPtr = data)
        {
            int subresource = D3D11.CalculateSubResourceIndex(mipLevel, arraySlice, description.MipLevels);
            DXGI.FormatHelper.GetSurfaceInfo(
                description.Format,
                description.GetWidth(mipLevel),
                description.GetHeight(mipLevel),
                out int rowPitch,
                out int slicePitch);

            UpdateSubresource(resource, subresource, null, (IntPtr)dataPtr, rowPitch, slicePitch);
        }
    }

    public unsafe void WriteTexture<T>(ID3D11Texture2D resource, int arraySlice, int mipLevel, ref T data) where T : unmanaged
    {
        Texture2DDescription description = resource.Description;
        fixed (T* dataPtr = &data)
        {
            int subresource = D3D11.CalculateSubResourceIndex(mipLevel, arraySlice, description.MipLevels);
            DXGI.FormatHelper.GetSurfaceInfo(
                description.Format,
                description.GetWidth(mipLevel),
                description.GetHeight(mipLevel),
                out int rowPitch,
                out int slicePitch);

            UpdateSubresource(resource, subresource, null, (IntPtr)dataPtr, rowPitch, slicePitch);
        }
    }

    /// <summary>
    /// Copies data from the CPU to to a non-mappable subresource region.
    /// </summary>
    /// <param name="source">The source data.</param>
    /// <param name="resource">The destination resource.</param>
    /// <param name="subresource">The destination subresource.</param>
    public void UpdateSubresource(MappedSubresource source, ID3D11Resource resource, int subresource = 0)
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
    public void UpdateSubresource(MappedSubresource source, ID3D11Resource resource, int subresource, Box region)
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
    public void UpdateSubresourceSafe<T>(ref T value, ID3D11Resource resource, int srcBytesPerElement, int subresource = 0, int rowPitch = 0, int depthPitch = 0, bool isCompressedResource = false) where T : unmanaged
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
    public void UpdateSubresourceSafe<T>(T[] data, ID3D11Resource resource, int srcBytesPerElement, int subresource = 0, int rowPitch = 0, int depthPitch = 0, bool isCompressedResource = false) where T : unmanaged
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
    public unsafe void UpdateSubresourceSafe<T>(Span<T> data, ID3D11Resource resource, int srcBytesPerElement, int subresource = 0, int rowPitch = 0, int depthPitch = 0, bool isCompressedResource = false) where T : unmanaged
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
    public void UpdateSubresourceSafe(MappedSubresource source, ID3D11Resource resource, int srcBytesPerElement, int subresource = 0, bool isCompressedResource = false)
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
    public void UpdateSubresourceSafe(MappedSubresource source, ID3D11Resource resource, int srcBytesPerElement, int subresource, Box region, bool isCompressedResource = false)
    {
        UpdateSubresourceSafe(resource, subresource, region, source.DataPointer, source.RowPitch, source.DepthPitch, srcBytesPerElement, isCompressedResource);
    }

    internal unsafe bool UpdateSubresourceSafe(
        ID3D11Resource dstResource, int dstSubresource, Box? dstBox,
        IntPtr pSrcData, int srcRowPitch, int srcDepthPitch, int srcBytesPerElement, bool isCompressedResource)
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
