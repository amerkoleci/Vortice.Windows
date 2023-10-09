// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D11;
using Vortice.Direct3D12;

namespace Vortice.Direct3D11on12;

public partial class ID3D11On12Device
{
    public T CreateWrappedResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IUnknown d3d12Resource, ResourceFlags flags, ResourceStates inState, ResourceStates outState) where T : ID3D11Resource
    {
        CreateWrappedResource(d3d12Resource, flags, inState, outState, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateWrappedResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IUnknown d3d12Resource, ResourceFlags flags, ResourceStates inState, ResourceStates outState, out T? resource11) where T : ID3D11Resource
    {
        Result result = CreateWrappedResource(d3d12Resource, flags, inState, outState, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Success)
        {
            resource11 = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        resource11 = null;
        return result;
    }

    public void AcquireWrappedResources(ID3D11Resource[] resources)
    {
        AcquireWrappedResources(resources, resources.Length);
    }

    public void ReleaseWrappedResources(ID3D11Resource[] resources)
    {
        ReleaseWrappedResources(resources, resources.Length);
    }
}
