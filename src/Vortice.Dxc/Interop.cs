// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

internal unsafe class Interop
{
    public static unsafe IntPtr* AllocToPointers(string[] values, int count = 0)
    {
        if (values == null || values.Length == 0)
            return null;

        if (count == 0)
            count = values.Length;

        // Allocate unmanaged memory for string pointers.
        var stringHandlesPtr = (IntPtr*)NativeMemory.Alloc((nuint)(sizeof(IntPtr) * count));

        // Store the pointer to the string.
        for (int i = 0; i < count; i++)
        {
            stringHandlesPtr[i] = Marshal.StringToHGlobalUni(values[i]);
        }

        return stringHandlesPtr;
    }
}
