// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XInput;

/// <summary>
/// Describes the current state of the Xbox 360 Controller.
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct Gamepad
{
    public const short LeftThumbDeadZone = 7849;
    public const short RightThumbDeadZone = 8689;
    public const byte TriggerThreshold = 30;

    /// <summary>
    /// Bitmask of the device digital buttons, as follows. 
    /// A set bit indicates that the corresponding button is pressed. 
    /// </summary>
    public GamepadButtons Buttons;

    /// <summary>
    /// The current value of the left trigger analog control. 
    /// The value is between 0 and 255.
    /// </summary>
    public byte LeftTrigger;

    /// <summary>
    /// The current value of the right trigger analog control. 
    /// The value is between 0 and 255.
    /// </summary>
    public byte RightTrigger;

    /// <summary>
    /// Left thumbstick x-axis value. 
    /// Each of the thumbstick axis members is a signed value between -32768 and 32767 describing the position of the thumbstick. 
    /// A value of 0 is centered. 
    /// Negative values signify down or to the left. 
    /// Positive values signify up or to the right. 
    /// The constants <see cref="LeftThumbDeadZone" /> or <see cref="RightThumbDeadZone" /> can be used as a positive and negative value to filter a thumbstick input. 
    /// </summary>
    public short LeftThumbX;

    /// <summary>
    /// Left thumbstick y-axis value. The value is between -32768 and 32767.
    /// </summary>
    public short LeftThumbY;

    /// <summary>
    /// Right thumbstick x-axis value. The value is between -32768 and 32767.
    /// </summary>
    public short RightThumbX;

    /// <summary>
    /// Right thumbstick y-axis value. The value is between -32768 and 32767.
    /// </summary>
    public short RightThumbY;

    public override string ToString()
    {
        return $"Buttons: {Buttons}, LeftTrigger: {LeftTrigger}, RightTrigger: {RightTrigger}, LeftThumbX: {LeftThumbX}, LeftThumbY: {LeftThumbY}, RightThumbX: {RightThumbX}, RightThumbY: {RightThumbY}";
    }
}
