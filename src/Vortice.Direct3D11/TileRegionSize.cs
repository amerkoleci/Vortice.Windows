﻿// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D11
{
    /// <summary>
    /// Describes the size of a tiled region.
    /// </summary>
    public partial struct TileRegionSize
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TileRegionSize"/> struct.
        /// </summary>
        /// <param name="numTiles">The number of tiles in the tiled region.</param>
        public TileRegionSize(int numTiles)
        {
            NumTiles = numTiles;
            UseBox = false;
            Width = Height = Depth = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileRegionSize"/> struct.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        public TileRegionSize(int width, ushort height, ushort depth)
        {
            Width = width >= 1 ? width : 1;
            Height = height >= 1 ? height : (ushort)1;
            Depth = depth >= 1 ? depth : (ushort)1;
            NumTiles = Width * Height * Depth;
            UseBox = true;
        }
    }
}
