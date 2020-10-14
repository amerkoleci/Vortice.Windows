// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DXGI.WinUI
{
    [Guid("9e43c18e-7816-474c-840f-5c9c8b0e2207")]
    public partial class IVirtualSurfaceImageSourceNative : Vortice.DXGI.ISurfaceImageSourceNative
    {
        public IVirtualSurfaceImageSourceNative(IntPtr nativePtr) : base(nativePtr)
        {
        }

        public static explicit operator IVirtualSurfaceImageSourceNative(IntPtr nativePtr) => nativePtr == IntPtr.Zero ? null : new IVirtualSurfaceImageSourceNative(nativePtr);

        /// <summary>
        /// Gets the set of regions that must be updated on the shared surface.
        /// </summary>
        public RawRect[] UpdateRectangles
        {
            get
            {
                if (GetUpdateRectCount(out int count).Failure)
                    return Array.Empty<RawRect>();

                var regionToUpdate = new RawRect[count];
                GetUpdateRects(regionToUpdate, count);
                return regionToUpdate;
            }
        }

        public RawRect VisibleBounds
        {
            get
            {
                GetVisibleBounds(out RawRect bounds);
                return bounds;
            }
        }

        public unsafe Result Invalidate(RawRect updateRect)
        {
            return LocalInterop.CalliStdCallint0(_nativePointer, updateRect, (*(void***)_nativePointer)[6]);
        }

        private unsafe Result GetUpdateRectCount(out int count)
        {
            Result result;
            fixed (void* count_ = &count)
            {
                result = LocalInterop.CalliStdCallint(_nativePointer, count_, (*(void***)_nativePointer)[7]);
            }

            return result;
        }

        private unsafe Result GetUpdateRects(RawRect[] updates, int count)
        {
            Result result;
            fixed (void* updates_ = updates)
            {
                result = LocalInterop.CalliStdCallint(_nativePointer, updates_, count, (*(void***)_nativePointer)[8]);
            }

            return result;
        }

        private unsafe void GetVisibleBounds(out RawRect bounds)
        {
            bounds = default;
            Result result;
            fixed (void* bounds_ = &bounds)
            {
                result = LocalInterop.CalliStdCallint(_nativePointer, bounds_, (*(void***)_nativePointer)[9]);
            }

            result.CheckError();
        }

        /// <summary>
        /// No documentation.
        /// </summary>
        /// <param name = "callback">No documentation.</param>
        /// <returns>No documentation.</returns>
        /// <unmanaged>HRESULT IVirtualSurfaceImageSourceNative::RegisterForUpdatesNeeded([In, Optional] IVirtualSurfaceUpdatesCallbackNative* callback)</unmanaged>
        /// <unmanaged-short>IVirtualSurfaceImageSourceNative::RegisterForUpdatesNeeded</unmanaged-short>
        public unsafe void RegisterForUpdatesNeeded(Vortice.DXGI.IVirtualSurfaceUpdatesCallbackNative callback)
        {
            Result result = LocalInterop.CalliStdCallint(_nativePointer, (void*)callback.NativePointer.ToPointer(), (*(void***)_nativePointer)[10]);
            System.GC.KeepAlive(callback);
            result.CheckError();
        }

        public unsafe Result Resize(int newWidth, int newHeight)
        {
            return LocalInterop.CalliStdCallint(_nativePointer, newWidth, newHeight, (*(void***)_nativePointer)[11]);
        }
    }
}
