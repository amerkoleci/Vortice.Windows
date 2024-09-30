// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGISurface
{
    /// <summary>
    /// Acquires access to the surface data.
    /// </summary>
    /// <param name="flags"><see cref="MapFlags"/> specifying CPU access permissions.</param>
    /// <returns>A <see cref="DataRectangle" /> for accessing the mapped data.</returns>.
    public DataRectangle Map(MapFlags flags)
    {
        Map(out MappedRect mappedRect, (uint)flags).CheckError();
        return new DataRectangle(mappedRect.Bits, (uint)mappedRect.Pitch);
    }

    /// <summary>
    /// Acquires access to the surface data.
    /// </summary>
    /// <param name="flags"><see cref="MapFlags"/> specifying CPU access permissions.</param>
    /// <returns><see cref="DataStream"/> to contain the surface data.</returns>
    public DataStream MapDataStream(MapFlags flags)
    {
        Map(out MappedRect mappedRect, (uint)flags).CheckError();
        return new DataStream(mappedRect.Bits, Description.Height * mappedRect.Pitch, true, true);
    }
}
