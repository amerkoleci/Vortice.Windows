// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

public struct TotalStatistics
{
    public DetailedStatistics[] HeapType;
    public DetailedStatistics[] MemorySegmentGroup;
    public DetailedStatistics Total;

    [StructLayout(LayoutKind.Sequential)]
    internal struct __Native
    {
        public DetailedStatistics HeapType0;
        public DetailedStatistics HeapType1;
        public DetailedStatistics HeapType2;
        public DetailedStatistics HeapType3;
        public DetailedStatistics HeapType4;
        public DetailedStatistics MemorySegment0;
        public DetailedStatistics MemorySegment1;
        public DetailedStatistics Total;
    }

    internal void __MarshalFrom(ref __Native native)
    {
        HeapType = [native.HeapType0, native.HeapType1, native.HeapType2, native.HeapType3, native.HeapType4];
        MemorySegmentGroup = [native.MemorySegment0, native.MemorySegment1];
        Total = native.Total;
    }
}
