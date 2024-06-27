// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

/// <summary>
/// Describes the coordinates of a tiled resource.
/// </summary>
public partial struct TiledResourceCoordinate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TiledResourceCoordinate"/> struct.
    /// </summary>
    /// <param name="x">The x position of a tiled resource. Used for buffer and 1D, 2D, and 3D textures.</param>
    /// <param name="y">The y position of a tiled resource. Used for 2D and 3D textures.</param>
    /// <param name="z">The z position of a tiled resource. Used for 3D textures.</param>
    /// <param name="subresource">
    /// A subresource index value into mipmaps and arrays. Used for 1D, 2D, and 3D textures.
    /// For mipmaps that use nonstandard tiling, or are packed, or both use nonstandard tiling and are packed, any subresource value that indicates any of the packed mipmaps all refer to the same tile.
    /// </param>
    public TiledResourceCoordinate(int x, int y, int z, int subresource)
    {
        X = x;
        Y = y;
        Z = z;
        Subresource = subresource;
    }
}