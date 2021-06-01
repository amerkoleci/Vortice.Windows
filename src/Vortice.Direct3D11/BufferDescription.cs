// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D11
{
    /// <summary>
    /// Describes a buffer resource.
    /// </summary>
    public partial struct BufferDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BufferDescription"/> struct.
        /// </summary>
        /// <param name="sizeInBytes">The size in bytes.</param>
        /// <param name="bindFlags">The bind flags.</param>
        /// <param name="usage">The usage.</param>
        public BufferDescription(int sizeInBytes, BindFlags bindFlags, ResourceUsage usage)
        {
            SizeInBytes = sizeInBytes;
            BindFlags = bindFlags;
            Usage = usage;
            CpuAccessFlags = CpuAccessFlags.None;
            OptionFlags = ResourceOptionFlags.None;
            StructureByteStride = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferDescription"/> struct.
        /// </summary>
        /// <param name="sizeInBytes">The size in bytes.</param>
        /// <param name="usage">The usage.</param>
        /// <param name="bindFlags">The bind flags.</param>
        /// <param name="cpuAccessFlags">The CPU access flags.</param>
        /// <param name="optionFlags">The option flags.</param>
        /// <param name="structureByteStride">The structure byte stride.</param>
        public BufferDescription(int sizeInBytes, ResourceUsage usage, BindFlags bindFlags, CpuAccessFlags cpuAccessFlags, ResourceOptionFlags optionFlags = ResourceOptionFlags.None, int structureByteStride = 0)
        {
            SizeInBytes = sizeInBytes;
            Usage = usage;
            BindFlags = bindFlags;
            CpuAccessFlags = cpuAccessFlags;
            OptionFlags = optionFlags;
            StructureByteStride = structureByteStride;
        }
    }
}
