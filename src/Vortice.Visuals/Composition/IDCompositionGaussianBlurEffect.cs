using System;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionGaussianBlurEffect
    {
        public IDCompositionGaussianBlurEffect(IDCompositionDevice3 device) : base(IntPtr.Zero)
        {
            device.CreateGaussianBlurEffect(this);
        }
    }
}
