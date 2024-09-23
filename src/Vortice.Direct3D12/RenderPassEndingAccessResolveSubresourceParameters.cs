// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

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
        uint srcSubresource,
        uint dstSubresource,
        uint dstX,
        uint dstY,
        RawRect srcRect)
    {
        SrcSubresource = srcSubresource;
        DstSubresource = dstSubresource;
        DstX = dstX;
        DstY = dstY;
        SrcRect = srcRect;
    }
}
