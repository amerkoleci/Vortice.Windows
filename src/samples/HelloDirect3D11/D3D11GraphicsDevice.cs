// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using System.Runtime.CompilerServices;
using SharpGen.Runtime;
using Vortice;
using Vortice.D3DCompiler;
using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.Direct3D11.Debug;
using Vortice.DXGI;
using Vortice.DXGI.Debug;
using Vortice.Mathematics;
using static Vortice.Direct3D11.D3D11;
using static Vortice.DXGI.DXGI;

namespace HelloDirect3D11;

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
    public readonly SizeI Size;
    public readonly IDXGIFactory2 Factory;
    public readonly ID3D11Device1 Device;
    public readonly FeatureLevel FeatureLevel;
    public readonly ID3D11DeviceContext1 DeviceContext;
    public readonly IDXGISwapChain1? SwapChain;
    public readonly ID3D11Texture2D? BackBufferTexture;
    public readonly ID3D11Texture2D? OffscreenTexture;
    public readonly ID3D11RenderTargetView RenderTargetView;

    public readonly ID3D11Texture2D? DepthStencilTexture;
    public readonly ID3D11DepthStencilView? DepthStencilView;

    private readonly ID3D11Buffer _vertexBuffer;
    private readonly ID3D11VertexShader _vertexShader;
    private readonly ID3D11PixelShader _pixelShader;
    private readonly ID3D11InputLayout _inputLayout;

    public static bool IsSupported()
    {
        return true;
    }

    public D3D11GraphicsDevice(Window window, Format depthStencilFormat = Format.D32_Float)
        : this(window, window.ClientSize, depthStencilFormat)
    {
    }

    public D3D11GraphicsDevice(SizeI size, Format depthStencilFormat = Format.D32_Float)
        : this(null, size, depthStencilFormat)
    {
    }

    private D3D11GraphicsDevice(Window? window, SizeI size, Format depthStencilFormat = Format.D32_Float)
    {
        Window = window;
        Size = size;

        Factory = CreateDXGIFactory1<IDXGIFactory2>();

        using (IDXGIAdapter1 adapter = GetHardwareAdapter())
        {
            DeviceCreationFlags creationFlags = DeviceCreationFlags.BgraSupport;
#if DEBUG
            if (SdkLayersAvailable())
            {
                creationFlags |= DeviceCreationFlags.Debug;
            }
#endif

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
                    IntPtr.Zero,
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

            SwapChainDescription1 swapChainDescription = new()
            {
                Width = window.ClientSize.Width,
                Height = window.ClientSize.Height,
                Format = Format.R8G8B8A8_UNorm,
                BufferCount = FrameCount,
                BufferUsage = Usage.RenderTargetOutput,
                SampleDescription = SampleDescription.Default,
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
            OffscreenTexture = Device.CreateTexture2D(Format.R8G8B8A8_UNorm, Size.Width, Size.Height, 1, 1, null, BindFlags.ShaderResource | BindFlags.RenderTarget);
            RenderTargetView = Device.CreateRenderTargetView(OffscreenTexture);
        }

        if (depthStencilFormat != Format.Unknown)
        {
            DepthStencilTexture = Device.CreateTexture2D(depthStencilFormat, Size.Width, Size.Height, 1, 1, null, BindFlags.DepthStencil);
            DepthStencilView = Device.CreateDepthStencilView(DepthStencilTexture!, new DepthStencilViewDescription(DepthStencilTexture, DepthStencilViewDimension.Texture2D));
        }

        ReadOnlySpan<VertexPositionColor> triangleVertices = stackalloc VertexPositionColor[]
        {
            new VertexPositionColor(new Vector3(0f, 0.5f, 0.0f), new Color4(1.0f, 0.0f, 0.0f, 1.0f)),
            new VertexPositionColor(new Vector3(0.5f, -0.5f, 0.0f), new Color4(0.0f, 1.0f, 0.0f, 1.0f)),
            new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.0f), new Color4(0.0f, 0.0f, 1.0f, 1.0f))
        };

        bool dynamic = false;
        if (dynamic)
        {
            _vertexBuffer = Device.CreateBuffer(VertexPositionColor.SizeInBytes * 3, BindFlags.VertexBuffer, ResourceUsage.Dynamic, CpuAccessFlags.Write);
            MappedSubresource mappedSubresource = DeviceContext.Map(_vertexBuffer, 0, MapMode.WriteDiscard);
            triangleVertices.CopyTo(mappedSubresource.AsSpan<VertexPositionColor>(3));
            DeviceContext.Unmap(_vertexBuffer, 0);
        }
        else
        {
            _vertexBuffer = Device.CreateBuffer(triangleVertices, BindFlags.VertexBuffer);
        }

        InputElementDescription[] inputElementDescs = new[]
        {
            new InputElementDescription("POSITION", 0, Format.R32G32B32_Float, 0, 0),
            new InputElementDescription("COLOR", 0, Format.R32G32B32A32_Float, 12, 0)
        };

        Span<byte> vertexShaderByteCode = CompileBytecode("Triangle.hlsl", "VSMain", "vs_4_0");
        Span<byte> pixelShaderByteCode = CompileBytecode("Triangle.hlsl", "PSMain", "ps_4_0");

        _vertexShader = Device.CreateVertexShader(vertexShaderByteCode);
        _pixelShader = Device.CreatePixelShader(pixelShaderByteCode);
        _inputLayout = Device.CreateInputLayout(inputElementDescs, vertexShaderByteCode);
    }

    public void Dispose()
    {
        _vertexBuffer.Dispose();
        _vertexShader.Dispose();
        _pixelShader.Dispose();
        _inputLayout.Dispose();

        BackBufferTexture?.Dispose();
        OffscreenTexture?.Dispose();
        RenderTargetView.Dispose();
        DepthStencilTexture?.Dispose();
        DepthStencilView?.Dispose();

        DeviceContext.ClearState();
        DeviceContext.Flush();
        DeviceContext.Dispose();
        SwapChain?.Dispose();

#if DEBUG
        uint refCount = Device.Release();
        if (refCount > 0)
        {
            System.Diagnostics.Debug.WriteLine($"Direct3D11: There are {refCount} unreleased references left on the device");

            ID3D11Debug? d3d11Debug = Device.QueryInterfaceOrNull<ID3D11Debug>();
            if (d3d11Debug != null)
            {
                d3d11Debug.ReportLiveDeviceObjects(ReportLiveDeviceObjectFlags.Detail | ReportLiveDeviceObjectFlags.IgnoreInternal);
                d3d11Debug.Dispose();
            }
        }
#else
        Device.Dispose();
#endif
        Factory.Dispose();

#if DEBUG
        if (DXGIGetDebugInterface1(out IDXGIDebug1? dxgiDebug).Success)
        {
            dxgiDebug!.ReportLiveObjects(DebugAll, ReportLiveObjectFlags.Summary | ReportLiveObjectFlags.IgnoreInternal);
            dxgiDebug!.Dispose();
        }
#endif
    }

    private IDXGIAdapter1 GetHardwareAdapter()
    {
        IDXGIFactory6? factory6 = Factory.QueryInterfaceOrNull<IDXGIFactory6>();
        if (factory6 != null)
        {
            for (int adapterIndex = 0;
                factory6.EnumAdapterByGpuPreference(adapterIndex, GpuPreference.HighPerformance, out IDXGIAdapter1? adapter).Success;
                adapterIndex++)
            {
                if (adapter == null)
                {
                    continue;
                }

                AdapterDescription1 desc = adapter.Description1;

                if ((desc.Flags & AdapterFlags.Software) != AdapterFlags.None)
                {
                    // Don't select the Basic Render Driver adapter.
                    adapter.Dispose();
                    continue;
                }

                factory6.Dispose();
                return adapter;
            }

            factory6.Dispose();
        }

        for (int adapterIndex = 0;
            Factory.EnumAdapters1(adapterIndex, out IDXGIAdapter1? adapter).Success;
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

        throw new InvalidOperationException("Cannot detect D3D11 adapter");
    }

    public unsafe bool DrawFrame(Action<int, int> draw, [CallerMemberName] string? frameName = null)
    {
        var clearColor = new Color4(0.0f, 0.2f, 0.4f, 1.0f);
        DeviceContext.ClearRenderTargetView(RenderTargetView, clearColor);
        if (DepthStencilView != null)
        {
            DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
        }

        DeviceContext.OMSetRenderTargets(RenderTargetView, DepthStencilView);
        DeviceContext.RSSetViewport(new Viewport(Size.Width, Size.Height));
        DeviceContext.RSSetScissorRect(Size.Width, Size.Height);

        DeviceContext.IASetPrimitiveTopology(PrimitiveTopology.TriangleList);
        DeviceContext.VSSetShader(_vertexShader);
        DeviceContext.PSSetShader(_pixelShader);
        DeviceContext.IASetInputLayout(_inputLayout);
        DeviceContext.IASetVertexBuffer(0, _vertexBuffer, sizeof(VertexPositionColor));
        DeviceContext.Draw(3, 0);

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

    public ID3D11Texture2D CaptureTexture(ID3D11Texture2D source)
    {
        ID3D11Texture2D stagingTexture;
        Texture2DDescription desc = source.Description;

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
                throw new NotSupportedException($"Format {format} doesn't support multisample resolve");
            }

            for (int item = 0; item < desc.ArraySize; ++item)
            {
                for (int level = 0; level < desc.MipLevels; ++level)
                {
                    int index = CalculateSubResourceIndex(level, item, desc.MipLevels);
                    DeviceContext.ResolveSubresource(temp, index, source, index, format);
                }
            }

            desc.BindFlags = BindFlags.None;
            desc.MiscFlags &= ResourceOptionFlags.TextureCube;
            desc.CPUAccessFlags = CpuAccessFlags.Read;
            desc.Usage = ResourceUsage.Staging;

            stagingTexture = Device.CreateTexture2D(desc);

            DeviceContext.CopyResource(stagingTexture, temp);
        }
        else if ((desc.Usage == ResourceUsage.Staging) && ((desc.CPUAccessFlags & CpuAccessFlags.Read) != CpuAccessFlags.None))
        {
            // Handle case where the source is already a staging texture we can use directly
            stagingTexture = source;
        }
        else
        {
            // Otherwise, create a staging texture from the non-MSAA source
            desc.BindFlags = 0;
            desc.MiscFlags &= ResourceOptionFlags.TextureCube;
            desc.CPUAccessFlags = CpuAccessFlags.Read;
            desc.Usage = ResourceUsage.Staging;

            stagingTexture = Device.CreateTexture2D(desc);

            DeviceContext.CopyResource(stagingTexture, source);
        }

        return stagingTexture;
    }

    private static Span<byte> CompileBytecode(string shaderName, string entryPoint, string profile)
    {
        string assetsPath = Path.Combine(AppContext.BaseDirectory, "Assets");
        string shaderFile = Path.Combine(assetsPath, shaderName);
        //string shaderSource = File.ReadAllText(Path.Combine(assetsPath, shaderName));

        using Blob blob = Compiler.CompileFromFile(shaderFile, entryPoint, profile);
        return blob.AsSpan();
    }
}
