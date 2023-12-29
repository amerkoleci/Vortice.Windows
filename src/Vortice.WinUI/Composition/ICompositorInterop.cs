// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

#if WINDOWS
using Microsoft.UI.Composition;

namespace Vortice.WinUI.Composition;

[Guid("FAB19398-6D19-4D8A-B752-8F096C396069")]
public partial class ICompositorInterop : ComObject
{
    public ICompositorInterop(IntPtr nativePtr) : base(nativePtr)
    {
    }

    public static explicit operator ICompositorInterop?(IntPtr nativePtr) => nativePtr == IntPtr.Zero ? null : new ICompositorInterop(nativePtr);

    public unsafe Result CreateGraphicsDevice(IUnknown device, out CompositionGraphicsDevice? resultDevice)
    {
        IntPtr devicePtr = MarshallingHelpers.ToCallbackPtr<IUnknown>(device);
        IntPtr outDevice = default;
        Result result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, IntPtr*, int>)this[3])(NativePointer, (void*)devicePtr, &outDevice);
        if (result.Failure)
        {
            resultDevice = default;
            return result;
        }

        resultDevice = CompositionGraphicsDevice.FromAbi(outDevice);
        return result;
    }
}

#endif
