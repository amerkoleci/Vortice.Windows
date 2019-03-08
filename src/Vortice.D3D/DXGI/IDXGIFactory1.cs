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
            var result = Vortice.DXGIInternal.CreateDXGIFactory1(typeof(IDXGIFactory1).GUID, out var nativePtr);
            if (result.Success)
            {
                factory = new IDXGIFactory1(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }

        public IDXGIAdapter1[] EnumerateAdapters()
        {
            var adapters = new List<IDXGIAdapter1>();
            for (uint adapterIndex = 0; EnumAdapters1(adapterIndex, out var adapter).Success; ++adapterIndex)
            {
                adapters.Add(adapter);
            }

            return adapters.ToArray();
        }
    }
}
