// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

/// <summary>A virtual range owned by its originating block. Free it through <see cref="VirtualBlock.FreeAllocation"/>.</summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct VirtualAllocation : IEquatable<VirtualAllocation>
{
    public VirtualAllocation(ulong handle) => Handle = handle;
    public readonly ulong Handle;
    public bool IsNull => Handle == 0;
    public bool Equals(VirtualAllocation other) => Handle == other.Handle;
    public override bool Equals(object? obj) => obj is VirtualAllocation other && Equals(other);
    public override int GetHashCode() => Handle.GetHashCode();
    public static bool operator ==(VirtualAllocation left, VirtualAllocation right) => left.Equals(right);
    public static bool operator !=(VirtualAllocation left, VirtualAllocation right) => !left.Equals(right);
}
