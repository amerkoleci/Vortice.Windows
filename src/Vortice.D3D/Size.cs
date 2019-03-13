// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Vortice
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("Width: {Width}, Height: {Height}")]
    public readonly struct Size
    {
        /// <summary>
        /// Width.
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// Height.
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// A zero size with (width, height) = (0,0)
        /// </summary>
        public static readonly Size Zero = new Size(0, 0);

        /// <summary>
        /// A zero size with (width, height) = (0,0)
        /// </summary>
        public static readonly Size Empty = Zero;


        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> struct.
        /// </summary>
        /// <param name="width">The x.</param>
        /// <param name="height">The y.</param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
