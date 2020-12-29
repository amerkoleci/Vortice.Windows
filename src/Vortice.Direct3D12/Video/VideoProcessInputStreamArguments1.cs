// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D12.Video
{
    public partial struct VideoProcessInputStreamArguments1
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

        /// <summary>
        /// A value from the <see cref="VideoFieldType"/> enum specfying the interlaced field type of the input source.
        /// When working with mixed content, use <see cref="ID3D12VideoProcessCommandList1.ProcessFrames1(ID3D12VideoProcessor, ref VideoProcessOutputStreamArguments, int, VideoProcessInputStreamArguments1[])"/> which supports changing the field type for each call.
        /// </summary>
        /// <unmanaged>FieldType</unmanaged>
        /// <unmanaged-short>FieldType</unmanaged-short>
        public VideoFieldType FieldType;

        #region Nested
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
            public VideoFieldType FieldType;
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
            @ref.FieldType = FieldType;
        }
        #endregion
    }
}
