// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12.Video;

public partial struct VideoProcessInputStreamArguments
{
    /// <summary>
    /// An array of <see cref="VideoProcessInputStream"/> structures containing the set of references for video processing.
    /// </summary>
    public VideoProcessInputStream[] InputStream
    {
        get => _inputStream ??= new VideoProcessInputStream[2];
        private set => _inputStream = value;
    }

    private VideoProcessInputStream[] _inputStream;

    /// <summary>
    /// A <see cref="VideoProcessTransform"/> structure specifying the flip, rotation, scale and destination translation for the video input.
    /// </summary>
    public VideoProcessTransform Transform;

    /// <summary>
    /// A value from the <see cref="VideoProcessInputStreamFlags"/> specifying the options for the input stream.
    /// </summary>
    public VideoProcessInputStreamFlags Flags;

    /// <summary>
    /// A <see cref="VideoProcessInputStreamRate"/> specifying the framerate and input and output indicies for framerate conversion and deinterlacing.
    /// </summary>
    public VideoProcessInputStreamRate RateInfo;

    /// <summary>
    /// The level to apply for each enabled filter.
    /// The filter level is specified in the order that filters appear in the <see cref="VideoProcessFilterFlags"/>.
    /// Specify 0 if a filter is not enabled or the filter index is reserved.
    /// </summary>
    public unsafe fixed int FilterLevels[32];

    /// <summary>
    /// A <see cref="VideoProcessAlphaBlending"/> specifying the planar alpha for an input stream on the video processor.
    /// </summary>
    public VideoProcessAlphaBlending AlphaBlending;

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    internal partial struct __Native
    {
        public VideoProcessInputStream.__Native InputStream0;
        public VideoProcessInputStream.__Native InputStream1;
        public VideoProcessTransform Transform;
        public VideoProcessInputStreamFlags Flags;
        public VideoProcessInputStreamRate RateInfo;
        public unsafe fixed int FilterLevels[32];
        public VideoProcessAlphaBlending AlphaBlending;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        InputStream[0].__MarshalFree(ref @ref.InputStream0);
        InputStream[1].__MarshalFree(ref @ref.InputStream1);
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        InputStream[0].__MarshalTo(ref @ref.InputStream0);
        InputStream[1].__MarshalTo(ref @ref.InputStream1);
        @ref.Transform = Transform;
        @ref.Flags = Flags;
        @ref.RateInfo = RateInfo;
        fixed (int* pFilterLevels = FilterLevels)
        {
            fixed (int* pFilterLevelsNative = @ref.FilterLevels)
            {
                Unsafe.CopyBlockUnaligned(pFilterLevelsNative, pFilterLevels, 32 * sizeof(int));
            }
        }
        @ref.AlphaBlending = AlphaBlending;
    }
    #endregion
}
