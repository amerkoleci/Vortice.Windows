// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;

namespace Vortice.WinUI.Composition;

[Guid("D4B71A65-3052-4ABE-9183-E98DE02A41A9")]
public unsafe partial class ICompositionDrawingSurfaceInterop2 : ComObject
{
    public ICompositionDrawingSurfaceInterop2(IntPtr nativePtr) : base(nativePtr)
    {
    }

    public static explicit operator ICompositionDrawingSurfaceInterop2?(IntPtr nativePtr) => nativePtr == IntPtr.Zero ? null : new(nativePtr);

    public Result CopySurface(IUnknown destinationResource, int destinationOffsetX, int destinationOffsetY)
    {
        IntPtr destinationResourcePtr = MarshallingHelpers.ToCallbackPtr<IUnknown>(destinationResource);
        return CopySurface(destinationResourcePtr, destinationOffsetX, destinationOffsetY, null);
    }

    public Result CopySurface(IUnknown destinationResource, int destinationOffsetX, int destinationOffsetY, in Rectangle sourceRectangle)
    {
        IntPtr destinationResourcePtr = MarshallingHelpers.ToCallbackPtr<IUnknown>(destinationResource);
        RawRect sourceRectangleRaw = sourceRectangle;
        return CopySurface(destinationResourcePtr, destinationOffsetX, destinationOffsetY, &sourceRectangleRaw);
    }

    private Result CopySurface(IntPtr destinationResource, int destinationOffsetX, int destinationOffsetY, RawRect* sourceRectangle)
    {
        return ((delegate* unmanaged<IntPtr, IntPtr, int, int, RawRect *, int>)this[3])(NativePointer, destinationResource, destinationOffsetX, destinationOffsetY, sourceRectangle);
    }
}
