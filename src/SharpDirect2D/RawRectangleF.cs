using System.Diagnostics;
using System.Runtime.InteropServices;
using Vortice.Mathematics;

namespace SharpDirect2D
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

        /// <summary>
        /// Performs an implicit conversion from <see cref="RectangleF"/> to <see cref="RawRectangleF"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RawRectangleF(RectangleF value)
        {
            return new RawRectangleF(value.Left, value.Top, value.Right, value.Bottom);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="RawRectangleF"/> to <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectangleF(RawRectangleF value)
        {
            return new RectangleF(value.Left, value.Top, value.Right - value.Left, value.Bottom - value.Top);
        }
    }
}
