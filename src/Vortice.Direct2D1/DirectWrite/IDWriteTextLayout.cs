using System;
using System.Collections.Generic;
using System.Text;
using Vortice.DirectWrite;

namespace Vortice.DirectWrite
{
    partial class IDWriteTextLayout
    {
        public LineMetrics[] GetLineMetrics()
        {
            GetLineMetrics(null, 0, out var actualLineCount);
            var lineMetrics = new LineMetrics[actualLineCount];
            if (actualLineCount > 0)
                GetLineMetrics(lineMetrics, lineMetrics.Length, out _).CheckError();
            return lineMetrics;
        }
    }
}
