// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial class ID3D11DeviceContext2
{
    public Result CopyTileMappings(
        ID3D11Resource destTiledResource, TiledResourceCoordinate destRegionStartCoordinate,
        ID3D11Resource sourceTiledResource, TiledResourceCoordinate sourceRegionStartCoordinate,
        TileRegionSize tileRegionSize)
    {
        return CopyTileMappings(destTiledResource, destRegionStartCoordinate, sourceTiledResource, sourceRegionStartCoordinate, tileRegionSize, TileMappingFlags.None);
    }

    public void CopyTiles(ID3D11Resource tiledResource, TiledResourceCoordinate tileRegionStartCoordinate, TileRegionSize tileRegionSize, ID3D11Buffer buffer, ulong bufferStartOffsetInBytes)
    {
        CopyTiles(tiledResource, tileRegionStartCoordinate, tileRegionSize, buffer, bufferStartOffsetInBytes, TileCopyFlags.None);
    }

    public Result UpdateTileMappings(
        ID3D11Resource tiledResource,
        TiledResourceCoordinate[] tiledResourceRegionStartCoordinates,
        TileRegionSize[] tiledResourceRegionSizes,
        ID3D11Buffer tilePool,
        TileRangeFlags[] rangeFlags,
        int[] tilePoolStartOffsets,
        int[] rangeTileCounts,
        TileMappingFlags flags = TileMappingFlags.None)
    {
        return UpdateTileMappings(tiledResource,
            tiledResourceRegionStartCoordinates.Length, tiledResourceRegionStartCoordinates, tiledResourceRegionSizes,
            tilePool,
            rangeFlags.Length, rangeFlags,
            tilePoolStartOffsets,
            rangeTileCounts,
            flags);
    }
}
