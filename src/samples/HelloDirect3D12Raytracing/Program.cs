// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice;
using SharpGen.Runtime.Diagnostics;
using SharpGen.Runtime;

namespace HelloDirect3D12Raytracing;

public static class Program
{
    private class TestApplication : Application
    {
        protected override void InitializeBeforeRun()
        {
            var validation = false;
#if DEBUG
            validation = true;
#endif

            _graphicsDevice = new D3D12GraphicsDevice(validation, MainWindow!);
        }
    }

    public static void Main()
    {
#if DEBUG
        Configuration.EnableObjectTracking = true;
#endif

        using TestApplication app = new();
        app.Run();
    }
}
