using System;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionArithmeticCompositeEffect
    {
        public IDCompositionArithmeticCompositeEffect(IDCompositionDevice3 device) : base(IntPtr.Zero)
        {
            device.CreateArithmeticCompositeEffect(this);
        }
    }
}
