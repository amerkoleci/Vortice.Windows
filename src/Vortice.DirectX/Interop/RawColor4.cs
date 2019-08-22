// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Vortice.Interop
{
    /// <summary>
    /// Interop type for a Color3 (RGB, 3 floats).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("R: {R}, G: {G}, B: {B}")]
    public readonly struct RawColor4
    {
        /// <summary>
        /// The red component of the color.
        /// </summary>
        public readonly float R;

        /// <summary>
        /// The green component of the color.
        /// </summary>
        public readonly float G;

        /// <summary>
        /// The blue component of the color.
        /// </summary>
        public readonly float B;

        /// <summary>
        /// The alpha component of the color.
        /// </summary>
        public readonly float A;

        /// <summary>
        /// Initializes a new instance of the <see cref="RawColor4"/> struct.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        /// <param name="a">The a.</param>
        public RawColor4(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(R)}: {R}, {nameof(G)}: {G}, {nameof(B)}: {B}, {nameof(A)}: {A}";
        }
    }
}
