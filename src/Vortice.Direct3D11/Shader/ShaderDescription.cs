// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11.Shader;

public partial struct ShaderDescription
{
    public ShaderVersionType Type => (ShaderVersionType)(((Version) >> 16) & 0xffff);
    public uint Major => ((Version) >> 4) & 0xf;
    public uint Minor => ((Version) >> 0) & 0xf;
}
