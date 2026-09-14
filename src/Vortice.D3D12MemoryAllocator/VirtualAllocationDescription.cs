// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

[StructLayout(LayoutKind.Sequential)]
public struct VirtualAllocationDescription
{
    public VirtualAllocationFlags Flags;
    public ulong Size;
    public ulong Alignment;
    public nint PrivateData;

    public VirtualAllocationDescription(ulong size, ulong alignment = 0, VirtualAllocationFlags flags = VirtualAllocationFlags.None)
    {
        Size = size;
        Alignment = alignment;
        Flags = flags;
    }
}
