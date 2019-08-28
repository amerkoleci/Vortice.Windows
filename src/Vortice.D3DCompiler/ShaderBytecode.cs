// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using Vortice.DirectX.Direct3D;

namespace Vortice.D3DCompiler
{
    public readonly struct ShaderBytecode
    {
        public readonly byte[] Data;

        /// <summary>
        /// Initialize new instance of <see cref="ShaderBytecode"/> struct.
        /// </summary>
        /// <param name="data">The bytecode data.</param>
        public ShaderBytecode(byte[] data)
        {
            Data = data;
        }

        public ShaderBytecode(IntPtr bytecode, PointerSize length)
        {
            Data = new byte[length];
            MemoryHelpers.Read(bytecode, Data, 0, Data.Length);
        }

        public ShaderBytecode(Blob blob)
        {
            Data = new byte[blob.BufferSize];
            MemoryHelpers.Read(blob.BufferPointer, Data, 0, Data.Length);
        }
    }
}
