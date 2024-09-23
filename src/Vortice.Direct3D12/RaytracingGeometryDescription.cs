// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes a set of geometry that is used in the <see cref="BuildRaytracingAccelerationStructureInputs"/> structure to provide input data to a raytracing acceleration structure build operation.
/// </summary>
public partial struct RaytracingGeometryDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RaytracingGeometryDescription"/> struct.
    /// </summary>
    /// <param name="triangles">A <see cref="RaytracingGeometryTrianglesDescription"/> describing triangle geometry.</param>
    /// <param name="flags">The geometry flags.</param>
    public RaytracingGeometryDescription(RaytracingGeometryTrianglesDescription triangles, RaytracingGeometryFlags flags = RaytracingGeometryFlags.None)
        : this()
    {
        Type = RaytracingGeometryType.Triangles;
        Flags = flags;
        Triangles = triangles;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RaytracingGeometryDescription"/> struct.
    /// </summary>
    /// <param name="aabbs">A <see cref="RaytracingGeometryAabbsDescription"/> describing triangle geometry.</param>
    /// <param name="flags">The geometry flags.</param>
    public RaytracingGeometryDescription(RaytracingGeometryAabbsDescription aabbs, RaytracingGeometryFlags flags = RaytracingGeometryFlags.None)
        : this()
    {
        Type = RaytracingGeometryType.ProceduralPrimitiveAabbs;
        Flags = flags;
        AABBs = aabbs;
    }
}
