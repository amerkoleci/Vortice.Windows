// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory2
    {
        /// <summary>
        /// Try to create new instance of <see cref="IDXGIFactory2"/>.
        /// </summary>
        /// <param name="debug">Whether to enable debug callback.</param>
        /// <param name="factory">The <see cref="IDXGIFactory2"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result TryCreate(bool debug, out IDXGIFactory2 factory)
        {
            int flags = debug ? DXGIInternal.CreateFactoryDebug : 0x00;
            var result = DXGIInternal.CreateDXGIFactory2(flags, typeof(IDXGIFactory2).GUID, out var nativePtr);
            if (result.Success)
            {
                factory = new IDXGIFactory2(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }
    }
}
