// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Vortice.DirectX;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes a DXIL library state subobject that can be included in a state object.
    /// </summary>
    public partial class DxilLibraryDescription : IStateSubObjectDescription, IStateSubObjectDescriptionMarshal
    {
        StateSubObjectType IStateSubObjectDescription.SubObjectType => StateSubObjectType.DxilLibrary;

        /// <summary>
        /// Initializes a new instance of the <see cref="DxilLibraryDescription"/> class.
        /// </summary>
        /// <param name="dxilLibrary">The library to include in the state object. Must have been compiled with library target 6.3 or higher. It is fine to specify the same library multiple times either in the same state object / collection or across multiple, as long as the names exported each time don’t conflict in a given state object.</param>
        /// <param name="exports">Optional exports. For more information, see <see cref="ExportDescription"/>.</param>
        public DxilLibraryDescription(ShaderBytecode dxilLibrary, params ExportDescription[] exports)
        {
            DxilLibrary = dxilLibrary;
            Exports = exports;
        }

        /// <summary>
        /// >The library to include in the state object.
        /// </summary>
        public ShaderBytecode DxilLibrary { get; private set; }

        /// <summary>	
        /// Optional exports.
        /// </summary>	
        public ExportDescription[] Exports { get; private set; }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal unsafe struct __Native
        {
            public ShaderBytecode.__Native DXILLibrary;
            public int NumExports;
            public ExportDescription.__Native* pExports;
        }

        unsafe IntPtr IStateSubObjectDescriptionMarshal.__MarshalAlloc(Dictionary<StateSubObject, IntPtr> subObjectLookup)
        {
            __Native* native = (__Native*)Marshal.AllocHGlobal(sizeof(__Native));

            DxilLibrary.__MarshalTo(ref native->DXILLibrary);
            native->NumExports = Exports?.Length ?? 0;
            if (native->NumExports > 0)
            {
                var nativeExports = (ExportDescription.__Native*)Interop.Alloc<ExportDescription.__Native>(native->NumExports);
                for (int i = 0; i < native->NumExports; i++)
                {
                    Exports[i].__MarshalTo(ref nativeExports[i]);
                }

                native->pExports = nativeExports;
            }

            return (IntPtr)native;
        }

        unsafe void IStateSubObjectDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
        {
            ref __Native nativeLibrary = ref Unsafe.AsRef<__Native>(pDesc.ToPointer());
            DxilLibrary.__MarshalFree(ref nativeLibrary.DXILLibrary);

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
