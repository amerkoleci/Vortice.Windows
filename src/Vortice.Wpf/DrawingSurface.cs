// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.Direct3D11.Debug;
using Vortice.DXGI;
using static Vortice.Direct3D11.D3D11;

namespace Vortice.Wpf;

public class DrawingSurface : Image
{
    /// <summary>
    /// Occurs when the control has initialized the GraphicsDevice.
    /// </summary>
    public event EventHandler<DrawingSurfaceEventArgs>? LoadContent;

    /// <summary>
    /// Occurs when the DrawingSurface has been invalidated.
    /// </summary>
    public event EventHandler<DrawEventArgs>? Draw;

    /// <summary>
    /// Occurs when the control is unloading the GraphicsDevice.
    /// </summary>
    public event EventHandler<DrawingSurfaceEventArgs>? UnloadContent;

    private ID3D11Device1? _device;
    private ID3D11DeviceContext1? _deviceContext;
    private D3D11ImageSource? _d3dSurface;

    private bool _isRendering;
    private bool _contentNeedsRefresh;

    /// <summary>
    /// Gets or sets a value indicating whether this control will redraw every time the CompositionTarget.Rendering event is fired.
    /// Defaults to false.
    /// </summary>
    public bool AlwaysRefresh { get; set; }

    public int TextureWidth { get; private set; }
    public int TextureHeight { get; private set; }

    public Format ColorFormat { get; set; } = Format.B8G8R8A8_UNorm;
    public ID3D11Texture2D? ColorTexture { get; private set; }
    public ID3D11RenderTargetView? ColorTextureView { get; private set; }

    public Format DepthStencilFormat { get; set; } = Format.D32_Float;
    public ID3D11Texture2D? DepthStencilTexture { get; private set; }
    public ID3D11DepthStencilView? DepthStencilView { get; private set; }


    static DrawingSurface()
    {
        StretchProperty.OverrideMetadata(typeof(DrawingSurface), new FrameworkPropertyMetadata(Stretch.Fill));
    }

    public DrawingSurface()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (IsInDesignMode)
            return;

