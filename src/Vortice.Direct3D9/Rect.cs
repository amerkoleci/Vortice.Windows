// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

/// <summary>
/// Defines an integer rectangle (Left, Top, Right, Bottom)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public readonly record struct Rect
{
    /// <summary>
    /// A <see cref="Rect"/> with all of its components set to zero.
    /// </summary>
    public static readonly Rect Empty = default;

    public Rect(int left, int top, int right, int bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    /// <summary>
    /// The left position.
    /// </summary>
    public readonly int Left;

    /// <summary>
    /// The top position.
    /// </summary>
    public readonly int Top;

    /// <summary>
    /// The right position
    /// </summary>
    public readonly int Right;

    /// <summary>
    /// The bottom position.
    /// </summary>
    public readonly int Bottom;

    public readonly int Width => Right - Left;
    public readonly int Height => Bottom - Top;
    public readonly Size Size => new(Width, Height);

    /// <summary>
    /// Performs an implicit conversion from <see cre ="Rect"/> to <see cref="System.Drawing.Rectangle" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Vortice.Mathematics.RectI(Rect value) => System.Drawing.Rectangle.FromLTRB(value.Left, value.Top, value.Right, value.Bottom);

    /// <summary>
    /// Performs an implicit conversion from <see cre ="System.Drawing.Rectangle"/> to <see cref="Rect" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Rect(System.Drawing.Rectangle value) => new(value.Left, value.Top, value.Right, value.Bottom);

    /// <summary>
    /// Performs an implicit conversion from <see cre ="Rect"/> to <see cref="System.Drawing.Rectangle" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator System.Drawing.Rectangle(Rect value) => System.Drawing.Rectangle.FromLTRB(value.Left, value.Top, value.Right, value.Bottom);

    /// <summary>
    /// Performs an implicit conversion from <see cre ="System.Drawing.Rectangle"/> to <see cref="Rect" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Rect(System.Drawing.Rectangle value) => new(value.Left, value.Top, value.Right, value.Bottom);
}
