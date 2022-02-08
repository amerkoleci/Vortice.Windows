// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes a memory range in a 64-bit address space.
/// </summary>
public partial struct RangeUInt64 : IEquatable<RangeUInt64>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RangeUInt64"/> struct.
    /// </summary>
    /// <param name="begin">The offset, in bytes, denoting the beginning of a memory range.</param>
    /// <param name="end">The offset, in bytes, denoting the end of a memory range. End is one-past-the-end.</param>
    /// <remarks>
    /// End is one-past-the-end. When Begin equals End, the range is empty. The size of the range is (End - Begin).
    /// </remarks>
    public RangeUInt64(ulong begin, ulong end)
    {
        Begin = begin;
        End = end;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is RangeUInt64 value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="RangeUInt64"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="RangeUInt64"/> to compare with this instance.</param>
    public bool Equals(RangeUInt64 other)
    {
        return Begin == other.Begin && End == other.End;
    }

    /// <summary>
    /// Compares two <see cref="RangeUInt64"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="RangeUInt64"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="RangeUInt64"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    public static bool operator ==(RangeUInt64 left, RangeUInt64 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="RangeUInt64"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="RangeUInt64"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="RangeUInt64"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    public static bool operator !=(RangeUInt64 left, RangeUInt64 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(Begin, End);

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{nameof(RangeUInt64)}({Begin}, {End})";
    }
}
