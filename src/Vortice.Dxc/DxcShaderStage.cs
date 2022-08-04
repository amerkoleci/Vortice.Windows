// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.
// Implementation based on https://github.com/tgjones/DotNetDxc

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
