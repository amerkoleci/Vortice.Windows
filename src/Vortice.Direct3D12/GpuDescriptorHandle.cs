// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

public partial struct GpuDescriptorHandle : IEquatable<GpuDescriptorHandle>
{
    public static GpuDescriptorHandle Default => default;

    public GpuDescriptorHandle(in GpuDescriptorHandle other, int offsetScaledByIncrementSize)
    {
        InitOffsetted(in other, offsetScaledByIncrementSize);
    }

    public GpuDescriptorHandle(in GpuDescriptorHandle other, int offsetInDescriptors, int descriptorIncrementSize)
    {
        InitOffsetted(in other, offsetInDescriptors, descriptorIncrementSize);
    }

    public static bool operator ==(GpuDescriptorHandle left, GpuDescriptorHandle right)
        => left.Ptr == right.Ptr;

    public static bool operator !=(GpuDescriptorHandle left, GpuDescriptorHandle right)
        => left.Ptr != right.Ptr;

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj) => (obj is GpuDescriptorHandle other) && Equals(other);

    /// <inheritdoc/>
    public readonly bool Equals(GpuDescriptorHandle other) => this.Ptr == other.Ptr;

    /// <inheritdoc/>
    public override readonly int GetHashCode() => Ptr.GetHashCode();

    /// <summary>
    /// Adds an offset to a descriptor handle.
    /// </summary>
    /// <param name = "left">Initial descriptor handle</param>
    /// <param name = "offsetScaledByIncrementSize">Offset to apply, use <see cref="ID3D12Device.GetDescriptorHandleIncrementSize(DescriptorHeapType)"/> with the relevant heap type.</param>
    /// <returns>Offsetted descriptor handle</returns>
    public static GpuDescriptorHandle operator +(GpuDescriptorHandle left, int offsetScaledByIncrementSize) => new(left, offsetScaledByIncrementSize);

    public void InitOffsetted(in GpuDescriptorHandle @base, int offsetScaledByIncrementSize)
    {
        InitOffsetted(ref this, @base, offsetScaledByIncrementSize);
    }

    public void InitOffsetted(in GpuDescriptorHandle @base, int offsetInDescriptors, int descriptorIncrementSize)
    {
        InitOffsetted(ref this, @base, offsetInDescriptors, descriptorIncrementSize);
    }

    public static void InitOffsetted(ref GpuDescriptorHandle handle, in GpuDescriptorHandle @base, int offsetScaledByIncrementSize)
    {
        handle.Ptr = unchecked((ulong)((long)(@base.Ptr) + (long)(offsetScaledByIncrementSize)));
    }

    public static void InitOffsetted(ref GpuDescriptorHandle handle, in GpuDescriptorHandle @base, int offsetInDescriptors, int descriptorIncrementSize)
    {
        handle.Ptr = unchecked((ulong)((long)(@base.Ptr) + (long)(offsetInDescriptors) * (long)(descriptorIncrementSize)));
    }

    [UnscopedRef]
    public ref GpuDescriptorHandle Offset(int offsetInDescriptors, int descriptorIncrementSize)
    {
        Ptr = unchecked((ulong)((long)(Ptr) + (long)(offsetInDescriptors) * (long)(descriptorIncrementSize)));
        return ref this;
    }

    [UnscopedRef]
    public ref GpuDescriptorHandle Offset(int offsetScaledByIncrementSize)
    {
        Ptr = unchecked((ulong)((long)(Ptr) + (long)(offsetScaledByIncrementSize)));
        return ref this;
    }
}
