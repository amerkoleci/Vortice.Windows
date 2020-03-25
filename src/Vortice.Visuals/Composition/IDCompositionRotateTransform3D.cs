using System;

namespace Vortice.DirectComposition
{
    partial class IDCompositionRotateTransform3D
    {
        public IDCompositionRotateTransform3D(IDCompositionDevice device) : base(IntPtr.Zero)
        {
            device.CreateRotateTransform3D(this);
        }

        public IDCompositionRotateTransform3D(IDCompositionDevice2 device)
            : base(IntPtr.Zero)
        {
            device.CreateRotateTransform3D(this);
        }
    }
}
