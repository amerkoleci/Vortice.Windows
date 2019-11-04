// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectWrite
{
    public partial class IDWriteTextLayout
    {
        public LineMetrics[] GetLineMetrics()
        {
            GetLineMetrics(null, 0, out var actualLineCount);
            var lineMetrics = new LineMetrics[actualLineCount];
            if (actualLineCount > 0)
            {
                GetLineMetrics(lineMetrics, lineMetrics.Length, out _).CheckError();
            }

            return lineMetrics;
        }
    }
}
