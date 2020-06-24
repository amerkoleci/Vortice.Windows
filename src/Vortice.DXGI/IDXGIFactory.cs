// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory
    {
        /// <summary>
        /// Get the number of available adapters from this factory.
        /// </summary>
        /// <returns>The number of adapters</returns>
        public virtual int GetAdapterCount()
        {
            int count = 0;
            while (true)
            {
                var result = EnumAdapters(count, out var adapter);
                if (adapter != null)
                    adapter.Dispose();
                if (result == ResultCode.NotFound)
                    break;

                count++;
            }

            return count;
        }

        /// <summary>
        /// Get an instance of <see cref="IDXGIAdapter"/> or null if not found.
        /// </summary>
        /// <remarks>
        /// Make sure to dispose the <see cref="IDXGIAdapter"/> instance.
        /// </remarks>
        /// <param name="index">The index to get from.</param>
        /// <returns>Instance of <see cref="IDXGIAdapter"/> or null if not found.</returns>
        public IDXGIAdapter GetAdapter(int index)
        {
            var result = EnumAdapters(index, out var adapter);
            if (result == ResultCode.NotFound)
            {
                return null;
            }

            return adapter;
        }

        /// <summary>
        /// Enumerate an array of <see cref="IDXGIAdapter"/>.
        /// </summary>
        /// <remarks>
        /// Make sure to dispose the array instance, using <see cref="Utilities.Dispose{T}(T[])"/>
        /// </remarks>
        /// <returns>An array of <see cref="IDXGIAdapter"/></returns>
        public IDXGIAdapter[] EnumAdapters()
        {
            var index = 0;
            var adapters = new List<IDXGIAdapter>();
            while (true)
            {
                var result = EnumAdapters(index++, out var adapter);
                if (result == ResultCode.NotFound)
                    break;
                adapters.Add(adapter);
            }

            return adapters.ToArray();
        }
    }
}
