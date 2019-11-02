using System;

namespace Vortice.Direct2D1.Effects
{
    public class Invert : ID2D1Effect
    {
        public Invert(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Invert, this);
        }
    }
}
