using System;

namespace Vortice.DirectComposition
{
    partial class IDCompositionEffectGroup
    {
        public IDCompositionEffectGroup(IDCompositionDevice device)
            : base(IntPtr.Zero)
        {
            device.CreateEffectGroup(this);
        }

        public IDCompositionEffectGroup(IDCompositionDevice2 device) : base(IntPtr.Zero)
        {
            device.CreateEffectGroup(this);
        }
    }
}
