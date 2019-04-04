using System.Diagnostics;
using System.Runtime.InteropServices;
using Vortice.Mathematics;

namespace SharpDXGI
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("Left: {Left}, Top: {Top}, Right: {Right}, Bottom: {Bottom}")]
    public readonly struct RawRectangle
    {
        /// <summary>
        /// The left position.
        /// </summary>
        public readonly int Left;

        /// <summary>
        /// The top position.
        /// </summary>
        public readonly int Top;

        /// <summary>
        /// The right position
        /// </summary>
        public readonly int Right;

        /// <summary>
        /// The bottom position.
        /// </summary>
        public readonly int Bottom;

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty => Left == 0 && Top == 0 && Right == 0 && Bottom == 0;

        public RawRectangle(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rectangle"/> to <see cref="RawRectangle"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RawRectangle(Rectangle value)
        {
            return new RawRectangle(value.Left, value.Top, value.Right, value.Bottom);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="RawRectangle"/> to <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rectangle(RawRectangle value)
        {
            return new Rectangle(value.Left, value.Top, value.Right - value.Left, value.Bottom - value.Top);
        }
    }
}
