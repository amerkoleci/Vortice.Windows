// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.Versioning;
using Vortice.DXGI;

namespace Vortice.Direct3D11;

public static partial class D3D11
{
    /// <summary>
    /// Creates an instance of IDirect3DDevice from an <see cref="IDXGIDevice"/>.
    /// </summary>
    /// <typeparam name="T">Type of IDirect3DDevice.</typeparam>
    /// <param name="dxgiDevice"></param>
    /// <returns></returns>
#if NET6_0_OR_GREATER
    [SupportedOSPlatform("windows")]
#endif
    public static T CreateDirect3D11DeviceFromDXGIDevice<T>(IDXGIDevice dxgiDevice)
    {
        CreateDirect3D11DeviceFromDXGIDevice(dxgiDevice, out IntPtr graphicsDevicePtr).CheckError();
        T graphicsDevice = (T)Marshal.GetObjectForIUnknown(graphicsDevicePtr);
        Marshal.Release(graphicsDevicePtr);
        return graphicsDevice;
    }

#if NET6_0_OR_GREATER
    [SupportedOSPlatform("windows")]
#endif
    public static Result CreateDirect3D11DeviceFromDXGIDevice<T>(IDXGIDevice dxgiDevice, out T? graphicsDevice)
    {
        Result result = CreateDirect3D11DeviceFromDXGIDevice(dxgiDevice, out IntPtr graphicsDevicePtr);
        if (result.Failure)
        {
            graphicsDevice = default;
            return result;
        }

        graphicsDevice = (T)Marshal.GetObjectForIUnknown(graphicsDevicePtr);
        Marshal.Release(graphicsDevicePtr);
        return result;
    }

    /// <summary>
    /// Creates an instance of IDirect3DSurface from an <see cref="IDXGISurface"/>.
    /// </summary>
    /// <typeparam name="T">Type of IDirect3DSurface class.</typeparam>
    /// <param name="dgxiSurface"></param>
    /// <returns></returns>
#if NET6_0_OR_GREATER
    [SupportedOSPlatform("windows")]
#endif
    public static T CreateDirect3D11SurfaceFromDXGISurface<T>(IDXGISurface dgxiSurface)
    {
        CreateDirect3D11SurfaceFromDXGISurface(dgxiSurface, out IntPtr graphicsSurfacePtr).CheckError();
        T graphicsSurface = (T)Marshal.GetObjectForIUnknown(graphicsSurfacePtr);
        Marshal.Release(graphicsSurfacePtr);
        return graphicsSurface;
    }

#if NET6_0_OR_GREATER
    [SupportedOSPlatform("windows")]
#endif
    public static Result CreateDirect3D11SurfaceFromDXGISurface<T>(IDXGISurface dgxiSurface, out T? graphicsSurface)
    {
        Result result = CreateDirect3D11SurfaceFromDXGISurface(dgxiSurface, out IntPtr graphicsSurfacePtr);
        if (result.Failure)
        {
            graphicsSurface = default;
            return result;
        }

        graphicsSurface = (T)Marshal.GetObjectForIUnknown(graphicsSurfacePtr);
        Marshal.Release(graphicsSurfacePtr);
        return result;
    }

    public static IDXGIDevice GetDXGIDevice(object direct3DDevice)
    {
        IDirect3DDxgiInterfaceAccess dxgiSurfaceInterfaceAccess = (IDirect3DDxgiInterfaceAccess)direct3DDevice;
        return dxgiSurfaceInterfaceAccess.GetInterface<IDXGIDevice>();
    }

    public static IDXGISurface GetDXGISurface(object direct3DSurface)
    {
        IDirect3DDxgiInterfaceAccess dxgiSurfaceInterfaceAccess = (IDirect3DDxgiInterfaceAccess)direct3DSurface;
        return dxgiSurfaceInterfaceAccess.GetInterface<IDXGISurface>();
    }
}