        StartD3D();
        StartRendering();
    }

    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        if (IsInDesignMode)
            return;

        StopRendering();
        EndD3D();
    }

    private void StartD3D()
    {
        {
            DeviceCreationFlags creationFlags = DeviceCreationFlags.BgraSupport;
#if DEBUG
            if (SdkLayersAvailable())
            {
                creationFlags |= DeviceCreationFlags.Debug;
            }
#endif
            D3D11CreateDevice(IntPtr.Zero,
                DriverType.Hardware,
                creationFlags,
                FeatureLevel.Level_11_0,
                out ID3D11Device? tempDevice, out ID3D11DeviceContext? tempContext).CheckError();

            _device = tempDevice!.QueryInterface<ID3D11Device1>();
            _deviceContext = tempContext!.QueryInterface<ID3D11DeviceContext1>();
            tempContext.Dispose();
            tempDevice.Dispose();
        }

        Window window = Window.GetWindow(this);

        window.Closed -= Window_Closed;
        window.Closed += Window_Closed;
        _d3dSurface = new D3D11ImageSource(window);
        _d3dSurface.IsFrontBufferAvailableChanged += OnIsFrontBufferAvailableChanged;

        CreateAndBindTargets();

        Source = _d3dSurface;

        RaiseLoadContent(new DrawingSurfaceEventArgs(_device, _deviceContext));

        _contentNeedsRefresh = true;
        _isRendering = true;
    }

    private void Window_Closed(object? sender, EventArgs e)
    {
        Window window = (Window)sender!;
        window.Closed -= Window_Closed;

        if (IsInDesignMode)
            return;

        StopRendering();
        EndD3D();
    }

    private void EndD3D()
    {
        _isRendering = false;

        RaiseUnloadContent(new DrawingSurfaceEventArgs(_device!, _deviceContext!));

        if (_d3dSurface != null)
        {
            _d3dSurface.IsFrontBufferAvailableChanged -= OnIsFrontBufferAvailableChanged;
        }

        Source = null;

        if (_d3dSurface != null)
        {
            _d3dSurface.Dispose();
            _d3dSurface = default;
        }

        DisposeTextures();

        _deviceContext!.ClearState();
        _deviceContext!.Flush();
        _deviceContext!.Dispose();

        if (_device != null)
        {
#if DEBUG
            uint refCount = _device.Release();
            if (refCount > 0)
            {
                System.Diagnostics.Debug.WriteLine($"Direct3D11: There are {refCount} unreleased references left on the device");

                ID3D11Debug? d3d11Debug = _device.QueryInterfaceOrNull<ID3D11Debug>();
                if (d3d11Debug != null)
                {
                    d3d11Debug.ReportLiveDeviceObjects(ReportLiveDeviceObjectFlags.Detail | ReportLiveDeviceObjectFlags.IgnoreInternal);
                    d3d11Debug.Dispose();
                }
            }
#else
            _device.Dispose();
#endif

            _device = default;
        }
    }

    private void CreateAndBindTargets()
    {
        if (_device == null)
            return;

        _d3dSurface!.SetRenderTargetDX10(null);
        DisposeTextures();

        TextureWidth = Math.Max((int)ActualWidth, 100);
        TextureHeight = Math.Max((int)ActualHeight, 100);

        ColorTexture = _device.CreateTexture2D(new Texture2DDescription
        {
            BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
            Format = Format.B8G8R8A8_UNorm,
            Width = (uint)TextureWidth,
            Height = (uint)TextureHeight,
            MipLevels = 1,
            SampleDescription = new SampleDescription(1, 0),
            Usage = ResourceUsage.Default,
            MiscFlags = ResourceOptionFlags.Shared,
            CPUAccessFlags = CpuAccessFlags.None,
            ArraySize = 1
        });
        ColorTextureView = _device.CreateRenderTargetView(ColorTexture);

        if (DepthStencilFormat != Format.Unknown)
        {
            DepthStencilTexture = _device.CreateTexture2D(DepthStencilFormat, (uint)TextureWidth, (uint)TextureHeight, 1, 1, null, BindFlags.DepthStencil);
            DepthStencilView = _device.CreateDepthStencilView(DepthStencilTexture!, new DepthStencilViewDescription(DepthStencilTexture, DepthStencilViewDimension.Texture2D));
        }

        _d3dSurface.SetRenderTargetDX10(ColorTexture);
    }

    private void DisposeTextures()
    {
        if (DepthStencilView != null)
        {
            DepthStencilView.Dispose();
            DepthStencilView = default;
        }

        if (DepthStencilTexture != null)
        {
            DepthStencilTexture.Dispose();
            DepthStencilTexture = default;
        }

        if (ColorTextureView != null)
        {
            ColorTextureView.Dispose();
            ColorTextureView = default;
        }

        if (ColorTexture != null)
        {
            ColorTexture.Dispose();
            ColorTexture = default;
        }
    }

    private void StartRendering()
    {
        CompositionTarget.Rendering += OnRendering;
    }

    private void StopRendering()
    {
        CompositionTarget.Rendering -= OnRendering;
    }

    private void OnRendering(object? sender, EventArgs e)
    {
        if (!_isRendering)
            return;

        if (_contentNeedsRefresh || AlwaysRefresh)
        {
            Render();
            _d3dSurface!.InvalidateD3DImage();
        }
    }

    private void Render()
    {
        if (_device == null || _deviceContext == null || ColorTexture == null)
            return;

        _deviceContext.OMSetRenderTargets(ColorTextureView!, DepthStencilView);
        _deviceContext.RSSetViewport(0, 0, TextureWidth, TextureHeight);
        _deviceContext.RSSetScissorRect(0, 0, TextureWidth, TextureHeight);

        RaiseDraw(new DrawEventArgs(this, _device!, _deviceContext!));

        _device.ImmediateContext.Flush();
    }

    protected virtual void RaiseLoadContent(DrawingSurfaceEventArgs args)
    {
        LoadContent?.Invoke(this, args);
    }

    protected virtual void RaiseDraw(DrawEventArgs args)
    {
        Draw?.Invoke(this, args);
    }

    protected virtual void RaiseUnloadContent(DrawingSurfaceEventArgs args)
    {
        UnloadContent?.Invoke(this, args);
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        CreateAndBindTargets();
        _contentNeedsRefresh = true;

        base.OnRenderSizeChanged(sizeInfo);
    }

    private void OnIsFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        // this fires when the screensaver kicks in, the machine goes into sleep or hibernate
        // and any other catastrophic losses of the d3d device from WPF's point of view
        if (_d3dSurface.IsFrontBufferAvailable)
        {
            CreateAndBindTargets();
            _contentNeedsRefresh = true;
            StartRendering();
        }
        else
        {
            StopRendering();
        }
    }

    /// <summary>
    /// Gets a value indicating whether the control is in design mode
    /// (running in Blend or Visual Studio).
    /// </summary>
    public static bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());

    public void Invalidate()
    {
        _contentNeedsRefresh = true;
    }
}
