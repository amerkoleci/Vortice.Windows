using System;

namespace Vortice.DirectComposition
{
    public partial class IDCompositionTurbulenceEffect
    {
        public IDCompositionTurbulenceEffect(IDCompositionDevice3 device) : base(IntPtr.Zero)
        {
            device.CreateTurbulenceEffect(this);
        }
    }
}
