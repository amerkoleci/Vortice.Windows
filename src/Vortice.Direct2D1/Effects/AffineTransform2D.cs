using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = AffineTransform2DProperties;
    public class AffineTransform2D : ID2D1Effect
    {
        public AffineTransform2D(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.AffineTransform2D, this);
        }

        public AffineTransform2DInterpolationMode InterPolationMode
        {
            set => SetValue((int)Props.InterpolationMode, value);
            get => GetEnumValue<AffineTransform2DInterpolationMode>((int)Props.InterpolationMode);
        }
        public BorderMode BorderMode
        {
            set => SetValue((int)Props.BorderMode, value);
            get => GetEnumValue<BorderMode>((int)Props.BorderMode);
        }

        public Matrix3x2 TransformMatrix
        {
            set => SetValue((int)Props.TransformMatrix, value);
            get => GetMatrix3x2Value((int)Props.TransformMatrix);
        }

        public float Sharpness
        {
            set => SetValue((int)Props.Sharpness, value);
            get => GetFloatValue((int)Props.Sharpness);
        }
    }
}
