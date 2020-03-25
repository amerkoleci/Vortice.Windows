using System;
using SharpGen.Runtime;

namespace Vortice.DirectComposition
{
    partial class IDCompositionSurfaceFactory
    {
        public IDCompositionSurfaceFactory(IDCompositionDevice2 device, ComObject renderingDevice) : base(IntPtr.Zero)
        {
            device.CreateSurfaceFactory(renderingDevice, this);
        }
    }
}
