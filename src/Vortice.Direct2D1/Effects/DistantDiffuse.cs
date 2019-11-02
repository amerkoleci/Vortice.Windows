using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = DistantDiffuseProperties;
    public class DistantDiffuse : ID2D1Effect
    {
        public DistantDiffuse(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.DistantDiffuse, this);
        }

        public float Azimuth
        {
            set => SetValue((int)Props.Azimuth, value);
            get => GetFloatValue((int)Props.Azimuth);
        }
        public float Elevation
        {
            set => SetValue((int)Props.Elevation, value);
            get => GetFloatValue((int)Props.Elevation);
        }
        public float DiffuseConstant
        {
            set => SetValue((int)Props.DiffuseConstant, value);
            get => GetFloatValue((int)Props.DiffuseConstant);
        }
        public float SurfaceScale
        {
            set => SetValue((int)Props.SurfaceScale, value);
            get => GetFloatValue((int)Props.SurfaceScale);
        }
        public Vector3 Color
        {
            set => SetValue((int)Props.Color, value);
            get => GetVector3Value((int)Props.Color);
        }
        public Vector2 KernelUnitLength
        {
            set => SetValue((int)Props.KernelUnitLength, value);
            get => GetVector2Value((int)Props.KernelUnitLength);
        }
        public DistantDiffuseScaleMode ScaleMode
        {
            set => SetValue((int)Props.ScaleMode, value);
            get => GetEnumValue<DistantDiffuseScaleMode>((int)Props.ScaleMode);
        }
    }
}
