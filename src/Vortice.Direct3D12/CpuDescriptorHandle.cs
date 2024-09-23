// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

/// <include file="Documentation.xml" path="/comments/comment[@id='D3D12_CPU_DESCRIPTOR_HANDLE']/*" />
public partial struct CpuDescriptorHandle : IEquatable<CpuDescriptorHandle>
{
    public static CpuDescriptorHandle Default => default;

    /// <include file="Documentation.xml" path="/comments/comment[@id='D3D12_CPU_DESCRIPTOR_HANDLE::ptr']/*" />
    public nuint Ptr;

    public CpuDescriptorHandle(in CpuDescriptorHandle other, int offsetScaledByIncrementSize)
    {
        InitOffsetted(in other, offsetScaledByIncrementSize);
    }

    public CpuDescriptorHandle(in CpuDescriptorHandle other, int offsetInDescriptors, uint descriptorIncrementSize)
    {
        InitOffsetted(in other, offsetInDescriptors, descriptorIncrementSize);
    }

    /// <summary>
    /// Compares two <see cref="CpuDescriptorHandle"/> objects for equality.
    /// </summary>
    /// <param name="left">The CpuDescriptorHandle on the left hand of the operand.</param>
    /// <param name="right">The CpuDescriptorHandle on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(CpuDescriptorHandle left, CpuDescriptorHandle right) => left.Ptr == right.Ptr;

    /// <summary>
    /// Compares two <see cref="CpuDescriptorHandle"/> objects for inequality.
    /// </summary>
    /// <param name="left">The CpuDescriptorHandle on the left hand of the operand.</param>
    /// <param name="right">The CpuDescriptorHandle on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(CpuDescriptorHandle left, CpuDescriptorHandle right) => left.Ptr != right.Ptr;

    /// <inheritdoc/>
    public override readonly int GetHashCode() => Ptr.GetHashCode();

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => (obj is CpuDescriptorHandle other) && Equals(other);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Equals(CpuDescriptorHandle other) => Ptr == other.Ptr;

    /// <summary>
    /// Adds an offset to a descriptor handle
    /// </summary>
    /// <param name = "left">Initial descriptor handle</param>
    /// <param name = "offsetScaledByIncrementSize">Offset to apply, use <see cref="ID3D12Device.GetDescriptorHandleIncrementSize(DescriptorHeapType)"/> with the relevant heap type.</param>
    /// <returns>Offsetted descriptor handle</returns>
    public static CpuDescriptorHandle operator +(CpuDescriptorHandle left, int offsetScaledByIncrementSize) => new(left, offsetScaledByIncrementSize);

    public void InitOffsetted(in CpuDescriptorHandle @base, int offsetScaledByIncrementSize)
    {
        InitOffsetted(ref this, @base, offsetScaledByIncrementSize);
    }

    public void InitOffsetted(in CpuDescriptorHandle @base, int offsetInDescriptors, uint descriptorIncrementSize)
    {
        InitOffsetted(ref this, @base, offsetInDescriptors, descriptorIncrementSize);
    }

    public static void InitOffsetted(ref CpuDescriptorHandle handle, in CpuDescriptorHandle @base, int offsetScaledByIncrementSize)
    {
        handle.Ptr = unchecked((nuint)((long)(@base.Ptr) + (long)(offsetScaledByIncrementSize)));
    }

    public static void InitOffsetted(ref CpuDescriptorHandle handle, in CpuDescriptorHandle @base, int offsetInDescriptors, uint descriptorIncrementSize)
    {
        handle.Ptr = unchecked((nuint)((long)(@base.Ptr) + (long)(offsetInDescriptors) * (long)(descriptorIncrementSize)));
    }

    [UnscopedRef]
    public ref CpuDescriptorHandle Offset(int offsetInDescriptors, uint descriptorIncrementSize)
    {
        Ptr = unchecked((nuint)((long)(Ptr) + (long)(offsetInDescriptors) * (long)(descriptorIncrementSize)));
        return ref this;
    }

    [UnscopedRef]
    public ref CpuDescriptorHandle Offset(int offsetScaledByIncrementSize)
    {
        Ptr = unchecked((nuint)((long)(Ptr) + (long)(offsetScaledByIncrementSize)));
        return ref this;
    }
}
