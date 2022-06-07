using System;
using System.Runtime.InteropServices;

namespace Vortice;

/// <summary>
/// 24 bit unsigned integer.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 3)]
public struct UInt24 : IEquatable<UInt24>
{
    #region Constants

    public static readonly UInt24 MinValue = new UInt24(0);

    public static readonly UInt24 MaxValue = new UInt24(0xFFFFFF);

    #endregion

    #region Variables

    private ushort mValue0;
    private byte mValue1;

    #endregion

    #region Constructors

    public UInt24(uint value)
    {
        mValue0 = (ushort)(value & 0xFFFF);
        mValue1 = (byte)((value >> 16) & 0xFF);
    }

    #endregion

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (!(obj is UInt24))
            return false;

        return Equals((UInt24)obj);
    }

    /// <inheritdoc/>
    public bool Equals(UInt24 other)
    {
        return mValue0 == other.mValue0 && mValue1 == other.mValue1;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return (int)(uint)this;
    }

    #region Operators

    public static bool operator ==(UInt24 x, UInt24 y)
    {
        return x.Equals(y);
    }

    public static bool operator !=(UInt24 x, UInt24 y)
    {
        return !x.Equals(y);
    }


    public static implicit operator uint(UInt24 x)
    {
        return x.mValue0 | ((uint)x.mValue1 << 16);
    }

    public static explicit operator UInt24(uint x)
    {
        return new UInt24(x);
    }

    #endregion

    /// <inheritdoc/>
    public override string ToString()
    {
        return ((uint)this).ToString();
    }
}
