// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes a heap.
    /// </summary>
    public partial struct HeapDescription
    {
        public HeapDescription(long size, HeapProperties properties, long alignment = 0, HeapFlags flags = HeapFlags.None)
        {
            SizeInBytes = size;
            Properties = properties;
            Alignment = alignment;
            Flags = flags;
        }

        public HeapDescription(long size, HeapType type, long alignment = 0, HeapFlags flags = HeapFlags.None)
        {
            SizeInBytes = size;
            Properties = new HeapProperties(type);
            Alignment = alignment;
            Flags = flags;
        }

        public HeapDescription(long size, CpuPageProperty cpuPageProperty, MemoryPool memoryPoolPreference, long alignment = 0, HeapFlags flags = HeapFlags.None)
        {
            SizeInBytes = size;
            Properties = new HeapProperties(cpuPageProperty, memoryPoolPreference);
            Alignment = alignment;
            Flags = flags;
        }

        public HeapDescription(in ResourceAllocationInfo resourceAllocationInfo, HeapProperties properties, HeapFlags flags = HeapFlags.None)
        {
            SizeInBytes = resourceAllocationInfo.SizeInBytes;
            Properties = properties;
            Alignment = resourceAllocationInfo.Alignment;
            Flags = flags;
        }

        public HeapDescription(in ResourceAllocationInfo resourceAllocationInfo, HeapType type, HeapFlags flags = HeapFlags.None)
        {
            SizeInBytes = resourceAllocationInfo.SizeInBytes;
            Properties = new HeapProperties(type);
            Alignment = resourceAllocationInfo.Alignment;
            Flags = flags;
        }

        public HeapDescription(in ResourceAllocationInfo resourceAllocationInfo, CpuPageProperty cpuPageProperty, MemoryPool memoryPoolPreference, HeapFlags flags = HeapFlags.None)
        {
            SizeInBytes = resourceAllocationInfo.SizeInBytes;
            Properties = new HeapProperties(cpuPageProperty, memoryPoolPreference);
            Alignment = resourceAllocationInfo.Alignment;
            Flags = flags;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is cpu accessible.
        /// </summary>
        /// <value><c>true</c> if this instance is cpu accessible; otherwise, <c>false</c>.</value>
        public bool IsCpuAccessible => Properties.IsCpuAccessible;
    }
}
