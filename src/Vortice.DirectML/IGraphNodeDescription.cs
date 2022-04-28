// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public interface IGraphNodeDescription
{
    GraphNodeType GraphNodeType { get; }
}

internal interface IGraphNodeDescriptionMarshal
{
    IntPtr __MarshalAlloc();

    void __MarshalFree(ref IntPtr pDesc);
}
