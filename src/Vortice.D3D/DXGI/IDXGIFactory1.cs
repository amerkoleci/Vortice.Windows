// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory1
    {
        /// <summary>
        /// Try to create new instance of <see cref="IDXGIFactory1"/>.
        /// </summary>
        /// <param name="factory">The <see cref="IDXGIFactory1"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result TryCreate(out IDXGIFactory1 factory)
        {
            var result = DXGIInternal.CreateDXGIFactory1(typeof(IDXGIFactory1).GUID, out var nativePtr);
            if (result.Success)
            {
                factory = new IDXGIFactory1(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }

        public new IDXGIAdapter1[] EnumerateAdapters()
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
