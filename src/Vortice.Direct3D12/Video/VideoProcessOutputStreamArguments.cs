// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D12.Video
{
    /// <summary>
    /// Specifies output stream arguments for the output passed to ID3D12VideoCommandList::ProcessFrames.
    /// </summary>
    public partial struct VideoProcessOutputStreamArguments
    {
        /// <summary>
        /// An array of <see cref="VideoProcessOutputStream"/> representing the output surfaces for the video process command.
        /// If stereo output is enabled, index zero contains the left output while index 1 contains the right input.
        /// If stereo output is not enabled, only index 0 is used to specify the output while index 1 should be set to null.
        /// </summary>
        public OutputStreamInner OutputStream;

        /// <summary>
        /// The target rectangle is the area within the destination surface where the output will be drawn. The target rectangle is given in pixel coordinates, relative to the destination surface.
        /// </summary>
        public RawRect TargetRectangle;

        #region Nested
        public partial struct OutputStreamInner
        {
            public VideoProcessOutputStream e0;
            public VideoProcessOutputStream e1;

            public ref VideoProcessOutputStream this[int index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return ref AsSpan()[index];
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<VideoProcessOutputStream> AsSpan() => MemoryMarshal.CreateSpan(ref e0, 2);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
        internal partial struct __Native
        {
            public VideoProcessOutputStream.__Native OutputStream0;
            public VideoProcessOutputStream.__Native OutputStream1;
            public RawRect TargetRectangle;
        }

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            OutputStream[0].__MarshalFree(ref @ref.OutputStream0);
            OutputStream[1].__MarshalFree(ref @ref.OutputStream1);
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            OutputStream[0].__MarshalTo(ref @ref.OutputStream0);
            OutputStream[1].__MarshalTo(ref @ref.OutputStream1);
            @ref.TargetRectangle = TargetRectangle;
        }
        #endregion
    }
}
