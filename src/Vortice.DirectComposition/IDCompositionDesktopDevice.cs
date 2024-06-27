// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectComposition;

public partial class IDCompositionDesktopDevice
{
    public IUnknown CreateSurfaceFromHandle(IntPtr handle)
    {
        CreateSurfaceFromHandle(handle, out IUnknown surface).CheckError();
        return surface;
    }

    public IUnknown CreateSurfaceFromHwnd(IntPtr hwnd)
    {
        CreateSurfaceFromHwnd(hwnd, out IUnknown surface).CheckError();
        return surface;
    }

    public IDCompositionTarget CreateSurfaceFromHwnd(IntPtr hwnd, bool topmost)
    {
        CreateTargetForHwnd(hwnd, topmost, out IDCompositionTarget target).CheckError();
        return target;
    }
}
