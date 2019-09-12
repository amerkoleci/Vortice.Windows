// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using Vortice.DirectX;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes a DXIL library state subobject that can be included in a state object.
    /// </summary>
    public partial class DxilLibraryDescription
    {
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

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            DxilLibrary.__MarshalFree(ref @ref.DXILLibrary);

            if (@ref.pExports != null)
            {
                for (int i = 0; i < @ref.NumExports; i++)
                {
                    Exports[i].__MarshalFree(ref @ref.pExports[i]);
                }

                Marshal.FreeHGlobal((IntPtr)@ref.pExports);
            }
        }

        internal unsafe void __MarshalFrom(ref __Native @ref)
        {
            DxilLibrary = new ShaderBytecode();
            DxilLibrary.__MarshalFrom(ref @ref.DXILLibrary);
            if (@ref.NumExports > 0)
            {
                Exports = new ExportDescription[@ref.NumExports];
                for (var i = 0; i < @ref.NumExports; i++)
                {
                    Exports[i] = new ExportDescription();
                    Exports[i].__MarshalFrom(ref @ref.pExports[i]);
                }
            }
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            DxilLibrary.__MarshalTo(ref @ref.DXILLibrary);
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
