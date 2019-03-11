// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory6
    {
        public T[] EnumAdaptersByGpuPreference<T>(GpuPreference gpuPreference) where T : IDXGIAdapter
        {
            var adapters = new List<T>();
            var type = typeof(T);
            for (int adapterIndex = 0; EnumAdapterByGpuPreference(adapterIndex, gpuPreference, type.GUID, out IntPtr adapterPtr) != ResultCode.NotFound; ++adapterIndex)
            {
                var adapter = FromPointer<T>(adapterPtr);
                adapters.Add(adapter);
            }

            return adapters.ToArray();
        }
    }
}
