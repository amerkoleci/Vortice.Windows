// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Vortice.Interop
{
    /// <summary>
    /// Interop type for a Int3 (3 ints).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("X: {X}, Y: {Y}, Z: {Z}")]
    public readonly struct RawInt3
    {
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
        /// Initializes a new instance of the <see cref="RawInt3"/> struct.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public RawInt3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Z)}: {Z}";
        }
    }
}
