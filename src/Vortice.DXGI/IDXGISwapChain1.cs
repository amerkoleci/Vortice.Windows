// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using Vortice.Mathematics;

namespace Vortice.DXGI
{
    public partial class IDXGISwapChain1
    {
        /// <summary>
        /// Retrieves the underlying HWND for this swap-chain object.
        /// </summary>
        /// <returns>Native HWND handle</returns>
        public IntPtr GetHwnd()
        {
            if (GetHwnd(out var hwnd).Failure)
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
            var result = GetCoreWindow(typeof(T).GUID, out var nativePtr);
            if (result.Failure)
            {
                return default;
            }

            return FromPointer<T>(nativePtr);
        }

        public unsafe Result Present(int syncInterval, PresentFlags presentFlags, PresentParameters presentParameters)
        {
            bool hasScrollRectangle = presentParameters.ScrollRectangle.HasValue;
            bool hasScrollOffset = presentParameters.ScrollOffset.HasValue;

            var scrollRectangle = hasScrollRectangle ? presentParameters.ScrollRectangle.Value : new RawRect();
            var scrollOffset = hasScrollOffset ? presentParameters.ScrollOffset.Value : default;

            fixed (void* pDirtyRects = presentParameters.DirtyRectangles)
            {
                var native = default(PresentParameters.__Native);
                native.DirtyRectsCount = presentParameters.DirtyRectangles != null ? presentParameters.DirtyRectangles.Length : 0;
                native.PDirtyRects = (IntPtr)pDirtyRects;
                native.PScrollRect = hasScrollRectangle ? new IntPtr(&scrollRectangle) : IntPtr.Zero;
                native.PScrollOffset = hasScrollOffset ? new IntPtr(&scrollOffset) : IntPtr.Zero;

                return Present1(syncInterval, presentFlags, new IntPtr(&native));
            }
        }
    }
}
