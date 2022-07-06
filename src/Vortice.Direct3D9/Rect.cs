// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using Vortice.Mathematics;

namespace Vortice.Direct3D9;

/// <summary>
/// Defines an integer rectangle (Left, Top, Right, Bottom)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
[DebuggerDisplay("Left: {Left}, Top: {Top}, Right: {Right}, Bottom: {Bottom}")]
public readonly struct Rect
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
    public readonly SizeI Size => new(Width, Height);

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{nameof(Left)}: {Left}, {nameof(Top)}: {Top}, {nameof(Right)}: {Right}, {nameof(Bottom)}: {Bottom}";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cre ="Rect"/> to <see cref="RectI" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator RectI(Rect value) => RectI.FromLTRB(value.Left, value.Top, value.Right, value.Bottom);

    /// <summary>
    /// Performs an implicit conversion from <see cre ="RectangleI"/> to <see cref="Rect" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Rect(RectI value) => new(value.Left, value.Top, value.Right, value.Bottom);
}
