// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes the clear value to which resource(s) should be cleared at the beginning of a render pass.
    /// </summary>
    public partial struct RenderPassBeginningAccessClearParameters
    {
        /// <summary>
        /// Initialize new instance of <see cref="RenderPassBeginningAccessClearParameters"/> struct.
        /// </summary>
        /// <param name="clearValue">The clear value to which the resource(s) should be cleared.</param>
        public RenderPassBeginningAccessClearParameters(in ClearValue clearValue)
        {
            ClearValue = clearValue;
        }
    }
}
