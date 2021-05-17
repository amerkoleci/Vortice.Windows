// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory1
    {
        /// <summary>
        /// Get an instance of <see cref="IDXGIAdapter1"/> or null if not found.
        /// </summary>
        /// <remarks>
        /// Make sure to dispose the <see cref="IDXGIAdapter1"/> instance.
        /// </remarks>
        /// <param name="index">The index to get from.</param>
        /// <returns>Instance of <see cref="IDXGIAdapter1"/> or null if not found.</returns>
        public IDXGIAdapter1? GetAdapter1(int index)
        {
            Result result = EnumAdapters1(index, out IDXGIAdapter1 adapter);
            if (result == ResultCode.NotFound)
            {
                return null;
            }

            return adapter;
        }
    }
}
