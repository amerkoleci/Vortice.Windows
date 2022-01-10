// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D;

public unsafe partial class Blob
{
    public byte[] GetBytes()
    {
        byte[] result = new byte[GetBufferSize()];
        fixed (byte* pResult = result)
        {
            Unsafe.CopyBlockUnaligned(pResult, (void*)GetBufferPointer(), (uint)result.Length);
        }

        return result;
    }

    public string ConvertToString()
    {
        return Marshal.PtrToStringAnsi(BufferPointer);
    }
}
