// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct2D1;

namespace Vortice.WIC;

public partial class IWICImageEncoder
{
    public Result WriteFrame(ID2D1Image image, IWICBitmapFrameEncode frameEncode, WICImageParameters imageParameters)
    {
        return WriteFrame(image, frameEncode, ref imageParameters);
    }

    public Result WriteThumbnail(ID2D1Image image, IWICBitmapEncoder encoder, WICImageParameters imageParameters)
    {
        return WriteThumbnail(image, encoder, ref imageParameters);
    }

    public Result WriteFrameThumbnail(ID2D1Image image, IWICBitmapFrameEncode frameEncode, WICImageParameters imageParameters)
    {
        return WriteFrameThumbnail(image, frameEncode, ref imageParameters);
    }
}
