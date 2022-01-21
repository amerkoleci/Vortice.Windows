// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WinUI;

[Guid("81521d7e-ff74-4a6b-8289-44bfd11cf0cc")]
public partial class ISurfaceImageSourceManagerNative : ComObject
{
    public ISurfaceImageSourceManagerNative(IntPtr nativePtr) : base(nativePtr)
    {
    }

    public static explicit operator ISurfaceImageSourceManagerNative?(IntPtr nativePtr) => nativePtr == IntPtr.Zero ? null : new ISurfaceImageSourceManagerNative(nativePtr);

    public unsafe Result FlushAllSurfacesWithDevice(IUnknown device)
    {
        IntPtr device_ = MarshallingHelpers.ToCallbackPtr<IUnknown>(device);
        Result result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, int>)this[3])(NativePointer, (void*)device_);
        GC.KeepAlive(device);
        return result;
    }
}
