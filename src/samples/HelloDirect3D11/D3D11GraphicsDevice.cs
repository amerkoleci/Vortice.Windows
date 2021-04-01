// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using SharpGen.Runtime;
using Vortice;
using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.DXGI;
using Vortice.Mathematics;
using static Vortice.Direct3D11.D3D11;
using static Vortice.DXGI.DXGI;

namespace HelloDirect3D11
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

        public readonly Window? Window;
        public readonly Size Size;
        public readonly IDXGIFactory2 Factory;
        public readonly ID3D11Device1 Device;
        public readonly FeatureLevel FeatureLevel;
        public readonly ID3D11DeviceContext1 DeviceContext;
        public readonly IDXGISwapChain1 SwapChain;
        public readonly ID3D11Texture2D? BackBufferTexture;
        public readonly ID3D11Texture2D? OffscreenTexture;
        public readonly ID3D11RenderTargetView RenderTargetView;

        public static bool IsSupported()
        {
            return true;
        }

        public D3D11GraphicsDevice(Window window)
            : this(window, window.ClientSize)
        {
        }

        public D3D11GraphicsDevice(Size size)
            : this(null, size)
        {
        }

        private D3D11GraphicsDevice(Window? window, Size size)
        {
            Window = window;
            Size = size;

            if (CreateDXGIFactory1(out Factory).Failure)
            {
                throw new InvalidOperationException("Cannot create IDXGIFactory1");
            }

            using (IDXGIAdapter1? adapter = GetHardwareAdapter())
            {
                DeviceCreationFlags creationFlags = DeviceCreationFlags.BgraSupport;
#if DEBUG
                if (SdkLayersAvailable())
                {
                    creationFlags |= DeviceCreationFlags.Debug;
                }
#endif

                if (D3D11CreateDevice(
                    adapter!,
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

            if (window != null)
            {
                IntPtr hwnd = window.Handle;

                SwapChainDescription1 swapChainDescription = new SwapChainDescription1()
                {
                    Width = window.ClientSize.Width,
                    Height = window.ClientSize.Height,
                    Format = Format.R8G8B8A8_UNorm,
                    BufferCount = FrameCount,
                    Usage = Vortice.DXGI.Usage.RenderTargetOutput,
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

                BackBufferTexture = SwapChain.GetBuffer<ID3D11Texture2D>(0);
                RenderTargetView = Device.CreateRenderTargetView(BackBufferTexture);
            }
            else
            {
                // Create offscreen texture
                OffscreenTexture = Device.CreateTexture2D(new Texture2DDescription(Format.R8G8B8A8_UNorm, Size.Width, Size.Height, 1, 1, BindFlags.ShaderResource | BindFlags.RenderTarget));
                RenderTargetView = Device.CreateRenderTargetView(OffscreenTexture);
            }
        }

        public void Dispose()
        {
            BackBufferTexture?.Dispose();
            OffscreenTexture?.Dispose();
            RenderTargetView.Dispose();
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

        private IDXGIAdapter1? GetHardwareAdapter()
        {
            IDXGIAdapter1? adapter = null;
            IDXGIFactory6 factory6 = Factory.QueryInterfaceOrNull<IDXGIFactory6>();
            if (factory6 != null)
            {
                for (int adapterIndex = 0;
                    factory6.EnumAdapterByGpuPreference(adapterIndex, GpuPreference.HighPerformance, out adapter).Success;
                    adapterIndex++)
                {
                    if (adapter == null)
                        continue;

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
                    Factory.EnumAdapters1(adapterIndex, out adapter).Success;
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

        public bool DrawFrame(Action<int, int> draw, [CallerMemberName] string? frameName = null)
        {
            var clearColor = new Color4(0.0f, 0.2f, 0.4f, 1.0f);
            DeviceContext.ClearRenderTargetView(RenderTargetView, clearColor);
            DeviceContext.OMSetRenderTargets(RenderTargetView, /*depthStencil*/null);

            DeviceContext.RSSetViewport(new Viewport(Size.Width, Size.Height));

            // Call callback.
            draw(Size.Width, Size.Height);

            if (SwapChain != null)
            {
                Result result = SwapChain.Present(1, PresentFlags.None);
                if (result.Failure
                    && result.Code == Vortice.DXGI.ResultCode.DeviceRemoved.Code)
                {
                    return false;
                }
            }

            return true;
        }

        public ID3D11Texture2D? CaptureTexture(ID3D11Texture2D source)
        {
            ID3D11Texture2D? stagingTexture;
            var desc = source.Description;

            if (desc.ArraySize > 1 || desc.MipLevels > 1)
            {
                Console.WriteLine("WARNING: ScreenGrab does not support 2D arrays, cubemaps, or mipmaps; only the first surface is written. Consider using DirectXTex instead.");
                return null;
            }

            if (desc.SampleDescription.Count > 1)
            {
                // MSAA content must be resolved before being copied to a staging texture
                desc.SampleDescription.Count = 1;
                desc.SampleDescription.Quality = 0;

                ID3D11Texture2D temp = Device.CreateTexture2D(desc);
                Format format = desc.Format;

                FormatSupport formatSupport = Device.CheckFormatSupport(format);

                if ((formatSupport & FormatSupport.MultisampleResolve) == FormatSupport.None)
                {
                    return null;
                }

                for (int item = 0; item < desc.ArraySize; ++item)
                {
                    for (int level = 0; level < desc.MipLevels; ++level)
                    {
                        int index = ID3D11Resource.CalculateSubResourceIndex(level, item, desc.MipLevels);
                        DeviceContext.ResolveSubresource(temp, index, source, index, format);
                    }
                }

                desc.BindFlags = BindFlags.None;
                desc.OptionFlags &= ResourceOptionFlags.TextureCube;
                desc.CpuAccessFlags = CpuAccessFlags.Read;
                desc.Usage = Vortice.Direct3D11.Usage.Staging;

                stagingTexture = Device.CreateTexture2D(desc);

                DeviceContext.CopyResource(stagingTexture, temp);
            }
            else if ((desc.Usage == Vortice.Direct3D11.Usage.Staging) && ((desc.CpuAccessFlags & CpuAccessFlags.Read) != CpuAccessFlags.None))
            {
                // Handle case where the source is already a staging texture we can use directly
                stagingTexture = source;
            }
            else
            {
                // Otherwise, create a staging texture from the non-MSAA source
                desc.BindFlags = 0;
                desc.OptionFlags &= ResourceOptionFlags.TextureCube;
                desc.CpuAccessFlags = CpuAccessFlags.Read;
                desc.Usage = Vortice.Direct3D11.Usage.Staging;

                stagingTexture = Device.CreateTexture2D(desc);

                DeviceContext.CopyResource(stagingTexture, source);
            }

            return stagingTexture;
        }
    }
}
