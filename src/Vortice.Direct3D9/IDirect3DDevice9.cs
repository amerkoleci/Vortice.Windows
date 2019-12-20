// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using Vortice.Mathematics;

namespace Vortice.Direct3D9
{
    public partial class IDirect3DDevice9
    {
        /// <summary>
        /// Presents the contents of the next buffer in the sequence of back buffers to the screen.
        /// </summary>
        /// <returns>A <see cref="SharpGen.Runtime.Result" /> object describing the result of the operation.</returns>
        public Result Present()
        {
            return Present(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// Presents the contents of the next buffer in the sequence of back buffers to the screen.
        /// </summary>
        /// <param name="sourceRectangle">The area of the back buffer that should be presented.</param>
        /// <param name="destinationRectangle">The area of the front buffer that should receive the result of the presentation.</param>
        /// <returns>A <see cref="SharpGen.Runtime.Result" /> object describing the result of the operation.</returns>
        public Result Present(Rect sourceRectangle, Rect destinationRectangle)
        {
            return Present(sourceRectangle, destinationRectangle, IntPtr.Zero);
        }


        /// <summary>
        /// Presents the contents of the next buffer in the sequence of back buffers to the screen.
        /// </summary>
        /// <param name="sourceRectangle">The area of the back buffer that should be presented.</param>
        /// <param name="destinationRectangle">The area of the front buffer that should receive the result of the presentation.</param>
        /// <param name="windowOverride">The destination window whose client area is taken as the target for this presentation.</param>
        /// <returns>A <see cref="SharpGen.Runtime.Result" /> object describing the result of the operation.</returns>
        public Result Present(Rect sourceRectangle, Rect destinationRectangle, IntPtr windowOverride)
        {
            unsafe
            {
                return Present(new IntPtr(&sourceRectangle), new IntPtr(&destinationRectangle), windowOverride, IntPtr.Zero);
            }
        }
    }
}
