// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace Vortice.Direct3D11
{
    /// <summary>
    /// Describes a 3D box.
    /// </summary>
    public partial struct Box : IEquatable<Box>
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

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Box box && Equals(ref box);

        /// <summary>
        /// Determines whether the specified <see cref="Box"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Box"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Box other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Box"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Box"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Box other)
        {
            return Left == other.Left
                && Top == other.Top
                && Front == other.Front
                && Right == other.Right
                && Bottom == other.Bottom
                && Back == other.Back;
        }

        /// <summary>
        /// Compares two <see cref="Box"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Box"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Box"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Box left, Box right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Box"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Box"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Box"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Box left, Box right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Left.GetHashCode();
                hashCode = (hashCode * 397) ^ Top.GetHashCode();
                hashCode = (hashCode * 397) ^ Front.GetHashCode();
                hashCode = (hashCode * 397) ^ Right.GetHashCode();
                hashCode = (hashCode * 397) ^ Bottom.GetHashCode();
                hashCode = (hashCode * 397) ^ Back.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(Left)}={Left}, {nameof(Top)}={Top}, {nameof(Front)}={Front}, {nameof(Right)}={Right}, {nameof(Bottom)}={Bottom}, {nameof(Back)}={Back}";
        }
    }
}
