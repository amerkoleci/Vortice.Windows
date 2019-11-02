using System;
using System.Collections.Generic;
using System.Text;
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
