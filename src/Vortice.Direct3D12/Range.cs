// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes a memory range.
    /// </summary>
    public partial struct Range : IEquatable<Range>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Range"/> struct.
        /// </summary>
        /// <param name="begin">The offset, in bytes, denoting the beginning of a memory range.</param>
        /// <param name="end">The offset, in bytes, denoting the end of a memory range. End is one-past-the-end.</param>
        /// <remarks>
        /// End is one-past-the-end. When Begin equals End, the range is empty. The size of the range is (End - Begin).
        /// </remarks>
        public Range(PointerSize begin, PointerSize end)
        {
            Begin = begin;
            End = end;
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Range value && Equals(value);

        /// <summary>
        /// Determines whether the specified <see cref="Range"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Range"/> to compare with this instance.</param>
        public bool Equals(Range other)
        {
            return Begin.Equals(other.Begin)
                && End.Equals(other.End);
        }

        /// <summary>
        /// Compares two <see cref="Range"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Range"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Range"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator ==(Range left, Range right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="Range"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Range"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Range"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator !=(Range left, Range right) => !left.Equals(right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Begin.GetHashCode();
                hashCode = (hashCode * 397) ^ End.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(Range)}({Begin}, {End})";
        }
    }
}
