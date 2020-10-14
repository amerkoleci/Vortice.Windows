// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DirectComposition
{
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
}
