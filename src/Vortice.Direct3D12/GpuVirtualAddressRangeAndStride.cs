// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes a GPU virtual address range and stride.
/// </summary>
public partial struct GpuVirtualAddressRangeAndStride
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GpuVirtualAddressRangeAndStride"/> struct.
    /// </summary>
    /// <param name="startAddress">The beginning of the virtual address range.</param>
    /// <param name="sizeInBytes">The size of the virtual address range, in bytes.</param>
    /// <param name="strideInBytes">Defines the record-indexing stride within the memory range.</param>
    public GpuVirtualAddressRangeAndStride(ulong startAddress, ulong sizeInBytes, ulong strideInBytes)
    {
        StartAddress = startAddress;
        SizeInBytes = sizeInBytes;
        StrideInBytes = strideInBytes;
    }
}
