// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D;

public unsafe partial class Blob
{
    public string AsString()
    {
        return Marshal.PtrToStringAnsi(BufferPointer);
    }

    public byte[] AsBytes()
    {
        byte[] result = new byte[GetBufferSize()];
        fixed (byte* pResult = result)
        {
            Unsafe.CopyBlockUnaligned(pResult, (void*)GetBufferPointer(), (uint)result.Length);
        }

        return result;
    }

    public Span<byte> AsSpan()
    {
        Span<byte> result = new byte[GetBufferSize()];
        fixed (byte* pResult = result)
        {
            Unsafe.CopyBlockUnaligned(pResult, (void*)GetBufferPointer(), (uint)result.Length);
        }

        return result;
    }

    
}
