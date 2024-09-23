// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Video;

public partial struct VideoEncoderProfileDescription
{
    public uint DataSize;

    public VideoEncoderProfileH264[]? H264Profile;
    public VideoEncoderProfileHevc[]? HEVCProfile;

    #region Marshal
    [StructLayout(LayoutKind.Explicit, Pack = 0)]
    internal unsafe struct __Native
    {
        [FieldOffset(0)]
        public uint DataSize;
        [FieldOffset(4)]
        public void* pH264Profile;
        [FieldOffset(4)]
        public void* pHEVCProfile;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        if (@ref.pH264Profile != null)
        {
            NativeMemory.Free(@ref.pH264Profile);
        }
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.DataSize = DataSize;
        if (H264Profile != null)
        {
            @ref.pH264Profile = UnsafeUtilities.AllocToPointer<VideoEncoderProfileH264>(H264Profile);
        }

        if (HEVCProfile != null)
        {
            @ref.pHEVCProfile = UnsafeUtilities.AllocToPointer<VideoEncoderProfileHevc>(HEVCProfile);
        }
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        DataSize = @ref.DataSize;

        // TODO:
        if (@ref.pH264Profile != null)
        {
        }
    }
    #endregion
}
