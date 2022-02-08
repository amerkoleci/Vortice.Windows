// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes descriptors inline in the root signature version 1.0 that appear in shaders.
/// </summary>
public partial struct RootDescriptor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RootDescriptor"/> struct.
    /// </summary>
    /// <param name="shaderRegister">The shader register.</param>
    /// <param name="registerSpace">The register space.</param>
    public RootDescriptor(int shaderRegister, int registerSpace)
    {
        ShaderRegister = shaderRegister;
        RegisterSpace = registerSpace;
    }
}
