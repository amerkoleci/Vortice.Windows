// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

[Flags]
public enum AllocationFlags
{
    None = 0,
    Committed = 0x1,
    NeverAllocate = 0x2,
    WithinBudget = 0x4,
    UpperAddress = 0x8,
    CanAlias = 0x10,
    StrategyMinMemory = 0x10000,
    StrategyMinTime = 0x20000,
    StrategyMinOffset = 0x4000,
    StrategyBestFit = StrategyMinMemory,
    StrategyFirstFit = StrategyMinTime,
    StrategyMask = StrategyMinMemory | StrategyMinTime | StrategyMinOffset
}
