// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Win32;

/// <summary>
/// Identifies a change in the state of a button associated with a pointer.
/// </summary>
public enum PointerButtonChangeType
{
    None,
    /// <summary>
    /// The first button transitioned to a pressed state.
    /// </summary>
    FirstButtonDown,
    /// <summary>
    /// The first button transitioned to a released state.
    /// </summary>
    FirstButtonUp,
    /// <summary>
    /// The second button transitioned to a pressed state.
    /// </summary>
    SecondButtonDown,
    /// <summary>
    /// The second button transitioned to a released state.
    /// </summary>
    SecondButtonUp,
    /// <summary>
    /// The third button transitioned to a pressed state.
    /// </summary>
    ThirdButtonDown,
    /// <summary>
    /// The third button transitioned to a released state.
    /// </summary>
    ThirdButtonUp,
    /// <summary>
    /// The fourth button transitioned to a pressed state.
    /// </summary>
    FourthButtonDown,
    /// <summary>
    /// The fourth button transitioned to a released state.
    /// </summary>
    FourthButtonUp,
    /// <summary>
    /// The fifth button transitioned to a pressed state.
    /// </summary>
    FifthButtonDown,
    /// <summary>
    /// The fifth button transitioned to a released state.
    /// </summary>
    FifthButtonUp,
}
