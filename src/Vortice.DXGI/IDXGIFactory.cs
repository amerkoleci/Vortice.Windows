// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory
    {
        private ReadOnlyCollection<IDXGIAdapter> _adapters;

        public ReadOnlyCollection<IDXGIAdapter> Adapters
        {
            get
            {
                // Release old ones.
                ReleaseAdapters();

                var newAdapters = new List<IDXGIAdapter>();
                for (int adapterIndex = 0; EnumAdapters(adapterIndex, out var adapter) != ResultCode.NotFound; ++adapterIndex)
                {
                    newAdapters.Add(adapter);
                }

                _adapters = new ReadOnlyCollection<IDXGIAdapter>(newAdapters);
                return _adapters;
            }
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
            if (_adapters == null)
                return;

            var adapterCount = _adapters.Count;
            for (var i = 0; i < adapterCount; i++)
            {
                _adapters[i].Release();
            }
        }
    }
}
