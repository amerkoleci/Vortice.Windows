// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectX.Direct3D12
{
    /// <summary>
    /// Describes the access to resource(s) that is requested by an application at the transition into a render pass.
    /// </summary>
    public partial struct RenderPassBeginningAccess
    {
        /// <summary>
        /// Initialize new instance of <see cref="RenderPassBeginningAccess"/> struct.
        /// </summary>
        /// <param name="type">The type of access being requested.</param>
        public RenderPassBeginningAccess(RenderPassBeginningAccessType type)
        {
            Type = type;
            Clear = new RenderPassBeginningAccessClearParameters();
        }

        /// <summary>
        /// Initialize new instance of <see cref="RenderPassBeginningAccess"/> struct.
        /// </summary>
        /// <param name="type">The type of access being requested.</param>
        /// <param name="clear">Appropriate when Type is <see cref="RenderPassBeginningAccessType.Clear"/>. The clear value to which resource(s) should be cleared.</param>
        public RenderPassBeginningAccess(RenderPassBeginningAccessType type, RenderPassBeginningAccessClearParameters clear)
        {
            Type = type;
            Clear = clear;
        }

        /// <summary>
        /// Initialize new instance of <see cref="RenderPassBeginningAccess"/> struct.
        /// </summary>
        /// <param name="type">The type of access being requested.</param>
        /// <param name="clearValue">Appropriate when Type is <see cref="RenderPassBeginningAccessType.Clear"/>. The clear value to which resource(s) should be cleared.</param>
        public RenderPassBeginningAccess(RenderPassBeginningAccessType type, ClearValue clearValue)
        {
            Type = type;
            Clear = new RenderPassBeginningAccessClearParameters { ClearValue = clearValue };
        }
    }
}
