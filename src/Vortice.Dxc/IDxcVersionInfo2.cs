// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public partial class IDxcVersionInfo
{
    public int GetFlags()
    {
        GetFlags(out int result).CheckError();
        return result;
    }
}
