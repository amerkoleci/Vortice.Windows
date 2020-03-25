using System;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionSaturationEffect
    {
        public IDCompositionSaturationEffect(IDCompositionDevice3 device) : base(IntPtr.Zero)
        {
            device.CreateSaturationEffect(this);
        }
    }
}
