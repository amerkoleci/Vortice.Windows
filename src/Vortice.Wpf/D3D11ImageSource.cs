// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Windows;
using System.Windows.Interop;
using Vortice.Direct3D11;
using Vortice.Direct3D9;

namespace Vortice.Wpf;

public class D3D11ImageSource : D3DImage, IDisposable
{
    private IDirect3DTexture9? _renderTarget;

    public D3D11ImageSource(Window parentWindow)
    {
        D3D9DeviceService.Start(parentWindow);
    }

    public void Dispose()
    {
        SetRenderTargetDX10(null);
        if(_renderTarget != null)
        {
            _renderTarget.Dispose();
            _renderTarget = default;
        }
        D3D9DeviceService.End();
        GC.SuppressFinalize(this);
    }

    internal void InvalidateD3DImage()
    {
        if (_renderTarget == null)
            return;

        Lock();
        AddDirtyRect(new Int32Rect(0, 0, PixelWidth, PixelHeight));
        Unlock();
    }

    internal void SetRenderTargetDX10(ID3D11Texture2D? renderTarget)
    {
        if (_renderTarget != null)
        {
            _renderTarget = null;

            Lock();
            SetBackBuffer(D3DResourceType.IDirect3DSurface9, IntPtr.Zero);
            Unlock();
        }

        if (renderTarget == null)
            return;

        if (!renderTarget.IsShareable())
            throw new ArgumentException("Texture must be created with ResourceOptionFlags.Shared");

        var format = renderTarget.GetTranslatedFormat();
        if (format == Format.Unknown)
            throw new ArgumentException("Texture format is not compatible with OpenSharedResource");

        var handle = renderTarget.GetSharedHandle();
        if (handle == IntPtr.Zero)
            throw new ArgumentException("Handle could not be retrieved");

        _renderTarget = D3D9DeviceService.D3DDevice.CreateTexture(
            renderTarget.Description.Width,
            renderTarget.Description.Height,
            1, Usage.RenderTarget, format,
            Pool.Default, ref handle);

        using (IDirect3DSurface9 surface = _renderTarget.GetSurfaceLevel(0))
        {
            Lock();
            SetBackBuffer(D3DResourceType.IDirect3DSurface9, surface.NativePointer);
            Unlock();
        }
    }
}
