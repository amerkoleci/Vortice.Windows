// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12
{
    public partial struct HeapProperties
    {
        public static readonly HeapProperties DefaultHeapProperties = new(HeapType.Default, CpuPageProperty.Unknown, MemoryPool.Unknown);
        public static readonly HeapProperties UploadHeapProperties = new(HeapType.Upload, CpuPageProperty.Unknown, MemoryPool.Unknown);
        public static readonly HeapProperties ReadbackHeapProperties = new(HeapType.Readback, CpuPageProperty.Unknown, MemoryPool.Unknown);

        /// <summary>
        /// Initializes a new instance of the <see cref="HeapProperties"/> struct.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="cpuPageProperty">The cpu page properties.</param>
        /// <param name="memoryPoolPreference">The memory pool preference.</param>
        /// <param name="creationNodeMask"></param>
        /// <param name="visibleNodeMask"></param>
        public HeapProperties(
            HeapType type,
            CpuPageProperty cpuPageProperty,
            MemoryPool memoryPoolPreference,
            int creationNodeMask = 1,
            int visibleNodeMask = 1)
        {
            Type = type;
            CPUPageProperty = cpuPageProperty;
            MemoryPoolPreference = memoryPoolPreference;
            CreationNodeMask = creationNodeMask;
            VisibleNodeMask = visibleNodeMask;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeapProperties"/> struct with <see cref="HeapType.Custom"/>
        /// </summary>
        /// <param name="cpuPageProperty">The cpu page properties.</param>
        /// <param name="memoryPoolPreference">The memory pool preference.</param>
        /// <param name="creationNodeMask"></param>
        /// <param name="visibleNodeMask"></param>
        public HeapProperties(
            CpuPageProperty cpuPageProperty,
            MemoryPool memoryPoolPreference,
            int creationNodeMask = 1,
            int visibleNodeMask = 1)
        {
            Type = HeapType.Custom;
            CPUPageProperty = cpuPageProperty;
            MemoryPoolPreference = memoryPoolPreference;
            CreationNodeMask = creationNodeMask;
            VisibleNodeMask = visibleNodeMask;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeapProperties"/> struct.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="creationNodeMask"></param>
        /// <param name="visibleNodeMask"></param>
        public HeapProperties(HeapType type,
            int creationNodeMask = 1,
            int visibleNodeMask = 1)
        {
            Type = type;
            CPUPageProperty = CpuPageProperty.Unknown;
            MemoryPoolPreference = MemoryPool.Unknown;
            CreationNodeMask = creationNodeMask;
            VisibleNodeMask = visibleNodeMask;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is cpu accessible.
        /// </summary>
        /// <value><c>true</c> if this instance is cpu accessible; otherwise, <c>false</c>.</value>
        public bool IsCpuAccessible
        {
            get
            {
                return Type == HeapType.Upload
                    || Type == HeapType.Readback
                    || (Type == HeapType.Custom
                        && (CPUPageProperty == CpuPageProperty.WriteCombine || CPUPageProperty == CpuPageProperty.WriteBack));
            }
        }
    }
}
