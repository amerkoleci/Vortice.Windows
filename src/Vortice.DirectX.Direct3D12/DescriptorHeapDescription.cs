// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectX.Direct3D12
{
    public partial struct DescriptorHeapDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DescriptorHeapDescription"/> struct.
        /// </summary>
        /// <param name="type">The heap type.</param>
        /// <param name="descriptorCount">The descriptor count.</param>
        /// <param name="flags">The optional heap flags.</param>
        /// <param name="nodeMask">Multi GPU node mask.</param>
        public DescriptorHeapDescription(DescriptorHeapType type, int descriptorCount, DescriptorHeapFlags flags = DescriptorHeapFlags.None, int nodeMask = 0)
        {
            Type = type;
            DescriptorCount = descriptorCount;
            Flags = flags;
            NodeMask = nodeMask;
        }
    }
}
