// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.XAudio2.Fx;

public partial class VolumeMeterLevels
{
    public VolumeMeterLevels(float[] peakLevels, float[] rmsLevels, int channelCount)
    {
        PeakLevels = peakLevels;
        RMSLevels = rmsLevels;
        ChannelCount = channelCount;
    }

    /// <summary>	
    /// Array that will be filled with the maximum absolute level for each channel during a processing pass.
    /// The array must be at least ChannelCount × sizeof(float) bytes. <see cref="PeakLevels"/> may be null if <see cref="RMSLevels"/> is not null.
    /// </summary>	
    public float[] PeakLevels { get; }

    /// <summary>
    /// Array that will be filled with root mean square level for each channel during a processing pass.
    /// The array must be at least ChannelCount × sizeof(float) bytes. <see cref="RMSLevels"/> may be null if <see cref="PeakLevels"/> is not null.
    /// </summary>
    public float[] RMSLevels { get; }

    /// <summary>
    /// Number of channels being processed.
    /// </summary>
    public int ChannelCount { get; }

    #region Marshal
    internal unsafe struct __Native
    {
        internal float* pPeakLevels;
        internal float* pRMSLevels;
        public int ChannelCount;
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.ChannelCount = ChannelCount;
        if (PeakLevels != null && PeakLevels.Length > 0)
        {
            @ref.pPeakLevels = (float*)Unsafe.AsPointer(ref PeakLevels[0]);
        }
        if (RMSLevels != null && RMSLevels.Length > 0)
        {
            @ref.pRMSLevels = (float*)Unsafe.AsPointer(ref RMSLevels[0]);
        }
    }
    #endregion
}
