using System;

namespace Vortice.DirectComposition
{
    partial class IDCompositionRectangleClip
    {
        public IDCompositionRectangleClip(IDCompositionDevice device) : base(IntPtr.Zero)
        {
            device.CreateRectangleClip(this);
        }

        public IDCompositionRectangleClip(IDCompositionDevice2 device)
            : base(IntPtr.Zero)
        {
            device.CreateRectangleClip(this);
        }
    }
}
