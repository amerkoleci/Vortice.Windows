
// Copyright (c) 2010-2013 SharpDX - Alexandre Mutel
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Vortice
{
    /// <summary>
    /// Defines an integer rectangle (Left, Top, Right, Bottom)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("Left: {Left}, Top: {Top}, Right: {Right}, Bottom: {Bottom}")]
    public struct RawRect
    {
        public RawRect(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// The left position.
        /// </summary>
        public int Left;

        /// <summary>
        /// The top position.
        /// </summary>
        public int Top;

        /// <summary>
        /// The right position
        /// </summary>
        public int Right;

        /// <summary>
        /// The bottom position.
        /// </summary>
        public int Bottom;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(Left)}: {Left}, {nameof(Top)}: {Top}, {nameof(Right)}: {Right}, {nameof(Bottom)}: {Bottom}";
        }

        /// <summary>
        /// Performs an implicit conversion from <see cre ="RawRect"/> to <see cref="System.Drawing.Rectangle" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rectangle(RawRect value) => Rectangle.FromLTRB(value.Left, value.Top, value.Right, value.Bottom);

        /// <summary>
        /// Performs an implicit conversion from <see cre ="Rectangle"/> to <see cref="RawRect" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RawRect(Rectangle value) => new RawRect(value.Left, value.Top, value.Right, value.Bottom);
    }
}
