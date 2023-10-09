// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;

namespace Vortice.WinUI.Composition;

[Guid("2D6355C2-AD57-4EAE-92E4-4C3EFF65D578")]
public unsafe partial class ICompositionDrawingSurfaceInterop : ComObject
{
    public ICompositionDrawingSurfaceInterop(IntPtr nativePtr) : base(nativePtr)
    {
    }

    public static explicit operator ICompositionDrawingSurfaceInterop?(IntPtr nativePtr) => nativePtr == IntPtr.Zero ? null : new(nativePtr);

    public Result BeginDraw<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in Rectangle updateRect, out T? updateObject, out Point offset) where T : ComObject
    {
        RawRect updateRectRaw = updateRect;
        IntPtr updateObjectPtr = IntPtr.Zero;
        offset = default;

        fixed (Point* offsetPtr = &offset)
        {
            Guid iid = typeof(T).GUID;
            Result result = ((delegate* unmanaged<IntPtr, RawRect, void*, void*, Point*, int>)this[3])(NativePointer, updateRectRaw, &iid, &updateObjectPtr, offsetPtr);
            updateObject = updateObjectPtr != IntPtr.Zero ? MarshallingHelpers.FromPointer<T>(updateObjectPtr)! : null;
            return result;
        }
    }

    public T BeginDraw<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in Rectangle updateRect, out Point offset) where T : ComObject
    {
        RawRect updateRectRaw = updateRect;
        IntPtr updateObjectPtr = IntPtr.Zero;

        fixed (Point* offsetPtr = &offset)
        {
            Guid iid = typeof(T).GUID;
            Result result = ((delegate* unmanaged<IntPtr, RawRect, void*, void*, Point*, int>)this[3])(NativePointer, updateRectRaw, &iid, &updateObjectPtr, offsetPtr);
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

    public Result Scroll(Rectangle clipRect, int offsetX, int offsetY)
    {
        RawRect clipRectRaw = clipRect;
        return ((delegate* unmanaged<IntPtr, RawRect*, RawRect*, int, int, int>)this[6])(NativePointer, null, &clipRectRaw, offsetX, offsetY);
    }

    public Result Scroll(Rectangle scrollRect, Rectangle clipRect, int offsetX, int offsetY)
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
