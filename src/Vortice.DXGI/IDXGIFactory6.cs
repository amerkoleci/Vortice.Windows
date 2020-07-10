// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory6
    {
        public Result EnumAdapterByGpuPreference<T>(int index, GpuPreference gpuPreference, out T adapter) where T : IDXGIAdapter
        {
            Result result = EnumAdapterByGpuPreference(index, gpuPreference, typeof(T).GUID, out IntPtr adapterPtr);
            if (result.Success)
            {
                adapter = FromPointer<T>(adapterPtr);
                return result;
            }

            adapter = null;
            return result;
        }

        public T EnumAdapterByGpuPreference<T>(int index, GpuPreference gpuPreference) where T : IDXGIAdapter
        {
            Result result = EnumAdapterByGpuPreference(index, gpuPreference, typeof(T).GUID, out IntPtr adapterPtr);
            if (result.Failure)
            {
                return default;
            }

            return FromPointer<T>(adapterPtr);
        }
    }
}
