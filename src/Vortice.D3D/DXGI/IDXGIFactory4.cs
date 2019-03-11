// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory4
    {
        /// <summary>
        /// Try to create new instance of <see cref="IDXGIFactory4"/>.
        /// </summary>
        /// <param name="factory">The <see cref="IDXGIFactory4"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result TryCreate(out IDXGIFactory4 factory)
        {
            var result = DXGIInternal.CreateDXGIFactory1(typeof(IDXGIFactory4).GUID, out var nativePtr);
            if (result.Success)
            {
                factory = new IDXGIFactory4(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }

        /// <summary>
        /// Try to create new instance of <see cref="IDXGIFactory4"/>.
        /// </summary>
        /// <param name="debug">Whether to enable debug callback.</param>
        /// <param name="factory">The <see cref="IDXGIFactory4"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result TryCreate(bool debug, out IDXGIFactory4 factory)
        {
            int flags = debug ? DXGIInternal.CreateFactoryDebug : 0x00;
            var result = DXGIInternal.CreateDXGIFactory2(flags, typeof(IDXGIFactory4).GUID, out var nativePtr);
            if (result.Success)
            {
                factory = new IDXGIFactory4(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }

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
