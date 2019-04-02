// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace SharpDirect3D12.Shader
{
    public partial class ID3D12ShaderReflection
    {
        /// <summary>
        /// Create new instance of <see cref="ID3D12ShaderReflection"/> class.
        /// </summary>
        /// <param name="shaderBytecode">The bytecode data.</param>
        public unsafe ID3D12ShaderReflection(byte[] shaderBytecode)
        {
            Compiler.D3DReflect<ID3D12ShaderReflection>(shaderBytecode, out var nativePtr);
            NativePointer = nativePtr;
        }
    }
}
