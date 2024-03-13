// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes the coordinates of a tiled resource.
/// </summary>
public partial struct TiledResourceCoordinate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TiledResourceCoordinate"/> struct.
    /// </summary>
    /// <param name="x">The x-coordinate of the tiled resource.</param>
    /// <param name="y">The y-coordinate of the tiled resource.</param>
    /// <param name="z">The z-coordinate of the tiled resource.</param>
    /// <param name="subresource">The index of the subresource for the tiled resource.</param>
    public TiledResourceCoordinate(int x, int y, int z, int subresource = 0)
    {
        X = x;
        Y = y;
        Z = z;
        Subresource = subresource;
    }
}
