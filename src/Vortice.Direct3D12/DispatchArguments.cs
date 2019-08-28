// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.DirectX.DXGI;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes dispatch parameters, for use by the compute shader.
    /// </summary>
    public partial struct DispatchArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchArguments"/> struct.
        /// </summary>
        /// <param name="threadGroupCountX">The size, in thread groups, of the x-dimension of the thread-group grid.</param>
        /// <param name="threadGroupCountY">The size, in thread groups, of the y-dimension of the thread-group grid.</param>
        /// <param name="threadGroupCountZ">The size, in thread groups, of the z-dimension of the thread-group grid.</param>
        public DispatchArguments(int threadGroupCountX, int threadGroupCountY, int threadGroupCountZ)
        {
            ThreadGroupCountX = threadGroupCountX;
            ThreadGroupCountY = threadGroupCountY;
            ThreadGroupCountZ = threadGroupCountZ;
        }
    }
}
