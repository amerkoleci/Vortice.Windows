// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;

namespace Vortice.Direct2D1;

public partial struct SvgViewbox
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SvgViewbox"/> struct.
    /// </summary>
    public SvgViewbox(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
}
