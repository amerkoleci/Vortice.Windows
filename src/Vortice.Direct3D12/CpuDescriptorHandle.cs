﻿// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

public partial struct CpuDescriptorHandle : IEquatable<CpuDescriptorHandle>
{
    public static CpuDescriptorHandle Default => default;

    /// <summary>	
    /// The address of  the descriptor.
    /// </summary>	
    public nuint ptr;

    public CpuDescriptorHandle(in CpuDescriptorHandle other, int offsetScaledByIncrementSize)
    {
        InitOffsetted(out this, other, offsetScaledByIncrementSize);
    }

    public CpuDescriptorHandle(in CpuDescriptorHandle other, int offsetInDescriptors, int descriptorIncrementSize)
    {
        InitOffsetted(out this, other, offsetInDescriptors, descriptorIncrementSize);
    }

    public CpuDescriptorHandle Offset(int offsetInDescriptors, int descriptorIncrementSize)
    {
        ptr = unchecked((nuint)((long)ptr + ((long)offsetInDescriptors * (long)descriptorIncrementSize)));
        return this;
    }

    public CpuDescriptorHandle Offset(int offsetScaledByIncrementSize)
    {
        ptr = unchecked((nuint)((long)ptr + (long)offsetScaledByIncrementSize));
        return this;
    }

    public void InitOffsetted(in CpuDescriptorHandle @base, int offsetScaledByIncrementSize)
    {
        InitOffsetted(out this, @base, offsetScaledByIncrementSize);
    }

    public void InitOffsetted(in CpuDescriptorHandle @base, int offsetInDescriptors, int descriptorIncrementSize)
    {
        InitOffsetted(out this, @base, offsetInDescriptors, descriptorIncrementSize);
    }

    public static void InitOffsetted(out CpuDescriptorHandle handle, in CpuDescriptorHandle @base, int offsetScaledByIncrementSize)
    {
        handle.ptr = (nuint)((long)@base.ptr + (long)offsetScaledByIncrementSize);
    }

    public static void InitOffsetted(out CpuDescriptorHandle handle, in CpuDescriptorHandle @base, int offsetInDescriptors, int descriptorIncrementSize)
    {
        handle.ptr = (nuint)((long)@base.ptr + ((long)offsetInDescriptors * (long)descriptorIncrementSize));
    }

    /// <summary>
    /// Adds an offset to a descriptor handle
    /// </summary>
    /// <param name = "left">Initial descriptor handle</param>
    /// <param name = "offsetScaledByIncrementSize">Offset to apply, use <see cref="ID3D12Device.GetDescriptorHandleIncrementSize(DescriptorHeapType)"/> with the relevant heap type.</param>
    /// <returns>Offsetted descriptor handle</returns>
    public static CpuDescriptorHandle operator +(CpuDescriptorHandle left, int offsetScaledByIncrementSize)
    {
        return new CpuDescriptorHandle(left, offsetScaledByIncrementSize);
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
    public static bool operator ==(CpuDescriptorHandle left, CpuDescriptorHandle right) => (left.ptr == right.ptr);

    /// <summary>
    /// Compares two <see cref="CpuDescriptorHandle"/> objects for inequality.
    /// </summary>
    /// <param name="left">The CpuDescriptorHandle on the left hand of the operand.</param>
    /// <param name="right">The CpuDescriptorHandle on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(CpuDescriptorHandle left, CpuDescriptorHandle right) => (left.ptr != right.ptr);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator nuint(CpuDescriptorHandle left) => left.ptr;

    /// <inheritdoc/>
    public override int GetHashCode() => ptr.GetHashCode();

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is CpuDescriptorHandle handle && Equals( handle);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(CpuDescriptorHandle other) => this == other;
}
