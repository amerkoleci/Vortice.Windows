// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

/// <summary>Unmanaged callbacks for CPU allocations. Keep callback code and private data alive until native disposal.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct AllocationCallbacks
{
    public delegate* unmanaged[Cdecl]<nuint, nuint, void*, void*> Allocate;
    public delegate* unmanaged[Cdecl]<void*, void*, void> Free;
    public nint PrivateData;
}
