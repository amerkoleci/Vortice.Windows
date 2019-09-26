// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Direct2D1
{
    /// <summary>
    /// Defines the properties of a vertex buffer that are standard for all vertex shader definitions.
    /// </summary>
    public partial class VertexBufferProperties
    {
        public VertexBufferProperties(
            int inputCount,
            VertexUsage usage,
            byte[] data,
            int byteWidth)
        {
            InputCount = inputCount;
            Usage = usage;
            Data = (byte[])data.Clone();
            ByteWidth = byteWidth;
        }

        /// <summary>
        /// The number of inputs to the vertex shader.
        /// </summary>
        public int InputCount { get; set; }

        /// <summary>
        /// Indicates how frequently the vertex buffer is likely to be updated.
        /// </summary>
        public VertexUsage Usage { get; set; }

        /// <summary>
        /// The initial contents of the vertex buffer.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// The size of the vertex buffer, in bytes.
        /// </summary>
        public int ByteWidth { get; set; }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal unsafe struct __Native
        {
            public int inputCount;
            public VertexUsage usage;
            public IntPtr data;
            public int byteWidth;
        }

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            if (@ref.data != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(@ref.data);
            }
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.inputCount = InputCount;
            @ref.usage = Usage;
            if (Data?.Length > 0)
            {
                @ref.data = Interop.AllocToPointer(Data);
            }
            else
            {
                @ref.data = IntPtr.Zero;
            }
            @ref.byteWidth = ByteWidth;
        }
        #endregion
    }
}
