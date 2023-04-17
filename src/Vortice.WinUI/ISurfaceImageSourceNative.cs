// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;
using Vortice.DXGI;
using System.Drawing;

#if WINDOWS
using WinRT;
using Microsoft.UI.Xaml.Media.Imaging;
#endif

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

#if WINDOWS
    public ISurfaceImageSourceNative(SurfaceImageSource owner)
        : base(((IWinRTObject)owner).NativeObject.GetRef())
    {
    }

    public static explicit operator ISurfaceImageSourceNative(SurfaceImageSource owner) => new(owner);
#endif

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

    public Result BeginDraw(in Rectangle updateRect, out IDXGISurface? surface, out Point offset)
    {
        RawRect updateRectRaw = updateRect;
        IntPtr surfacePtr = IntPtr.Zero;
        offset = default;

        Result result;
        fixed (Point* offsetPtr = &offset)
        {
            result = ((delegate* unmanaged<IntPtr, RawRect, void*, Point*, int>)this[4])(NativePointer, updateRectRaw, &surfacePtr, offsetPtr);
        }

        surface = surfacePtr != IntPtr.Zero ? new IDXGISurface(surfacePtr) : null;
        return result;
    }

    public Result EndDraw()
    {
        Result result = ((delegate* unmanaged<IntPtr, int>)this[5])(NativePointer);
        return result;
    }
}
