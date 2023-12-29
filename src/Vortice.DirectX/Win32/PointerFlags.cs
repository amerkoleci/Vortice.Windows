// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Win32;

[Flags]
public enum PointerFlags : uint
{
    None = 0x00000000,

    /// <summary>
    /// Indicates the arrival of a new pointer.
    /// </summary>
    New = 0x00000001,

    /// <summary>
    /// Indicates that this pointer continues to exist. When this flag is not set, it indicates the pointer has left detection range.
    /// </summary>
    InRange = 0x00000002,

    /// <summary>
    /// Indicates that this pointer is in contact with the digitizer surface. When this flag is not set, it indicates a hovering pointer.
    /// </summary>
    InContact = 0x00000004,

    /// <summary>
    /// Indicates a primary action, analogous to a left mouse button down.
    /// </summary>
    FirstButton = 0x00000010,

    /// <summary>
    /// Indicates a secondary action, analogous to a right mouse button down.
    /// </summary>
    SecondButton = 0x00000020,

    /// <summary>
    /// Analogous to a mouse wheel button down.
    /// </summary>
    ThirdButton = 0x00000040,

    /// <summary>
    /// Analogous to a first extended mouse (XButton1) button down.
    /// </summary>
    FourthButton = 0x00000080,

    /// <summary>
    /// Analogous to a second extended mouse (XButton2) button down.
    /// </summary>
    FifthButton = 0x00000100,

    /// <summary>
    /// Indicates that this pointer has been designated as the primary pointer.
    /// A primary pointer is a single pointer that can perform actions beyond those available to non-primary pointers. 
    /// </summary>
    Primary = 0x00002000,

    /// <summary>
    /// Confidence is a suggestion from the source device about whether the pointer represents an intended or accidental interaction, which is especially relevant for PT_TOUCH pointers where an accidental interaction (such as with the palm of the hand) can trigger input. 
    /// </summary>
    Confidence = 0x00004000,

    /// <summary>
    /// Indicates that the pointer is departing in an abnormal manner, such as when the system receives invalid input for the pointer or when a device with active pointers departs abruptly. 
    /// </summary>
    Canceled = 0x00008000,

    /// <summary>
    /// Indicates that this pointer transitioned to a down state; that is, it made contact with the digitizer surface.
    /// </summary>
    Down = 0x00010000,

    /// <summary>
    /// Indicates that this is a simple update that does not include pointer state changes.
    /// </summary>
    Update = 0x00020000,

    /// <summary>
    /// Indicates that this pointer transitioned to an up state; that is, contact with the digitizer surface ended.
    /// </summary>
    Up = 0x00040000,

    /// <summary>
    /// Indicates input associated with a pointer wheel.
    /// For mouse pointers, this is equivalent to the action of the mouse scroll wheel (WM_MOUSEHWHEEL).
    /// </summary>
    Wheel = 0x00080000,

    /// <summary>
    /// Indicates input associated with a pointer h-wheel.
    /// For mouse pointers, this is equivalent to the action of the mouse horizontal scroll wheel (WM_MOUSEHWHEEL).
    /// </summary>
    HorizontalWheel = 0x00100000,

    /// <summary>
    /// Indicates that this pointer was captured by (associated with) another element and the original element has lost capture (see WM_POINTERCAPTURECHANGED).
    /// </summary>
    CaptureChanged = 0x00200000,

    /// <summary>
    /// Indicates that this pointer has an associated transform.
    /// </summary>
    HasTransform = 0x00400000
}
