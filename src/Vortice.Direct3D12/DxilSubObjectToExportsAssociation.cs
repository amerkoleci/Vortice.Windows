// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// This subobject is unsupported in the current release.
    /// </summary>
    public partial class DxilSubObjectToExportsAssociation
    {
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

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            Marshal.FreeHGlobal(@ref.pSubobjectToAssociate);

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
            @ref.pSubobjectToAssociate = Marshal.StringToHGlobalUni(SubObjectToAssociate);
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
