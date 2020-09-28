// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes a GPU virtual address range.
    /// </summary>
    public partial struct GpuVirtualAddressRange
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpuVirtualAddressRange"/> struct.
        /// </summary>
        /// <param name="startAddress">The beginning of the virtual address range.</param>
        /// <param name="sizeInBytes">The size of the virtual address range, in bytes.</param>
        public GpuVirtualAddressRange(ulong startAddress, long sizeInBytes)
        {
            StartAddress = startAddress;
            SizeInBytes = sizeInBytes;
        }
    }
}
