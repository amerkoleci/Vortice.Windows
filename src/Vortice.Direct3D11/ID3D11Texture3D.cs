// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct3D11
{
    public partial class ID3D11Texture3D
    {
        /// <inheritdoc/>
        public override int CalculateSubResourceIndex(int mipSlice, int arraySlice, out int mipSize)
        {
            var desc = Description;
            mipSize = CalculateMipSize(mipSlice, desc.Depth);
            return CalculateSubResourceIndex(mipSlice, arraySlice, desc.MipLevels);
        }
    }
}
