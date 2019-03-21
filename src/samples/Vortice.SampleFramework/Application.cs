// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;

namespace Vortice
{
    public abstract partial class Application : IDisposable
    {
        private bool _useDirect3D12;
        private bool _paused;
        private bool _exitRequested;

        private IGraphicsDevice _graphicsDevice;
        public Window MainWindow { get; private set; }

        protected Application(bool useDirect3D12)
        {
            _useDirect3D12 = useDirect3D12;
            PlatformConstruct();
        }

        public void Dispose()
        {
            _graphicsDevice.Dispose();
        }

        public void Tick()
        {
            _graphicsDevice.DrawFrame(OnDraw);
        }

        public void Run()
        {
            PlatformRun();
        }

        protected virtual void OnActivated()
        {
        }

        protected virtual void OnDeactivated()
        {
        }

        protected virtual void OnDraw(int width, int height)
        {

        }

        private void InitializeBeforeRun()
        {
            if (_useDirect3D12
               && !D3D12GraphicsDevice.IsSupported())
            {
                _useDirect3D12 = false;
            }

            var validation = false;
#if DEBUG
            validation = true;
#endif

            if (_useDirect3D12)
            {
                _graphicsDevice = new D3D12GraphicsDevice(validation, MainWindow);
            }
            else
            {
                _graphicsDevice = new D3D11GraphicsDevice(validation, MainWindow);
            }
        }
    }
}
