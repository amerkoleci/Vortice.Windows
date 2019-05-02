// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace SharpDirect2D
{
    internal partial class ID2D1GeometrySinkNative
    {
        public void AddLine(Vector2 point) => AddLine_(point);

        public void AddBezier(BezierSegment bezier) => AddBezier_(ref bezier);

        public void AddQuadraticBezier(QuadraticBezierSegment bezier) => AddQuadraticBezier_(bezier);

        public void AddQuadraticBeziers(QuadraticBezierSegment[] beziers) => AddQuadraticBeziers_(beziers, beziers.Length);

        public void AddArc(ArcSegment arc) => AddArc_(ref arc);
    }
}
