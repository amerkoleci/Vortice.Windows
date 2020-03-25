using System;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionLinearTransferEffect
    {
        public IDCompositionLinearTransferEffect(IDCompositionDevice3 device) : base(IntPtr.Zero)
        {
            device.CreateLinearTransferEffect(this);
        }
    }
}
