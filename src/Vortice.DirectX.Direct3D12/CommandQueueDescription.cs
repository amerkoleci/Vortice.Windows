// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectX.Direct3D12
{
    public partial struct CommandQueueDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandQueueDescription"/> struct.
        /// </summary>
        /// <param name="type">The queue type.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="flags">Options flags.</param>
        /// <param name="nodeMask">Node mask.</param>
        public CommandQueueDescription(CommandListType type, int priority = 0, CommandQueueFlags flags = CommandQueueFlags.None, int nodeMask = 0)
        {
            Type = type;
            Priority = priority;
            Flags = flags;
            NodeMask = nodeMask;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandQueueDescription"/> struct.
        /// </summary>
        /// <param name="type">The queue type.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="flags">Options flags.</param>
        /// <param name="nodeMask">Node mask.</param>
        public CommandQueueDescription(CommandListType type, CommandQueuePriority priority, CommandQueueFlags flags = CommandQueueFlags.None, int nodeMask = 0)
        {
            Type = type;
            Priority = (int)priority;
            Flags = flags;
            NodeMask = nodeMask;
        }
    }
}
