// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial class ID3D11Texture2D
{
    /// <inheritdoc/>
    public override uint CalculateSubResourceIndex(uint mipSlice, uint arraySlice, out uint mipSize)
    {
        var desc = GetDescription();
        mipSize = D3D11.CalculateMipSize(mipSlice, desc.Height);
        return D3D11.CalculateSubResourceIndex(mipSlice, arraySlice, desc.MipLevels);
    }
}
