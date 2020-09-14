// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes the format, width, height, depth, and row-pitch of the subresource into the parent resource.
    /// </summary>
    public partial struct SubresourceFootPrint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubresourceFootPrint"/> struct.
        /// </summary>
        /// <param name="format">A <see cref="DXGI.Format"/> value that specifies the viewing format.</param>
        /// <param name="width">The width of the subresource.</param>
        /// <param name="height">The height of the subresource.</param>
        /// <param name="depth">The depth of the subresource.</param>
        /// <param name="rowPitch">The row pitch, or width, or physical size, in bytes, of the subresource data. This must be a multiple of <see cref="D3D12.TextureDataPitchAlignment"/> (256), and must be greater than or equal to the size of the data within a row.</param>
        public SubresourceFootPrint(Format format, int width, int height, int depth, int rowPitch)
        {
            Format = format;
            Width = width;
            Height = height;
            Depth = depth;
            RowPitch = rowPitch;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubresourceFootPrint"/> struct.
        /// </summary>
        /// <param name="description">A <see cref="ResourceDescription"/> describing the resource.</param>
        /// <param name="rowPitch">The row pitch, or width, or physical size, in bytes, of the subresource data. This must be a multiple of <see cref="D3D12.TextureDataPitchAlignment"/> (256), and must be greater than or equal to the size of the data within a row.</param>
        public SubresourceFootPrint(ResourceDescription description, int rowPitch)
        {
            Format = description.Format;
            Width = (int)description.Width;
            Height = description.Height;
            Depth = (description.Dimension == ResourceDimension.Texture3D ? description.DepthOrArraySize : 1);
            RowPitch = rowPitch;
        }
    }
}
