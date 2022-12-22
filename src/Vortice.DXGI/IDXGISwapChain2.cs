// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;

namespace Vortice.DXGI;

public partial class IDXGISwapChain2
{
    public Size SourceSize
    {
        get
        {
            GetSourceSize(out int width, out int height);
            return new(width, height);
        }
        set => SetSourceSize(value.Width, value.Height);
    }
}
