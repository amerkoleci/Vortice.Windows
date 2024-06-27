// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial struct DepthStencilValue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilValue"/> struct.
    /// </summary>
    /// <param name="depth">Specifies the depth value.</param>
    /// <param name="stencil">Specifies the stencil value.</param>
    public DepthStencilValue(float depth, byte stencil = 0)
    {
        Depth = depth;
        Stencil = stencil;
    }
}
