// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory
    {
        public ImmutableArray<IDXGIAdapter> EnumAdapters()
        {
            var adapters = ImmutableArray.CreateBuilder<IDXGIAdapter>();
            for (int index = 0; EnumAdapters(index, out var adapter) != ResultCode.NotFound; ++index)
            {
                adapters.Add(adapter);
            }

            return adapters.ToImmutable();
        }
    }
}
