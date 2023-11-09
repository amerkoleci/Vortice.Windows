// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Video;

public unsafe partial struct VideoEncoderAv1SegmentData
{
    public ulong EnabledFeatures;

    public fixed long FeatureValue[8];
}
