// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

#if WINDOWS
using Microsoft.UI.Input;

namespace Vortice.WinUI.Input;

[Guid("FAB19398-6D19-4D8A-B752-8F096C396069")]
public unsafe partial class IInputCursorStaticsInterop(nint nativePtr) : WinRTObject(nativePtr)
{
    public static explicit operator IInputCursorStaticsInterop?(nint nativePtr) => nativePtr == 0 ? default : new IInputCursorStaticsInterop(nativePtr);

    public Result CreateFromHCursor(nint cursor, out InputCursor? resultCursor)
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
