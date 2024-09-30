// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

/// <summary>
/// Describes a buffer resource.
/// </summary>
public partial struct BufferDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BufferDescription"/> struct.
    /// </summary>
    /// <param name="byteWidth">The size in bytes.</param>
    /// <param name="bindFlags">The bind flags.</param>
    /// <param name="usage">The usage.</param>
    /// <param name="cpuAccessFlags">The CPU access flags.</param>
    /// <param name="miscFlags">The option flags.</param>
    /// <param name="structureByteStride">The structure byte stride.</param>
    public BufferDescription(uint byteWidth,
        BindFlags bindFlags,
        ResourceUsage usage = ResourceUsage.Default,
        CpuAccessFlags cpuAccessFlags = CpuAccessFlags.None,
        ResourceOptionFlags miscFlags = ResourceOptionFlags.None,
        uint structureByteStride = 0)
    {
        ByteWidth = byteWidth;
        BindFlags = bindFlags;
        Usage = usage;
        CPUAccessFlags = cpuAccessFlags;
        MiscFlags = miscFlags;
        StructureByteStride = structureByteStride;
    }
}
