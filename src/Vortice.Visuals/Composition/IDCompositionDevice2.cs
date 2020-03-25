using SharpGen.Runtime;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionDevice2
    {
        /// <summary>
        /// Used for inherited constructors
        /// </summary>
        protected IDCompositionDevice2() { }

		/// <summary>
		/// Creates a new device object that can be used to create other Microsoft DirectComposition objects.
		/// </summary>
		/// <param name="renderingDevice">An optional reference to a DirectX device to be used to create DirectComposition surface 
		/// objects. Must be a reference to an object implementing the <strong><see cref="DXGI.IDXGIDevice"/></strong> or
		/// <strong><see cref="Direct2D1.ID2D1Device"/></strong> interfaces.</param>
		public IDCompositionDevice2(ComObject renderingDevice)
		{
            DComp.CreateDevice2(renderingDevice, typeof(IDCompositionDevice2).GUID, out var temp);
			NativePointer = temp;
		}
	}
}
