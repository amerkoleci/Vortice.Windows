// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Video;

public unsafe partial struct VideoEncoderAv1ReferencePictureWarpedMotionInfo
{
    public VideoEncoderAv1ReferenceWarpedMotionTransformation TransformationType;
    public fixed int TransformationMatrix[8];
    public RawBool InvalidAffineSet;
}
