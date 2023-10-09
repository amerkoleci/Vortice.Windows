// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D9;

namespace Vortice.Direct3D9on12;

public partial class IDirect3DDevice9On12
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

    public T UnwrapUnderlyingResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IDirect3DResource9 resource, ComObject commandQueue) where T : ComObject
    {
        UnwrapUnderlyingResource(resource, commandQueue, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result UnwrapUnderlyingResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IDirect3DResource9 resource, ComObject commandQueue, out T? resource12) where T : ComObject
    {
        Result result = UnwrapUnderlyingResource(resource, commandQueue, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Success)
        {
            resource12 = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        resource12 = null;
        return result;
    }

    public unsafe Result ReturnUnderlyingResource(IDirect3DResource9 resource, ulong[] signalValues, ComObject[] fences)
    {
        if (signalValues.Length != fences.Length)
        {
            throw new ArgumentException($"{nameof(signalValues)} and {nameof(fences)} length must be the same");
        }

        IntPtr* ppFences = stackalloc IntPtr[fences.Length];
        for (int i = 0; i < fences.Length; i++)
        {
            ppFences[i] = (fences[i] == null) ? IntPtr.Zero : fences[i].NativePointer;
        }

        fixed (ulong* pSignalValues = signalValues)
        {
            return ReturnUnderlyingResource(resource, signalValues.Length, pSignalValues, ppFences);
        }
    }

    public unsafe Result ReturnUnderlyingResource(IDirect3DResource9 resource, ReadOnlySpan<ulong> signalValues, ReadOnlySpan<ComObject> fences)
    {
        if (signalValues.Length != fences.Length)
        {
            throw new ArgumentException($"{nameof(signalValues)} and {nameof(fences)} length must be the same");
        }

        IntPtr* ppFences = stackalloc IntPtr[fences.Length];
        for (int i = 0; i < fences.Length; i++)
        {
            ppFences[i] = (fences[i] == null) ? IntPtr.Zero : fences[i].NativePointer;
        }

        fixed (ulong* pSignalValues = signalValues)
        {
            return ReturnUnderlyingResource(resource, signalValues.Length, pSignalValues, ppFences);
        }
    }
}
