// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Vortice.Interop
{
    /// <summary>
    /// Interop type for a Rectangle (4 float) in left, top, right, bottom.
    /// </summary>
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

        /// <summary>
        /// Gets the width of the <see cref="RawRectangleF" />.
        /// </summary>
        public float Width => Right - Left;

        /// <summary>
        /// Gets the height of the <see cref="RawRectangleF" />.
        /// </summary>
        public float Height => Bottom - Top;

        public RawRectangleF(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Creates a new rectangle with the specified location and size.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="width">The rectangle width.</param>
        /// <param name="height">The rectangle height.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static RawRectangleF Create(float x, float y, float width, float height)
        {
            return new RawRectangleF(x, y, x + width, y + height);
        }

        public static implicit operator RawRectangleF(RawRectangle rect)
        {
            return new RawRectangleF(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rectangle"/> to <see cref="RawRectangle"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RawRectangleF(RectangleF value)
        {
            return new RawRectangleF(value.Left, value.Top, value.Right, value.Bottom);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="RawRectangle"/> to <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectangleF(RawRectangleF value)
        {
            return RectangleF.FromLTRB(value.Left, value.Top, value.Right, value.Bottom);
        }
    }
}
