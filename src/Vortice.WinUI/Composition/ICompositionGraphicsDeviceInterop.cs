// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WinUI.Composition;

[Guid("4AFA8030-BC70-4B0C-B1C7-6E69F933DC83")]
public partial class ICompositionGraphicsDeviceInterop : ComObject
{
    public ICompositionGraphicsDeviceInterop(IntPtr nativePtr) : base(nativePtr)
    {
    }

    public static explicit operator ICompositionGraphicsDeviceInterop?(IntPtr nativePtr) => nativePtr == IntPtr.Zero ? null : new(nativePtr);

    public unsafe Result GetRenderingDevice<T>(out T? device) where T : ComObject
    {
        IntPtr devicePtr = default;
        Result result = (Result)((delegate* unmanaged<IntPtr, void*, int>)this[3])(NativePointer, &devicePtr);

        if (result.Failure)
        {
            device = default;
            return result;
        }

        device = MarshallingHelpers.FromPointer<T>(devicePtr);
        return result;
    }

    public unsafe Result SetRenderingDevice(IUnknown device) 
    {
        IntPtr devicePtr = MarshallingHelpers.ToCallbackPtr<IUnknown>(device);
        return (Result)((delegate* unmanaged<IntPtr, IntPtr, int>)this[4])(NativePointer, devicePtr);
    }
}
