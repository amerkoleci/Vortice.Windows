// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Represents a subobject with in a state object description. 
/// Use with <see cref="StateObjectDescription"/>.
/// </summary>
public partial class StateSubObject
{
    public StateSubObjectType Type => Description.SubObjectType;

    public IStateSubObjectDescription Description { get; set; }

    public StateSubObject(IStateSubObjectDescription description)
    {
        Description = description;
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe struct __Native
    {
        public StateSubObjectType Type;
        public IntPtr pDesc;
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        if (Description is IStateSubObjectDescriptionMarshal descriptionMarshal)
        {
            descriptionMarshal.__MarshalFree(ref @ref.pDesc);
        }
    }

    internal unsafe void __MarshalTo(ref __Native @ref, Dictionary<StateSubObject, IntPtr> subObjectLookup)
    {
        @ref.Type = Type;
        if (Description is IStateSubObjectDescriptionMarshal descriptionMarshal)
        {
            @ref.pDesc = descriptionMarshal.__MarshalAlloc(subObjectLookup);
        }
    }
    #endregion
}
