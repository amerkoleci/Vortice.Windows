// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace SharpDirect3D11
{
    internal unsafe static class Compiler
    {
        public static Result D3DReflect<T>(byte[] shaderBytecode, out IntPtr nativePtr) where T : ComObject
        {
            var interfaceGuid = typeof(T).GUID;
            Result result = Result.Fail;
            fixed (void* shaderBytecodePtr = shaderBytecode)
            {
                PointerSize srcDataSize = shaderBytecode.Length;
                fixed (IntPtr* ptr = &nativePtr)
                {
                    result = D3DReflect(shaderBytecodePtr, srcDataSize, &interfaceGuid, ptr);
                }
            }

            return result;
        }

        [DllImport("d3dcompiler_47.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int D3DReflect(void* pSrcData, void* srcDataSize, void* pInterface, void* ppReflector);
    }
}
