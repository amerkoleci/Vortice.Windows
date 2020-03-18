// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct2D1
{
    public partial class ID2D1TransformNative
    {
        public void MapInputRectsToOutputRect(RawRect[] inputRects, RawRect[] inputOpaqueSubRects, out RawRect outputRect, out RawRect outputOpaqueSubRect) => MapInputRectsToOutputRect_(inputRects, inputOpaqueSubRects, out outputRect, out outputOpaqueSubRect);
        public void MapInvalidRect(int inputIndex, RawRect invalidInputRect, out RawRect invalidOutputRect) => MapInvalidRect_(inputIndex, invalidInputRect, out invalidOutputRect);
        public void MapOutputRectToInputRects(RawRect outputRect, RawRect[] inputRects) => MapOutputRectToInputRects_(outputRect, inputRects);
    }

    
}
