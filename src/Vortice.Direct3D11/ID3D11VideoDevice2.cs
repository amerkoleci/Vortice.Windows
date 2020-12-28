// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct3D11
{
    public partial class ID3D11VideoDevice2
    {
        public unsafe T CheckFeatureSupport<T>(FeatureVideo feature) where T : unmanaged
        {
            T featureSupport = default;
            CheckFeatureSupport(feature, new IntPtr(&featureSupport), sizeof(T));
            return featureSupport;
        }

        public unsafe bool CheckFeatureSupport<T>(FeatureVideo feature, ref T featureSupport) where T : unmanaged
        {
            fixed (void* featureSupportPtr = &featureSupport)
            {
                return CheckFeatureSupport(feature, (IntPtr)featureSupportPtr, sizeof(T)).Success;
            }
        }
    }
}
