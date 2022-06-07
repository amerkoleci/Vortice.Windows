﻿// Copyright © Amer Koleci and Contributors.
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
        var stringHandlesPtr = (IntPtr*)Marshal.AllocHGlobal(sizeof(IntPtr) * count);

        // Store the pointer to the string.
        for (int i = 0; i < count; i++)
        {
            stringHandlesPtr[i] = Marshal.StringToHGlobalUni(values[i]);
        }

        return stringHandlesPtr;
    }

    public static void Free(IntPtr pointer) => Marshal.FreeHGlobal(pointer);

    public static void Free(void* pointer) => Free(new IntPtr(pointer));
}
