using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Vortice
{
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay("X: {X}, Y: {Y}")]
    public readonly struct Point
    {
        /// <summary>
        /// Left coordinate.
        /// </summary>
        public readonly int X;

        /// <summary>
        /// Top coordinate.
        /// </summary>
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
