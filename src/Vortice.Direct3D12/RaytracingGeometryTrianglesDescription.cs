// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D12;

/// <summary>
/// Describes a set of triangles used as raytracing geometry. The geometry pointed to by this struct are always in triangle list form, indexed or non-indexed. Triangle strips are not supported.
/// </summary>
public partial struct RaytracingGeometryTrianglesDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RaytracingGeometryTrianglesDescription"/> struct.
    /// </summary>
    /// <param name="vertexBuffer">
    /// Array of vertices including a stride. The alignment on the address and stride must be a multiple of the component size, so 4 bytes for formats with 32bit components and 2 bytes for formats with 16bit components. 
    /// The memory pointed to must be in state <see cref="ResourceStates.NonPixelShaderResource"/>
    /// </param>
    /// <param name="vertexFormat">Format of the vertices in VertexBuffer.</param>
    /// <param name="vertexCount">Number of vertices in VertexBuffer.</param>
    /// <param name="transform3x4">
    /// Address of a 3x4 affine transform matrix in row-major layout to be applied to the vertices in the VertexBuffer during an acceleration structure build. The contents of VertexBuffer are not modified. If a 2D vertex format is used, the transformation is applied with the third vertex component assumed to be zero.
    /// If Transform3x4 is 0 the vertices will not be transformed. Using Transform3x4 may result in increased computation and/or memory requirements for the acceleration structure build.
    /// The memory pointed to must be in state <see cref="ResourceStates.NonPixelShaderResource"/>. The address must be aligned to 16 bytes, defined as <see cref="D3D12.RaytracingTransform3x4ByteAlignment"/>.
    /// </param>
    /// <param name="indexBuffer"></param>
    /// <param name="indexFormat">
    /// Format of the indices in the IndexBuffer. 
    /// Must be one of the following: <see cref="Format.Unknown"/> when <see cref="IndexBuffer"/> is 0, <see cref="Format.R32_UInt"/> or <see cref="Format.R16_UInt"/>
    /// </param>
    /// <param name="indexCount">Number of indices in IndexBuffer. Must be 0 if <see cref="IndexBuffer"/> is 0.</param>
    public RaytracingGeometryTrianglesDescription(
        in GpuVirtualAddressAndStride vertexBuffer,
        Format vertexFormat,
        int vertexCount,
        ulong transform3x4 = 0,
        ulong indexBuffer = 0,
        Format indexFormat = Format.Unknown,
        int indexCount = 0)
    {
        Transform3x4 = transform3x4;
        IndexFormat = indexFormat;
        VertexFormat = vertexFormat;
        IndexCount = indexCount;
        VertexCount = vertexCount;
        IndexBuffer = indexBuffer;
        VertexBuffer = vertexBuffer;
    }
}
