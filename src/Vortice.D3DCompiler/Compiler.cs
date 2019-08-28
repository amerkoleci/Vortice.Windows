// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.DirectX.Direct3D;

namespace Vortice.D3DCompiler
{
    public unsafe static partial class Compiler
    {
        public static Result Reflect<T>(byte[] shaderBytecode, out T reflection) where T : ComObject
        {
            var interfaceGuid = typeof(T).GUID;
            fixed (void* shaderBytecodePtr = shaderBytecode)
            {
                PointerSize srcDataSize = shaderBytecode.Length;
                var result = Reflect(new IntPtr(shaderBytecodePtr), srcDataSize, interfaceGuid, out var nativePtr);
                if (result.Success)
                {
                    reflection = CppObject.FromPointer<T>(nativePtr);
                    return result;
                }

                reflection = null;
                return result;
            }
        }

        public static ShaderBytecode CompressShaders(params ShaderBytecode[] shaderBytecodes)
        {
            Blob blob = null;
            var shaderData = new ShaderData[shaderBytecodes.Length];
            var handles = new GCHandle[shaderBytecodes.Length];
            try
            {
                for (int i = 0; i < shaderBytecodes.Length; i++)
                {
                    handles[i] = GCHandle.Alloc(shaderBytecodes[i].Data, GCHandleType.Pinned);

                    shaderData[i] = new ShaderData
                    {
                        BytecodePtr = handles[i].AddrOfPinnedObject(),
                        BytecodeLength = shaderBytecodes[i].Data.Length
                    };
                }
                CompressShaders(shaderBytecodes.Length, shaderData, 1, out blob);
            }
            finally
            {
                foreach (var handle in handles)
                {
                    handle.Free();
                }
            }

            if (blob == null)
            {
                return default;
            }

            return new ShaderBytecode(blob);
        }
    }
}
