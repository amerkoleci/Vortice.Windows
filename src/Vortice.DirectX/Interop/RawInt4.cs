// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Vortice.Interop
{
    /// <summary>
    /// Interop type for a Int4 (4 ints).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("X: {X}, Y: {Y}, Z: {Z}, W: {W}")]
    public readonly struct RawInt4
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawInt4"/> struct.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="w">The w.</param>
        public RawInt4(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public readonly int X;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public readonly int Y;

        /// <summary>
        /// The Z component of the vector.
        /// </summary>
        public readonly int Z;

        /// <summary>
        /// The W component of the vector.
        /// </summary>
        public readonly int W;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Z)}: {Z}, {nameof(W)}: {W}";
        }
    }
}
