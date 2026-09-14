// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

public struct VirtualBlockDescription
{
    public VirtualBlockFlags Flags;
    public ulong Size;
    public AllocationCallbacks? AllocationCallbacks;

    public VirtualBlockDescription(ulong size, VirtualBlockFlags flags = VirtualBlockFlags.None)
    {
        Size = size;
        Flags = flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct __Native
    {
        public VirtualBlockFlags Flags;
        public ulong Size;
        public AllocationCallbacks* AllocationCallbacks;
    }
}
