// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

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
    public RootConstants(uint shaderRegister, uint registerSpace, uint num32BitValues)
    {
        ShaderRegister = shaderRegister;
        RegisterSpace = registerSpace;
        Num32BitValues = num32BitValues;
        Num32BitValues = num32BitValues;
    }
}
