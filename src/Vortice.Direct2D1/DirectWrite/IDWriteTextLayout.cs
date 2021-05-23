// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.DirectWrite
{
    public partial class IDWriteTextLayout
    {
        public LineMetrics[] LineMetrics
        {
            get
            {
                GetLineMetrics(null, out var actualLineCount);
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
                GetClusterMetrics(null, out var actualClusterCount);
                var clusterMetrics = new ClusterMetrics[actualClusterCount];
                if (actualClusterCount > 0)
                    GetClusterMetrics(clusterMetrics, out _).CheckError();

                return clusterMetrics;
            }
        }

        public unsafe string GetFontFamilyName(int currentPosition, out TextRange textRange)
        {
            GetFontFamilyNameLength(currentPosition, out var nameLength, out textRange);

            var bufferLength = nameLength + 1;
            var fontFamilyNamePtr = stackalloc char[bufferLength];

            textRange = GetFontFamilyName(currentPosition, new IntPtr(fontFamilyNamePtr), bufferLength);

            return new string(fontFamilyNamePtr, 0, nameLength);
        }

        public unsafe string GetLocaleName(int currentPosition, out TextRange textRange)
        {
            GetLocaleNameLength(currentPosition, out var nameLength, out textRange);

            var bufferLength = nameLength + 1;
            var localeNamePtr = stackalloc char[bufferLength];

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
    }
}
