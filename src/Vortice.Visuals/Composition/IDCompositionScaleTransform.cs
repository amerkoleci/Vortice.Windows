using System;

namespace Vortice.DirectComposition
{
    partial class IDCompositionScaleTransform
    {
        public IDCompositionScaleTransform(IDCompositionDevice device) : base(IntPtr.Zero)
        {
            device.CreateScaleTransform(this);
        }

        public IDCompositionScaleTransform(IDCompositionDevice2 device)
            : base(IntPtr.Zero)
        {
            device.CreateScaleTransform(this);
        }
    }
}
