// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace SharpDirect3D11
{
    /// <summary>
    /// Describes a vertex buffer view.
    /// </summary>
    public readonly struct VertexBufferView
    {
        public readonly ID3D11Buffer Buffer;
        public readonly int Stride;
        public readonly int Offset;

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexBufferView"/> struct.
        /// </summary>
        /// <param name="buffer">The <see cref="ID3D11Buffer"/> to bind.</param>
        /// <param name="stride">Specifies the size in bytes of each vertex entry.</param>
        /// <param name="offset">Offset.</param>
        public VertexBufferView(ID3D11Buffer buffer, int stride, int offset = 0)
        {
            Buffer = buffer;
            Stride = stride;
            Offset = offset;
        }
    }
}
