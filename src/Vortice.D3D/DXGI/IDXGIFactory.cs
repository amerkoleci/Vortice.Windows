// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory
    {
        /// <summary>
        /// Try to create new instance of <see cref="IDXGIFactory"/>.
        /// </summary>
        /// <param name="factory">The <see cref="IDXGIFactory"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result TryCreate(out IDXGIFactory factory)
        {
            var result = DXGIInternal.CreateDXGIFactory(typeof(IDXGIFactory).GUID, out var nativePtr);
            if (result.Success)
            {
                factory = new IDXGIFactory(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }

        public IDXGIAdapter[] EnumerateAdapters()
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
