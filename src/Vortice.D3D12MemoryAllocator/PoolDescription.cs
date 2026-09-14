// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D12;

namespace Vortice.D3D12MemoryAllocator;

public struct PoolDescription
{
    public PoolFlags Flags;
    public HeapProperties HeapProperties;
    public HeapFlags HeapFlags;
    public ulong BlockSize;
    public uint MinBlockCount;
    public uint MaxBlockCount;
    public ulong MinAllocationAlignment;
    public ID3D12ProtectedResourceSession? ProtectedSession;
    public ResidencyPriority ResidencyPriority;

    public PoolDescription(HeapType heapType)
    {
        HeapProperties = new HeapProperties(heapType);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct __Native
    {
        public PoolFlags Flags;
        public HeapProperties HeapProperties;
        public HeapFlags HeapFlags;
        public ulong BlockSize;
        public uint MinBlockCount;
        public uint MaxBlockCount;
        public ulong MinAllocationAlignment;
        public nint ProtectedSession;
        public ResidencyPriority ResidencyPriority;
    }

    internal readonly void __MarshalTo(ref __Native native)
    {
        native.Flags = Flags;
        native.HeapProperties = HeapProperties;
        native.HeapFlags = HeapFlags;
        native.BlockSize = BlockSize;
        native.MinBlockCount = MinBlockCount;
        native.MaxBlockCount = MaxBlockCount;
        native.MinAllocationAlignment = MinAllocationAlignment;
        native.ProtectedSession = ProtectedSession?.NativePointer ?? 0;
        native.ResidencyPriority = ResidencyPriority;
    }

    internal void __MarshalFrom(ref __Native native)
    {
        Flags = native.Flags;
        HeapProperties = native.HeapProperties;
        HeapFlags = native.HeapFlags;
        BlockSize = native.BlockSize;
        MinBlockCount = native.MinBlockCount;
        MaxBlockCount = native.MaxBlockCount;
        MinAllocationAlignment = native.MinAllocationAlignment;
        ProtectedSession = D3D12MA.GetReference<ID3D12ProtectedResourceSession>(native.ProtectedSession);
        ResidencyPriority = native.ResidencyPriority;
    }
}
