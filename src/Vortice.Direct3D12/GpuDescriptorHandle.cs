// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

public partial struct GpuDescriptorHandle : IEquatable<GpuDescriptorHandle>
{
    public static GpuDescriptorHandle Default => default;

    public GpuDescriptorHandle(in GpuDescriptorHandle other, int offsetScaledByIncrementSize)
    {
        InitOffsetted(out this, other, offsetScaledByIncrementSize);
    }

    public GpuDescriptorHandle(in GpuDescriptorHandle other, int offsetInDescriptors, int descriptorIncrementSize)
    {
        InitOffsetted(out this, other, offsetInDescriptors, descriptorIncrementSize);
    }

    public GpuDescriptorHandle Offset(int offsetInDescriptors, int descriptorIncrementSize)
    {
        ptr = (ulong)((long)ptr + ((long)offsetInDescriptors * (long)descriptorIncrementSize));
        return this;
    }

    public GpuDescriptorHandle Offset(int offsetScaledByIncrementSize)
    {
        ptr = (ulong)((long)ptr + (long)offsetScaledByIncrementSize);
        return this;
    }

    public void InitOffsetted(in GpuDescriptorHandle @base, int offsetScaledByIncrementSize)
    {
        InitOffsetted(out this, @base, offsetScaledByIncrementSize);
    }

    public void InitOffsetted(in GpuDescriptorHandle @base, int offsetInDescriptors, int descriptorIncrementSize)
    {
        InitOffsetted(out this, @base, offsetInDescriptors, descriptorIncrementSize);
    }

    public static void InitOffsetted(out GpuDescriptorHandle handle, in GpuDescriptorHandle @base, int offsetScaledByIncrementSize)
    {
        handle.ptr = (ulong)((long)@base.ptr + (long)offsetScaledByIncrementSize);
    }

    public static void InitOffsetted(out GpuDescriptorHandle handle, in GpuDescriptorHandle @base, int offsetInDescriptors, int descriptorIncrementSize)
    {
        handle.ptr = (ulong)((long)@base.ptr + ((long)offsetInDescriptors * (long)descriptorIncrementSize));
    }

    /// <summary>
    /// Adds an offset to a descriptor handle.
    /// </summary>
    /// <param name = "left">Initial descriptor handle</param>
    /// <param name = "offsetScaledByIncrementSize">Offset to apply, use <see cref="ID3D12Device.GetDescriptorHandleIncrementSize(DescriptorHeapType)"/> with the relevant heap type.</param>
    /// <returns>Offsetted descriptor handle</returns>
    public static GpuDescriptorHandle operator +(GpuDescriptorHandle left, int offsetScaledByIncrementSize)
    {
        return new(left, offsetScaledByIncrementSize);
    }

    /// <summary>
    /// Compares two <see cref="GpuDescriptorHandle"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="GpuDescriptorHandle"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="GpuDescriptorHandle"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(GpuDescriptorHandle left, GpuDescriptorHandle right) => (left.ptr == right.ptr);

    /// <summary>
    /// Compares two <see cref="GpuDescriptorHandle"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="GpuDescriptorHandle"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="GpuDescriptorHandle"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(GpuDescriptorHandle left, GpuDescriptorHandle right) => (left.ptr != right.ptr);

    /// <inheritdoc/>
    public override int GetHashCode() => ptr.GetHashCode();

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is GpuDescriptorHandle handle && Equals(handle);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(GpuDescriptorHandle other) => this == other;
}
