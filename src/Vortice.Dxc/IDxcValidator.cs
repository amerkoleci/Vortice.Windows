// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public partial class IDxcValidator
{
    public IDxcOperationResult Validate(IDxcBlob shader, DxcValidatorFlags flags)
    {
        Validate(shader, flags, out IDxcOperationResult result).CheckError();
        return result;
    }
}
