// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.WinUI.Composition;

[Guid("2D6355C2-AD57-4EAE-92E4-4C3EFF65D578")]
public unsafe partial class ICompositionDrawingSurfaceInterop : ComObject
{
    public ICompositionDrawingSurfaceInterop(IntPtr nativePtr) : base(nativePtr)
    {
    }

    public static explicit operator ICompositionDrawingSurfaceInterop?(IntPtr nativePtr) => nativePtr == IntPtr.Zero ? null : new(nativePtr);

    public Result BeginDraw<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in RectI updateRect, out T? updateObject, out Int2 offset) where T : ComObject
    {
        RawRect updateRectRaw = updateRect;
        IntPtr updateObjectPtr = IntPtr.Zero;
        offset = default;

        fixed (Int2* offsetPtr = &offset)
        {
            Guid iid = typeof(T).GUID;
            Result result = ((delegate* unmanaged<IntPtr, RawRect, void*, void*, Int2*, int>)this[3])(NativePointer, updateRectRaw, &iid, &updateObjectPtr, offsetPtr);
            updateObject = updateObjectPtr != IntPtr.Zero ? MarshallingHelpers.FromPointer<T>(updateObjectPtr)! : null;
            return result;
        }
    }

    public T BeginDraw<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in RectI updateRect, out Int2 offset) where T : ComObject
    {
        RawRect updateRectRaw = updateRect;
        IntPtr updateObjectPtr = IntPtr.Zero;

        fixed (Int2* offsetPtr = &offset)
        {
            Guid iid = typeof(T).GUID;
            Result result = ((delegate* unmanaged<IntPtr, RawRect, void*, void*, Int2*, int>)this[3])(NativePointer, updateRectRaw, &iid, &updateObjectPtr, offsetPtr);
            result.CheckError();
            return MarshallingHelpers.FromPointer<T>(updateObjectPtr)!;
        }
    }

    public Result EndDraw()
    {
        Result result = ((delegate* unmanaged<IntPtr, int>)this[4])(NativePointer);
        return result;
    }

    public Result Resize(Size sizePixels)
    {
        return ((delegate* unmanaged<IntPtr, Size, int>)this[5])(NativePointer, sizePixels);
    }

    public Result Scroll(int offsetX, int offsetY)
    {
        return ((delegate* unmanaged<IntPtr, RawRect*, RawRect*, int, int, int>)this[6])(NativePointer, null, null, offsetX, offsetY);
    }

    public Result Scroll(RectI clipRect, int offsetX, int offsetY)
    {
        RawRect clipRectRaw = clipRect;
        return ((delegate* unmanaged<IntPtr, RawRect*, RawRect*, int, int, int>)this[6])(NativePointer, null, &clipRectRaw, offsetX, offsetY);
    }

    public Result Scroll(RectI scrollRect, RectI clipRect, int offsetX, int offsetY)
    {
        RawRect scrollRectRaw = scrollRect;
        RawRect clipRectRaw = clipRect;
        return ((delegate* unmanaged<IntPtr, RawRect*, RawRect*, int, int, int>)this[6])(NativePointer, &scrollRectRaw, &clipRectRaw, offsetX, offsetY);
    }

    public Result SuspendDraw()
    {
        Result result = ((delegate* unmanaged<IntPtr, int>)this[7])(NativePointer);
        return result;
    }

    public Result ResumeDraw()
    {
        Result result = ((delegate* unmanaged<IntPtr, int>)this[8])(NativePointer);
        return result;
    }
}
