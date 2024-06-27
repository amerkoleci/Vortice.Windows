// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public interface IStateSubObjectDescription
{
    StateSubObjectType SubObjectType { get; }
}

internal interface IStateSubObjectDescriptionMarshal
{
    IntPtr __MarshalAlloc(Dictionary<StateSubObject, IntPtr> subObjectLookup);
    void __MarshalFree(ref IntPtr pDesc);
}
