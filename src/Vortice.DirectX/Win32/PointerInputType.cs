// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Win32;

/// <summary>
/// Identifies the pointer input types.
/// </summary>
public enum PointerInputType
{
    /// <summary>
    /// Generic pointer type
    /// </summary>
    Pointer = 1,

    /// <summary>
    /// Touch pointer type.
    /// </summary>
    Touch = 2,

    /// <summary>
    /// Pen pointer type.
    /// </summary>
    Pen = 3,

    /// <summary>
    /// Mouse pointer type.
    /// </summary>
    Mouse = 4,

    /// <summary>
    /// Touchpad pointer type (Windows 8.1 and later).
    /// </summary>
    Touchpad = 5,
}
