// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D11;

namespace Vortice.Direct3D11on12;

public partial class ID3D11On12Device2
{
    public T UnwrapUnderlyingResource<T>(ID3D11Resource resource, ComObject commandQueue) where T : ComObject
    {
        UnwrapUnderlyingResource(resource, commandQueue, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result UnwrapUnderlyingResource<T>(ID3D11Resource resource, ComObject commandQueue, out T? resource12) where T : ComObject
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

    public unsafe Result ReturnUnderlyingResource(ID3D11Resource resource11, ulong[] signalValues, ComObject[] fences)
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
            return ReturnUnderlyingResource(resource11, signalValues.Length, pSignalValues, ppFences);
        }
    }

    public unsafe Result ReturnUnderlyingResource(ID3D11Resource resource11, ReadOnlySpan<ulong> signalValues, ReadOnlySpan<ComObject> fences)
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
            return ReturnUnderlyingResource(resource11, signalValues.Length, pSignalValues, ppFences);
        }
    }
}
