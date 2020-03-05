// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Drawing;

namespace Vortice.Direct2D1
{
    internal partial class ID2D1SimplifiedGeometrySinkNative
    {
        public void AddBeziers(BezierSegment[] beziers) => AddBeziers_(beziers, beziers.Length);

        public void AddLines(PointF[] points) => AddLines_(points, points.Length);

        public void BeginFigure(PointF startPoint, FigureBegin figureBegin) => BeginFigure_(startPoint, figureBegin);

        public void Close() => Close_();

        public void EndFigure(FigureEnd figureEnd) => EndFigure_(figureEnd);

        public void SetFillMode(FillMode fillMode) => SetFillMode_(fillMode);

        public void SetSegmentFlags(PathSegment vertexFlags) => SetSegmentFlags_(vertexFlags);
    }
}
