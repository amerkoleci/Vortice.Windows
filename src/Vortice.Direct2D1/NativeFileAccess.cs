// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.IO;

namespace Vortice.DirectX
{
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
}
