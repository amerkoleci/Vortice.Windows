// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public partial class IDxcContainerReflection
{
    public uint FindFirstPartKind(uint kind)
    {
        FindFirstPartKind(kind, out uint result).CheckError();
        return result;
    }

    public uint GetPartKind(uint index)
    {
        GetPartKind(index, out uint result).CheckError();
        return result;
    }

    public IDxcBlob GetPartContent(uint index)
    {
        GetPartContent(index, out IDxcBlob result).CheckError();
        return result;
    }

    public uint GetPartCount()
    {
        GetPartCount(out uint result).CheckError();
        return result;
    }

    public T? GetPartReflection<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(uint index)
        where T : ComObject
    {
        Result result = GetPartReflection(index, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            return default;
        }

        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    public Result GetPartReflection<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(uint index, out T? @object)
        where T : ComObject
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
