using System;

namespace Vortice.DirectComposition
{
    partial class IDCompositionScaleTransform3D
    {
        public IDCompositionScaleTransform3D(IDCompositionDevice device) : base(IntPtr.Zero)
        {
            device.CreateScaleTransform3D(this);
        }

        public IDCompositionScaleTransform3D(IDCompositionDevice2 device)
            : base(IntPtr.Zero)
        {
            device.CreateScaleTransform3D(this);
        }
    }
}
