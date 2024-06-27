// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

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
    unsafe IntPtr IStateSubObjectDescriptionMarshal.__MarshalAlloc(Dictionary<StateSubObject, IntPtr> subObjectLookup)
    {
        __Native* native = (__Native*)Marshal.AllocHGlobal(sizeof(__Native));
        native->RootSignature = MarshallingHelpers.ToCallbackPtr<ID3D12RootSignature>(RootSignature);
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
