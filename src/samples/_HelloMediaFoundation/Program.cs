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
using HelloMediaFoundation;
using WICPixelFormat = Vortice.WIC.PixelFormat;

namespace HelloMediaFoundation
{
    public static class Program
    {
      

        private class TestApplication : Application
        {
            VideoPlayer _player;
            private bool _screenshot;
            D3D11GraphicsDevice _dxObject;

            public TestApplication(bool headless = false)
                : base(headless)
            {
            }

            protected override void InitializeBeforeRun()
            {
                if (Headless)
                {
                    _dxObject = new D3D11GraphicsDevice(new System.Drawing.Size(800, 600));
                    _screenshot = true;
                }
                else
                {
                    _dxObject = new D3D11GraphicsDevice(MainWindow!);
                }

                
                _graphicsDevice = _dxObject;

                if (!Headless)
                {
                    _player = new VideoPlayer(MainWindow);

                    _player.Initialise(_dxObject.Device, "D:\\Development\\Pacific Strafe\\Pacific Strafe\\Assets\\Video\\Intro_strafe.mp4", MainWindow);
                }

            }

            protected override void OnKeyboardEvent(KeyboardKey key, bool pressed)
            {
                if (key == KeyboardKey.P && pressed)
                {
                    _screenshot = true;
                }
            }

            protected override void OnDraw(int width, int height)
            {
                ((D3D11GraphicsDevice)_graphicsDevice!).DeviceContext.Flush();

                if (_screenshot)
                {
                    _screenshot = false;
                }
            }

       
        }

        public static void Main()
        {
#if DEBUG
            Configuration.EnableObjectTracking = true;
#endif

            {
                using var app = new TestApplication(headless: false);
                app.Run();
            }

#if DEBUG
            Console.WriteLine(ObjectTracker.ReportActiveObjects());
#endif
        }
    }
}
