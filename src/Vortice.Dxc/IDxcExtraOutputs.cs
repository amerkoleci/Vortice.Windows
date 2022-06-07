// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;

namespace Vortice.Dxc;

public partial class IDxcExtraOutputs
{
    public T GetOutput<T>(int index, out IDxcBlobUtf16 outputType, out IDxcBlobUtf16 outputName) where T : IDxcBlob
    {
        GetOutput(index, typeof(T).GUID, out IntPtr nativePtr, out outputType, out outputName).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result GetOutput<T>(int index, out T? @object, out IDxcBlobUtf16 outputType, out IDxcBlobUtf16 outputName) where T : IDxcBlob
    {
        Result result = GetOutput(index, typeof(T).GUID, out IntPtr nativePtr, out outputType, out outputName);
        if (result.Failure)
        {
            @object = default;
            return result;
        }

        @object = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
