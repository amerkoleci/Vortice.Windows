// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11on12;

public partial class ID3D11On12Device1
{
    public Result GetD3D12Device<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(out T? device) where T : ComObject
    {
        Result result = GetD3D12Device(typeof(T).GUID, out IntPtr devicePtr);
        if (result.Failure)
        {
            device = default;
            return result;
        }

        device = MarshallingHelpers.FromPointer<T>(devicePtr);
        return result;
    }

    public T GetD3D12Device<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>() where T : ComObject
    {
        GetD3D12Device(typeof(T).GUID, out IntPtr devicePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(devicePtr)!;
    }
}
