// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

[StructLayout(LayoutKind.Sequential)]
public struct VirtualAllocationInfo
{
    public ulong Offset;
    public ulong Size;
    public nint PrivateData;
}
