// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Dxc;

public unsafe partial class IDxcBlob
{
    private nuint BufferSize { get => GetBufferSize(); }

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
        nuint bufferSize = GetBufferSize();
        return new ReadOnlySpan<byte>(GetBufferPointer().ToPointer(), (int)bufferSize);
    }

    public ReadOnlyMemory<byte> AsMemory()
    {
        nuint bufferSize = GetBufferSize();
        return new ReadOnlySpan<byte>(GetBufferPointer().ToPointer(), (int)bufferSize).ToArray();
    }
}
