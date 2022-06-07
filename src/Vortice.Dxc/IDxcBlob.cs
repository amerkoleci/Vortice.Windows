// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

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
