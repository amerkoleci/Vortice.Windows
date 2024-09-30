// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1;

public partial class ID2D1GeometryGroup
{
    public ID2D1Geometry[] GetSourceGeometry()
    {
        return GetSourceGeometry(SourceGeometryCount);
    }

    public ID2D1Geometry[] GetSourceGeometry(uint geometriesCount)
    {
        var geometries = new ID2D1Geometry[geometriesCount];
        GetSourceGeometries(geometries, geometriesCount);
        return geometries;
    }

    public void GetSourceGeometry(ID2D1Geometry[] geometries)
    {
        GetSourceGeometries(geometries, (uint)geometries.Length);
    }
}
