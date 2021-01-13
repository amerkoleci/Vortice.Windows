// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.XInput
{
    [System.Flags]
    public enum GamepadVirtualKey : short
    {
        None = 0,
        /// <summary>
        /// A button
        /// </summary>
        A = 0x5800,
        /// <summary>
        /// A button
        /// </summary>
        B = 0x5801,
        /// <summary>
        /// X button
        /// </summary>
        X = 0x5802,
        /// <summary>
        /// Y button
        /// </summary>
        Y = 0x5803,
        /// <summary>
        /// Right shoulder button 
        /// </summary>
        RightShoulder = 0x5804,
        /// <summary>
        /// Left shoulder button 
        /// </summary>
        LeftShoulder = 0x5805,
        /// <summary>
        /// Left trigger 
        /// </summary>
        LeftTrigger = 0x5806,
        /// <summary>
        /// Right trigger
        /// </summary>
        RightTrigger = 0x5807,
        /// <summary>
        /// Directional pad up
        /// </summary>
        DirectionalPadUp = 0x5810,
        /// <summary>
        /// Directional pad down
        /// </summary>
        DirectionalPadDown = 0x5811,
        /// <summary>
        /// Directional pad left
        /// </summary>
        DirectionalPadLeft = 0x5812,
        /// <summary>
        /// Directional pad right
        /// </summary>
        DirectionalPadRight = 0x5813,
        /// <summary>
        /// START button.
        /// </summary>
        Start = 0x5814,
        /// <summary>
        /// BACK button
        /// </summary>
        Back = 0x5815,
        /// <summary>
        /// Left thumbstick click
        /// </summary>
        LeftThumbPress = 0x5816,
        /// <summary>
        /// Right thumbstick click
        /// </summary>
        RightThumbPress = 0x5817,
        /// <summary>
        /// Left thumbstick up
        /// </summary>
        LeftThumbUp = 0x5820,
        /// <summary>
        /// Left thumbstick down
        /// </summary>
        LeftThumbDown = 0x5821,
        /// <summary>
        /// Left thumbstick right
        /// </summary>
        LeftThumbRight = 0x5822,
        /// <summary>
        /// Left thumbstick left
        /// </summary>
        LeftThumbLeft = 0x5823,
        /// <summary>
        /// Left thumbstick up and left 
        /// </summary>
        LeftThumbUpLeft = 0x5824,
        /// <summary>
        /// Left thumbstick up and right
        /// </summary>
        LeftThumbUpRight = 0x5825,
        /// <summary>
        /// Left thumbstick down and right.
        /// </summary>
        LeftThumbDownRight = 0x5826,
        /// <summary>
        /// Left thumbstick down and left
        /// </summary>
        LeftThumbDownLeft = 0x5827,
        /// <summary>
        /// Right thumbstick up
        /// </summary>
        RightThumbUp = 0x5830,
        /// <summary>
        /// Right thumbstick down
        /// </summary>
        RightThumbDown = 0x5831,
        /// <summary>
        /// Right thumbstick right
        /// </summary>
        RightThumbRight = 0x5832,
        /// <summary>
        /// Right thumbstick left
        /// </summary>
        RightThumbLeft = 0x5833,
        /// <summary>
        /// Right thumbstick up and left
        /// </summary>
        RightThumbUpLeft = 0x5834,
        /// <summary>
        /// Right thumbstick up and right
        /// </summary>
        RightThumbUpRight = 0x5835,
        /// <summary>
        /// Right thumbstick down and right
        /// </summary>
        RightThumbDownRight = 0x5836,
        /// <summary>
        /// Right thumbstick down and left
        /// </summary>
        RightThumbDownLeft = 0x5837
    }
}
