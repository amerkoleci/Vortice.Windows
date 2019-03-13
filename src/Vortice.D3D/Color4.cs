// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice
{
    /// <summary>
	/// Defines a floating point RGBA color.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
    //[DataContract(Name = nameof(Color4))]
    public readonly struct Color4 : IEquatable<Color4>
    {
        /// <summary>
        /// Specifies the red component of the color.
        /// </summary>
        public readonly float R;

        /// <summary>
        /// Specifies the green component of the color.
        /// </summary>
        public readonly float G;

        /// <summary>
        /// Specifies the blue component of the color.
        /// </summary>
        public readonly float B;

        /// <summary>
        /// Specifies the alpha component of the color.
        /// </summary>
        public readonly float A;

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Color4(float value)
        {
            R = G = B = A = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        public Color4(float red, float green, float blue)
        {
            R = red;
            G = green;
            B = blue;
            A = 1.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        /// <param name="alpha">The alpha component of the color.</param>
        public Color4(float red, float green, float blue, float alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The red, green, blue, and alpha components of the color.</param>
        public Color4(Vector4 value)
        {
            R = value.X;
            G = value.Y;
            B = value.Z;
            A = value.W;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The red, green, and blue components of the color.</param>
        /// <param name="alpha">The alpha component of the color.</param>
        public Color4(Vector3 value, float alpha)
        {
            R = value.X;
            G = value.Y;
            B = value.Z;
            A = alpha;
        }

        /// <summary>
        /// Compares two <see cref="Color4"/> objects for equality.
        /// </summary>
        /// <param name="left">The Color4 on the left hand of the operand.</param>
        /// <param name="right">The Color4 on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Color4 left, Color4 right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Color4"/> objects for inequality.
        /// </summary>
        /// <param name="left">The Color4 on the left hand of the operand.</param>
        /// <param name="right">The Color4 on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Color4 left, Color4 right) => !left.Equals(ref right);

        /// <inheritdoc/>
        public override int GetHashCode() => HashHelpers.Combine(
            R.GetHashCode(), G.GetHashCode(),
            B.GetHashCode(), A.GetHashCode());

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Color4 [ R={R}, G={G}, B={B}, A={A} ]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Color4 rect && Equals(ref rect);

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Color4 other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Color4"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Color4"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Color4 other)
        {
            return R == other.R
                 && G == other.G
                 && B == other.B
                 && A == other.A;
        }
    }
}
