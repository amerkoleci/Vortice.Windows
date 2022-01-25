// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial class ID3D11VideoDecoder
{
    public VideoDecoderDescription CreationDescription
    {
        get
        {
            GetCreationParameters(out VideoDecoderDescription desc, out _);
            return desc;
        }
    }

    public VideoDecoderConfig CreationConfig
    {
        get
        {
            GetCreationParameters(out _, out VideoDecoderConfig config);
            return config;
        }
    }
}
