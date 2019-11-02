using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = CropProperties;
    public class Crop : ID2D1Effect
    {
        public Crop(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Crop, this);
        }

        public Vector4 Rectangle
        {
            set => SetValue((int)Props.Rectangle, value);
            get => GetVector4Value((int)Props.Rectangle);
        }
        public BorderMode BorderMode
        {
            set => SetValue((int)Props.BorderMode, value);
            get => GetEnumValue<BorderMode>((int)Props.BorderMode);
        }
    }
}
