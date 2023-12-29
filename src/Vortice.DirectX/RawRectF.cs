// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice;

/// <summary>
/// Defines a floating-point rectangle (Left, Top, Right, Bottom)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public readonly record struct RawRectF
{
    public RawRectF(float left, float top, float right, float bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    /// <summary>
    /// The left position.
    /// </summary>
    public readonly float Left;

    /// <summary>
    /// The top position.
    /// </summary>
    public readonly float Top;

    /// <summary>
    /// The right position
    /// </summary>
    public readonly float Right;

    /// <summary>
    /// The bottom position.
    /// </summary>
    public readonly float Bottom;

    /// <summary>
    /// Performs an implicit conversion from <see cre ="Vortice.RawRectF"/> to <see cref="Rect" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Rect(RawRectF value) => Rect.FromLTRB(value.Left, value.Top, value.Right, value.Bottom);

    /// <summary>
    /// Performs an implicit conversion from <see cre ="Rect"/> to <see cref="RawRectF" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator RawRectF(in Rect value) => new(value.Left, value.Top, value.Right, value.Bottom);

    /// <summary>
    /// Performs an implicit conversion from <see cre ="Vortice.RawRectF"/> to <see cref="System.Drawing.RectangleF" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator System.Drawing.RectangleF(RawRectF value) => System.Drawing.RectangleF.FromLTRB(value.Left, value.Top, value.Right, value.Bottom);

    /// <summary>
    /// Performs an implicit conversion from <see cre ="System.Drawing.RectangleF"/> to <see cref="RawRectF" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator RawRectF(in System.Drawing.RectangleF value) => new(value.Left, value.Top, value.Right, value.Bottom);
}
