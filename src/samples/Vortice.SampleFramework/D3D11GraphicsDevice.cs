// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.DXGI;
using static Vortice.DXGI.DXGI;
using static Vortice.Direct3D11.D3D11;
using System.Runtime.CompilerServices;
using Vortice.Mathematics;
using SharpGen.Runtime;

namespace Vortice
{
    public sealed class D3D11GraphicsDevice : IGraphicsDevice
    {
        private static readonly FeatureLevel[] s_featureLevels = new[]
        {
            FeatureLevel.Level_11_1,
            FeatureLevel.Level_11_0,
            FeatureLevel.Level_10_1,
            FeatureLevel.Level_10_0
        };

        private const int FrameCount = 2;

        public readonly Window Window;
        public readonly IDXGIFactory2 Factory;
        public readonly ID3D11Device1 Device;
        public readonly FeatureLevel FeatureLevel;
        public readonly ID3D11DeviceContext1 DeviceContext;
        public readonly IDXGISwapChain1 SwapChain;
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

            using (IDXGIAdapter1 adapter = GetHardwareAdapter())
            {
                DeviceCreationFlags creationFlags = DeviceCreationFlags.BgraSupport;
                if (validation && SdkLayersAvailable())
                {
                    creationFlags |= DeviceCreationFlags.Debug;
                }

                if (D3D11CreateDevice(
                    adapter,
                    DriverType.Unknown,
                    creationFlags,
                    s_featureLevels,
                    out ID3D11Device tempDevice, out FeatureLevel, out ID3D11DeviceContext tempContext).Failure)
                {
                    // If the initialization fails, fall back to the WARP device.
                    // For more information on WARP, see:
                    // http://go.microsoft.com/fwlink/?LinkId=286690
                    D3D11CreateDevice(
                        null,
                        DriverType.Warp,
                        creationFlags,
                        s_featureLevels,
                        out tempDevice, out FeatureLevel, out tempContext).CheckError();
                }

                Device = tempDevice.QueryInterface<ID3D11Device1>();
                DeviceContext = tempContext.QueryInterface<ID3D11DeviceContext1>();
                tempContext.Dispose();
                tempDevice.Dispose();
            }

            IntPtr hwnd = window.Handle;

            SwapChainDescription1 swapChainDescription = new SwapChainDescription1()
            {
                Width = window.Width,
                Height = window.Height,
                Format = Format.B8G8R8A8_UNorm,
                BufferCount = FrameCount,
                Usage = DXGI.Usage.RenderTargetOutput,
                SampleDescription = new SampleDescription(1, 0),
                Scaling = Scaling.Stretch,
                SwapEffect = SwapEffect.FlipDiscard,
                AlphaMode = AlphaMode.Ignore
            };

            SwapChainFullscreenDescription fullscreenDescription = new SwapChainFullscreenDescription
            {
                Windowed = true
            };


            SwapChain = Factory.CreateSwapChainForHwnd(Device, hwnd, swapChainDescription, fullscreenDescription);
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

            if (DXGIGetDebugInterface1(out IDXGIDebug1 dxgiDebug).Success)
            {
                dxgiDebug.ReportLiveObjects(All, ReportLiveObjectFlags.Summary | ReportLiveObjectFlags.IgnoreInternal);
                dxgiDebug.Dispose();
            }
        }

        private IDXGIAdapter1 GetHardwareAdapter()
        {
            IDXGIAdapter1 adapter = null;
            IDXGIFactory6 factory6 = Factory.QueryInterfaceOrNull<IDXGIFactory6>();
            if (factory6 != null)
            {
                for (int adapterIndex = 0;
                    factory6.EnumAdapterByGpuPreference(adapterIndex, GpuPreference.HighPerformance, out adapter) != Vortice.DXGI.ResultCode.NotFound;
                    adapterIndex++)
                {
                    AdapterDescription1 desc = adapter.Description1;

                    if ((desc.Flags & AdapterFlags.Software) != AdapterFlags.None)
                    {
                        // Don't select the Basic Render Driver adapter.
                        adapter.Dispose();
                        continue;
                    }

                    return adapter;
                }


                factory6.Dispose();
            }

            if (adapter == null)
            {
                for (int adapterIndex = 0;
                    Factory.EnumAdapters1(adapterIndex, out adapter) != Vortice.DXGI.ResultCode.NotFound;
                    adapterIndex++)
                {
                    AdapterDescription1 desc = adapter.Description1;

                    if ((desc.Flags & AdapterFlags.Software) != AdapterFlags.None)
                    {
                        // Don't select the Basic Render Driver adapter.
                        adapter.Dispose();
                        continue;
                    }

                    return adapter;
                }
            }

            return adapter;
        }

        public bool DrawFrame(Action<int, int> draw, [CallerMemberName] string frameName = null)
        {
            DeviceContext.RSSetViewport(new Viewport(Window.Width, Window.Height));
            var clearColor = new Color4(0.0f, 0.2f, 0.4f, 1.0f);
            DeviceContext.ClearRenderTargetView(RenderTargetView, clearColor);

            // Call callback.
            draw(Window.Width, Window.Height);

            var result = SwapChain.Present(1, PresentFlags.None);
            if (result.Failure
                && result.Code == Vortice.DXGI.ResultCode.DeviceRemoved.Code)
            {
                return false;
            }

            return true;
        }
    }
}
