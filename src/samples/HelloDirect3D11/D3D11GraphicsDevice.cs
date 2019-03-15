// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using Vortice;
using Vortice.Direct3D;
using SharpD3D11;
using Vortice.DXGI;
using static Vortice.DXGI.DXGI;
using static SharpD3D11.D3D11;

namespace HelloDirect3D11
{
    public sealed class D3D11GraphicsDevice : IGraphicsDevice
    {
        private static readonly FeatureLevel[] _featureLevels = new FeatureLevel[]
        {
            FeatureLevel.Level_11_1,
            FeatureLevel.Level_11_0
        };

        private const int FrameCount = 2;
        public readonly IDXGIFactory1 DXGIFactory;
        private readonly ID3D11Device _d3d11Device;
        private readonly ID3D11DeviceContext _d3d11DeviceContext;
        public ID3D11Device D3D11Device => _d3d11Device;
        public IDXGISwapChain SwapChain { get; }

        public static bool IsSupported()
        {
            return true;
        }

        public D3D11GraphicsDevice(bool validation, Window window)
        {
            if (CreateDXGIFactory1(out DXGIFactory).Failure)
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
                out _d3d11Device,
                out _d3d11DeviceContext).Failure)
            {
                // Remove debug flag not being supported.
                creationFlags &= ~DeviceCreationFlags.Debug;

                Debug.Assert(D3D11CreateDevice(
                    null,
                    DriverType.Hardware,
                    creationFlags,
                    _featureLevels,
                    out _d3d11Device,
                    out _d3d11DeviceContext).Success);
            }

            var swapChainDescription = new SwapChainDescription()
            {
                BufferCount = FrameCount,
                BufferDescription = new ModeDescription(window.Width, window.Height, Format.B8G8R8A8_UNorm),
                IsWindowed = true,
                OutputWindow = window.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Vortice.Usage.RenderTargetOutput
            };

            SwapChain = DXGIFactory.CreateSwapChain(_d3d11Device, swapChainDescription);
            DXGIFactory.MakeWindowAssociation(window.Handle, WindowAssociationFlags.IgnoreAltEnter);

            var backBuffer = SwapChain.GetBuffer<ID3D11Texture2D>(0);
            var renderView = _d3d11Device.CreateRenderTargetView(backBuffer);
        }

        public void Dispose()
        {
            SwapChain.Dispose();
            _d3d11DeviceContext.Dispose();
            _d3d11Device.Dispose();
            DXGIFactory.Dispose();
        }

        public void Present()
        {
            var result = SwapChain.Present(1, PresentFlags.None);
            if (result.Failure
                && result.Code == Vortice.DXGI.ResultCode.DeviceRemoved.Code)
            {
            }
        }
    }
}
