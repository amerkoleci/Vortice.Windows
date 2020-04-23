// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Immutable;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory6
    {
        public ImmutableArray<T> EnumAdaptersByGpuPreference<T>(GpuPreference gpuPreference) where T : IDXGIAdapter
        {
            var adapters = ImmutableArray.CreateBuilder<T>();
            var type = typeof(T);
            for (int index = 0; EnumAdapterByGpuPreference(index, gpuPreference, type.GUID, out IntPtr adapterPtr) != ResultCode.NotFound; ++index)
            {
                adapters.Add(FromPointer<T>(adapterPtr));
            }

            return adapters.ToImmutable();
        }
    }
}
