// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes the arguments (parameters) of a command signature.
    /// </summary>
    public partial class CommandSignatureDescription
    {
        public CommandSignatureDescription(int byteStride, IndirectArgumentDescription[] indirectArguments, int nodeMask = 0)
        {
            ByteStride = byteStride;
            IndirectArguments = indirectArguments;
            NodeMask = nodeMask;
        }

        public CommandSignatureDescription(params IndirectArgumentDescription[] indirectArguments)
        {
            IndirectArguments = indirectArguments;
        }

        public int ByteStride { get; set; }

        /// <summary>	
        /// An array of <see cref="InputElementDescription"/> that describe the command signature.
        /// </summary>	
        public IndirectArgumentDescription[] IndirectArguments { get; set; }

        public int NodeMask { get; set; }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal unsafe struct __Native
        {
            public int ByteStride;
            public int NumArgumentDescs;
            public IntPtr pArgumentDescs;
            public int NodeMask;
        }

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            if (@ref.pArgumentDescs != null)
            {
                Marshal.FreeHGlobal(@ref.pArgumentDescs);
            }
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.ByteStride = ByteStride;
            @ref.NumArgumentDescs = IndirectArguments?.Length ?? 0;
            if (@ref.NumArgumentDescs > 0)
            {
                @ref.pArgumentDescs = UnsafeUtilities.Alloc<IndirectArgumentDescription>(@ref.NumArgumentDescs);
                fixed (void* indirectArgumentsPtr = &IndirectArguments![0])
                {
                    MemoryHelpers.CopyMemory(
                        @ref.pArgumentDescs,
                        (IntPtr)indirectArgumentsPtr,
                        @ref.NumArgumentDescs * sizeof(IndirectArgumentDescription));
                }
            }
            @ref.NodeMask = NodeMask;
        }
        #endregion
    }
}
