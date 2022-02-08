// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;
using Vortice.DXGI;

namespace Vortice.WinUI;


[Guid("e4cecd6c-f14b-4f46-83c3-8bbda27c6504")]
public unsafe class ISurfaceImageSourceNative : ComObject
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

    public Result SetDevice(IDXGIDevice device)
    {
        IntPtr devicePtr = MarshallingHelpers.ToCallbackPtr<IDXGIDevice>(device);
        Result result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, int>)this[3])(NativePointer, (void*)devicePtr);
        GC.KeepAlive(device);
        result.CheckError();
        return result;
    }

    public Result BeginDraw(in RawRect updateRect, out IDXGISurface? surface, out Int2 offset)
    {
        IntPtr surfacePtr = IntPtr.Zero;
        offset = default;

        Result result;
        fixed (Int2* offsetPtr = &offset)
        {
            result = ((delegate* unmanaged[Stdcall]<IntPtr, RawRect, void*, Int2*, int>)this[4])(NativePointer, updateRect, &surfacePtr, offsetPtr);
        }

        surface = surfacePtr != IntPtr.Zero ? new IDXGISurface(surfacePtr) : null;
        return result;
    }

    public Result EndDraw()
    {
        Result result = ((delegate* unmanaged[Stdcall]<IntPtr, int>)this[5])(NativePointer);
        return result;
    }
}
