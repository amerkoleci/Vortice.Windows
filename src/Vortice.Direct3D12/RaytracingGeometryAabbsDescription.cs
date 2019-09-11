// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes a set of Axis-aligned bounding boxes that are used in the <see cref="BuildRaytracingAccelerationStructureInputs"/> structure to provide input data to a raytracing acceleration structure build operation.
    /// </summary>
    public partial struct RaytracingGeometryAabbsDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RaytracingGeometryAabbsDescription"/> struct.
        /// </summary>
        /// <param name="aabbCount">The number of AABBs pointed to in the contiguous array at AABBs.</param>
        /// <param name="aabbs">the GPU memory location where an array of AABB descriptions is to be found, including the data stride between AABBs. The address and stride must each be aligned to 8 bytes, defined as The address must be aligned to 16 bytes, defined as <see cref="RaytracingAABBByteAlignment"/>. The stride can be 0.</param>
        public RaytracingGeometryAabbsDescription(ulong aabbCount, GpuVirtualAddressAndStride aabbs)
        {
            AABBCount = aabbCount;
            AABBs = aabbs;
        }
    }
}
