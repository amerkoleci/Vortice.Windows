// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.DXGI;

public partial class IDXGISwapChain1
{
    /// <summary>
    /// Retrieves the underlying HWND for this swap-chain object.
    /// </summary>
    /// <returns>Native HWND handle</returns>
    public nint GetHwnd()
    {
        if (GetHwnd(out nint hwnd).Failure)
        {
            return 0;
        }

        return hwnd;
    }

    /// <summary>
    /// Retrieves the underlying CoreWindow object for this swap-chain object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetCoreWindow<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>() where T : ComObject
    {
        GetCoreWindow(typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    /// <summary>
    /// Retrieves the underlying CoreWindow object for this swap-chain object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="coreWindow"></param>
    /// <returns></returns>
    public Result GetCoreWindow<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(out T? coreWindow) where T : ComObject
    {
        Result result = GetCoreWindow(typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            coreWindow = default;
            return default;
        }

        coreWindow = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    /// <include file="Documentation.xml" path="/comments/comment[@id='IDXGISwapChain1::Present1']/*" />
    /// <unmanaged>HRESULT IDXGISwapChain1::Present1([In] UINT SyncInterval, [In] UINT PresentFlags, [In] const DXGI_PRESENT_PARAMETERS* pPresentParameters)</unmanaged>
    /// <unmanaged-short>IDXGISwapChain1::Present1</unmanaged-short>
    public unsafe Result Present1(int syncInterval, PresentFlags presentFlags, PresentParameters presentParameters)
    {
        bool hasScrollRectangle = presentParameters.ScrollRectangle.HasValue;
        bool hasScrollOffset = presentParameters.ScrollOffset.HasValue;

        RawRect scrollRectangle = hasScrollRectangle ? presentParameters.ScrollRectangle!.Value : default;
        Int2 scrollOffset = hasScrollOffset ? presentParameters.ScrollOffset!.Value : default;

        fixed (RawRect* pDirtyRects = presentParameters.DirtyRectangles)
        {
            PresentParameters.__Native native = default;
            native.DirtyRectsCount = presentParameters.DirtyRectangles != null ? presentParameters.DirtyRectangles.Length : 0;
            native.pDirtyRects = pDirtyRects;
            native.pScrollRect = hasScrollRectangle ? &scrollRectangle : null;
            native.pScrollOffset = hasScrollOffset ? &scrollOffset : null;

            return Present1(syncInterval, presentFlags, &native);
        }
    }

    public unsafe Result Present1(
        int syncInterval,
        PresentFlags presentFlags,
        ReadOnlySpan<RawRect> dirtyRectangles,
        RawRect? scrollRectangle = default,
        Int2? scrollOffset = default)
    {
        bool hasScrollRectangle = scrollRectangle.HasValue;
        bool hasScrollOffset = scrollOffset.HasValue;

        RawRect scrollRectangleCall = hasScrollRectangle ? scrollRectangle!.Value : default;
        Int2 scrollOffsetCall = hasScrollOffset ? scrollOffset!.Value : default;

        fixed (RawRect* pDirtyRects = dirtyRectangles)
        {
            PresentParameters.__Native native = default;
            native.DirtyRectsCount = dirtyRectangles.Length > 0 ? dirtyRectangles.Length : 0;
            native.pDirtyRects = pDirtyRects;
            native.pScrollRect = hasScrollRectangle ? &scrollRectangleCall : null;
            native.pScrollOffset = hasScrollOffset ? &scrollOffsetCall : null;

            return Present1(syncInterval, presentFlags, &native);
        }
    }
}
