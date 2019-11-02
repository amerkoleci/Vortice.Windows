using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = YCbCrProperties;
    public class YCbCr : ID2D1Effect
    {
        public YCbCr(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.YCbCr, this);
        }

        public YCbCrChromaSubSampling ChromaSubsampling
        {
            set => SetValue((int)Props.ChromaSubsampling, value);
            get => GetEnumValue<YCbCrChromaSubSampling>((int)Props.ChromaSubsampling);
        }
        public Matrix3x2 TransformMatrix
        {
            set => SetValue((int)Props.TransformMatrix, value);
            get => GetMatrix3x2Value((int)Props.TransformMatrix);
        }
        public YCbCrInterpolationMode InterpolationMode
        {
            set => SetValue((int)Props.InterpolationMode, value);
            get => GetEnumValue<YCbCrInterpolationMode>((int)Props.InterpolationMode);
        }
    }
}
