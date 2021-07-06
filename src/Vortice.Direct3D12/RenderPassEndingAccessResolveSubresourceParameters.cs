// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Drawing;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes the subresources involved in resolving at the conclusion of a render pass.
    /// </summary>
    public partial struct RenderPassEndingAccessResolveSubresourceParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderPassEndingAccessResolveSubresourceParameters"/> struct.
        /// </summary>
        /// <param name="srcSubresource">The source subresource.</param>
        /// <param name="dstSubresource">The destination subresource.</param>
        /// <param name="dstX">The x coordinate within the destination subresource.</param>
        /// <param name="dstY">The y coordinate within the destination subresource.</param>
        /// <param name="srcRect">The <see cref="RawRect"/> within the source subresource.</param>
        public RenderPassEndingAccessResolveSubresourceParameters(
            int srcSubresource,
            int dstSubresource,
            int dstX,
            int dstY,
            RawRect srcRect)
        {
            SrcSubresource = srcSubresource;
            DstSubresource = dstSubresource;
            DstX = dstX;
            DstY = dstY;
            SrcRect = srcRect;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderPassEndingAccessResolveSubresourceParameters"/> struct.
        /// </summary>
        /// <param name="srcSubresource">The source subresource.</param>
        /// <param name="dstSubresource">The destination subresource.</param>
        /// <param name="dstX">The x coordinate within the destination subresource.</param>
        /// <param name="dstY">The y coordinate within the destination subresource.</param>
        /// <param name="srcRect">The <see cref="RawRect"/> within the source subresource.</param>
        public RenderPassEndingAccessResolveSubresourceParameters(
            int srcSubresource,
            int dstSubresource,
            int dstX,
            int dstY,
            Rectangle srcRect)
        {
            SrcSubresource = srcSubresource;
            DstSubresource = dstSubresource;
            DstX = dstX;
            DstY = dstY;
            SrcRect = srcRect;
        }
    }
}
