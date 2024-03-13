// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D11;
using Vortice.DXGI;
using DxgiFormat = Vortice.DXGI.Format;
using Format = Vortice.Direct3D9.Format;

namespace Vortice.Wpf;

internal static class D3D9Extensions
{
    public static bool IsShareable(this ID3D11Texture2D texture)
    {
        return texture.Description.MiscFlags.HasFlag(ResourceOptionFlags.Shared);
    }

    public static Format GetTranslatedFormat(this ID3D11Texture2D texture)
    {
        return texture.Description.Format switch
        {
            DxgiFormat.R10G10B10A2_UNorm => Format.A2B10G10R10,
            DxgiFormat.R16G16B16A16_Float => Format.A16B16G16R16F,
            DxgiFormat.B8G8R8A8_UNorm => Format.A8R8G8B8,
            _ => Format.Unknown,
        };
    }

    public static nint GetSharedHandle(this ID3D11Texture2D texture)
    {
        IDXGIResource resource = texture.QueryInterface<IDXGIResource>();
        nint result = resource.SharedHandle;
        resource.Dispose();
        return result;
    }
}
