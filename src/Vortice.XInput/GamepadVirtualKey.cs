// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.XInput
{
    [Flags]
    public enum GamepadVirtualKey : short
    {
        None = 0,
        A = 0x5800,
        B = 0x5801,
        X = 0x5802,
        Y = 0x5803,
        RightShoulder = 0x5804,
        LeftShoulder = 0x5805,
        LeftTrigger = 0x5806,
        RightTrigger = 0x5807,
        DPadUp = 0x5810,
        DPadDown = 0x5811,
        DPadLeft = 0x5812,
        DPadRight = 0x5813,
        Start = 0x5814,
        Back = 0x5815,
        LeftThumbPress = 0x5816,
        RightThumbPress = 0x5817,
        LeftThumbUp = 0x5820,
        LeftThumbDown = 0x5821,
        LeftThumbRight = 0x5822,
        LeftThumbLeft = 0x5823,
        RightThumbUpLeft = 0x5824,
        LeftThumbUpright = 0x5825,
        LeftThumbDownright = 0x5826,
        RightThumbDownLeft = 0x5827,
        RightThumbUp = 0x5830,
        RightThumbDown = 0x5831,
        RightThumbRight = 0x5832,
        RightThumbLeft = 0x5833,
        RightThumbUpleft = 0x5834,
        RightThumbUpRight = 0x5835,
        RightThumbDownRight = 0x5836,
        RightThumbDownleft = 0x5837
    }
}
