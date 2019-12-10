// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct3D11
{
    public partial class ID3D11Resource
    {
        /// <summary>
        /// Calculates the sub resource index from a miplevel.
        /// </summary>
        /// <param name="mipSlice">A zero-based index for the mipmap level to address; 0 indicates the first, most detailed mipmap level.</param>
        /// <param name="arraySlice">The zero-based index for the array level to address; always use 0 for volume (3D) textures.</param>
        /// <param name="mipLevels">Number of mipmap levels in the resource.</param>
        /// <returns>
        /// The index which equals mipSlice + (arraySlice * mipLevels).
        /// </returns>
        public static int CalculateSubResourceIndex(int mipSlice, int arraySlice, int mipLevels)
        {
            return (mipLevels * arraySlice) + mipSlice;
        }

        /// <summary>
        /// Calculates the resulting size at a single level for an original size.
        /// </summary>
        /// <param name="mipLevel">The mip level to get the size.</param>
        /// <param name="baseSize">Size of the base.</param>
        /// <returns>
        /// Size of the mipLevel
        /// </returns>
        public static int CalculateMipSize(int mipLevel, int baseSize)
        {
            baseSize = baseSize >> mipLevel;
            return baseSize > 0 ? baseSize : 1;
        }

        /// <summary>
        /// Calculates the sub resource index for a particular mipSlice and arraySlice.
        /// </summary>
        /// <param name="mipSlice">The mip slice.</param>
        /// <param name="arraySlice">The array slice.</param>
        /// <param name="mipSize">The size of slice. This values is resource dependent. Texture1D -> mipSize of the Width. Texture2D -> mipSize of the Height. Texture3D -> mipsize of the Depth</param>
        /// <returns>The resulting miplevel calulated for this instance with this mipSlice and arraySlice.</returns>
        public virtual int CalculateSubResourceIndex(int mipSlice, int arraySlice, out int mipSize)
        {
            throw new NotImplementedException("This method is not implemented for this kind of resource");
        }
    }
}
