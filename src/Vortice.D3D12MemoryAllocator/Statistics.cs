// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

[StructLayout(LayoutKind.Sequential)]
public struct Statistics
{
    public uint BlockCount;
    public uint AllocationCount;
    public ulong BlockBytes;
    public ulong AllocationBytes;
}
