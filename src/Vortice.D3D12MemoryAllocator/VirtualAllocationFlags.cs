// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

[Flags]
public enum VirtualAllocationFlags
{
    None = 0,
    UpperAddress = 0x8,
    StrategyMinMemory = 0x10000,
    StrategyMinTime = 0x20000,
    StrategyMinOffset = 0x4000,
    StrategyMask = StrategyMinMemory | StrategyMinTime | StrategyMinOffset
}
