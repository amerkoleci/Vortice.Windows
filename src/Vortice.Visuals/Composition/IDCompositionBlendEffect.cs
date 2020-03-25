using System;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionBlendEffect
    {
        public IDCompositionBlendEffect(IDCompositionDevice3 device) : base(IntPtr.Zero)
        {
            device.CreateBlendEffect(this);
        }
    }
}
