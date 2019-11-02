using System;

namespace Vortice.Direct2D1.Effects
{
    public class Grayscale : ID2D1Effect
    {
        public Grayscale(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Grayscale, this);
        }
    }
}
