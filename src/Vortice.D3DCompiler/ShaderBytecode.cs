// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using Vortice.Direct3D;

namespace Vortice.D3DCompiler;

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

    /// <summary>
    /// Initialize new instance of <see cref="ShaderBytecode"/> struct.
    /// </summary>
    /// <param name="data">The stream containing the compiled bytecode.</param>
    public ShaderBytecode(Stream data)
    {
        int size = (int)(data.Length - data.Position);

        byte[] localBuffer = new byte[size];
        data.Read(localBuffer, 0, size);
        Data = localBuffer;
    }
}
