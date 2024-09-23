// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes the purpose of a query heap. A query heap contains an array of individual queries.
/// </summary>
public partial struct QueryHeapDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QueryHeapDescription"/> struct.
    /// </summary>
    /// <param name="type">The <see cref="QueryHeapType"/> type.</param>
    /// <param name="count">Specifies the number of queries the heap should contain.</param>
    /// <param name="nodeMask">For single GPU operation, set this to zero. If there are multiple GPU nodes, set a bit to identify the node (the device's physical adapter) to which the query heap applies.</param>
    public QueryHeapDescription(QueryHeapType type, uint count, uint nodeMask = 0)
    {
        Type = type;
        Count = count;
        NodeMask = nodeMask;
    }
}
