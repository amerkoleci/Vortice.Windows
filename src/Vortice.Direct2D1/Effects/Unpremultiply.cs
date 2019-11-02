using System;

namespace Vortice.Direct2D1.Effects
{
    public class Unpremultiply : ID2D1Effect
    {
        public Unpremultiply(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.UnPremultiply, this);
        }
    }
}
