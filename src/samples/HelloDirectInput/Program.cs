// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.IO;
using SharpGen.Runtime;
using SharpGen.Runtime.Diagnostics;
using Vortice;
using Vortice.Direct3D11;
using Vortice.Mathematics;
using Vortice.WIC;
using WICPixelFormat = Vortice.WIC.PixelFormat;

namespace HelloDirectInput
{
    public static class Program
    {
      

        private class TestApplication : Application
        {
            protected DirectInputDevice _directInputDevice;

            public TestApplication(bool headless = false)
                : base(headless)
            {
            }

            protected override void InitializeBeforeRun()
            {
                if (Headless)
                {
                    _graphicsDevice = new D3D11GraphicsDevice(new System.Drawing.Size(800, 600));
                }
                else
                {
                    _graphicsDevice = new D3D11GraphicsDevice(MainWindow!);

                    _directInputDevice = new DirectInputDevice();

                    _directInputDevice.Initialise(MainWindow.Handle);
                }


          
            }

            protected override void OnKeyboardEvent(KeyboardKey key, bool pressed)
            {
            }

            protected override void OnDraw(int width, int height)
            {
                ((D3D11GraphicsDevice)_graphicsDevice!).DeviceContext.Flush();

                _directInputDevice.GetKeyboardUpdates();

                _directInputDevice.GetKJoystickUpdates();
            }

        }

        public static void Main()
        {
#if DEBUG
            Configuration.EnableObjectTracking = true;
#endif

            using var app = new TestApplication(headless: false);
            app.Run();
#if DEBUG
            Console.WriteLine(ObjectTracker.ReportActiveObjects());
#endif
        }
    }
}
