// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.Dxc
{
    public partial class IDxcBlob
    {
        public unsafe Span<byte> AsByte()
        {
            return new Span<byte>((byte*)GetBufferPointer(), GetBufferSize());
        }
    }
}
