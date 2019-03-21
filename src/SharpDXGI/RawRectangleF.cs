using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SharpDXGI
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("Left: {Left}, Top: {Top}, Right: {Right}, Bottom: {Bottom}")]
    public readonly struct RawRectangleF
    {
        /// <summary>
        /// The left position.
        /// </summary>
        public readonly float Left;

        /// <summary>
        /// The top position.
        /// </summary>
        public readonly float Top;

        /// <summary>
        /// The right position
        /// </summary>
        public readonly float Right;

        /// <summary>
        /// The bottom position.
        /// </summary>
        public readonly float Bottom;

        public RawRectangleF(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }
}
