// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;
using Vortice.Mathematics;

namespace Vortice.Direct2D1
{

    [Shadow(typeof(ID2D1TransformShadow))]
    public partial interface ID2D1Transform
    {
        void MapOutputRectToInputRects(Rect outputRect, Rect[] inputRects);
        void MapInputRectsToOutputRect(Rect[] inputRects, Rect[] inputOpaqueSubRects, out Rect outputRect, out Rect outputOpaqueSubRect);
        void MapInvalidRect(int inputIndex, Rect invalidInputRect, out Rect invalidOutputRect);
    }
}
