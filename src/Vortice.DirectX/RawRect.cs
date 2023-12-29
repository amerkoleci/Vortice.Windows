// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice;

/// <summary>
/// Defines an integer rectangle (Left, Top, Right, Bottom)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public readonly record struct RawRect
{
    public RawRect(int left, int top, int right, int bottom)
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

    /// <summary>
    /// Performs an implicit conversion from <see cre ="RawRect"/> to <see cref="RectI" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator RectI(in RawRect value) => RectI.FromLTRB(value.Left, value.Top, value.Right, value.Bottom);

    /// <summary>
    /// Performs an implicit conversion from <see cre ="RectI"/> to <see cref="RawRect" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator RawRect(in RectI value) => new(value.Left, value.Top, value.Right, value.Bottom);

    /// <summary>
    /// Performs an implicit conversion from <see cre ="RawRect"/> to <see cref="System.Drawing.Rectangle" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator System.Drawing.Rectangle(in RawRect value) => System.Drawing.Rectangle.FromLTRB(value.Left, value.Top, value.Right, value.Bottom);

    /// <summary>
    /// Performs an implicit conversion from <see cre ="System.Drawing.Rectangle"/> to <see cref="RawRect" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator RawRect(in System.Drawing.Rectangle value) => new(value.Left, value.Top, value.Right, value.Bottom);
}
