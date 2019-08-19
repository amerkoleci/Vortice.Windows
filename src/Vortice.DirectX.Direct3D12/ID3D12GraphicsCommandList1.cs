// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using SharpGen.Runtime;
using Vortice.DirectX.Direct3D;
using Vortice.DirectX.DXGI;

namespace Vortice.DirectX.Direct3D12
{
    public partial class ID3D12GraphicsCommandList1
    {
        /// <summary>
        /// Copy a region of a multisampled or compressed resource into a non-multisampled or non-compressed resource.
        /// </summary>
        /// <param name="dstResource">Destination resource.</param>
        /// <param name="dstSubresource">A zero-based index that identifies the destination subresource. Use <see cref="ID3D12Resource.CalculateSubresource(int, int, int, int, int)"/> to calculate the subresource index if the parent resource is complex.</param>
        /// <param name="dstX">The X coordinate of the left-most edge of the destination region. The width of the destination region is the same as the width of the source rect.</param>
        /// <param name="dstY">The Y coordinate of the top-most edge of the destination region. The height of the destination region is the same as the height of the source rect.</param>
        /// <param name="srcResource">Source resource. Must be multisampled or compressed.</param>
        /// <param name="srcSubresource">A zero-based index that identifies the source subresource.</param>
        /// <param name="format">A <see cref="Format"/> that specifies how the source and destination resource formats are consolidated.</param>
        /// <param name="resolveMode">Specifies the operation used to resolve the source samples.</param>
        public void ResolveSubresourceRegion(
            ID3D12Resource dstResource, 
            int dstSubresource, 
            int dstX, int dstY, 
            ID3D12Resource srcResource,
            int srcSubresource, 
            Format format, 
            ResolveMode resolveMode = ResolveMode.Decompress)
        {
            ResolveSubresourceRegion_(
                dstResource, dstSubresource, dstX, dstY,
                srcResource, srcSubresource, null,
                format, resolveMode);
        }

        /// <summary>
        /// Copy a region of a multisampled or compressed resource into a non-multisampled or non-compressed resource.
        /// </summary>
        /// <param name="dstResource">Destination resource.</param>
        /// <param name="dstSubresource">A zero-based index that identifies the destination subresource. Use <see cref="ID3D12Resource.CalculateSubresource(int, int, int, int, int)"/> to calculate the subresource index if the parent resource is complex.</param>
        /// <param name="dstX">The X coordinate of the left-most edge of the destination region. The width of the destination region is the same as the width of the source rect.</param>
        /// <param name="dstY">The Y coordinate of the top-most edge of the destination region. The height of the destination region is the same as the height of the source rect.</param>
        /// <param name="srcResource">Source resource. Must be multisampled or compressed.</param>
        /// <param name="srcSubresource">A zero-based index that identifies the source subresource.</param>
        /// <param name="srcRect">Specifies the rectangular region of the source resource to be resolved.</param>
        /// <param name="format">A <see cref="Format"/> that specifies how the source and destination resource formats are consolidated.</param>
        /// <param name="resolveMode">Specifies the operation used to resolve the source samples.</param>
        public void ResolveSubresourceRegion(
            ID3D12Resource dstResource,
            int dstSubresource,
            int dstX, int dstY,
            ID3D12Resource srcResource,
            int srcSubresource, 
            Rectangle srcRect,
            Format format, 
            ResolveMode resolveMode = ResolveMode.Decompress)
        {
            ResolveSubresourceRegion_(
                dstResource, dstSubresource, dstX, dstY,
                srcResource, srcSubresource, srcRect,
                format, resolveMode);
        }
    }
}
