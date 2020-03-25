namespace Vortice.DirectComposition
{
	public partial class IDCompositionDevice
    {
		/// <summary>
		/// Creates a new device object that can be used to create other Microsoft DirectComposition objects.
		/// </summary>
		/// <param name="device">The DXGI device to use to create DirectComposition surface objects.</param>
		public IDCompositionDevice(DXGI.IDXGIDevice device)
		{
            DComp.CreateDevice(device, typeof(IDCompositionDevice).GUID, out var temp);
			NativePointer = temp;
		}
	}
}
