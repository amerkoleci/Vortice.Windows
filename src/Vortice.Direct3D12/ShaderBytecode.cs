// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using static Vortice.UnsafeUtilities;
using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

/// <summary>
/// Describes shader data.
/// </summary>
public partial class ShaderBytecode
{
    public byte[]? Data { get; set; }

    public ShaderBytecode()
    {
    }

    public ShaderBytecode(byte[] data)
    {
        Data = data;
    }

    public ShaderBytecode(IntPtr bytecode, PointerSize length)
    {
        Data = new byte[length];
        UnsafeUtilities.Read(bytecode, Data);
    }

    public static implicit operator ShaderBytecode(byte[] buffer)
    {
        return new ShaderBytecode(buffer);
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe struct __Native
    {
        public void* pShaderBytecode;

        public nuint BytecodeLength;

        internal void __MarshalFree()
        {
            if (pShaderBytecode != null)
            {
                NativeMemory.Free(pShaderBytecode);
            }
        }
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        if (Data?.Length > 0)
        {
            @ref.pShaderBytecode = AllocWithData(Data);
            @ref.BytecodeLength = (uint)Data.Length;
        }
        else
        {
            @ref.pShaderBytecode = null;
            @ref.BytecodeLength = 0;
        }
    }
    #endregion
}
