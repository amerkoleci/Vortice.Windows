namespace Vortice.DXGI
{
    public partial class IDXGIFactory
    {
        /// <summary>
        /// Try to create new instance of <see cref="IDXGIFactory"/>.
        /// </summary>
        /// <param name="factory">The <see cref="IDXGIFactory"/> being created.</param>
        /// <returns>True if succed, false otherwise.</returns>
        public static bool TryCreate(out IDXGIFactory factory)
        {
            if (Vortice.DXGIInternal.CreateDXGIFactory(typeof(IDXGIFactory).GUID, out var nativePtr).Success)
            {
                factory = new IDXGIFactory(nativePtr);
                return true;
            }

            factory = null;
            return false;
        }
    }
}
