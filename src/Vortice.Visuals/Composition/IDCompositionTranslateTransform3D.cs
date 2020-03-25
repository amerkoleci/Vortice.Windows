using System;

namespace Vortice.DirectComposition
{
	partial class IDCompositionTranslateTransform3D
    {
		public IDCompositionTranslateTransform3D(IDCompositionDevice device) : base(IntPtr.Zero)
		{
			device.CreateTranslateTransform3D(this);
		}

		public IDCompositionTranslateTransform3D(IDCompositionDevice2 device)
			: base(IntPtr.Zero)
		{
			device.CreateTranslateTransform3D(this);
		}
	}
}
