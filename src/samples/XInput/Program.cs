// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.XInput;

namespace HelloXInput;

public static class Program
{
    private static void ClearLine()
    {
        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
    }

    static void Main()
    {
        uint userIndex = 0;

        while (true)
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).KeyChar)
                {
                    case 'q':
                        return;

                    case char c when c >= '1' && c <= '4':
                        userIndex = (uint)(c - '1');
                        break;
                }
            }

            bool ok = XInput.GetState(userIndex, out State state);

            if (ok)
            {
                Vibration vibration = new((ushort)(state.Gamepad.LeftTrigger << 8), (ushort)(state.Gamepad.RightTrigger << 8));
                XInput.SetVibration(userIndex, vibration);
            }
            else
            {
                state = new State();    // empty state variable if GetState failed
            }

            Console.SetCursorPosition(0, 0);

            ClearLine(); Console.WriteLine($"=========================================================================");
            ClearLine(); Console.WriteLine($"Press 1-4 to select gamepad, triggers control rumble, press 'q' to quit..");
            ClearLine(); Console.WriteLine($"=========================================================================");
            ClearLine(); Console.WriteLine($"Gamepad       : {userIndex + 1} {(ok ? "(ok)" : "(not ok)")}");
            ClearLine(); Console.WriteLine($"Buttons       : {state.Gamepad.Buttons}");
            ClearLine(); Console.WriteLine($"Left Thumb    : X = {state.Gamepad.LeftThumbX} Y = {state.Gamepad.LeftThumbY}");
            ClearLine(); Console.WriteLine($"Left Trigger  : {state.Gamepad.LeftTrigger}");
            ClearLine(); Console.WriteLine($"Right Thumb   : X = {state.Gamepad.RightThumbX} Y = {state.Gamepad.RightThumbY}");
            ClearLine(); Console.WriteLine($"Right Trigger : {state.Gamepad.RightTrigger}");

            Thread.Sleep(10);
        }
    }
}
