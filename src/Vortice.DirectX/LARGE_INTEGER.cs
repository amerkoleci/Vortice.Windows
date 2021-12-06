using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice;

[StructLayout(LayoutKind.Explicit)]
public partial struct LARGE_INTEGER
{
    [FieldOffset(0)]
    public _Anonymous_e__Struct Anonymous;

    [FieldOffset(0)]
    public _u_e__Struct u;

    [FieldOffset(0)]
    public long QuadPart;

    public ref uint LowPart
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
#if NET5_0_OR_GREATER
            return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.LowPart, 1));
#else
            return ref UnsafeUtilities.GetReference(UnsafeUtilities.CreateSpan(ref Anonymous.LowPart, 1));
#endif
        }
    }

    public ref int HighPart
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
#if NET5_0_OR_GREATER
            return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.HighPart, 1));
#else
            return ref UnsafeUtilities.GetReference(UnsafeUtilities.CreateSpan(ref Anonymous.HighPart, 1));
#endif
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
