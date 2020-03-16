// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory1
    {
        private ReadOnlyCollection<IDXGIAdapter1> _adapters1;

        public ReadOnlyCollection<IDXGIAdapter1> Adapters1
        {
            get
            {
                // Release old ones.
                ReleaseAdapters1();

                var newAdapters = new List<IDXGIAdapter1>();
                for (int adapterIndex = 0; EnumAdapters1(adapterIndex, out var adapter) != ResultCode.NotFound; ++adapterIndex)
                {
                    newAdapters.Add(adapter);
                }

                _adapters1 = new ReadOnlyCollection<IDXGIAdapter1>(newAdapters);
                return _adapters1;
            }
        }

        /// <inheritdoc/>
        protected override unsafe void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReleaseAdapters1();
            }

            base.Dispose(disposing);
        }

        private void ReleaseAdapters1()
        {
            if (_adapters1 == null)
                return;

            var adapterCount = _adapters1.Count;
            for (var i = 0; i < adapterCount; i++)
            {
                _adapters1[i].Release();
            }
        }
    }
}
