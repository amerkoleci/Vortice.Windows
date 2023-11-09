// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Video;

/// <summary>
/// Provides methods for getting information about the parameters to the call to ID3D12VideoDevice::CreateVideoProcessor that created the video processor.
/// </summary>
public unsafe partial class ID3D12VideoProcessor
{
    public Result GetInputStreamDescriptions(VideoProcessInputStreamDescription[] descriptions)
    {
        fixed (VideoProcessInputStreamDescription* descriptionsPtr = &descriptions[0])
        {
            return GetInputStreamDescriptions(descriptions.Length, descriptions);
        }
    }

    public Result GetInputStreamDescriptions(Span<VideoProcessInputStreamDescription> descriptions)
    {
        fixed (VideoProcessInputStreamDescription* descriptionsPtr = &MemoryMarshal.GetReference(descriptions))
        {
            return GetInputStreamDescriptions(descriptions.Length, (IntPtr)descriptionsPtr);
        }
    }

    public Result GetInputStreamDescriptions(int count, VideoProcessInputStreamDescription[] descriptions)
    {
        fixed (VideoProcessInputStreamDescription* descriptionsPtr = &descriptions[0])
        {
            return GetInputStreamDescriptions(count, (IntPtr)descriptionsPtr);
        }
    }

    public Result GetInputStreamDescriptions(int count, Span<VideoProcessInputStreamDescription> descriptions)
    {
        fixed (VideoProcessInputStreamDescription* descriptionsPtr = &MemoryMarshal.GetReference(descriptions))
        {
            return GetInputStreamDescriptions(count, (IntPtr)descriptionsPtr);
        }
    }
}
