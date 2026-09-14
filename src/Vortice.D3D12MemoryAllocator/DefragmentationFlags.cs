// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

[Flags]
public enum DefragmentationFlags
{
    None = 0,
    AlgorithmFast = 0x1,
    AlgorithmBalanced = 0x2,
    AlgorithmFull = 0x4,
    AlgorithmMask = AlgorithmFast | AlgorithmBalanced | AlgorithmFull
}
