// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Mathematics;

namespace Vortice.DXGI.WinUI
{
    [Guid("cb833102-d5d1-448b-a31a-52a9509f24e6")]
    public partial class ISurfaceImageSourceNativeWithD2D : ComObject
    {
        public ISurfaceImageSourceNativeWithD2D(IntPtr nativePtr) : base(nativePtr)
        {
        }

        public static explicit operator ISurfaceImageSourceNativeWithD2D(IntPtr nativePtr) => (nativePtr == IntPtr.Zero) ? null : new ISurfaceImageSourceNativeWithD2D(nativePtr);

        public unsafe Result SetDevice(IUnknown device)
        {
            IntPtr devicePtr = CppObject.ToCallbackPtr<SharpGen.Runtime.IUnknown>(device);
            Result result = LocalInterop.CalliStdCallint(_nativePointer, devicePtr.ToPointer(), (*(void***)_nativePointer)[3]);
            GC.KeepAlive(device);
            return result;
        }

        public T BeginDraw<T>(RawRect? updateRect, out Point updateOffset) where T : ComObject
        {
            BeginDraw(updateRect, typeof(T).GUID, out IntPtr updateObjectPtr, out updateOffset).CheckError();
            return FromPointer<T>(updateObjectPtr);
        }

        private unsafe Result BeginDraw(RawRect? updateRect, Guid iid, out IntPtr updateObject, out Point offset)
        {
            RawRect updateRectCall = updateRect.GetValueOrDefault();
            offset = default;
            Result result;
            fixed (void* offset_ = &offset)
            {
                fixed (void* updateObject_ = &updateObject)
                {
                    result = LocalInterop.CalliStdCallint(_nativePointer, updateRect.HasValue ? &updateRectCall : (void*)0, &iid, updateObject_, offset_, (*(void***)_nativePointer)[4]);
                }
            }

            return result;
        }

        public unsafe Result EndDraw()
        {
            return LocalInterop.CalliStdCallint(_nativePointer, (*(void***)_nativePointer)[5]);
        }

        public unsafe Result SuspendDraw()
        {
            return LocalInterop.CalliStdCallint(_nativePointer, (*(void***)_nativePointer)[6]);
        }

        public unsafe Result ResumeDraw()
        {
            return LocalInterop.CalliStdCallint(_nativePointer, (*(void***)_nativePointer)[7]);
        }
    }
}
