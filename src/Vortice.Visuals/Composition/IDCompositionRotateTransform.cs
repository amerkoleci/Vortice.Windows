using System;

namespace Vortice.DirectComposition
{
    partial class IDCompositionRotateTransform
    {
        public IDCompositionRotateTransform(IDCompositionDevice device) : base(IntPtr.Zero)
        {
            device.CreateRotateTransform(this);
        }

        public IDCompositionRotateTransform(IDCompositionDevice2 device)
            : base(IntPtr.Zero)
        {
            device.CreateRotateTransform(this);
        }
    }
}
