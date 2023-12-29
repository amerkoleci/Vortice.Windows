// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice;

/// <summary>
/// Provides access to data organized in 2D.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct DataRectangle
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataRectangle"/> class.
    /// </summary>
    /// <param name="dataPointer">The data pointer.</param>
    /// <param name="pitch">The pitch.</param>
    public DataRectangle(IntPtr dataPointer, int pitch)
    {
        DataPointer = dataPointer;
        Pitch = pitch;
    }

    /// <summary>
    /// Pointer to the data.
    /// </summary>
    public readonly IntPtr DataPointer;

    /// <summary>
    /// Gets the number of bytes per row.
    /// </summary>
    public readonly int Pitch;
}
