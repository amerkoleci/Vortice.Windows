// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public partial class IDxcVersionInfo3
{
    public unsafe Result GetCustomVersionString(out string? versionString)
    {
        sbyte* ptr = default;
        Result result = GetCustomVersionString(&ptr);
        versionString = new string(ptr);
        Marshal.FreeCoTaskMem(new IntPtr(ptr));
        return result;
    }

    public unsafe string GetCustomVersionString()
    {
        sbyte* ptr = default;
        GetCustomVersionString(&ptr).CheckError();
        string versionString = new string(ptr);
        Marshal.FreeCoTaskMem(new IntPtr(ptr));
        return versionString;
    }

    private unsafe Result GetCustomVersionString(sbyte** versionString)
    {
        return (Result)((delegate* unmanaged[Stdcall]<nint, sbyte**, int>)this[3])(NativePointer, versionString);
    }
}
