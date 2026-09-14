// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

[Flags]
public enum VirtualBlockFlags
{
    None = 0,
    AlgorithmLinear = 1,
    AlgorithmMask = AlgorithmLinear
}
