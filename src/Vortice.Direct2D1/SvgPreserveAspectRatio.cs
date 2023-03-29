// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;

namespace Vortice.Direct2D1;

public partial struct SvgPreserveAspectRatio
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SvgLength"/> struct.
    /// </summary>
    public SvgPreserveAspectRatio(bool defer, SvgAspectAlign align, SvgAspectScaling meetOrSlice)
    {
        Defer = defer;
        Align = align;
        MeetOrSlice = meetOrSlice;
    }
}
