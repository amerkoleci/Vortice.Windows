using System;

namespace Vortice.Direct2D1.Effects
{
    public class Premultiply : ID2D1Effect
    {
        public Premultiply(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Premultiply, this);
        }
    }
}
