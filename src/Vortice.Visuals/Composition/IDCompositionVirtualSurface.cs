using System;

namespace Vortice.DirectComposition
{
	partial class IDCompositionVirtualSurface
    {
		public IDCompositionVirtualSurface(IDCompositionDevice device, int initialWidth, int initialHeight, DXGI.Format format, DXGI.AlphaMode alphaMode) : base(IntPtr.Zero)
		{
			device.CreateVirtualSurface(initialWidth, initialHeight, format, alphaMode, this);
		}

		public IDCompositionVirtualSurface(IDCompositionDevice2 device, int initialWidth, int initialHeight, DXGI.Format format, DXGI.AlphaMode alphaMode)
			: base(IntPtr.Zero)
		{
			device.CreateVirtualSurface(initialWidth, initialHeight, format, alphaMode, this);
		}

		public IDCompositionVirtualSurface(IDCompositionSurfaceFactory factory, int initialWidth, int initialHeight, DXGI.Format format, DXGI.AlphaMode alphaMode)
			: base(IntPtr.Zero)
		{
			factory.CreateVirtualSurface(initialWidth, initialHeight, format, alphaMode, this);
		}
	}
}
