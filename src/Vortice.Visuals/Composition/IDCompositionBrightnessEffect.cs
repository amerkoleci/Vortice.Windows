using System;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionBrightnessEffect
    {
        public IDCompositionBrightnessEffect(IDCompositionDevice3 device) : base(IntPtr.Zero)
        {
            device.CreateBrightnessEffect(this);
        }
    }
}
