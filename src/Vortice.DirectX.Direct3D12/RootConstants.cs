// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectX.Direct3D12
{
    /// <summary>
    /// Describes constants inline in the root signature that appear in shaders as one constant buffer.
    /// </summary>
    public partial struct RootConstants
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootDescriptor"/> struct.
        /// </summary>
        /// <param name="shaderRegister">The shader register.</param>
        /// <param name="registerSpace">The register space.</param>
        /// <param name="num32BitValues">The number of constants that occupy a single shader slot (these constants appear like a single constant buffer). All constants occupy a single root signature bind slot.</param>
        public RootConstants(int shaderRegister, int registerSpace, int num32BitValues)
        {
            ShaderRegister = shaderRegister;
            RegisterSpace = registerSpace;
            Num32BitValues = num32BitValues;
            Num32BitValues = num32BitValues;
        }
    }
}
