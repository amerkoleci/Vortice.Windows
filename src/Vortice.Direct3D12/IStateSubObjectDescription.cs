// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct3D12
{
    public interface IStateSubObjectDescription
    {
        StateSubObjectType SubObjectType { get; }
    }

    internal interface IStateSubObjectDescriptionMarshal
    {
        IntPtr __MarshalAlloc();
        void __MarshalFree(ref IntPtr pDesc);
    }
}
