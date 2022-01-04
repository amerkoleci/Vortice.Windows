// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11.Shader;

[Flags]
public enum RegisterComponentMaskFlags : byte
{
    None = 0,
    ComponentX = 1,
    ComponentY = 2,
    ComponentZ = 4,
    ComponentW = 8,
    All
}
