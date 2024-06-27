// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial class ID3D11VideoDevice2
{
    public unsafe T CheckFeatureSupport<T>(FeatureVideo feature) where T : unmanaged
    {
        T featureSupport = default;
        CheckFeatureSupport(feature, &featureSupport, sizeof(T));
        return featureSupport;
    }

    public unsafe bool CheckFeatureSupport<T>(FeatureVideo feature, ref T featureSupport) where T : unmanaged
    {
        fixed (T* featureSupportPtr = &featureSupport)
        {
            return CheckFeatureSupport(feature, featureSupportPtr, sizeof(T)).Success;
        }
    }
}
