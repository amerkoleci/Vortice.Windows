// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

[StructLayout(LayoutKind.Sequential)]
public struct DetailedStatistics
{
    public Statistics Statistics;
    public uint UnusedRangeCount;
    public ulong AllocationSizeMin;
    public ulong AllocationSizeMax;
    public ulong UnusedRangeSizeMin;
    public ulong UnusedRangeSizeMax;
}
