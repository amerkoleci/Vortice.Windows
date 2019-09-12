// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.DirectX;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// A state subobject describing an existing collection that can be included in a state object.
    /// </summary>
    public partial class ExistingCollectionDescription
    {
        public ExistingCollectionDescription(ID3D12StateObject existingCollection, params ExportDescription[] exports)
        {
            ExistingCollection = existingCollection;
            Exports = exports;
        }

        /// <summary>
        /// The collection to include in a state object. The enclosing state object holds a reference to the existing collection.
        /// </summary>
        public ID3D12StateObject ExistingCollection { get; private set; }

        /// <summary>	
        /// Optional exports array. For more information, see <see cref="ExportDescription"/>.
        /// </summary>	
        public ExportDescription[] Exports { get; private set; }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal unsafe struct __Native
        {
            public IntPtr pExistingCollection;
            public int NumExports;
            public ExportDescription.__Native* pExports;
        }

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            GC.KeepAlive(ExistingCollection);

            if (@ref.NumExports > 0)
            {
                for (int i = 0; i < @ref.NumExports; i++)
                {
                    Exports[i].__MarshalFree(ref @ref.pExports[i]);
                }

                Marshal.FreeHGlobal((IntPtr)@ref.pExports);
            }
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.pExistingCollection = CppObject.ToCallbackPtr<ID3D12StateObject>(ExistingCollection);
            @ref.NumExports = Exports?.Length ?? 0;
            if (@ref.NumExports > 0)
            {
                var nativeExports = (ExportDescription.__Native*)Interop.Alloc<ExportDescription.__Native>(@ref.NumExports);
                for (int i = 0; i < @ref.NumExports; i++)
                {
                    Exports[i].__MarshalTo(ref nativeExports[i]);
                }

                @ref.pExports = nativeExports;
            }
        }
        #endregion
    }
}
