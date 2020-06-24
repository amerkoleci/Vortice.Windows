// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory6
    {
        public Result EnumAdaptersByGpuPreference<T>(int index, GpuPreference gpuPreference, out T adapter) where T : IDXGIAdapter
        {
            var result = EnumAdapterByGpuPreference(index, gpuPreference, typeof(T).GUID, out IntPtr adapterPtr);
            if (result.Success)
            {
                adapter = FromPointer<T>(adapterPtr);
                return result;
            }

            adapter = null;
            return result;
        }

        public T[] EnumAdaptersByGpuPreference<T>(GpuPreference gpuPreference) where T : IDXGIAdapter
        {
            var adapters = new List<T>();
            var type = typeof(T);
            for (int index = 0; EnumAdapterByGpuPreference(index, gpuPreference, type.GUID, out IntPtr adapterPtr) != ResultCode.NotFound; ++index)
            {
                adapters.Add(FromPointer<T>(adapterPtr));
            }

            return adapters.ToArray();
        }
    }
}
