// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIFactory6
{
    public Result EnumAdapterByGpuPreference<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(uint index, GpuPreference gpuPreference, out T? adapter)
        where T : IDXGIAdapter
    {
        Result result = EnumAdapterByGpuPreference(index, gpuPreference, typeof(T).GUID, out IntPtr adapterPtr);
        if (result.Success)
        {
            adapter = MarshallingHelpers.FromPointer<T>(adapterPtr);
            return result;
        }

        adapter = null;
        return result;
    }

    public T EnumAdapterByGpuPreference<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(uint index, GpuPreference gpuPreference)
        where T : IDXGIAdapter
    {
        EnumAdapterByGpuPreference(index, gpuPreference, typeof(T).GUID, out IntPtr adapterPtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(adapterPtr)!;
    }

    /// <summary>
    /// Gets the number of available adapters by given <see cref="GpuPreference"/> from this factory.
    /// </summary>
    /// <param name="gpuPreference">The <see cref="GpuPreference"/></param>
    /// <returns>The number of adapters</returns>
    public int GetAdapterByGpuPreference(GpuPreference gpuPreference)
    {
        int count = 0;

        for (uint adapterIndex = 0; EnumAdapterByGpuPreference(adapterIndex, gpuPreference, out IDXGIAdapter1? adapter).Success; adapterIndex++)
        {
            count++;
            adapter!.Dispose();
        }

        return count;
    }
}
