using System;

namespace Vortice.DirectComposition
{
    partial class IDCompositionMatrixTransform3D
    {
        public IDCompositionMatrixTransform3D(IDCompositionDevice device) : base(IntPtr.Zero)
        {
            device.CreateMatrixTransform3D(this);
        }

        public IDCompositionMatrixTransform3D(IDCompositionDevice2 device)
            : base(IntPtr.Zero)
        {
            device.CreateMatrixTransform3D(this);
        }
    }
}
