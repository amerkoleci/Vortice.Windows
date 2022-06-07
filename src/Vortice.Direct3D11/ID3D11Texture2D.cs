﻿// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial class ID3D11Texture2D
{
    /// <inheritdoc/>
    public override int CalculateSubResourceIndex(int mipSlice, int arraySlice, out int mipSize)
    {
        var desc = GetDescription();
        mipSize = D3D11.CalculateMipSize(mipSlice, desc.Height);
        return D3D11.CalculateSubResourceIndex(mipSlice, arraySlice, desc.MipLevels);
    }
}
