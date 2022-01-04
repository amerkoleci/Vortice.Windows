// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11.Shader;

public partial struct ShaderDescription
{
    public ShaderVersionType Type => (ShaderVersionType)(((Version) >> 16) & 0xffff);
    public int Major => ((Version) >> 4) & 0xf;
    public int Minor => ((Version) >> 0) & 0xf;
}
