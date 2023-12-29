// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice;

[StructLayout(LayoutKind.Explicit)]
internal struct LARGE_INTEGER
{
    [FieldOffset(0)]
    public _Anonymous_e__Struct Anonymous;

    [FieldOffset(0)]
    public _u_e__Struct u;

    [FieldOffset(0)]
    public long QuadPart;

    [UnscopedRef]
    public ref uint LowPart
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return ref Anonymous.LowPart;
        }
    }

    [UnscopedRef]
    public ref int HighPart
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return ref Anonymous.HighPart;
        }
    }

    public partial struct _Anonymous_e__Struct
    {
        public uint LowPart;
        public int HighPart;
    }

    public partial struct _u_e__Struct
    {
        public uint LowPart;
        public int HighPart;
    }
}
