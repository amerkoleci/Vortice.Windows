// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct Size2D
{
    /// <summary>
    /// Constructs a <see cref="Size2D"/> with the given width and height.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public Size2D(uint width, uint height)
    {
        Width = width;
        Height = height;
    }

    ///<inheritdoc></inheritdoc>
    public override readonly string ToString() => $"Width={Width} Height={Height}";
}
