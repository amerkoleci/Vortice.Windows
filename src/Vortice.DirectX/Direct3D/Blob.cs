// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D
{
    public partial class Blob
    {
        public byte[] GetBytes()
        {
            unsafe
            {
                var result = new byte[GetBufferSize()];
                fixed (byte* pResult = result)
                {
                    Unsafe.CopyBlockUnaligned(pResult, (void*)GetBufferPointer(), (uint)result.Length);
                }

                return result;
            }
        }

        public string ConvertToString()
        {
            return Marshal.PtrToStringAnsi(BufferPointer);
        }
    }
}
