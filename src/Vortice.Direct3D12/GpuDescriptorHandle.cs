// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12
{
    public partial struct GpuDescriptorHandle : IEquatable<GpuDescriptorHandle>
    {
        /// <summary>
        /// Adds an offset to a descriptor handle.
        /// </summary>
        /// <param name = "left">Initial descriptor handle</param>
        /// <param name = "right">Offset to apply, use <see cref="ID3D12Device.GetDescriptorHandleIncrementSize(DescriptorHeapType)"/> with the relevant heap type.</param>
        /// <returns>Offsetted descriptor handle</returns>
        public static GpuDescriptorHandle operator +(GpuDescriptorHandle left, uint right)
        {
            return new GpuDescriptorHandle()
            {
                Ptr = left.Ptr + (ulong)right
            };
        }

        /// <summary>
        ///   Adds an offset to a descriptor handle
        /// </summary>
        /// <param name = "left">Initial descriptor handle</param>
        /// <param name = "right">Offset to apply, use <see cref="ID3D12Device.GetDescriptorHandleIncrementSize(DescriptorHeapType)"/> with the relevant heap type.</param>
        /// <returns>Offsetted descriptor handle</returns>
        public static GpuDescriptorHandle operator +(GpuDescriptorHandle left, ulong right)
        {
            return new GpuDescriptorHandle()
            {
                Ptr = left.Ptr + right
            };
        }

        /// <summary>
        /// Compares two <see cref="GpuDescriptorHandle"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="GpuDescriptorHandle"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="GpuDescriptorHandle"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(GpuDescriptorHandle left, GpuDescriptorHandle right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="GpuDescriptorHandle"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="GpuDescriptorHandle"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="GpuDescriptorHandle"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(GpuDescriptorHandle left, GpuDescriptorHandle right) => !left.Equals(ref right);

        /// <inheritdoc/>
        public override int GetHashCode() => Ptr.GetHashCode();

        /// <inheritdoc/>
		public override bool Equals(object? obj) => obj is GpuDescriptorHandle handle && Equals(ref handle);

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(GpuDescriptorHandle other) => Equals(ref other);

        /// <summary>
        /// Determines whether the specified <see cref="GpuDescriptorHandle"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="GpuDescriptorHandle"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref GpuDescriptorHandle other) => Ptr == other.Ptr;
    }
}
