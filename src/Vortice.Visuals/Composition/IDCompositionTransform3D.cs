using System;
using SharpGen.Runtime;

namespace Vortice.DirectComposition
{
	partial class IDCompositionTransform3D
    {
		public IDCompositionTransform3D(IDCompositionDevice dev, params IDCompositionTransform3D[] effects)
			: this(IntPtr.Zero)
		{
			dev.CreateTransform3DGroup(effects, effects.Length, this);
		}

		public IDCompositionTransform3D(IDCompositionDevice2 dev, params IDCompositionTransform3D[] effects)
			: this(IntPtr.Zero)
		{
			dev.CreateTransform3DGroup(effects, effects.Length, this);
		}

		public IDCompositionTransform3D(IDCompositionDevice dev, InterfaceArray<IDCompositionTransform3D> effects)
			: this(IntPtr.Zero)
		{
			dev.CreateTransform3DGroup(effects, effects.Length, this);
		}

		public IDCompositionTransform3D(IDCompositionDevice2 dev, InterfaceArray<IDCompositionTransform3D> effects)
			: this(IntPtr.Zero)
		{
			dev.CreateTransform3DGroup(effects, effects.Length, this);
		}
	}
}
