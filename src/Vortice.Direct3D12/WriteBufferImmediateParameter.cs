// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Specifies the immediate value and destination address written using <see cref="ID3D12GraphicsCommandList2.WriteBufferImmediate(WriteBufferImmediateParameter[], WriteBufferImmediateMode[])"/>.
    /// </summary>
    public partial struct WriteBufferImmediateParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteBufferImmediateParameter"/> struct.
        /// </summary>
        /// <param name="destination">The GPU virtual address at which to write the value. The address must be aligned to a 32-bit (4-byte) boundary.</param>
        /// <param name="value">The 32-bit value to write.</param>
        public WriteBufferImmediateParameter(ulong destination, int value)
        {
            Dest = destination;
            Value = value;
        }
    }
}
