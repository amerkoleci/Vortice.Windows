// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes a sub-pixel sample position for use with programmable sample positions.
    /// </summary>
    public partial struct SamplePosition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePosition"/> struct.
        /// </summary>
        /// <param name="x">A signed sub-pixel coordinate value in the X axis.</param>
        /// <param name="y">A signed sub-pixel coordinate value in the Y axis.</param>
        public SamplePosition(byte x, byte y)
        {
            X = x;
            Y = y;
        }
    }
}
