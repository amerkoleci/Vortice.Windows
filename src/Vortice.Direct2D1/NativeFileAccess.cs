// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Win32;

[Flags]
internal enum NativeFileAccess : uint
{
    None = 0,
    GenericRead = 0x80000000u,
    GenericWrite = 0x40000000u,
}

internal static class NativeFileAccessExtensions
{
    public static NativeFileAccess ToNative(this FileAccess access)
    {
        switch (access)
        {
            case FileAccess.Read:
                return NativeFileAccess.GenericRead;

            case FileAccess.Write:
                return NativeFileAccess.GenericWrite;

            case FileAccess.ReadWrite:
                return NativeFileAccess.GenericRead | NativeFileAccess.GenericWrite;
            default:
                return NativeFileAccess.None;
        }
    }
}
