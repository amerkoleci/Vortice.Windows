﻿// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class ID3D12Device7
{
    public T AddToStateObject<T>(StateObjectDescription addition, ID3D12StateObject stateObjectToGrowFrom) where T : ID3D12StateObject
    {
        AddToStateObject(addition, stateObjectToGrowFrom, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    public Result AddToStateObject<T>(StateObjectDescription addition, ID3D12StateObject stateObjectToGrowFrom, out T? newStateObject) where T : ID3D12StateObject
    {
        Result result = AddToStateObject(addition, stateObjectToGrowFrom, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            newStateObject = default;
            return result;
        }

        newStateObject = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public T CreateProtectedResourceSession1<T>(ProtectedResourceSessionDescription1 description) where T : ID3D12ProtectedResourceSession
    {
        CreateProtectedResourceSession1(ref description, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    public Result CreateProtectedResourceSession1<T>(ProtectedResourceSessionDescription1 description, out T? session) where T : ID3D12ProtectedResourceSession
    {
        Result result = CreateProtectedResourceSession1(ref description, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            session = default;
            return result;
        }

        session = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
