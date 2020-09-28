// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
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
        public GpuVirtualAddressRangeAndStride(ulong startAddress, long sizeInBytes, long strideInBytes)
        {
            StartAddress = startAddress;
            SizeInBytes = sizeInBytes;
            StrideInBytes = strideInBytes;
        }
    }
}
