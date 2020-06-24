// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory4
    {
        /// <summary>
        /// Gets the default warp adapter.
        /// </summary>
        /// <returns>The warp adapter.</returns>
        public Result EnumWarpAdapter<T>(out T adapter) where T : IDXGIAdapter
        {
            var result = EnumWarpAdapter(typeof(T).GUID, out var nativePtr);
            if (result.Success)
            {
                adapter = FromPointer<T>(nativePtr);
                return result;
            }

            adapter = null;
            return result;
        }

        /// <summary>
        /// Gets the adapter for the specified LUID.
        /// </summary>
        /// <param name="adapterLuid">A unique value that identifies the adapter.</param>
        /// <param name="adapter">The adapter instance of <see cref="IDXGIAdapter"/>, make sure to dispose the instance.</param>
        /// <returns>The <see cref="Result"/>.</returns>
        public Result EnumAdapterByLuid<T>(long adapterLuid, out T adapter) where T : IDXGIAdapter
        {
            var result = EnumAdapterByLuid(adapterLuid, typeof(T).GUID, out var nativePtr);
            if (result.Success)
            {
                adapter = FromPointer<T>(nativePtr);
                return result;
            }

            adapter = null;
            return result;
        }
    }
}
