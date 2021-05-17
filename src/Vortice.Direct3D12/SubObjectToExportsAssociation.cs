// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Associates a subobject defined directly in a state object with shader exports.
    /// </summary>
    public partial class SubObjectToExportsAssociation : IStateSubObjectDescription, IStateSubObjectDescriptionMarshal
    {
        StateSubObjectType IStateSubObjectDescription.SubObjectType => StateSubObjectType.SubObjectToExportsAssociation;

        public SubObjectToExportsAssociation(StateSubObject subObjectToAssociate, params string[] exports)
        {
            SubObjectToAssociate = subObjectToAssociate;
            Exports = exports;
        }

        /// <summary>
        /// The <see cref="StateSubObject"/> in current state object to define an association to.
        /// </summary>
        public StateSubObject SubObjectToAssociate { get; private set; }

        /// <summary>	
        /// An array of exports with which the subobject is associated.
        /// </summary>	
        public string[] Exports { get; private set; }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal unsafe struct __Native
        {
            public StateSubObject.__Native* pSubobjectToAssociate;
            public int NumExports;
            public IntPtr* pExports;
        }

        unsafe IntPtr IStateSubObjectDescriptionMarshal.__MarshalAlloc(Dictionary<StateSubObject, IntPtr> subObjectLookup)
        {
            __Native* native = (__Native*)Marshal.AllocHGlobal(sizeof(__Native));

            if (subObjectLookup.ContainsKey(SubObjectToAssociate) == false)
            {
                throw new InvalidOperationException("Associated StateSubObject must be a reference to an element of the array in StateObjectDescription");
            }

            native->pSubobjectToAssociate = (StateSubObject.__Native*)subObjectLookup[SubObjectToAssociate].ToPointer();

            native->NumExports = Exports?.Length ?? 0;
            if (native->NumExports > 0)
            {
                var nativeExports = (IntPtr*)Marshal.AllocHGlobal(IntPtr.Size * native->NumExports);
                for (int i = 0; i < native->NumExports; i++)
                {
                    nativeExports[i] = Marshal.StringToHGlobalUni(Exports![i]);
                }

                native->pExports = nativeExports;
            }

            return (IntPtr)native;
        }

        unsafe void IStateSubObjectDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
        {
            ref __Native native = ref Unsafe.AsRef<__Native>(pDesc.ToPointer());
            // native->pSubobjectToAssociate will be freed in StateObjectDescription
            native.pSubobjectToAssociate = null;

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
