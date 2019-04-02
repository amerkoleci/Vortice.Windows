// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpDXGI;
using SharpGen.Runtime;

namespace SharpDirect3D12
{
    /// <summary>
    /// Describes the input-buffer data for the input-assembler stage.
    /// </summary>
    public partial class InputLayoutDescription
    {
        public InputLayoutDescription() { }

        public InputLayoutDescription(params InputElementDescription[] elements)
        {
            Elements = elements;
        }

        /// <summary>	
        /// An array of <see cref="InputElementDescription"/> that describe the data types of the input-assembler stage.
        /// </summary>	
        public InputElementDescription[] Elements { get; set; }

        /// <summary>
        /// Implicitely converts to an <see cref="InputLayoutDescription"/> from an array of <see cref="InputElementDescription"/>
        /// </summary>
        /// <param name="elements">Array of <see cref="InputElementDescription"/>.</param>
        public static implicit operator InputLayoutDescription(InputElementDescription[] elements)
        {
            return new InputLayoutDescription(elements);
        }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal unsafe struct __Native
        {
            public InputElementDescription.__Native* pInputElementDescs;

            public int NumElements;

            internal void __MarshalFree()
            {
                //if (InputElementsPointer != IntPtr.Zero)
                //{
                //    for (int i = 0; i < NumElements; i++)
                //    {
                //        elements[i].__MarshalFree(ref nativeElements[i]);
                //    }

                //    Marshal.FreeHGlobal(InputElementsPointer);
                //}
            }
        }

        internal void __MarshalFree(ref __Native @ref)
        {
            @ref.__MarshalFree();
        }

        internal void __MarshalFrom(ref __Native @ref)
        {
            Elements = new InputElementDescription[@ref.NumElements];
            if (@ref.NumElements > 0)
            {
                //Interop.Read(@ref.PCategoryList, Categories);
            }
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.NumElements = Elements?.Length ?? 0;
            if (@ref.NumElements > 0)
            {
                var nativeElements = (InputElementDescription.__Native*)Interop.Alloc<InputElementDescription.__Native>(@ref.NumElements);
                for (int i = 0; i < @ref.NumElements; i++)
                {
                    Elements[i].__MarshalTo(ref nativeElements[i]);
                }

                @ref.pInputElementDescs = nativeElements;
            }
        }
        #endregion
    }
}
