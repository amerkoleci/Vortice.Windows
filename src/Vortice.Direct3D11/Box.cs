// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D11
{
    /// <summary>
    /// Describes a 3D box.
    /// </summary>
    public partial struct Box
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> struct.
        /// </summary>
        /// <param name="left">The x position of the left hand side of the box.</param>
        /// <param name="top">The y position of the top of the box.</param>
        /// <param name="front">The z position of the front of the box.</param>
        /// <param name="right">The x position of the right hand side of the box, plus 1. This means that right - left equals the width of the box.</param>
        /// <param name="bottom">The y position of the bottom of the box, plus 1. This means that top - bottom equals the height of the box.</param>
        /// <param name="back">The z position of the back of the box, plus 1. This means that front - back equals the depth of the box.</param>
        public Box(int left, int top, int front, int right, int bottom, int back)
        {
            Left = left;
            Top = top;
            Front = front;
            Right = right;
            Bottom = bottom;
            Back = back;
        }
    }
}
