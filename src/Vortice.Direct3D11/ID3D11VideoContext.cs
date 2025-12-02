// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public unsafe partial class ID3D11VideoContext
{
    //public AuthenticatedConfigureOutput ConfigureAuthenticatedChannel(ID3D11AuthenticatedChannel channel, int inputSize, IntPtr input)
    //{
    //    ConfigureAuthenticatedChannel(channel, inputSize, input, out AuthenticatedConfigureOutput output).CheckError();
    //    return output;
    //}

    public Span<byte> GetDecoderBuffer(ID3D11VideoDecoder decoder, VideoDecoderBufferType type)
    {
        GetDecoderBuffer(decoder, type, out uint size, out nint dataPtr);

        return new Span<byte>(dataPtr.ToPointer(), (int)size);
    }

    public Result VideoProcessorBlt(
        ID3D11VideoProcessor videoProcessor,
        ID3D11VideoProcessorOutputView view,
        uint outputFrame,
        Vortice.Direct3D11.VideoProcessorStream[] streams)
    {
        return VideoProcessorBlt(videoProcessor, view, outputFrame, (uint)streams.Length, streams);
    }
}
