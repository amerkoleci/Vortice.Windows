// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

#if WINDOWS
using System;
using Microsoft.UI.Composition;
using Microsoft.UI.Input;

namespace Vortice.WinUI.Input;

[Guid("FAB19398-6D19-4D8A-B752-8F096C396069")]
public partial class IInputCursorStaticsInterop : WinRTObject
{
    public IInputCursorStaticsInterop(IntPtr nativePtr) : base(nativePtr)
    {
    }

    public static explicit operator IInputCursorStaticsInterop?(IntPtr nativePtr) => nativePtr == IntPtr.Zero ? null : new IInputCursorStaticsInterop(nativePtr);

    public unsafe Result CreateFromHCursor(nint cursor, out InputCursor? resultCursor)
    {
        IntPtr outDevice = default;
        Result result = ((delegate* unmanaged[Stdcall]<IntPtr, nint, IntPtr*, int>)this[6])(NativePointer, cursor, &outDevice);
        if (result.Failure)
        {
            resultCursor = default;
            return result;
        }

        resultCursor = InputCursor.FromAbi(outDevice);
        return result;
    }
}

#endif
