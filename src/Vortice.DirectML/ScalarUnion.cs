// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

[StructLayout(LayoutKind.Explicit)]
public unsafe partial struct ScalarUnion
{
    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SCALAR_UNION ::Bytes']/*" />
    [FieldOffset(0)]
    public fixed byte Bytes[8];

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SCALAR_UNION ::Int8']/*" />
    [FieldOffset(0)]
    public sbyte Int8;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SCALAR_UNION ::UInt8']/*" />
    [FieldOffset(0)]
    public byte UInt8;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SCALAR_UNION ::Int16']/*" />
    [FieldOffset(0)]
    public short Int16;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SCALAR_UNION ::UInt16']/*" />
    [FieldOffset(0)]
    public ushort UInt16;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SCALAR_UNION ::Int32']/*" />
    [FieldOffset(0)]
    public int Int32;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SCALAR_UNION ::UInt32']/*" />
    [FieldOffset(0)]
    public uint UInt32;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SCALAR_UNION ::Int64']/*" />
    [FieldOffset(0)]
    public long Int64;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SCALAR_UNION ::UInt64']/*" />
    [FieldOffset(0)]
    public ulong UInt64;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SCALAR_UNION ::Float32']/*" />
    [FieldOffset(0)]
    public float Float32;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SCALAR_UNION ::Float64']/*" />
    [FieldOffset(0)]
    public double Float64;

    public static implicit operator ScalarUnion(byte[] value)
    {
        int length = Math.Min(value.Length, 8);
        var union = new ScalarUnion();
        for (int i = 0; i < length; i++)
        {
            union.Bytes[i] = value[i];
        }
        return union;
    }

    public static implicit operator ScalarUnion(sbyte value) => new(){ Int8 = value };

    public static implicit operator ScalarUnion(byte value) => new(){ UInt8 = value };

    public static implicit operator ScalarUnion(short value) => new(){ Int16 = value };

    public static implicit operator ScalarUnion(ushort value) => new(){ UInt16 = value };

    public static implicit operator ScalarUnion(int value) => new(){ Int32 = value };

    public static implicit operator ScalarUnion(uint value) => new(){ UInt32 = value };

    public static implicit operator ScalarUnion(long value) => new(){ Int64 = value };

    public static implicit operator ScalarUnion(ulong value) => new(){ UInt64 = value };

    public static implicit operator ScalarUnion(float value) => new(){ Float32 = value };

    public static implicit operator ScalarUnion(double value) => new(){ Float64 = value };
}
