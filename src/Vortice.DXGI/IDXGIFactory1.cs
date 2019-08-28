// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory1
    {
        public IDXGIAdapter1[] EnumAdapters1()
        {
            var adapters = new List<IDXGIAdapter1>();
            for (int adapterIndex = 0; EnumAdapters1(adapterIndex, out var adapter) != ResultCode.NotFound; ++adapterIndex)
            {
                adapters.Add(adapter);
            }

            return adapters.ToArray();
        }
    }
}
