// Copyright © Amer Koleci and Contributors.
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
}
