// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace SharpDirect3D11
{
    [Flags]
    public enum SharedResourceFlags : uint
    {
        None = 0,
        Write = 1,
        Read = 0x80000000,
        GenericWrite = 0x40000000,
        GenericRead = 0x80000000,
        GenericExecute = 0x20000000,
        GenericAll = 0x10000000
    }
}
