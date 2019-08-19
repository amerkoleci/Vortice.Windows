// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectX.Direct3D12
{
    /// <summary>
    /// Describes parameters for drawing indexed instances.
    /// </summary>
    public partial struct DrawIndexedArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawIndexedArguments"/> struct.
        /// </summary>
        /// <param name="indexCountPerInstance">The number of indices read from the index buffer for each instance.</param>
        /// <param name="instanceCount">The number of instances to draw.</param>
        /// <param name="startIndexLocation">The location of the first index read by the GPU from the index buffer.</param>
        /// <param name="baseVertexLocation">A value added to each index before reading a vertex from the vertex buffer.</param>
        /// <param name="startInstanceLocation">A value added to each index before reading per-instance data from a vertex buffer.</param>
        public DrawIndexedArguments(int indexCountPerInstance, int instanceCount, int startIndexLocation, int baseVertexLocation, int startInstanceLocation)
        {
            IndexCountPerInstance = indexCountPerInstance;
            InstanceCount = instanceCount;
            StartIndexLocation = startIndexLocation;
            BaseVertexLocation = baseVertexLocation;
            StartInstanceLocation = startInstanceLocation;
        }
    }
}
