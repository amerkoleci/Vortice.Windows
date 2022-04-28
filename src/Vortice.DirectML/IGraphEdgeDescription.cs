// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public interface IGraphEdgeDescription
{
    GraphEdgeType GraphEdgeType { get; }
}

internal interface IGraphEdgeDescriptionMarshal
{
    IntPtr __MarshalAlloc();

    void __MarshalFree(ref IntPtr pDesc);
}
