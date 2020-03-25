using System;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionHueRotationEffect
    {
        public IDCompositionHueRotationEffect(IDCompositionDevice3 device) : base(IntPtr.Zero)
        {
            device.CreateHueRotationEffect(this);
        }
    }
}
