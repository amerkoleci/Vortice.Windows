// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using SharpGen.Runtime;

namespace SharpDXGI
{
    public partial class IDXGIFactory
    {
        public IDXGIAdapter[] EnumAdapters()
        {
            var adapters = new List<IDXGIAdapter>();
            for (int adapterIndex = 0; EnumAdapters(adapterIndex, out var adapter) != ResultCode.NotFound; ++adapterIndex)
            {
                adapters.Add(adapter);
            }

            return adapters.ToArray();
        }
    }
}
