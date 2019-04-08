// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpDXGI.Direct3D;
using SharpDirect3D11;
using SharpDXGI;
using static SharpDXGI.DXGI;
using static SharpDirect3D11.D3D11;
using System.Numerics;
using System.Runtime.CompilerServices;
using Vortice.Mathematics;

namespace Vortice
{
    public sealed class D3D11GraphicsDevice : IGraphicsDevice
    {
        private static readonly FeatureLevel[] _featureLevels = new[]
        {
            FeatureLevel.Level_11_1,
            FeatureLevel.Level_11_0,
            FeatureLevel.Level_10_1,
            FeatureLevel.Level_10_0
        };

        private static readonly FeatureLevel[] _featureLevelsNoLevel11 = new[]
        {
            FeatureLevel.Level_11_0,
            FeatureLevel.Level_10_1,
            FeatureLevel.Level_10_0
        };

        private const int FrameCount = 2;

        public readonly Window Window;
        public readonly IDXGIFactory1 Factory;
        public readonly ID3D11Device Device;
        public readonly FeatureLevel FeatureLevel;
        public readonly ID3D11DeviceContext DeviceContext;
        public readonly IDXGISwapChain SwapChain;
        public readonly ID3D11Texture2D BackBuffer;
        public readonly ID3D11RenderTargetView RenderTargetView;

        public static bool IsSupported()
        {
            return true;
        }

        public D3D11GraphicsDevice(bool validation, Window window)
        {
            Window = window;
            if (CreateDXGIFactory1(out Factory).Failure)
            {
                throw new InvalidOperationException("Cannot create IDXGIFactory1");
            }

            var creationFlags = DeviceCreationFlags.BgraSupport;
            if (validation)
            {
                creationFlags |= DeviceCreationFlags.Debug;
            }

            if (D3D11CreateDevice(
                null,
                DriverType.Hardware,
                creationFlags,
                _featureLevels,
                out Device, out FeatureLevel, out DeviceContext).Failure)
            {
                // Remove debug flag not being supported.
                creationFlags &= ~DeviceCreationFlags.Debug;

                var result = D3D11CreateDevice(null, DriverType.Hardware,
                    creationFlags, _featureLevels,
                    out Device, out FeatureLevel, out DeviceContext);
                if (result.Failure)
                {
                    // This will fail on Win 7 due to lack of 11.1, so re-try again without it
                    D3D11CreateDevice(
                        null,
                        DriverType.Hardware,
                        creationFlags,
                        _featureLevelsNoLevel11,
                        out Device, out FeatureLevel, out DeviceContext).CheckError();
                }
            }

            var hwnd = (IntPtr)window.Handle;

            var swapChainDescription = new SwapChainDescription()
            {
                BufferCount = FrameCount,
                BufferDescription = new ModeDescription(window.Width, window.Height, Format.B8G8R8A8_UNorm),
                IsWindowed = true,
                OutputWindow = hwnd,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = SharpDXGI.Usage.RenderTargetOutput
            };

            SwapChain = Factory.CreateSwapChain(Device, swapChainDescription);
            Factory.MakeWindowAssociation(hwnd, WindowAssociationFlags.IgnoreAltEnter);

            BackBuffer = SwapChain.GetBuffer<ID3D11Texture2D>(0);
            RenderTargetView = Device.CreateRenderTargetView(BackBuffer);
        }

        public void Dispose()
        {
            RenderTargetView.Dispose();
            BackBuffer.Dispose();
            DeviceContext.ClearState();
            DeviceContext.Flush();
            DeviceContext.Dispose();
            Device.Dispose();
            SwapChain.Dispose();
            Factory.Dispose();
        }

        public bool DrawFrame(Action<int, int> draw, [CallerMemberName]string frameName = null)
        {
            DeviceContext.RSSetViewport(new Viewport(Window.Width, Window.Height));
            var clearColor = new Color4(0.0f, 0.2f, 0.4f, 1.0f);
            DeviceContext.ClearRenderTargetView(RenderTargetView, clearColor);

            // Call callback.
            draw(Window.Width, Window.Height);

            var result = SwapChain.Present(1, PresentFlags.None);
            if (result.Failure
                && result.Code == SharpDXGI.ResultCode.DeviceRemoved.Code)
            {
                return false;
            }

            return true;
        }
    }
}
