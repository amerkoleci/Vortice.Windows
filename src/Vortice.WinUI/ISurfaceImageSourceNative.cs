// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;
using Vortice.Mathematics;

namespace Vortice.WinUI;

[Guid("e4cecd6c-f14b-4f46-83c3-8bbda27c6504")]
public unsafe class ISurfaceImageSourceNative : ComObject
{
    public ISurfaceImageSourceNative(nint nativePtr)
        : base(nativePtr)
    {
    }

    public static explicit operator ISurfaceImageSourceNative?(nint nativePtr) => (nativePtr == 0) ? default : new ISurfaceImageSourceNative(nativePtr);

#if WINDOWS
    public ISurfaceImageSourceNative(Microsoft.UI.Xaml.Media.Imaging.SurfaceImageSource imageSource)
        : base(WinUIHelpers.GetNativeObject(typeof(ISurfaceImageSourceNative).GUID, imageSource))
    {
    }

    public static explicit operator ISurfaceImageSourceNative(Microsoft.UI.Xaml.Media.Imaging.SurfaceImageSource imageSource) => new(imageSource);
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

    public Result BeginDraw(in RectI updateRect, out IDXGISurface? surface, out Int2 offset)
    {
        RawRect updateRectRaw = updateRect;
        IntPtr surfacePtr = IntPtr.Zero;
        offset = default;

        Result result;
        fixed (Int2* offsetPtr = &offset)
        {
            result = ((delegate* unmanaged<IntPtr, RawRect, void*, Int2*, int>)this[4])(NativePointer, updateRectRaw, &surfacePtr, offsetPtr);
        }

        surface = surfacePtr != IntPtr.Zero ? new IDXGISurface(surfacePtr) : null;
        return result;
    }

#if WINDOWS
    public Result BeginDraw(in Windows.Foundation.Rect updateRect, out IDXGISurface? surface, out Windows.Foundation.Point offset)
    {
        RawRect updateRectRaw = updateRect;
        nint surfacePtr = IntPtr.Zero;
        Int2 offsetCall;
        Result result = ((delegate* unmanaged<IntPtr, RawRect, void*, Int2*, int>)this[4])(NativePointer, updateRectRaw, &surfacePtr, &offsetCall);

        surface = surfacePtr != IntPtr.Zero ? new IDXGISurface(surfacePtr) : null;
        offset = new Windows.Foundation.Point(offsetCall.X, offsetCall.Y);
        return result;
    }
#endif

    public Result EndDraw()
    {
        Result result = ((delegate* unmanaged<IntPtr, int>)this[5])(NativePointer);
        return result;
    }
}
