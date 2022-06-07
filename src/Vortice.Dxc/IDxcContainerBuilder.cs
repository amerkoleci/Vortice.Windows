// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public partial class IDxcContainerBuilder
{
    public IDxcOperationResult SerializeContainer(IDxcBlob source)
    {
        SerializeContainer(out IDxcOperationResult result).CheckError();
        return result;
    }
}
