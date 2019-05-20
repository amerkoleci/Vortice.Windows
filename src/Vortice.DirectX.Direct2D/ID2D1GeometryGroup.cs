// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;
using Vortice.Mathematics;

namespace Vortice.DirectX.Direct2D
{
    public partial class ID2D1GeometryGroup
    {
        public ID2D1Geometry[] GetSourceGeometry()
        {
            return GetSourceGeometry(SourceGeometryCount);
        }

        public ID2D1Geometry[] GetSourceGeometry(int geometriesCount)
        {
            var geometries = new ID2D1Geometry[geometriesCount];
            GetSourceGeometries(geometries, geometriesCount);
            return geometries;
        }

        public void GetSourceGeometry(ID2D1Geometry[] geometries)
        {
            GetSourceGeometries(geometries, geometries.Length);
        }
    }
}
