// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public partial class IDxcVersionInfo2
{
    public unsafe Result GetCommitInfo(out uint commitCount, out string? commitHash)
    {
        Result result = GetCommitInfo(out commitCount, out nint commitHashPtr);
        commitHash = Marshal.PtrToStringUTF8(commitHashPtr);
        Marshal.FreeCoTaskMem(commitHashPtr);
        return result;
    }

}
