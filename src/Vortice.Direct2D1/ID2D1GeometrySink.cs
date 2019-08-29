// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;
using SharpGen.Runtime;

namespace Vortice.Direct2D1
{
    [Shadow(typeof(ID2D1GeometrySinkShadow))]
    public partial interface ID2D1GeometrySink
    {
        /// <summary>	
        /// Creates a line segment between the current point and the specified end point and adds it to the geometry sink. 	
        /// </summary>	
        /// <param name="point">The end point of the line to draw.</param>
        void AddLine(Vector2 point);

        /// <summary>	
        ///  Creates  a cubic Bezier curve between the current point and the specified endpoint.	
        /// </summary>	
        /// <param name="bezier">A structure that describes the control points and endpoint of the Bezier curve to add. </param>
        void AddBezier(BezierSegment bezier);

        /// <summary>	
        /// Creates  a quadratic Bezier curve between the current point and the specified endpoint.	
        /// </summary>	
        /// <param name="bezier">A structure that describes the control point and the endpoint of the quadratic Bezier curve to add.</param>
        /// <unmanaged>void AddQuadraticBezier([In] const D2D1_QUADRATIC_BEZIER_SEGMENT* bezier)</unmanaged>
        void AddQuadraticBezier(QuadraticBezierSegment bezier);


        /// <summary>	
        /// Adds a sequence of quadratic Bezier segments as an array in a single call.	
        /// </summary>	
        /// <param name="beziers">An array of a sequence of quadratic Bezier segments.</param>
        void AddQuadraticBeziers(QuadraticBezierSegment[] beziers);

        /// <summary>	
        /// Adds a single arc to the path geometry.	
        /// </summary>	
        /// <param name="arc">The arc segment to add to the figure.</param>
        void AddArc(ArcSegment arc);
    }
}
