// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace HelloDirectInput;

public static class Program
{
    public static void Main()
    {
        DIInputDevice directInputDevice = new DIInputDevice();
        directInputDevice.Initialize(IntPtr.Zero);

        while (true)
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).KeyChar)
                {
                    case 'q':
                        return;
                }
            }

            directInputDevice.GetKeyboardUpdates();
            directInputDevice.GetKJoystickUpdates();
        }
    }
}
