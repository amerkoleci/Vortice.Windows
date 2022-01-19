// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XInput;

[Flags]
public enum KeyStrokeFlags : ushort
{
    /// <summary>
    /// None.
    /// </summary>
    None = 0,
    /// <summary>
    /// The key was pressed. 
    /// </summary>
    KeyDown = 0x0001,
    /// <summary>
    /// The key was released. 
    /// </summary>
    KeyUp = 0x0002,
    /// <summary>
    /// A repeat of a held key. 
    /// </summary>
    Repeat = 0x0004,
}
