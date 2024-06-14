// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

#if WINDOWS
using Microsoft.UI.Composition;

namespace Vortice.WinUI.Composition;

[Guid("FAB19398-6D19-4D8A-B752-8F096C396069")]
public partial class ICompositorInterop(nint nativePtr) : ComObject(nativePtr)
{
    public static explicit operator ICompositorInterop?(nint nativePtr) => nativePtr == 0 ? null : new ICompositorInterop(nativePtr);

    public unsafe Result CreateGraphicsDevice(IUnknown device, out CompositionGraphicsDevice? resultDevice)
    {
        nint devicePtr = MarshallingHelpers.ToCallbackPtr<IUnknown>(device);
        nint outDevice = default;
        Result result = ((delegate* unmanaged[Stdcall]<nint, void*, nint*, int>)this[3])(NativePointer, (void*)devicePtr, &outDevice);
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
