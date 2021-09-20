// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Drawing;

namespace Vortice.Direct2D1
{
    public partial class ID2D1DeviceContext
    {
        public ID2D1BitmapBrush1 CreateBitmapBrush(ID2D1Bitmap bitmap)
        {
            return CreateBitmapBrush(bitmap, null, null);
        }

        public void DrawBitmap(ID2D1Bitmap bitmap, float opacity, InterpolationMode interpolationMode)
        {
            DrawBitmap(bitmap, null, opacity, interpolationMode, null, null);
        }

        public void DrawBitmap(ID2D1Bitmap bitmap, float opacity, InterpolationMode interpolationMode, in Matrix4x4 perspectiveTransform)
        {
            DrawBitmap(bitmap, null, opacity, interpolationMode, null, perspectiveTransform);
        }

        public void DrawBitmap(ID2D1Bitmap bitmap, float opacity, InterpolationMode interpolationMode, in RawRectF sourceRectangle, in Matrix4x4 perspectiveTransform)
        {
            DrawBitmap(bitmap, null, opacity, interpolationMode, sourceRectangle, perspectiveTransform);
        }

        public void DrawBitmap(ID2D1Bitmap bitmap, in RectangleF destinationRectangle, float opacity, InterpolationMode interpolationMode, in RectangleF sourceRectangle, in Matrix4x4 perspectiveTransform)
        {
            RawRectF destRect = destinationRectangle;
            RawRectF sourceRect = sourceRectangle;
            DrawBitmap(bitmap, (RawRectF?)destRect, opacity, interpolationMode, (RawRectF?)sourceRect, perspectiveTransform);
        }

        public void DrawBitmap(ID2D1Bitmap bitmap, float opacity, InterpolationMode interpolationMode, in RectangleF sourceRectangle, in Matrix4x4 perspectiveTransform)
        {
            RawRectF sourceRect = sourceRectangle;
            DrawBitmap(bitmap, null, opacity, interpolationMode, (RawRectF?)sourceRect, perspectiveTransform);
        }

        public void DrawImage(
            ID2D1Image image,
            in PointF targetOffset,
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
            in PointF targetOffset,
            InterpolationMode interpolationMode = InterpolationMode.Linear,
            CompositeMode compositeMode = CompositeMode.SourceOver)
        {
            using var output = effect.Output;
            DrawImage(output, targetOffset, null, interpolationMode, compositeMode);
        }

        public void DrawImage(
            ID2D1Effect effect,
            InterpolationMode interpolationMode = InterpolationMode.Linear,
            CompositeMode compositeMode = CompositeMode.SourceOver)
        {
            using var output = effect.Output;
            DrawImage(output, null, null, interpolationMode, compositeMode);
        }

        public void PushLayer(LayerParameters1 layerParameters, ID2D1Layer layer)
        {
            PushLayer(ref layerParameters, layer);
        }

        public RawRectF[] GetEffectInvalidRectangles(ID2D1Effect effect)
        {
            var invalidRects = new RawRectF[GetEffectInvalidRectangleCount(effect)];
            if (invalidRects.Length == 0)
            {
                return invalidRects;
            }

            GetEffectInvalidRectangles(effect, invalidRects, invalidRects.Length);
            return invalidRects;
        }

        public void GetEffectInvalidRectangles(ID2D1Effect effect, RawRectF[] invalidRects)
        {
            GetEffectInvalidRectangles(effect, invalidRects, invalidRects.Length);
        }

        public RawRectF[] GetEffectRequiredInputRectangles(ID2D1Effect renderEffect, EffectInputDescription[] inputDescriptions)
        {
            var result = new RawRectF[inputDescriptions.Length];
            GetEffectRequiredInputRectangles(renderEffect, null, inputDescriptions, result, inputDescriptions.Length);
            return result;
        }

        public RawRectF[] GetEffectRequiredInputRectangles(ID2D1Effect renderEffect, RawRectF renderImageRectangle, EffectInputDescription[] inputDescriptions)
        {
            var result = new RawRectF[inputDescriptions.Length];
            GetEffectRequiredInputRectangles(renderEffect, renderImageRectangle, inputDescriptions, result, inputDescriptions.Length);
            return result;
        }

        public void GetEffectRequiredInputRectangles(ID2D1Effect renderEffect, EffectInputDescription[] inputDescriptions, RawRectF[] requiredInputRects)
        {
            GetEffectRequiredInputRectangles(renderEffect, null, inputDescriptions, requiredInputRects, inputDescriptions.Length);
        }

        public void GetEffectRequiredInputRectangles(ID2D1Effect renderEffect, RawRectF renderImageRectangle, EffectInputDescription[] inputDescriptions, RawRectF[] requiredInputRects)
        {
            GetEffectRequiredInputRectangles(renderEffect, renderImageRectangle, inputDescriptions, requiredInputRects, inputDescriptions.Length);
        }
    }
}
