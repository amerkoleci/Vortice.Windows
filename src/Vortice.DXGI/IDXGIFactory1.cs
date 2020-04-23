// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory1
    {
        public ImmutableArray<IDXGIAdapter1> EnumAdapters1()
        {
            var adapters = ImmutableArray.CreateBuilder<IDXGIAdapter1>();
            for (int index = 0; EnumAdapters1(index, out var adapter) != ResultCode.NotFound; ++index)
            {
                adapters.Add(adapter);
            }

            return adapters.ToImmutable();
        }
    }
}
