using System;

namespace Vortice.DirectComposition
{
	partial class IDCompositionAnimation
    {
		public IDCompositionAnimation(IDCompositionDevice device) : base(IntPtr.Zero)
		{
			device.CreateAnimation(this);
		}

		public IDCompositionAnimation(IDCompositionDevice2 device) : base(IntPtr.Zero)
		{
			device.CreateAnimation(this);
		}
	}
}
