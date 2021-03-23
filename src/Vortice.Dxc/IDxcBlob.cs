// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace Vortice.Dxc
{
    public partial class IDxcBlob
    {
        public unsafe Span<byte> AsByte()
        {
            return new Span<byte>((byte*)GetBufferPointer(), GetBufferSize());
        }

        public unsafe byte[] ToArray()
        {
            byte[] result = new byte[GetBufferSize()];
            fixed (void* dstPtr = result)
            {
                Unsafe.CopyBlockUnaligned(dstPtr, (void*)GetBufferPointer(), (uint)result.Length);
                return result;
            }
        }
    }
}
