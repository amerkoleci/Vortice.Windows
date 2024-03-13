// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes a constant buffer to view.
/// </summary>
public partial struct ConstantBufferViewDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstantBufferViewDescription"/> struct.
    /// </summary>
    /// <param name="bufferLocation">The gpu virtual address of the constant buffer.</param>
    /// <param name="sizeInBytes">The size in bytes of the constant buffer.</param>
    public ConstantBufferViewDescription(ulong bufferLocation, int sizeInBytes)
    {
        BufferLocation = bufferLocation;
        SizeInBytes = sizeInBytes;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConstantBufferViewDescription"/> struct.
    /// </summary>
    /// <param name="resource">The <see cref="ID3D12Resource"/> to get gpu virtual address.</param>
    /// <param name="sizeInBytes">The size in bytes of the constant buffer.</param>
    public ConstantBufferViewDescription(ID3D12Resource resource, int sizeInBytes = 0)
    {
        BufferLocation = resource.GPUVirtualAddress;
        SizeInBytes = sizeInBytes == 0 ? (int)resource.Description.Width : sizeInBytes;
    }
}
