// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;

namespace Vortice.Direct2D1;

public partial struct SvgLength
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SvgLength"/> struct.
    /// </summary>
    public SvgLength(float value, SvgLengthUnits units)
    {
        Value = value;
        Units = units;
    }
}
