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
            var result = Vortice.DXGIInternal.CreateDXGIFactory(typeof(IDXGIFactory).GUID, out var nativePtr);
            if (result.Success)
            {
                factory = new IDXGIFactory(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }
    }
}
