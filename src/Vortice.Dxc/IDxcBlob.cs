// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Dxc;

public unsafe partial class IDxcBlob
{
    public byte[] AsBytes()
    {
        byte[] result = new byte[GetBufferSize()];
        fixed (void* dstPtr = result)
        {
            Unsafe.CopyBlockUnaligned(dstPtr, (void*)GetBufferPointer(), (uint)result.Length);
            return result;
        }
    }

    public ReadOnlySpan<byte> AsSpan()
    {
        return new ReadOnlySpan<byte>(GetBufferPointer().ToPointer(), (int)GetBufferSize());
    }

    public ReadOnlyMemory<byte> AsMemory()
    {
        return new ReadOnlySpan<byte>(GetBufferPointer().ToPointer(), (int)GetBufferSize()).ToArray();
    }
}
