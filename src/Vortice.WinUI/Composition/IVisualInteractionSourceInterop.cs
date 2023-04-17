// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Win32;

namespace Vortice.WinUI.Composition;

[Guid("AA170AEE-01D7-4954-89D2-8554415D6946")]
public partial class IVisualInteractionSourceInterop : ComObject
{
    public IVisualInteractionSourceInterop(IntPtr nativePtr) : base(nativePtr)
    {
    }

    public static explicit operator IVisualInteractionSourceInterop?(IntPtr nativePtr) => nativePtr == IntPtr.Zero ? null : new IVisualInteractionSourceInterop(nativePtr);

    public unsafe Result TryRedirectForManipulation(ref PointerInfo pointerInfo)
    {
        return ((delegate* unmanaged<IntPtr, ref PointerInfo, int>)this[3])(NativePointer, ref pointerInfo);
    }
}
