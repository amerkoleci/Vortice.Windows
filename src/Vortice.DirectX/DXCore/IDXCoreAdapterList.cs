// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXCore;

public partial class IDXCoreAdapterList
{
    public T GetAdapter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(uint index)
        where T : IDXCoreAdapter
    {
        GetAdapter(index, typeof(T).GUID, out IntPtr adapterPtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(adapterPtr)!;
    }

    public Result GetAdapter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(uint index, out T? adapter)
        where T : IDXCoreAdapter
    {
        Result result = GetAdapter(index, typeof(T).GUID, out IntPtr adapterPtr);
        if (result.Failure)
        {
            adapter = default;
            return result;
        }

        adapter = MarshallingHelpers.FromPointer<T>(adapterPtr);
        return result;
    }

    public T GetFactory<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>() where T : IDXCoreAdapterFactory
    {
        GetFactory(typeof(T).GUID, out IntPtr factoryPtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(factoryPtr)!;
    }

    public Result GetFactory<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(out T? factory) where T : IDXCoreAdapterFactory
    {
        Result result = GetFactory(typeof(T).GUID, out IntPtr factoryPtr);
        if (result.Failure)
        {
            factory = default;
            return result;
        }

        factory = MarshallingHelpers.FromPointer<T>(factoryPtr);
        return result;
    }

    public Result Sort(AdapterPreference[] preferences) => Sort((uint)preferences.Length, preferences);
}
