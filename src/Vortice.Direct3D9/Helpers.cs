// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using Vortice.Mathematics;

namespace Vortice.Direct3D9;

internal static class Helpers
{
    public static readonly Guid DebugObjectName = new(0x429B8C22, 0x9188, 0x4B0C, 0x87, 0x42, 0xAC, 0xB0, 0xBF, 0x85, 0xC2, 0x00);

    /// <summary>
    /// Converts the color into a packed integer.
    /// </summary>
    /// <returns>A packed integer containing all four color components.</returns>
    public static int ToBgra(in Color color)
    {
        int value = color.B;
        value |= color.G << 8;
        value |= color.R << 16;
        value |= color.A << 24;

        return (int)value;
    }

#if !NET5_0_OR_GREATER
    public static unsafe Span<T> CreateSpan<T>(ref T value, int length)
    {
        return new(Unsafe.AsPointer(ref value), length);
    }
#endif
}
