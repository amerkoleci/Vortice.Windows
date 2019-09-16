// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.DirectX;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Defines a local root signature state subobject that will be used with associated shaders.
    /// </summary>
    public partial struct LocalRootSignature : IStateSubObjectDescription, IStateSubObjectDescriptionMarshal
    {
        StateSubObjectType IStateSubObjectDescription.SubObjectType => StateSubObjectType.LocalRootSignature;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalRootSignature"/> struct.
        /// </summary>
        /// <param name="rootSignature"></param>
        public LocalRootSignature(ID3D12RootSignature rootSignature)
        {
            RootSignature = rootSignature;
        }

        #region Marshal
        unsafe IntPtr IStateSubObjectDescriptionMarshal.__MarshalAlloc()
        {
            __Native* native = (__Native*)Marshal.AllocHGlobal(sizeof(__Native));
            native->RootSignature = CppObject.ToCallbackPtr<ID3D12RootSignature>(RootSignature);
            return (IntPtr)native;
        }

        unsafe void IStateSubObjectDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
        {
            ref __Native native = ref Unsafe.AsRef<__Native>(pDesc.ToPointer());
            __MarshalFree(ref native);
            Marshal.FreeHGlobal(pDesc);
        }
        #endregion Marshal
    }
}
