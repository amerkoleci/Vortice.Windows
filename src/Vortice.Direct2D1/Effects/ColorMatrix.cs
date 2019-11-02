using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = ColorMatrixProperties;
    public class ColorMatrix : ID2D1Effect
    {
        public ColorMatrix(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.ColorMatrix, this);
        }
        public RawMatrix5x4 Matrix
        {
            set => SetValue((int)Props.ColorMatrix, value);
            get => GetMatrix5x4Value((int)Props.ColorMatrix);
        }

        public ColorMatrixAlphaMode AlphaMode
        {
            set => SetValue((int)Props.AlphaMode, value);
            get => GetEnumValue<ColorMatrixAlphaMode>((int)Props.AlphaMode);
        }

    }
}
