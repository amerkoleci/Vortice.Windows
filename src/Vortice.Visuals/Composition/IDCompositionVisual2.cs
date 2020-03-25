using System;

namespace Vortice.DirectComposition
{
	partial class IDCompositionVisual2
    {
		public IDCompositionVisual2(IDCompositionDevice2 device)
			: base(IntPtr.Zero)
		{
			device.CreateVisual(this);
		}
	}
}
