// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public partial class IDxcPdbUtils
{
    public IDxcResult CompileForFullPDB()
    {
        CompileForFullPDB(out IDxcResult result).CheckError();
        return result;
    }
}
