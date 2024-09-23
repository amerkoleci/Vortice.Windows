// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

/// <summary>
/// A state subobject that identifies the GPU nodes to which the state object applies.
/// </summary>
public partial struct NodeMask : IStateSubObjectDescription, IStateSubObjectDescriptionMarshal
{
    StateSubObjectType IStateSubObjectDescription.SubObjectType => StateSubObjectType.NodeMask;

    /// <summary>
    /// Initializes a new instance of the <see cref="NodeMask"/> struct.
    /// </summary>
    /// <param name="mask">The node mask.</param>
    public NodeMask(uint mask)
    {
        Mask = mask;
    }

    #region Marshal
    unsafe IntPtr IStateSubObjectDescriptionMarshal.__MarshalAlloc(Dictionary<StateSubObject, IntPtr> subObjectLookup)
    {
        var native = Marshal.AllocHGlobal(sizeof(NodeMask));
        Unsafe.Write(native.ToPointer(), Mask);
        return native;
    }

    unsafe void IStateSubObjectDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        Marshal.FreeHGlobal(pDesc);
    }
    #endregion Marshal
}
