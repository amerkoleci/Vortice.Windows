// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XInput;

/// <summary>
/// Describes Device subtypes available in <see cref="Capabilities"/>.
/// </summary>
public enum DeviceSubType : byte
{
    /// <summary>
    /// The controller type is unknown.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Gamepad controller.
    /// Includes Left and Right Sticks, Left and Right Triggers, Directional Pad, and all standard buttons (A, B, X, Y, START, BACK, LB, RB, LSB, RSB).
    /// </summary>
    Gamepad = 1,
    /// <summary>
    /// Racing wheel controller. 
    /// Left Stick X reports the wheel rotation, Right Trigger is the acceleration pedal, and Left Trigger is the brake pedal. Includes Directional Pad and most standard buttons (A, B, X, Y, START, BACK, LB, RB). LSB and RSB are optional.
    /// </summary>
    Wheel = 2,
    /// <summary>
    /// Arcade stick controller.
    /// Includes a Digital Stick that reports as a DPAD (up, down, left, right), and most standard buttons (A, B, X, Y, START, BACK). The Left and Right Triggers are implemented as digital buttons and report either 0 or 0xFF. LB, LSB, RB, and RSB are optional.
    /// </summary>
    ArcadeStick = 3,
    /// <summary>
    /// Flight stick controller. 
    /// Includes a pitch and roll stick that reports as the Left Stick, a POV Hat which reports as the Right Stick, a rudder (handle twist or rocker) that reports as Left Trigger, and a throttle control as the Right Trigger. Includes support for a primary weapon (A), secondary weapon (B), and other standard buttons (X, Y, START, BACK). LB, LSB, RB, and RSB are optional.
    /// </summary>
    FlightStick = 4,
    /// <summary>
    /// Dance pad controller. 
    /// Includes the Directional Pad and standard buttons (A, B, X, Y) on the pad, plus BACK and START.
    /// </summary>
    DancePad = 5,
    /// <summary>
    /// Guitar controller.
    /// The strum bar maps to DPAD (up and down), and the frets are assigned to A (green), B (red), Y (yellow), X (blue), and LB (orange). Right Stick Y is associated with a vertical orientation sensor; Right Stick X is the whammy bar. Includes support for BACK, START, DPAD (left, right). Left Trigger (pickup selector), Right Trigger, RB, LSB (fret modifier), RSB are optional.
    /// </summary>
    Guitar = 6,
    /// <summary>
    /// Guitar controller. 
    /// Guitar Alt supports a larger range of movement for the vertical orientation sensor.
    /// </summary>
    GuitarAlternate = 7,
    /// <summary>
    /// Drum controller.
    /// The drum pads are assigned to buttons: A for green (Floor Tom), B for red (Snare Drum), X for blue (Low Tom), Y for yellow (High Tom), and LB for the pedal (Bass Drum). Includes Directional-Pad, BACK, and START. RB, LSB, and RSB are optional.
    /// </summary>
    DrumKit = 8,
    /// <summary>
    /// Guitar controller. 
    /// Guitar Bass is identical to <see cref="Guitar"/>, with the distinct subtype to simplify setup.
    /// </summary>
    GuitarBass = 11,
    /// <summary>
    /// Arcade pad controller. 
    /// Includes Directional Pad and most standard buttons (A, B, X, Y, START, BACK, LB, RB). The Left and Right Triggers are implemented as digital buttons and report either 0 or 0xFF. Left Stick, Right Stick, LSB, and RSB are optional.
    /// </summary>
    ArcadePad = 19
}
