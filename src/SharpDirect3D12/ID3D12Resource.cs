// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace SharpDirect3D12
{
    public partial class ID3D12Resource
    {
        public static int CalculateSubresource(int mipSlice, int arraySlice, int planeSlice, int mipLevels, int arraySize)
        {
            return mipSlice + (arraySlice * mipLevels) + (planeSlice * mipLevels * arraySize);
        }
    }
}
