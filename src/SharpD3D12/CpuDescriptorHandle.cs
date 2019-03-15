// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using SharpGen.Runtime;

namespace SharpD3D12
{
    public partial struct CpuDescriptorHandle : IEquatable<CpuDescriptorHandle>
    {
        /// <summary>	
        /// The address of  the descriptor.
        /// </summary>	
        public PointerSize Ptr;

        /// <summary>
        /// Adds an offset to a descriptor handle
        /// </summary>
        /// <param name = "left">Initial descriptor handle</param>
        /// <param name = "right">Offset to apply, use <see cref="Direct3D12.Device.GetDescriptorHandleIncrementSize(DescriptorHeapType)"/> with the relevant heap type.</param>
        /// <returns>Offsetted descriptor handle</returns>
        public static CpuDescriptorHandle operator +(CpuDescriptorHandle left, int right)
        {
            return new CpuDescriptorHandle()
            {
                Ptr = left.Ptr + right
            };
        }

        /// <summary>
        /// Compares two <see cref="CpuDescriptorHandle"/> objects for equality.
        /// </summary>
        /// <param name="left">The CpuDescriptorHandle on the left hand of the operand.</param>
        /// <param name="right">The CpuDescriptorHandle on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(CpuDescriptorHandle left, CpuDescriptorHandle right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="CpuDescriptorHandle"/> objects for inequality.
        /// </summary>
        /// <param name="left">The CpuDescriptorHandle on the left hand of the operand.</param>
        /// <param name="right">The CpuDescriptorHandle on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(CpuDescriptorHandle left, CpuDescriptorHandle right) => !left.Equals(ref right);

        /// <inheritdoc/>
        public override int GetHashCode() => Ptr.GetHashCode();

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is CpuDescriptorHandle handle && Equals(ref handle);

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(CpuDescriptorHandle other) => Equals(ref other);

        /// <summary>
        /// Determines whether the specified <see cref="CpuDescriptorHandle"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="CpuDescriptorHandle"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref CpuDescriptorHandle other) => Ptr == other.Ptr;
    }
}
