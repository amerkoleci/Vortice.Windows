// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

/// <summary>
/// Defines general properties of a state object.
/// </summary>
public partial struct StateObjectConfig : IStateSubObjectDescription, IStateSubObjectDescriptionMarshal
{
    StateSubObjectType IStateSubObjectDescription.SubObjectType => StateSubObjectType.StateObjectConfig;

    /// <summary>
    /// Initializes a new instance of the <see cref="StateObjectConfig"/> struct.
    /// </summary>
    /// <param name="flags">A value from the <see cref="StateObjectFlags"/> enumeration that specifies the requirements for the state object.</param>
    public StateObjectConfig(StateObjectFlags flags)
    {
        Flags = flags;
    }

    #region Marshal
    unsafe IntPtr IStateSubObjectDescriptionMarshal.__MarshalAlloc(Dictionary<StateSubObject, IntPtr> subObjectLookup)
    {
        var description = Marshal.AllocHGlobal(sizeof(StateObjectConfig));
        Unsafe.WriteUnaligned(description.ToPointer(), this);
        return description;
    }

    unsafe void IStateSubObjectDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        Marshal.FreeHGlobal(pDesc);
    }
    #endregion Marshal
}
