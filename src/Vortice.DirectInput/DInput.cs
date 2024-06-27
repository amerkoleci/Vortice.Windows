// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DirectInput;

public static partial class DInput
{
    public static Result DirectInput8Create(IntPtr hinstance, out IDirectInput8? directInput)
    {
        Result result = DirectInput8Create(hinstance, SdkVersion, typeof(IDirectInput8).GUID, out IntPtr nativePtr, null);
        if (result.Failure)
        {
            directInput = default;
            return result;
        }

        directInput = new IDirectInput8(nativePtr);
        return result;
    }

    public static IDirectInput8 DirectInput8Create(IntPtr hinstance)
    {
        DirectInput8Create(hinstance, SdkVersion, typeof(IDirectInput8).GUID, out IntPtr nativePtr, null).CheckError();
        return new IDirectInput8(nativePtr);
    }

    public static Result DirectInput8Create(out IDirectInput8? directInput)
    {
        Result result = DirectInput8Create(GetModuleHandle(null), SdkVersion, typeof(IDirectInput8).GUID, out IntPtr nativePtr, null);
        if (result.Failure)
        {
            directInput = default;
            return result;
        }

        directInput = new IDirectInput8(nativePtr);
        return result;
    }

    public static IDirectInput8 DirectInput8Create()
    {
        DirectInput8Create(GetModuleHandle(null), SdkVersion, typeof(IDirectInput8).GUID, out IntPtr nativePtr, null).CheckError();
        return new IDirectInput8(nativePtr);
    }

    [DllImport("kernel32.dll", EntryPoint = "GetModuleHandleW", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string? moduleName);
}
