// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectWrite;

/// <summary>
/// Specifies a range of text positions where format is applied in the text represented by an <see cref="IDWriteTextLayout"/> object.
/// </summary>
public partial struct TextRange
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TextRange"/> struct.
    /// </summary>
    /// <param name="startPosition">The start position of the text range.</param>
    /// <param name="length">The number positions in the text range.</param>
    public TextRange(uint startPosition, uint length)
    {
        StartPosition = startPosition;
        Length = length;
    }
}
