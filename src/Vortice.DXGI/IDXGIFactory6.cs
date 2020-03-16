// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory6
    {
        private readonly List<IDXGIAdapter> _adaptersToRelease = new List<IDXGIAdapter>();

        public T[] EnumAdaptersByGpuPreference<T>(GpuPreference gpuPreference) where T : IDXGIAdapter
        {
            // Release any pending adapter first.
            ReleaseAdapters();

            _adaptersToRelease.Clear();
            var adapters = new List<T>();
            var type = typeof(T);
            for (int adapterIndex = 0; EnumAdapterByGpuPreference(adapterIndex, gpuPreference, type.GUID, out IntPtr adapterPtr) != ResultCode.NotFound; ++adapterIndex)
            {
                var adapter = FromPointer<T>(adapterPtr);
                adapters.Add(adapter);
                _adaptersToRelease.Add(adapter);
            }

            return adapters.ToArray();
        }

        /// <inheritdoc/>
        protected override unsafe void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReleaseAdapters();
            }

            base.Dispose(disposing);
        }

        private void ReleaseAdapters()
        {
            if (_adaptersToRelease == null)
                return;

            var adapterCount = _adaptersToRelease.Count;
            for (var i = 0; i < adapterCount; i++)
            {
                _adaptersToRelease[i].Release();
            }
        }
    }
}
