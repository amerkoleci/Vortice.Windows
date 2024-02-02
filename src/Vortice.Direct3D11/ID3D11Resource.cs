// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial class ID3D11Resource
{
    /// <summary>
    /// Calculates the sub resource index for a particular mipSlice and arraySlice.
    /// </summary>
    /// <param name="mipSlice">The mip slice.</param>
    /// <param name="arraySlice">The array slice.</param>
    /// <param name="mipSize">The size of slice. This values is resource dependent. Texture1D -> mipSize of the Width. Texture2D -> mipSize of the Height. Texture3D -> mipsize of the Depth</param>
    /// <returns>The resulting miplevel calulated for this instance with this mipSlice and arraySlice.</returns>
    public virtual int CalculateSubResourceIndex(int mipSlice, int arraySlice, out int mipSize)
    {
        throw new NotSupportedException("This method is not implemented for this kind of resource");
    }
}
