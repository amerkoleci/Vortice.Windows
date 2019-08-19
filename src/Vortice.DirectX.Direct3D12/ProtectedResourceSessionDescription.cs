// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectX.Direct3D12
{
    public partial struct ProtectedResourceSessionDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProtectedResourceSessionDescription"/> struct.
        /// </summary>
        /// <param name="nodeMask">The node mask.</param>
        /// <param name="flags">Optional flags</param>
        public ProtectedResourceSessionDescription(int nodeMask, ProtectedResourceSessionFlags flags = ProtectedResourceSessionFlags.None)
        {
            NodeMask = nodeMask;
            Flags = flags;
        }
    }
}
