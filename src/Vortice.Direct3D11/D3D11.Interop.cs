// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;
using Vortice.DXGI;

namespace Vortice.Direct3D11;

public static unsafe partial class D3D11
{
    public static IInspectable CreateDirect3D11DeviceFromDXGIDevice(IDXGIDevice dxgiDevice)
    {
        CreateDirect3D11DeviceFromDXGIDevice(dxgiDevice, out IInspectable graphicsDevice).CheckError();
        return graphicsDevice;
    }

    public static IInspectable CreateDirect3D11SurfaceFromDXGISurface(IDXGISurface dgxiSurface)
    {
        CreateDirect3D11SurfaceFromDXGISurface(dgxiSurface, out IInspectable graphicsSurface).CheckError();
        return graphicsSurface;
    }

    public static T GetDXGIInterfaceFromObject<T>(ComObject @object) where T: ComObject
    {
        IDirect3DDxgiInterfaceAccess dxgiInterfaceAccess = @object.QueryInterface<IDirect3DDxgiInterfaceAccess>(); 
        return dxgiInterfaceAccess.GetInterface<T>();
    }

    //public static IDirect3DDevice CreateDirect3DDevice(IDXGIDevice dxgiDevice)
    //{
    //    CreateDirect3D11DeviceFromDXGIDevice(dxgiDevice, out IInspectable graphicsDevice).CheckError();
    //    return graphicsDevice;
    //}
}
