// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Drawing;
using System.Numerics;
using Vortice.Interop;

namespace Vortice.DirectX.Direct2D
{
    public partial class ID2D1DeviceContext
    {
        public void DrawBitmap(ID2D1Bitmap bitmap, float opacity, InterpolationMode interpolationMode)
        {
            DrawBitmap(bitmap, null, opacity, interpolationMode, null, null);
        }

        public void DrawBitmap(ID2D1Bitmap bitmap, float opacity, InterpolationMode interpolationMode, Matrix4x4 perspectiveTransformRef)
        {
            DrawBitmap(bitmap, null, opacity, interpolationMode, null, perspectiveTransformRef);
        }

        public void DrawBitmap(ID2D1Bitmap bitmap, float opacity, InterpolationMode interpolationMode, RectangleF sourceRectangle, Matrix4x4 perspectiveTransformRef)
        {
            DrawBitmap(bitmap, null, opacity, interpolationMode, sourceRectangle, perspectiveTransformRef);
        }

        public void DrawImage(
            ID2D1Image image,
            Vector2 targetOffset,
            InterpolationMode interpolationMode = InterpolationMode.Linear,
            CompositeMode compositeMode = CompositeMode.SourceOver)
        {
            DrawImage(image, targetOffset, null, interpolationMode, compositeMode);
        }

        public void DrawImage(
            ID2D1Image image,
            InterpolationMode interpolationMode = InterpolationMode.Linear,
            CompositeMode compositeMode = CompositeMode.SourceOver)
        {
            DrawImage(image, null, null, interpolationMode, compositeMode);
        }

        public void DrawImage(
            ID2D1Effect effect,
            Vector2 targetOffset,
            InterpolationMode interpolationMode = InterpolationMode.Linear,
            CompositeMode compositeMode = CompositeMode.SourceOver)
        {
            using (var output = effect.Output)
            {
                DrawImage(output, targetOffset, null, interpolationMode, compositeMode);
            }
        }

        public void DrawImage(
            ID2D1Effect effect,
            InterpolationMode interpolationMode = InterpolationMode.Linear,
            CompositeMode compositeMode = CompositeMode.SourceOver)
        {
            using (var output = effect.Output)
            {
                DrawImage(output, null, null, interpolationMode, compositeMode);
            }
        }

        public void PushLayer(LayerParameters1 layerParameters, ID2D1Layer layer)
        {
            PushLayer(ref layerParameters, layer);
        }

        public RawRectangleF[] GetEffectInvalidRectangles(ID2D1Effect effect)
        {
            var invalidRects = new RawRectangleF[GetEffectInvalidRectangleCount(effect)];
            if (invalidRects.Length == 0)
            {
                return invalidRects;
            }

            GetEffectInvalidRectangles(effect, invalidRects, invalidRects.Length);
            return invalidRects;
        }

        public RawRectangleF[] GetEffectRequiredInputRectangles(ID2D1Effect renderEffect, EffectInputDescription[] inputDescriptions)
        {
            var result = new RawRectangleF[inputDescriptions.Length];
            GetEffectRequiredInputRectangles(renderEffect, null, inputDescriptions, result, inputDescriptions.Length);
            return result;
        }

        public RawRectangleF[] GetEffectRequiredInputRectangles(ID2D1Effect renderEffect, RectangleF renderImageRectangle, EffectInputDescription[] inputDescriptions)
        {
            var result = new RawRectangleF[inputDescriptions.Length];
            GetEffectRequiredInputRectangles(renderEffect, renderImageRectangle, inputDescriptions, result, inputDescriptions.Length);
            return result;
        }
    }
}
