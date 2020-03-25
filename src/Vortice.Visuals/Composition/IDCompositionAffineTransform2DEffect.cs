using System;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionAffineTransform2DEffect
    {
        public IDCompositionAffineTransform2DEffect(IDCompositionDevice3 device) : base(IntPtr.Zero)
        {
            device.CreateAffineTransform2DEffect(this);
        }
    }
}
