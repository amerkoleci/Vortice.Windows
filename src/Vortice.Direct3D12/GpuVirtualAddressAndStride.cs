// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes a GPU virtual address and indexing stride.
    /// </summary>
    public partial struct GpuVirtualAddressAndStride
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpuVirtualAddressAndStride"/> struct.
        /// </summary>
        /// <param name="startAddress">The beginning of the virtual address range.</param>
        /// <param name="strideInBytes">Defines indexing stride, such as for vertices. Only the bottom 32 bits are used. The field is 64 bits to make alignment of containing structures consistent everywhere.</param>
        public GpuVirtualAddressAndStride(ulong startAddress, long strideInBytes)
        {
            StartAddress = startAddress;
            StrideInBytes = strideInBytes;
        }
    }
}
