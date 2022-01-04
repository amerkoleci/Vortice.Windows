// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial class ID3D11Texture1D
{
    /// <inheritdoc />
    public override int CalculateSubResourceIndex(int mipSlice, int arraySlice, out int mipSize)
    {
        var description = GetDescription();
        mipSize = CalculateMipSize(mipSlice, description.Width);
        return CalculateSubResourceIndex(mipSlice, arraySlice, description.MipLevels);
    }
}
