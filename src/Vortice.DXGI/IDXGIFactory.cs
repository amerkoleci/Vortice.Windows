// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory
    {
        /// <summary>
        /// Get an instance of <see cref="IDXGIAdapter"/> or null if not found.
        /// </summary>
        /// <remarks>
        /// Make sure to dispose the <see cref="IDXGIAdapter"/> instance.
        /// </remarks>
        /// <param name="index">The index to get from.</param>
        /// <returns>Instance of <see cref="IDXGIAdapter"/> or null if not found.</returns>
        public IDXGIAdapter? GetAdapter(int index)
        {
            Result result = EnumAdapters(index, out IDXGIAdapter adapter);
            if (result == ResultCode.NotFound)
            {
                return null;
            }

            return adapter;
        }
    }
}
