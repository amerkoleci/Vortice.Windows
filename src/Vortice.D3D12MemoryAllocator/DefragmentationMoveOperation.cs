// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

public enum DefragmentationMoveOperation
{
    Copy = 0,
    Ignore = 1,
    /// <summary>EndPass releases the original source allocation reference. Detach its owning wrapper before ending the pass.</summary>
    Destroy = 2
}
