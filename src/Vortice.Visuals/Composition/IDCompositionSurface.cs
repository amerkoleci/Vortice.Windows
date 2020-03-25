using System;
using SharpGen.Runtime;
using Vortice.DXGI;
using Vortice.Mathematics;

namespace Vortice.DirectComposition
{
    partial class IDCompositionSurface
    {
        public IDCompositionSurface(IDCompositionDevice device, int width, int height, Format format,
            AlphaMode alphaMode) : base(IntPtr.Zero)
        {
            device.CreateSurface(width, height, format, alphaMode, this);
        }

        public IDCompositionSurface(IDCompositionDevice2 device, int width, int height, Format format,
            AlphaMode alphaMode)
            : base(IntPtr.Zero)
        {
            device.CreateSurface(width, height, format, alphaMode, this);
        }

        public IDCompositionSurface(IDCompositionSurfaceFactory factory, int initialWidth, int initialHeight,
            Format format, AlphaMode alphaMode)
            : base(IntPtr.Zero)
        {
            factory.CreateSurface(initialWidth, initialHeight, format, alphaMode, this);
        }

        public T BeginDraw<T>(RawRect? updateRect, out Point updateOffset) where T : ComObject
        {
            BeginDraw(updateRect, typeof(T).GUID, out var temp, out updateOffset);
            return FromPointer<T>(temp);
        }
    }
}
