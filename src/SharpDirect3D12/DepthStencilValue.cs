// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace SharpDirect3D12
{
    public partial struct DepthStencilValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DepthStencilValue"/> struct.
        /// </summary>
        /// <param name="depth">Specifies the depth value.</param>
        /// <param name="stencil">Specifies the stencil value.</param>
        public DepthStencilValue(float depth, byte stencil)
        {
            Depth = depth;
            Stencil = stencil;
        }
    }
}
