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
        public T GetWarpAdapter<T>() where T : IDXGIAdapter
        {
            if (EnumWarpAdapter(typeof(T).GUID, out IntPtr adapterPtr).Failure)
            {
                return default;
            }

            return FromPointer<T>(adapterPtr);
        }

        /// <summary>
        /// Gets the adapter for the specified LUID.
        /// </summary>
        /// <param name="adapterLuid">A unique value that identifies the adapter.</param>
        /// <returns>The adapter.</returns>
        public T GetAdapterByLuid<T>(long adapterLuid) where T : IDXGIAdapter
        {
            if (EnumAdapterByLuid(adapterLuid, typeof(T).GUID, out IntPtr adapterPtr).Failure)
            {
                return default;
            }

            return FromPointer<T>(adapterPtr);
        }
    }
}
