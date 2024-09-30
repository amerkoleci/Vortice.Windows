// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.XAudio2;

internal unsafe partial struct DistanceCurve
{
    public static IntPtr FromCurvePoints(Span<CurvePoint> points)
    {
        if (points == null || points.Length == 0)
            return IntPtr.Zero;

        var pDistanceCurve = (DistanceCurve*)Marshal.AllocHGlobal(Unsafe.SizeOf<DistanceCurve>() + points.Length * Unsafe.SizeOf<CurvePoint>());
        var pPoints = (CurvePoint*)&pDistanceCurve[1];
        pDistanceCurve->PointCount = (uint)points.Length;
        pDistanceCurve->PointsPointer = new IntPtr(pPoints);
        MemoryHelpers.Write(pDistanceCurve->PointsPointer, points, points.Length);
        return (IntPtr)pDistanceCurve;
    }
}
