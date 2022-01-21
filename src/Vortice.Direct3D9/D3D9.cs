// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

public partial class D3D9
{
    public static IDirect3D9 Create9() => Create9(SdkVersion);

    public static Result Create9Ex(out IDirect3D9Ex result) => Create9Ex(SdkVersion, out result);
}
