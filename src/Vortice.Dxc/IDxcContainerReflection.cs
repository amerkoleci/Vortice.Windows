// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public partial class IDxcContainerReflection
{
    public int FindFirstPartKind(int kind)
    {
        FindFirstPartKind(kind, out int result).CheckError();
        return result;
    }

    public int GetPartKind(int index)
    {
        GetPartKind(index, out int result).CheckError();
        return result;
    }

    public IDxcBlob GetPartContent(int index)
    {
        GetPartContent(index, out IDxcBlob result).CheckError();
        return result;
    }

    public int GetPartCount()
    {
        GetPartCount(out int result).CheckError();
        return result;
    }

    public T? GetPartReflection<T>(int index) where T : ComObject
    {
        Result result = GetPartReflection(index, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            return default;
        }

        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    public Result GetPartReflection<T>(int index, out T? @object) where T : ComObject
    {
        Result result = GetPartReflection(index, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            @object = default;
            return result;
        }

        @object = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
