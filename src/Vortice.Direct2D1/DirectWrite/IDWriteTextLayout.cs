// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;

namespace Vortice.DirectWrite;

public unsafe partial class IDWriteTextLayout
{
    public LineMetrics[] LineMetrics
    {
        get
        {
            GetLineMetrics(null, out int actualLineCount);
            var lineMetrics = new LineMetrics[actualLineCount];
            if (actualLineCount > 0)
                GetLineMetrics(lineMetrics, out _).CheckError();

            return lineMetrics;
        }
    }

    public ClusterMetrics[] ClusterMetrics
    {
        get
        {
            GetClusterMetrics(null, out int actualClusterCount);
            var clusterMetrics = new ClusterMetrics[actualClusterCount];
            if (actualClusterCount > 0)
                GetClusterMetrics(clusterMetrics, out _).CheckError();

            return clusterMetrics;
        }
    }

    public string GetFontFamilyName(int currentPosition, out TextRange textRange)
    {
        GetFontFamilyNameLength(currentPosition, out int nameLength, out textRange);

        int bufferLength = nameLength + 1;
        char* fontFamilyNamePtr = stackalloc char[bufferLength];

        textRange = GetFontFamilyName(currentPosition, new IntPtr(fontFamilyNamePtr), bufferLength);

        return new string(fontFamilyNamePtr, 0, nameLength);
    }

    public unsafe string GetLocaleName(int currentPosition, out TextRange textRange)
    {
        GetLocaleNameLength(currentPosition, out int nameLength, out textRange);

        int bufferLength = nameLength + 1;
        char* localeNamePtr = stackalloc char[bufferLength];

        textRange = GetLocaleName(currentPosition, new IntPtr(localeNamePtr), bufferLength);

        return new string(localeNamePtr, 0, nameLength);
    }

    public HitTestMetrics[] HitTestTextRange(int textPosition, int textLength, float originX, float originY)
    {
        HitTestTextRange(textPosition, textLength, originX, originY, null, out int actualHitTestMetricsCount);

        var hitTestMetrics = new HitTestMetrics[actualHitTestMetricsCount];
        if (actualHitTestMetricsCount > 0)
            HitTestTextRange(textPosition, textLength, originX, originY, hitTestMetrics, out _).CheckError();

        return hitTestMetrics;
    }

    public HitTestMetrics HitTestPoint(in Vector2 point, out RawBool isTrailingHit, out RawBool isInside)
    {
        HitTestPoint(point.X, point.Y, out isTrailingHit, out isInside, out HitTestMetrics hitTestMetrics);
        return hitTestMetrics;
    }

    public HitTestMetrics HitTestPoint(float pointX, float pointY, out RawBool isTrailingHit, out RawBool isInside)
    {
        HitTestPoint(pointX, pointY, out isTrailingHit, out isInside, out HitTestMetrics hitTestMetrics);
        return hitTestMetrics;
    }

    public HitTestMetrics HitTestTextPosition(int textPosition, bool isTrailingHit)
    {
        HitTestTextPosition(textPosition, isTrailingHit, out _, out _, out HitTestMetrics hitTestMetrics);
        return hitTestMetrics;
    }

    public HitTestMetrics HitTestTextPosition(int textPosition, bool isTrailingHit, out Vector2 point)
    {
        HitTestTextPosition(textPosition, isTrailingHit, out float x, out float y, out HitTestMetrics hitTestMetrics);
        point = new(x, y);
        return hitTestMetrics;
    }

    public void Draw(IDWriteTextRenderer renderer, float originX, float originY)
    {
        Draw(IntPtr.Zero, renderer, originX, originY);
    }

    public void Draw(IDWriteTextRenderer renderer, in Vector2 origin)
    {
        Draw(IntPtr.Zero, renderer, origin.X, origin.Y);
    }
}
