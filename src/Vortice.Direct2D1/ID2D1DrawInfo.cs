using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Vortice.Direct2D1
{
    partial class ID2D1DrawInfo
    {
        public unsafe void SetPixelShaderConstantBuffer<T>(ref T constants) where T : unmanaged
        {
            var size = sizeof(T);
            fixed (T* p = &constants)
            {
                SetPixelShaderConstantBuffer(new IntPtr(p), size);
            }
        }
    }
}
