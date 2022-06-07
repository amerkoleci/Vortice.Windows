// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Dxc;

public unsafe partial class IDxcBlob
{
    public Span<byte> AsByte()
    {
        return new Span<byte>((byte*)GetBufferPointer(), GetBufferSize());
    }

    public byte[] ToArray()
    {
        byte[] result = new byte[GetBufferSize()];
        fixed (void* dstPtr = result)
        {
            Unsafe.CopyBlockUnaligned(dstPtr, (void*)GetBufferPointer(), (uint)result.Length);
            return result;
        }
    }
}
