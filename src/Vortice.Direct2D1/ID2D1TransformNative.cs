using System;
using System.Collections.Generic;
using System.Text;
using SharpGen.Runtime;
using Vortice.Mathematics;

namespace Vortice.Direct2D1
{
    
    public partial class ID2D1TransformNative
    {
        public void MapInputRectsToOutputRect(Rect[] inputRects, Rect[] inputOpaqueSubRects, out Rect outputRect, out Rect outputOpaqueSubRect) => MapInputRectsToOutputRect_(inputRects, inputOpaqueSubRects, out outputRect, out outputOpaqueSubRect);
        public void MapInvalidRect(int inputIndex, Rect invalidInputRect, out Rect invalidOutputRect) => MapInvalidRect_(inputIndex, invalidInputRect, out invalidOutputRect);
        public void MapOutputRectToInputRects(Rect outputRect, Rect[] inputRects) => MapOutputRectToInputRects_(outputRect, inputRects);
    }

    
}
