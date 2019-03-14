// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice
{
    /// <summary>
	/// Defines a four dimensional mathematical vector (signed integers).
	/// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DataContract(Name = nameof(Int4))]
    public struct Int4 : IEquatable<Int4>
    {
        private static readonly string _formatString = "X:{0} Y:{1} Z:{2} W:{3}";

        /// <summary>
        /// The size of the <see cref="Int4" /> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Int4));

        /// <summary>
        /// A <see cref="Int4" /> with all of its components set to zero.
        /// </summary>
        public static readonly Int4 Zero;

        /// <summary>
        /// The X unit <see cref="Int4" /> (1, 0, 0, 0).
        /// </summary>
        public static readonly Int4 UnitX = new Int4(1, 0, 0, 0);

        /// <summary>
        /// The Y unit <see cref="Int4" /> (0, 1, 0, 0).
        /// </summary>
        public static readonly Int4 UnitY = new Int4(0, 1, 0, 0);

        /// <summary>
        /// The Z unit <see cref="Int4" /> (0, 0, 1, 0).
        /// </summary>
        public static readonly Int4 UnitZ = new Int4(0, 0, 1, 0);

        /// <summary>
        /// The W unit <see cref="Int4" /> (0, 0, 0, 1).
        /// </summary>
        public static readonly Int4 UnitW = new Int4(0, 0, 0, 1);

        /// <summary>
        /// A <see cref="Int4" /> with all of its components set to one.
        /// </summary>
        public static readonly Int4 One = new Int4(1, 1, 1, 1);

        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public int X;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public int Y;

        /// <summary>
        /// The Z component of the vector.
        /// </summary>
        public int Z;

        /// <summary>
        /// The W component of the vector.
        /// </summary>
        public int W;

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Int4(int value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Int4" /> struct.
        /// </summary>
        /// <param name="x">Initial value for the X component of the vector.</param>
        /// <param name="y">Initial value for the Y component of the vector.</param>
        /// <param name="z">Initial value for the Z component of the vector.</param>
        /// <param name="w">Initial value for the W component of the vector.</param>
        public Int4(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Int4"/> struct.
        /// </summary>
        /// <param name="value">The x, y, z, and w components of the vector.</param>
        public Int4(Vector4 value)
        {
            X = (int)value.X;
            Y = (int)value.Y;
            Z = (int)value.Z;
            W = (int)value.W;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Int4"/> struct.
        /// </summary>
        /// <param name="value">The x, y, and z components of the vector.</param>
        /// <param name="w">The w component of the vector.</param>
        public Int4(Vector3 value, int w)
        {
            X = (int)value.X;
            Y = (int)value.Y;
            Z = (int)value.Z;
            W = w;
        }

        /// <summary>
        /// Compares two <see cref="Int4"/> objects for equality.
        /// </summary>
        /// <param name="left">The Int4 on the left hand of the operand.</param>
        /// <param name="right">The Int4 on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Int4 left, Int4 right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Int4"/> objects for inequality.
        /// </summary>
        /// <param name="left">The Int4 on the left hand of the operand.</param>
        /// <param name="right">The Int4 on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Int4 left, Int4 right) => !left.Equals(ref right);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelpers.Combine(
            X.GetHashCode(), Y.GetHashCode(),
            Z.GetHashCode(), W.GetHashCode());

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, _formatString, X, Y, Z, W);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Int4 value && Equals(ref value);

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Int4 other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Int4"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Int4"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Int4 other)
        {
            return X == other.X
                 && Y == other.Y
                 && Z == other.Z
                 && W == other.W;
        }
    }
}
