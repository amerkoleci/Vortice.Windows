// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WinUI;

[Guid("e8e84ac7-b7b8-40f4-b033-f877a756c52b")]
public partial class IVirtualSurfaceUpdatesCallbackNative : ComObject
{
    public IVirtualSurfaceUpdatesCallbackNative(nint nativePtr) : base(nativePtr)
    {
    }

    public static explicit operator IVirtualSurfaceUpdatesCallbackNative?(nint nativePtr) => nativePtr == 0 ? default : new IVirtualSurfaceUpdatesCallbackNative(nativePtr);

    public unsafe Result UpdatesNeeded()
    {
        Result result = ((delegate* unmanaged[Stdcall]<nint, int>)this[3])(NativePointer);
        return result;
    }
}
