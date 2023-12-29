// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public partial class IDxcAssembler
{
    public IDxcOperationResult AssembleToContainer(IDxcBlob shader)
    {
        AssembleToContainer(shader, out IDxcOperationResult result).CheckError();
        return result;
    }
}
