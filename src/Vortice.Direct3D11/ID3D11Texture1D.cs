// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct3D11
{
    public partial class ID3D11Texture1D
    {
        /// <inheritdoc />
        public override int CalculateSubResourceIndex(int mipSlice, int arraySlice, out int mipSize)
        {
            var description = Description;
            mipSize = CalculateMipSize(mipSlice, description.Width);
            return CalculateSubResourceIndex(mipSlice, arraySlice, description.MipLevels);
        }
    }
}
