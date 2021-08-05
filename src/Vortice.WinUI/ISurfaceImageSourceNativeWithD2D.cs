// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.DXGI;

namespace Vortice.WinUI
{
    [Guid("cb833102-d5d1-448b-a31a-52a9509f24e6")]
    public class ISurfaceImageSourceNativeWithD2D : ComObject
    {
        public ISurfaceImageSourceNativeWithD2D(IntPtr nativePtr)
            : base(nativePtr)
        {
        }

        public static explicit operator ISurfaceImageSourceNativeWithD2D?(IntPtr nativePtr)
        {
            return (nativePtr == IntPtr.Zero) ? null : new ISurfaceImageSourceNativeWithD2D(nativePtr);
        }

        public IUnknown Device
        {
            set => SetDevice(value).CheckError();
        }

        public unsafe Result SetDevice(IUnknown device)
        {
            IntPtr device_ = MarshallingHelpers.ToCallbackPtr<IUnknown>(device);
            Result result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, int>)this[3])(NativePointer, (void*)device_);
            GC.KeepAlive(device);
            return result;
        }

        public Result BeginDraw<T>(RawRect updateRect, out T? updateObject, out Point offset) where T : ComObject
        {
            Result result = BeginDraw(updateRect, typeof(T).GUID, out IntPtr updateObjectPtr, out offset);
            if (result.Failure)
            {
                updateObject = default;
                return result;
            }

            updateObject = MarshallingHelpers.FromPointer<T>(updateObjectPtr);
            return result;
        }

        public T BeginDraw<T>(RawRect updateRect, out Point offset) where T : ComObject
        {
            Result result = BeginDraw(updateRect, typeof(T).GUID, out IntPtr updateObjectPtr, out offset);
            result.CheckError();
            return MarshallingHelpers.FromPointer<T>(updateObjectPtr);
        }

        internal unsafe Result BeginDraw(RawRect updateRect, Guid iid, out IntPtr updateObject, out Point offset)
        {
            offset = default;
            Result result;
            fixed (void* offset_ = &offset)
            fixed (void* updateObject_ = &updateObject)
            {
                result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, void*, void*, void*, int>)this[4])(NativePointer, &updateRect, &iid, updateObject_, offset_);
            }

            return result;
        }

        public unsafe Result EndDraw()
        {
            Result result = ((delegate* unmanaged[Stdcall]<IntPtr, int>)this[5])(NativePointer);
            return result;
        }

        public unsafe Result SuspendDraw()
        {
            Result result = ((delegate* unmanaged[Stdcall]<System.IntPtr, int>)this[6])(NativePointer);
            return result;
        }

        public unsafe Result ResumeDraw()
        {
            Result result = ((delegate* unmanaged[Stdcall]<IntPtr, int>)this[7])(NativePointer);
            return result;
        }
    }
}
