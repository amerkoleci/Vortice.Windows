// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXCore;

public partial class IDXCoreAdapterFactory
{
    public Result CreateAdapterList<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Guid[] filterAttributes, out T? adapterList) where T : IDXCoreAdapterList
    {
        return CreateAdapterList(filterAttributes.Length, filterAttributes, out adapterList);
    }

    public Result CreateAdapterList<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int numAttributes, Guid[] filterAttributes, out T? adapterList) where T : IDXCoreAdapterList
    {
        Result result = CreateAdapterList(numAttributes, filterAttributes, typeof(T).GUID, out IntPtr adapterListPtr);
        if (result.Failure)
        {
            adapterList = default;
            return result;
        }

        adapterList = MarshallingHelpers.FromPointer<T>(adapterListPtr);
        return result;
    }

    public T CreateAdapterList<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Guid[] filterAttributes) where T : IDXCoreAdapterList
    {
        return CreateAdapterList<T>(filterAttributes.Length, filterAttributes);
    }

    public T CreateAdapterList<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int numAttributes, Guid[] filterAttributes) where T : IDXCoreAdapterList
    {
        CreateAdapterList(numAttributes, filterAttributes, typeof(T).GUID, out IntPtr adapterListPtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(adapterListPtr)!;
    }

    public Result GetAdapterByLuid<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Luid adapterLUID, out T? adapter) where T : IDXCoreAdapter
    {
        Result result = GetAdapterByLuid(adapterLUID, typeof(T).GUID, out IntPtr adapterPtr);
        if (result.Failure)
        {
            adapter = default;
            return result;
        }

        adapter = MarshallingHelpers.FromPointer<T>(adapterPtr);
        return result;
    }

    public T GetAdapterByLuid<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Luid adapterLUID) where T : IDXCoreAdapter
    {
        GetAdapterByLuid(adapterLUID, typeof(T).GUID, out IntPtr adapterPtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(adapterPtr)!;
    }
}
