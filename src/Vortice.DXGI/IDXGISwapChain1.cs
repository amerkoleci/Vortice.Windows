// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.DXGI;

public partial class IDXGISwapChain1
{
    /// <summary>
    /// Retrieves the underlying HWND for this swap-chain object.
    /// </summary>
    /// <returns>Native HWND handle</returns>
    public IntPtr GetHwnd()
    {
        if (GetHwnd(out IntPtr hwnd).Failure)
        {
            return IntPtr.Zero;
        }

        return hwnd;
    }

    /// <summary>
    /// Retrieves the underlying CoreWindow object for this swap-chain object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetCoreWindow<T>() where T : ComObject
    {
        GetCoreWindow(typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    /// <summary>
    /// Retrieves the underlying CoreWindow object for this swap-chain object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="coreWindow"></param>
    /// <returns></returns>
    public Result GetCoreWindow<T>(out T? coreWindow) where T : ComObject
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

    public unsafe Result Present(int syncInterval, PresentFlags presentFlags, PresentParameters presentParameters)
    {
        bool hasScrollRectangle = presentParameters.ScrollRectangle.HasValue;
        bool hasScrollOffset = presentParameters.ScrollOffset.HasValue;

        RawRect scrollRectangle = hasScrollRectangle ? presentParameters.ScrollRectangle!.Value : new RawRect();
        Int2 scrollOffset = hasScrollOffset ? presentParameters.ScrollOffset!.Value : default;

        fixed (void* pDirtyRects = presentParameters.DirtyRectangles)
        {
            var native = default(PresentParameters.__Native);
            native.DirtyRectsCount = presentParameters.DirtyRectangles != null ? presentParameters.DirtyRectangles.Length : 0;
            native.PDirtyRects = (IntPtr)pDirtyRects;
            native.PScrollRect = hasScrollRectangle ? new IntPtr(&scrollRectangle) : IntPtr.Zero;
            native.PScrollOffset = hasScrollOffset ? new IntPtr(&scrollOffset) : IntPtr.Zero;

            return Present1(syncInterval, presentFlags, &native);
        }
    }
}
