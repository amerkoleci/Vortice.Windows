// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D11;

public partial class ID3D11Device2
{
    public unsafe ID3D11DeviceContext2 CreateDeferredContext2()
    {
        return CreateDeferredContext2(0);
    }

    public uint CheckMultisampleQualityLevels1(Format format, uint sampleCount)
    {
        return CheckMultisampleQualityLevels1(format, sampleCount, CheckMultisampleQualityLevelsFlags.TiledResource);
    }
}
