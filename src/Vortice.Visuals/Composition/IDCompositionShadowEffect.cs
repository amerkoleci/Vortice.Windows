using System;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionShadowEffect
    {
        public IDCompositionShadowEffect(IDCompositionDevice3 device) : base(IntPtr.Zero)
        {
            device.CreateShadowEffect(this);
        }
    }
}
