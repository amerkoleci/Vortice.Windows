// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace SharpDirect2D
{
    /// <summary>
    /// Defines a range of vertices that are used when rendering less than the full contents of a vertex buffer.
    /// </summary>
    public partial struct VertexRange
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VertexRange"/> struct.
        /// </summary>
        /// <param name="startVertex">The first vertex in the range to process.</param>
        /// <param name="vertexCount">The number of vertices to use.</param>
        public VertexRange(int startVertex, int vertexCount)
        {
            StartVertex = startVertex;
            VertexCount = vertexCount;
        }
    }
}
