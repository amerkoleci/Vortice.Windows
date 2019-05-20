// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using Vortice.DirectX.Direct2D.DirectWrite;
using Vortice.Mathematics;

namespace Vortice.DirectX.Direct2D
{
    public partial class ID2D1Factory1
    {
        public new ID2D1DrawingStateBlock1 CreateDrawingStateBlock()
        {
            return CreateDrawingStateBlock(null, null);
        }

        public ID2D1DrawingStateBlock1 CreateDrawingStateBlock(DrawingStateDescription1 drawingStateDescription)
        {
            return CreateDrawingStateBlock(drawingStateDescription, null);
        }

        public ID2D1DrawingStateBlock1 CreateDrawingStateBlock(DrawingStateDescription1 drawingStateDescription, IDWriteRenderingParams textRenderingParams)
        {
            Guard.NotNull(textRenderingParams, nameof(textRenderingParams));
            return CreateDrawingStateBlock(drawingStateDescription, textRenderingParams);
        }

        public ID2D1StrokeStyle1 CreateStrokeStyle(StrokeStyleProperties1 properties)
        {
            return CreateStrokeStyle(ref properties, null, 0);
        }

        public ID2D1StrokeStyle1 CreateStrokeStyle(StrokeStyleProperties1 properties, float[] dashes)
        {
            Guard.NotNullOrEmpty(dashes, nameof(dashes));

            return CreateStrokeStyle(ref properties, dashes, dashes.Length);
        }
    }
}
