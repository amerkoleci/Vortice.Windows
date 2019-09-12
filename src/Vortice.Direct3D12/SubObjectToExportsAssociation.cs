// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using Vortice.DirectX;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Associates a subobject defined directly in a state object with shader exports.
    /// </summary>
    public partial class SubObjectToExportsAssociation
    {
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
            public IntPtr pSubobjectToAssociate;
            public int NumExports;
            public IntPtr* pExports;
        }

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            SubObjectToAssociate.__MarshalFree();

            if (@ref.NumExports > 0)
            {
                for (int i = 0; i < @ref.NumExports; i++)
                {
                    Marshal.FreeHGlobal(@ref.pExports[i]);
                }

                Marshal.FreeHGlobal(new IntPtr(@ref.pExports));
            }
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            //SubObjectToAssociate.__MarshalTo(ref @ref.psu);
            @ref.NumExports = Exports?.Length ?? 0;
            if (@ref.NumExports > 0)
            {
                var nativeExports = (IntPtr*)Marshal.AllocHGlobal(IntPtr.Size * @ref.NumExports);
                for (int i = 0; i < @ref.NumExports; i++)
                {
                    nativeExports[i] = Marshal.StringToHGlobalUni(Exports[i]);
                }

                @ref.pExports = nativeExports;
            }
        }
        #endregion
    }
}
