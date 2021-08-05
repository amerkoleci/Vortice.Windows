// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.DXGI;

namespace Vortice.WinUI
{
    [Guid("e4cecd6c-f14b-4f46-83c3-8bbda27c6504")]
    public class ISurfaceImageSourceNative : ComObject
    {
        public ISurfaceImageSourceNative(IntPtr nativePtr)
            : base(nativePtr)
        {
        }

        public static explicit operator ISurfaceImageSourceNative?(IntPtr nativePtr)
        {
            return (nativePtr == IntPtr.Zero) ? null : new ISurfaceImageSourceNative(nativePtr);
        }

        public IDXGIDevice Device
        {
            set => SetDevice(value).CheckError();
        }

        public unsafe Result SetDevice(IDXGIDevice device)
        {
            IntPtr devicePtr = MarshallingHelpers.ToCallbackPtr<IDXGIDevice>(device);
            Result result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, int>)this[3])(NativePointer, (void*)devicePtr);
            GC.KeepAlive(device);
            result.CheckError();
            return result;
        }

        public unsafe Result BeginDraw(in RawRect updateRect, out IDXGISurface? surface, out Point offset)
        {
            IntPtr surfacePtr = IntPtr.Zero;
            offset = default;

            Result result;
            fixed (Point* offsetPtr = &offset)
            {
                result = ((delegate* unmanaged[Stdcall]<IntPtr, RawRect, void*, Point*, int>)this[4])(NativePointer, updateRect, &surfacePtr, offsetPtr);
            }

            surface = surfacePtr != IntPtr.Zero ? new IDXGISurface(surfacePtr) : null;
            return result;
        }

        public unsafe Result EndDraw()
        {
            Result result = ((delegate* unmanaged[Stdcall]<IntPtr, int>)this[5])(NativePointer);
            return result;
        }
    }

}
