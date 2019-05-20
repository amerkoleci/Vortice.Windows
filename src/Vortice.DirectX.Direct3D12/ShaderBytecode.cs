// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DirectX.Direct3D12
{
    /// <summary>
    /// Describes shader data.
    /// </summary>
    public partial class ShaderBytecode
    {
        public byte[] Data { get; set; }

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
            Interop.Read(bytecode, Data);
        }

        public static implicit operator ShaderBytecode(byte[] buffer)
        {
            return new ShaderBytecode(buffer);
        }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal struct __Native
        {
            public IntPtr Bytecode;

            public PointerSize Length;

            internal void __MarshalFree()
            {
                if (Bytecode != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(Bytecode);
                }
            }
        }

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            @ref.__MarshalFree();
        }

        internal unsafe void __MarshalFrom(ref __Native @ref)
        {
            Data = new byte[@ref.Length];
            if (@ref.Length > 0)
            {
                Interop.Read(@ref.Bytecode, Data);
            }
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            if (Data?.Length > 0)
            {
                @ref.Bytecode = Interop.AllocToPointer(Data);
                @ref.Length = Data.Length;
            }
            else
            {
                @ref.Bytecode = IntPtr.Zero;
                @ref.Length = 0;
            }
        }
        #endregion
    }
}
