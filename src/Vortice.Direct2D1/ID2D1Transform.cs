// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.Direct2D1
{

    [Shadow(typeof(ID2D1TransformShadow))]
    public partial interface ID2D1Transform
    {
        void MapOutputRectToInputRects(RawRect outputRect, RawRect[] inputRects);
        void MapInputRectsToOutputRect(RawRect[] inputRects, RawRect[] inputOpaqueSubRects, out RawRect outputRect, out RawRect outputOpaqueSubRect);
        void MapInvalidRect(int inputIndex, RawRect invalidInputRect, out RawRect invalidOutputRect);
    }
}
