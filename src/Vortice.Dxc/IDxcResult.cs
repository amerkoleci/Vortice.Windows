// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public partial class IDxcResult
{
    public string GetErrors()
    {
        using (IDxcBlobUtf8? errors = GetOutput<IDxcBlobUtf8>(DxcOutKind.Errors))
        {
            return errors!.StringPointer;
        }
    }

    public ReadOnlyMemory<byte> GetObjectBytecodeMemory()
    {
        using (IDxcBlob? blob = GetOutput(DxcOutKind.Object))
        {
            return blob!.AsMemory();
        }
    }

    public ReadOnlySpan<byte> GetObjectBytecode()
    {
        using (IDxcBlob? blob = GetOutput(DxcOutKind.Object))
        {
            return blob!.AsSpan();
        }
    }

    public ReadOnlySpan<byte> GetObjectBytecode(out IDxcBlobWide? outputName)
    {
        using (IDxcBlob? blob = GetOutput<IDxcBlob>(DxcOutKind.Object, out outputName))
        {
            return blob!.AsSpan();
        }
    }

    public byte[] GetObjectBytecodeArray()
    {
        using (IDxcBlob? blob = GetOutput(DxcOutKind.Object))
        {
            return blob!.AsBytes();
        }
    }

    public byte[] GetObjectBytecodeArray(out IDxcBlobWide? outputName)
    {
        using (IDxcBlob? blob = GetOutput<IDxcBlob>(DxcOutKind.Object, out outputName))
        {
            return blob!.AsBytes();
        }
    }

    public IDxcBlob GetOutput(DxcOutKind kind)
    {
        GetOutput(kind, typeof(IDxcBlob).GUID, out IntPtr nativePtr, out _).CheckError();
        return new IDxcBlob(nativePtr);
    }


    public T GetOutput<T>(DxcOutKind kind) where T : ComObject
    {
        GetOutput(kind, typeof(T).GUID, out IntPtr nativePtr, out _).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public unsafe T GetOutput<T>(DxcOutKind kind, out IDxcBlobWide? outputName) where T : ComObject
    {
        GetOutput(kind, typeof(T).GUID, out IntPtr nativePtr, out outputName).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result GetOutput<T>(DxcOutKind kind, out T? @object) where T : ComObject
    {
        Result result = GetOutput(kind, typeof(T).GUID, out IntPtr nativePtr, out _);
        if (result.Failure)
        {
            @object = default;
            return result;
        }

        @object = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public unsafe Result GetOutput<T>(DxcOutKind kind, out T? @object, out IDxcBlobWide? outputName) where T : ComObject
    {
        Result result = GetOutput(kind, typeof(T).GUID, out IntPtr nativePtr, out outputName);
        if (result.Failure)
        {
            @object = default;
            outputName = default;
            return result;
        }

        @object = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
