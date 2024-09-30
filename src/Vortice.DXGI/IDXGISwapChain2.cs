// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.DXGI;

public partial class IDXGISwapChain2
{
    public SizeI SourceSize
    {
        get
        {
            GetSourceSize(out uint width, out uint height);
            return new((int)width, (int)height);
        }
        set => SetSourceSize((uint)value.Width, (uint)value.Height);
    }
}
