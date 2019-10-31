// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.X3DAudio
{
    internal partial struct DistanceCurve
    {
        public static unsafe IntPtr FromCurvePoints(CurvePoint[] points)
        {
            if (points == null || points.Length == 0)
                return IntPtr.Zero;

            var pDistanceCurve = (DistanceCurve*)Marshal.AllocHGlobal(Unsafe.SizeOf<DistanceCurve>() + points.Length * Unsafe.SizeOf<CurvePoint>());
            var pPoints = (CurvePoint*)&pDistanceCurve[1];
            pDistanceCurve->PointCount = points.Length;
            pDistanceCurve->PointsPointer = new IntPtr(pPoints);
            MemoryHelpers.Write(pDistanceCurve->PointsPointer, new Span<CurvePoint>(points), points.Length);
            return (IntPtr)pDistanceCurve;
        }
    }
}
