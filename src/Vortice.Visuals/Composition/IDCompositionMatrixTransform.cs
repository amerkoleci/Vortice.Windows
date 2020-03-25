using System;

namespace Vortice.DirectComposition
{
    partial class IDCompositionMatrixTransform
    {
        public IDCompositionMatrixTransform(IDCompositionDevice device) : base(IntPtr.Zero)
        {
            device.CreateMatrixTransform(this);
        }

        public IDCompositionMatrixTransform(IDCompositionDevice2 device)
            : base(IntPtr.Zero)
        {
            device.CreateMatrixTransform(this);
        }
    }
}
