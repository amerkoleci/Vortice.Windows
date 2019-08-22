// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.XInput
{
    internal interface IXInput
    {
        int XInputGetState(int userIndex, out State state);
        int XInputSetState(int userIndex, Vibration vibration);
    }
}
