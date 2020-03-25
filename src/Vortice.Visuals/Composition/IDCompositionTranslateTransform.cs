using System;

namespace Vortice.DirectComposition
{
	partial class IDCompositionTranslateTransform
    {
		public IDCompositionTranslateTransform(IDCompositionDevice device) : base(IntPtr.Zero)
		{
			device.CreateTranslateTransform(this);
		}

		public IDCompositionTranslateTransform(IDCompositionDevice2 device)
			: base(IntPtr.Zero)
		{
			device.CreateTranslateTransform(this);
		}
	}
}
