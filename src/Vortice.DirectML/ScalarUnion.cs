// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

[StructLayout(LayoutKind.Explicit)]
public unsafe partial struct ScalarUnion
{
    [FieldOffset(0)]
    public fixed byte Bytes[8];

    [FieldOffset(0)]
    public sbyte Int8;

    [FieldOffset(0)]
    public byte UInt8;

    [FieldOffset(0)]
    public short Int16;

    [FieldOffset(0)]
    public ushort UInt16;

    [FieldOffset(0)]
    public int Int32;

    [FieldOffset(0)]
    public uint UInt32;

    [FieldOffset(0)]
    public long Int64;

    [FieldOffset(0)]
    public ulong UInt64;

    [FieldOffset(0)]
    public float Float32;

    [FieldOffset(0)]
    public double Float64;
}
