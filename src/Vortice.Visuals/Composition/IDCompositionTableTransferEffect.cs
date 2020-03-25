using System;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionTableTransferEffect
    {
        public IDCompositionTableTransferEffect(IDCompositionDevice3 device) : base(IntPtr.Zero)
        {
            device.CreateTableTransferEffect(this);
        }
    }
}
