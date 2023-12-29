// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public enum DxcShaderStage : uint
{
    Vertex,
    Hull,
    Domain,
    Geometry,
    Pixel,
    Compute,
    Amplification,
    Mesh,
    Library,
    Count,
}
