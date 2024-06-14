// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WinUI;

[Guid("9e43c18e-7816-474c-840f-5c9c8b0e2207")]
public partial class IVirtualSurfaceImageSourceNative : ISurfaceImageSourceNative
{
    public IVirtualSurfaceImageSourceNative(nint nativePtr)
        : base(nativePtr)
    {
    }

    public static explicit operator IVirtualSurfaceImageSourceNative?(nint nativePtr) => nativePtr == 0 ? default : new IVirtualSurfaceImageSourceNative(nativePtr);

#if WINDOWS
    public IVirtualSurfaceImageSourceNative(Microsoft.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource imageSource)
        : base(WinUIHelpers.GetNativeObject(typeof(IVirtualSurfaceImageSourceNative).GUID, imageSource))
    {
    }

    public static explicit operator IVirtualSurfaceImageSourceNative(Microsoft.UI.Xaml.Media.Imaging.VirtualSurfaceImageSource imageSource) => new(imageSource);
#endif

    public RawRect VisibleBounds { get => GetVisibleBounds(); }

    public unsafe Result Invalidate(RawRect updateRect)
    {
        Result result = ((delegate* unmanaged[Stdcall]<IntPtr, RawRect, int>)this[6])(NativePointer, updateRect);
        return result;
    }

    internal unsafe Result GetUpdateRectCount(out int count)
    {
        Result result;
        fixed (void* count_ = &count)
        {
            result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, int>)this[7])(NativePointer, count_);
        }

        return result;
    }


    internal unsafe Result GetUpdateRects(RawRect[] updates, int count)
    {
        Result result;
        fixed (RawRect* updates_ = updates)
        {
            result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, int, int>)this[8])(NativePointer, updates_, count);
        }

        return result;
    }

    internal unsafe RawRect GetVisibleBounds()
    {
        RawRect bounds;
        Result result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, int>)this[9])(NativePointer, &bounds);
        result.CheckError();
        return bounds;
    }

    public unsafe void RegisterForUpdatesNeeded(IVirtualSurfaceUpdatesCallbackNative callback)
    {
        IntPtr callback_ = MarshallingHelpers.ToCallbackPtr<IVirtualSurfaceUpdatesCallbackNative>(callback);
        Result result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, int>)this[10])(NativePointer, (void*)callback_);
        GC.KeepAlive(callback);
        result.CheckError();
    }

    public unsafe Result Resize(int newWidth, int newHeight)
    {
        Result result = ((delegate* unmanaged[Stdcall]<IntPtr, int, int, int>)this[11])(NativePointer, newWidth, newHeight);
        return result;
    }
}
