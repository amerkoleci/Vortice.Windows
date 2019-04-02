// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace SharpDirect3D11.Shader
{
    public partial class ID3D11ShaderReflection
    {
        /// <summary>
        /// Create new instance of <see cref="ID3D11ShaderReflection"/> class.
        /// </summary>
        /// <param name="shaderBytecode">The bytecode data.</param>
        public unsafe ID3D11ShaderReflection(byte[] shaderBytecode)
        {
            Compiler.D3DReflect<ID3D11ShaderReflection>(shaderBytecode, out var nativePtr);
            NativePointer = nativePtr;
        }
    }
}
