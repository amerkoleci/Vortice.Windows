using SharpGen.Runtime;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionDevice3
    {
        /// <summary>
        /// Creates a new device object that can be used to create other Microsoft DirectComposition objects.
        /// </summary>
        /// <param name="renderingDevice">An optional reference to a DirectX device to be used to create DirectComposition surface
        /// objects. Must be a reference to an object implementing the <strong><see cref="DXGI.IDXGIDevice"/></strong> or
        /// <strong><see cref="Direct2D1.ID2D1Device"/></strong> interfaces.</param>
        public IDCompositionDevice3(ComObject renderingDevice)
		{
            DComp.CreateDevice3(renderingDevice, typeof(IDCompositionDevice3).GUID, out var temp);
			NativePointer = temp;
		}
	}
}
