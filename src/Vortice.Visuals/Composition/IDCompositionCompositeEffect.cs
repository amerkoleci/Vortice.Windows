using System;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionCompositeEffect
    {
        public IDCompositionCompositeEffect(IDCompositionDevice3 device) : base(IntPtr.Zero)
        {
            device.CreateCompositeEffect(this);
        }
    }
}
