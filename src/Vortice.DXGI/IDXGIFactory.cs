// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory
    {
        private ImmutableArray<IDXGIAdapter> _adapters;

        public ImmutableArray<IDXGIAdapter> Adapters
        {
            get
            {
                // Release old ones.
                ReleaseAdapters();

                var newAdapters = ImmutableArray.CreateBuilder<IDXGIAdapter>();
                for (int adapterIndex = 0; EnumAdapters(adapterIndex, out var adapter) != ResultCode.NotFound; ++adapterIndex)
                {
                    newAdapters.Add(adapter);
                }

                _adapters = newAdapters.ToImmutable();
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
            if (_adapters.IsDefault)
                return;

            var adapterCount = _adapters.Length;
            for (var i = 0; i < adapterCount; i++)
            {
                _adapters[i].Release();
            }
        }
    }
}
