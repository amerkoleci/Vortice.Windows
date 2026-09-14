// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D12;

namespace Vortice.D3D12MemoryAllocator;

public struct AllocatorDescription
{
    public AllocatorFlags Flags;
    public ID3D12Device Device;
    public ulong PreferredBlockSize;
    public AllocationCallbacks? AllocationCallbacks;
    public Vortice.DXGI.IDXGIAdapter Adapter;

    public AllocatorDescription(ID3D12Device device, Vortice.DXGI.IDXGIAdapter adapter, AllocatorFlags flags = AllocatorFlags.None)
    {
        Device = device;
        Adapter = adapter;
        Flags = flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct __Native
    {
        public AllocatorFlags Flags;
        public nint Device;
        public ulong PreferredBlockSize;
        public AllocationCallbacks* AllocationCallbacks;
        public nint Adapter;
    }
}
