// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

public partial class D3D9
{
    public static IDirect3D9 Direct3DCreate9() => Direct3DCreate9(SdkVersion);

    public static Result Direct3DCreate9Ex(out IDirect3D9Ex result) => Direct3DCreate9Ex(SdkVersion, out result);

    public static IDirect3D9Ex Direct3DCreate9Ex()
    {
        Direct3DCreate9Ex(SdkVersion, out IDirect3D9Ex result).CheckError();
        return result;
    }
}
