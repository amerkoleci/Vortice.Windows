// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D12;

namespace Vortice.D3D12MemoryAllocator;

public struct AllocationDescription
{
    public AllocationFlags Flags;
    public HeapType HeapType;
    public HeapFlags ExtraHeapFlags;
    public Pool? CustomPool;
    public nint PrivateData;

    public AllocationDescription(HeapType heapType, AllocationFlags flags = AllocationFlags.None)
    {
        HeapType = heapType;
        Flags = flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct __Native
    {
        public AllocationFlags Flags;
        public HeapType HeapType;
        public HeapFlags ExtraHeapFlags;
        public nint CustomPool;
        public nint PrivateData;
    }

    internal readonly void __MarshalTo(ref __Native native)
    {
        native.Flags = Flags;
        native.HeapType = HeapType;
        native.ExtraHeapFlags = ExtraHeapFlags;
        native.CustomPool = CustomPool?.NativePointer ?? 0;
        native.PrivateData = PrivateData;
    }
}
