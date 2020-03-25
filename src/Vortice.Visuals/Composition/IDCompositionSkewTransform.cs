using System;

namespace Vortice.DirectComposition
{
    partial class IDCompositionSkewTransform
    {
        public IDCompositionSkewTransform(IDCompositionDevice device) : base(IntPtr.Zero)
        {
            device.CreateSkewTransform(this);
        }

        public IDCompositionSkewTransform(IDCompositionDevice2 device)
            : base(IntPtr.Zero)
        {
            device.CreateSkewTransform(this);
        }
    }
}
