// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// This subobject is unsupported in the current release.
    /// </summary>
    public partial class DxilSubObjectToExportsAssociation : IStateSubObjectDescription, IStateSubObjectDescriptionMarshal
    {
        StateSubObjectType IStateSubObjectDescription.SubObjectType => StateSubObjectType.DxilSubObjectToExportsAssociation;

        public DxilSubObjectToExportsAssociation(string subObjectToAssociate, params string[] exports)
        {
            SubObjectToAssociate = subObjectToAssociate;
            Exports = exports;
        }

        /// <summary>
        /// Size of the pExports array. If 0, this is being explicitly defined as a default association. Another way to define a default association is to omit this subobject association for that subobject completely.
        /// </summary>
        public string SubObjectToAssociate { get; private set; }

        /// <summary>	
        /// The array of exports with which the subobject is associated.
        /// </summary>	
        public string[] Exports { get; private set; }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal unsafe struct __Native
        {
            public IntPtr pSubobjectToAssociate;
            public int NumExports;
            public IntPtr* pExports;
        }


        unsafe IntPtr IStateSubObjectDescriptionMarshal.__MarshalAlloc()
        {
            __Native* native = (__Native*)Marshal.AllocHGlobal(sizeof(__Native));
            native->pSubobjectToAssociate = Marshal.StringToHGlobalUni(SubObjectToAssociate);
            native->NumExports = Exports?.Length ?? 0;
            if (native->NumExports > 0)
            {
                var nativeExports = (IntPtr*)Marshal.AllocHGlobal(IntPtr.Size * native->NumExports);
                for (int i = 0; i < native->NumExports; i++)
                {
                    nativeExports[i] = Marshal.StringToHGlobalUni(Exports[i]);
                }

                native->pExports = nativeExports;
            }

            return (IntPtr)native;
        }

        unsafe void IStateSubObjectDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
        {
            ref __Native native = ref Unsafe.AsRef<__Native>(pDesc.ToPointer());
            Marshal.FreeHGlobal(native.pSubobjectToAssociate);

            if (native.NumExports > 0)
            {
                for (int i = 0; i < native.NumExports; i++)
                {
                    Marshal.FreeHGlobal(native.pExports[i]);
                }

                Marshal.FreeHGlobal(new IntPtr(native.pExports));
            }

            Marshal.FreeHGlobal(pDesc);
        }
        #endregion
    }
}
