using System;
using SharpGen.Runtime;

namespace Vortice.DirectComposition
{
    partial class IDCompositionTransform
    {
        public IDCompositionTransform(IDCompositionDevice dev, params IDCompositionTransform[] effects)
            : this(IntPtr.Zero)
        {
            dev.CreateTransformGroup(effects, effects.Length, this);
        }

        public IDCompositionTransform(IDCompositionDevice2 dev, params IDCompositionTransform[] effects)
            : this(IntPtr.Zero)
        {
            dev.CreateTransformGroup(effects, effects.Length, this);
        }

        public IDCompositionTransform(IDCompositionDevice dev, InterfaceArray<IDCompositionTransform> effects)
            : this(IntPtr.Zero)
        {
            dev.CreateTransformGroup(effects, effects.Length, this);
        }

        public IDCompositionTransform(IDCompositionDevice2 dev, InterfaceArray<IDCompositionTransform> effects)
            : this(IntPtr.Zero)
        {
            dev.CreateTransformGroup(effects, effects.Length, this);
        }
    }
}
