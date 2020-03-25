using System;

namespace Vortice.DirectComposition
{
	partial class IDCompositionVisual
    {
		public IDCompositionVisual(IDCompositionDevice device)
			: base(IntPtr.Zero)
		{
			device.CreateVisual(this);
		}
	}
}
