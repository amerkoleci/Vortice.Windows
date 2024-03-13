// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes parameters needed to allocate resources.
/// </summary>
public partial struct ResourceAllocationInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceAllocationInfo"/> struct.
    /// </summary>
    /// <param name="sizeInBytes">The size, in bytes, of the resource.</param>
    /// <param name="alignment">The alignment value for the resource; one of 4KB (4096), 64KB (65536) and 4MB (4194304) alignment.</param>
    public ResourceAllocationInfo(ulong sizeInBytes, ulong alignment)
    {
        SizeInBytes = sizeInBytes;
        Alignment = alignment;
    }
}
