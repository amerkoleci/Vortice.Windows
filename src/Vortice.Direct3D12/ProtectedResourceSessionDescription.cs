// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial struct ProtectedResourceSessionDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProtectedResourceSessionDescription"/> struct.
    /// </summary>
    /// <param name="nodeMask">The node mask.</param>
    /// <param name="flags">Optional flags</param>
    public ProtectedResourceSessionDescription(uint nodeMask, ProtectedResourceSessionFlags flags = ProtectedResourceSessionFlags.None)
    {
        NodeMask = nodeMask;
        Flags = flags;
    }
}
