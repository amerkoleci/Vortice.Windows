using System;

namespace Vortice.Direct2D1.Effects
{
    public class AlphaMask : ID2D1Effect
    {
        public AlphaMask(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.AlphaMask, this);
        }
    }
}
