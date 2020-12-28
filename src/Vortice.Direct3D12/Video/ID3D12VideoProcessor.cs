// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Direct3D12.Video
{
    /// <summary>
    /// Provides methods for getting information about the parameters to the call to ID3D12VideoDevice::CreateVideoProcessor that created the video processor.
    /// </summary>
    public partial class ID3D12VideoProcessor
    {
        public unsafe Result GetInputStreamDescriptions(VideoProcessInputStreamDescription[] descriptions)
        {
            fixed (VideoProcessInputStreamDescription* descriptionsPtr = &descriptions[0])
            {
                return GetInputStreamDescriptions(descriptions.Length, descriptions);
            }
        }

        public unsafe Result GetInputStreamDescriptions(Span<VideoProcessInputStreamDescription> descriptions)
        {
            fixed (VideoProcessInputStreamDescription* descriptionsPtr = &MemoryMarshal.GetReference(descriptions))
            {
                return GetInputStreamDescriptions(descriptions.Length, (IntPtr)descriptionsPtr);
            }
        }

        public unsafe Result GetInputStreamDescriptions(int count, VideoProcessInputStreamDescription[] descriptions)
        {
            fixed (VideoProcessInputStreamDescription* descriptionsPtr = &descriptions[0])
            {
                return GetInputStreamDescriptions(count, (IntPtr)descriptionsPtr);
            }
        }

        public unsafe Result GetInputStreamDescriptions(int count, Span<VideoProcessInputStreamDescription> descriptions)
        {
            fixed (VideoProcessInputStreamDescription* descriptionsPtr = &MemoryMarshal.GetReference(descriptions))
            {
                return GetInputStreamDescriptions(count, (IntPtr)descriptionsPtr);
            }
        }
    }
}
