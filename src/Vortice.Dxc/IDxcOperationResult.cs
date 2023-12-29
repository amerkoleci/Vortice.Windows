// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public partial class IDxcOperationResult
{
    public IDxcBlobEncoding GetErrorBuffer()
    {
        GetErrorBuffer(out IDxcBlobEncoding result).CheckError();
        return result;
    }

    public IDxcBlob GetResult()
    {
        GetResult(out IDxcBlob result).CheckError();
        return result;
    }

    public Result GetStatus()
    {
        GetStatus(out Result result).CheckError();
        return result;
    }
}
