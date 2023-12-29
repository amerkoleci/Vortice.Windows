// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D9;

namespace Vortice.Direct3D9on12;

public unsafe partial class Apis
{
    public const uint MAX_D3D9ON12_QUEUES = 2u;
    public const uint MaxD3D9On12Queues = MAX_D3D9ON12_QUEUES;

    public static IDirect3D9 Direct3DCreate9On12(D3D9On12Arguments[] overrides)
    {
        D3D9On12Arguments.__Native* overrideList = stackalloc D3D9On12Arguments.__Native[overrides.Length];
        for (int i = 0; i < overrides.Length; i++)
        {
            overrides[i].__MarshalTo(ref overrideList[i]);
        }

        return Direct3DCreate9On12(Direct3D9.D3D9.SdkVersion, overrideList, overrides.Length);
    }

    public static IDirect3D9Ex Direct3DCreate9On12Ex(D3D9On12Arguments[] overrides)
    {
        D3D9On12Arguments.__Native* overrideList = stackalloc D3D9On12Arguments.__Native[overrides.Length];
        for (int i = 0; i < overrides.Length; i++)
        {
            overrides[i].__MarshalTo(ref overrideList[i]);
        }

        Direct3DCreate9On12Ex(Direct3D9.D3D9.SdkVersion, overrideList, overrides.Length, out IDirect3D9Ex outputInterface).CheckError();
        return outputInterface;
    }

    public static Result Direct3DCreate9On12Ex(D3D9On12Arguments[] overrides, out IDirect3D9Ex? outputInterface)
    {
        D3D9On12Arguments.__Native* overrideList = stackalloc D3D9On12Arguments.__Native[overrides.Length];
        for (int i = 0; i < overrides.Length; i++)
        {
            overrides[i].__MarshalTo(ref overrideList[i]);
        }

        return Direct3DCreate9On12Ex(Direct3D9.D3D9.SdkVersion, overrideList, overrides.Length, out outputInterface);
    }
}
