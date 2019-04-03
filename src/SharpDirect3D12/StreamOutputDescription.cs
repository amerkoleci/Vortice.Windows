// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpDXGI;
using SharpGen.Runtime;

namespace SharpDirect3D12
{
    /// <summary>
    /// Describes a streaming output buffer.
    /// </summary>
    public partial class StreamOutputDescription
    {
        public StreamOutputDescription() { }

        public StreamOutputDescription(params StreamOutputElement[] elements)
        {
            Elements = elements;
        }

        /// <summary>	
        /// An array of <see cref="StreamOutputElement"/>.
        /// </summary>	
        public StreamOutputElement[] Elements { get; set; }

        /// <summary>
        /// An array of buffer strides; each stride is the size of an element for that buffer.
        /// </summary>
        public int[] Strides { get; set; }

        /// <summary>
        /// The index number of the stream to be sent to the rasterizer stage.
        /// </summary>
        public int RasterizedStream { get; set; }

        /// <summary>
        /// Implicitely converts to an <see cref="StreamOutputDescription"/> from an array of <see cref="StreamOutputElement"/>
        /// </summary>
        /// <param name="elements">Array of <see cref="StreamOutputElement"/>.</param>
        public static implicit operator StreamOutputDescription(StreamOutputElement[] elements)
        {
            return new StreamOutputDescription(elements);
        }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal unsafe struct __Native
        {
            public StreamOutputElement.__Native* pSODeclaration;
            public int NumEntries;
            public IntPtr pBufferStrides;
            public int NumStrides;
            public int RasterizedStream;
        }

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            if (@ref.pSODeclaration != null)
            {
                for (int i = 0; i < @ref.NumEntries; i++)
                {
                    Elements[i].__MarshalFree(ref @ref.pSODeclaration[i]);
                }

                Marshal.FreeHGlobal((IntPtr)@ref.pSODeclaration);
            }

            if (@ref.pBufferStrides != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(@ref.pBufferStrides);
            }
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.NumEntries = Elements?.Length ?? 0;
            if (@ref.NumEntries > 0)
            {
                var nativeElements = (StreamOutputElement.__Native*)Interop.Alloc<StreamOutputElement.__Native>(@ref.NumEntries);
                for (int i = 0; i < @ref.NumEntries; i++)
                {
                    Elements[i].__MarshalTo(ref nativeElements[i]);
                }

                @ref.pSODeclaration = nativeElements;
            }

            @ref.NumStrides = Strides?.Length ?? 0;
            if (@ref.NumStrides > 0)
            {
                var nativeStrides = Interop.Alloc<int>(@ref.NumStrides);
                fixed (int* src = &Strides[0])
                {
                    MemoryHelpers.CopyMemory(nativeStrides, (IntPtr)src, @ref.NumStrides * sizeof(int));
                }

                @ref.pBufferStrides = nativeStrides;
            }

            @ref.RasterizedStream = RasterizedStream;
        }
        #endregion
    }
}
