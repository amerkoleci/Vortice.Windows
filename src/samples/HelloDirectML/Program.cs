// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;

namespace HelloDirectML;

public static class Program
{
    public static void Main()
    {
#if DEBUG
        Configuration.EnableObjectTracking = true;
#endif

        using HelloDml app = new();
        app.Run();
    }
}
