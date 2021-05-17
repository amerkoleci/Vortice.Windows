// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// A state subobject describing an existing collection that can be included in a state object.
    /// </summary>
    public partial class ExistingCollectionDescription : IStateSubObjectDescription, IStateSubObjectDescriptionMarshal
    {
        StateSubObjectType IStateSubObjectDescription.SubObjectType => StateSubObjectType.ExistingCollection;

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

        unsafe IntPtr IStateSubObjectDescriptionMarshal.__MarshalAlloc(Dictionary<StateSubObject, IntPtr> subObjectLookup)
        {
            __Native* native = (__Native*)Marshal.AllocHGlobal(sizeof(__Native));

            native->pExistingCollection = MarshallingHelpers.ToCallbackPtr<ID3D12StateObject>(ExistingCollection);
            native->NumExports = Exports?.Length ?? 0;
            if (native->NumExports > 0)
            {
                var nativeExports = (ExportDescription.__Native*)UnsafeUtilities.Alloc<ExportDescription.__Native>(native->NumExports);
                for (int i = 0; i < native->NumExports; i++)
                {
                    Exports![i].__MarshalTo(ref nativeExports[i]);
                }

                native->pExports = nativeExports;
            }

            return (IntPtr)native;
        }

        unsafe void IStateSubObjectDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
        {
            GC.KeepAlive(ExistingCollection);
            ref __Native nativeLibrary = ref Unsafe.AsRef<__Native>(pDesc.ToPointer());

            if (nativeLibrary.pExports != null)
            {
                for (int i = 0; i < nativeLibrary.NumExports; i++)
                {
                    Exports[i].__MarshalFree(ref nativeLibrary.pExports[i]);
                }

                Marshal.FreeHGlobal((IntPtr)nativeLibrary.pExports);
            }

            Marshal.FreeHGlobal(pDesc);
        }
        #endregion
    }
}
