// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Mathematics;

namespace Vortice.DXGI.WinUI
{
    [Guid("e4cecd6c-f14b-4f46-83c3-8bbda27c6504")]
    public partial class ISurfaceImageSourceNative : ComObject
    {
        public ISurfaceImageSourceNative(IntPtr nativePtr) : base(nativePtr)
        {
        }

        public static explicit operator ISurfaceImageSourceNative(IntPtr nativePtr)
        {
            return (nativePtr == IntPtr.Zero) ? null : new ISurfaceImageSourceNative(nativePtr);
        }

        public unsafe Result SetDevice(IDXGIDevice device)
        {
            Result result = LocalInterop.CalliStdCallint(_nativePointer, device.NativePointer.ToPointer(), (*(void***)_nativePointer)[3]);
            GC.KeepAlive(device);
            return result;
        }

        public unsafe Result BeginDraw(RawRect updateRect, out IDXGISurface surface, out Point offset)
        {
            IntPtr surfacePtr = IntPtr.Zero;
            Result result;
            fixed (void* offset_ = &offset)
            {
                result = LocalInterop.CalliStdCallint0(_nativePointer, updateRect, &surfacePtr, offset_, (*(void***)_nativePointer)[4]);
            }

            if (result.Failure)
            {
                surface = default;
                offset = default;
                return result;
            }

            surface = new IDXGISurface(surfacePtr);
            return result;
        }

        public unsafe Result EndDraw()
        {
            return LocalInterop.CalliStdCallint(_nativePointer, (*(void***)_nativePointer)[5]);
        }
    }
}
