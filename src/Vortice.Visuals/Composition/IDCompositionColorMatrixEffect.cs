using System;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionColorMatrixEffect
    {
        public IDCompositionColorMatrixEffect(IDCompositionDevice3 device) : base(IntPtr.Zero)
        {
            device.CreateColorMatrixEffect(this);
        }
    }
}
