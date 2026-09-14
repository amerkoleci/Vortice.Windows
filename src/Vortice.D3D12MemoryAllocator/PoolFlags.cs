// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

[Flags]
public enum PoolFlags
{
    None = 0,
    AlgorithmLinear = 0x1,
    MSAATexturesAlwaysCommitted = 0x2,
    AlgorithmMask = AlgorithmLinear
}
