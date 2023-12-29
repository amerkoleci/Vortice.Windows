// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Vortice;

/// <summary>
/// A locally unique identifier for a graphics device.
/// </summary>
public readonly struct Luid : IEquatable<Luid>
    , ISpanFormattable
{
    /// <summary>
    /// The low bits of the luid.
    /// </summary>
    public readonly uint LowPart;

    /// <summary>
    /// The high bits of the luid.
    /// </summary>
    public readonly int HighPart;

    public Luid(uint lowPart, int highPart)
    {
        LowPart = lowPart;
        HighPart = highPart;
    }

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Luid FromInt64(long int64)
    {
        LARGE_INTEGER val = new()
        {
            QuadPart = int64
        };

        return new Luid(val.Anonymous.LowPart, val.Anonymous.HighPart);
    }

    /// <inheritdoc/>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Luid other)
    {
        return
            LowPart == other.LowPart &&
            HighPart == other.HighPart;
    }

    /// <inheritdoc/>
    [Pure]
    public override bool Equals(object? other)
    {
        return other is Luid luid && Equals(luid);
    }

    /// <inheritdoc/>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(LowPart, HighPart);
    }

    /// <inheritdoc/>
    [Pure]
    public override string ToString()
    {
        return (((long)HighPart) << 32 | LowPart).ToString();
    }

    /// <inheritdoc/>
    [Pure]
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return (((long)HighPart) << 32 | LowPart).ToString(format, formatProvider);
    }

    /// <inheritdoc/>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        return (((long)HighPart) << 32 | LowPart).TryFormat(destination, out charsWritten, format, provider);
    }

    /// <summary>
    /// Check whether two <see cref="Luid"/> values are equal.
    /// </summary>
    /// <param name="a">The first <see cref="Luid"/> value to compare.</param>
    /// <param name="b">The second <see cref="Luid"/> value to compare.</param>
    /// <returns>Whether <paramref name="a"/> and <paramref name="b"/> are the same.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Luid a, Luid b)
    {
        return a.Equals(b);
    }

    /// <summary>
    /// Check whether two <see cref="Luid"/> values are different.
    /// </summary>
    /// <param name="a">The first <see cref="Luid"/> value to compare.</param>
    /// <param name="b">The second <see cref="Luid"/> value to compare.</param>
    /// <returns>Whether <paramref name="a"/> and <paramref name="b"/> are different.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Luid a, Luid b)
    {
        return !a.Equals(b);
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Luid"/> to <see cref="long"/>.
    /// </summary>
    /// <param name="luid">The <see cref="Luid"/></param>
    public static implicit operator long(Luid luid)
    {
        LARGE_INTEGER val = new LARGE_INTEGER();
        val.Anonymous.LowPart = luid.LowPart;
        val.Anonymous.HighPart = luid.HighPart;
        return val.QuadPart;
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="long"/> to <see cref="Luid"/>.
    /// </summary>
    /// <param name="int64">The <see cref="long"/> to convert from.</param>
    public static explicit operator Luid(long int64)
    {
        return FromInt64(int64);
    }
}
