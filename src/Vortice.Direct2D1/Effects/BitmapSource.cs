using System;
using System.Numerics;
using Vortice.WIC;

namespace Vortice.Direct2D1.Effects
{
    using Props = BitmapSourceProperties;
    public class BitmapSource : ID2D1Effect
    {
        public BitmapSource(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.BitmapSource, this);
        }

        public IWICBitmapSource WicBitmapSource
        {
            set => SetValue((int)Props.WicBitmapSource, value);
            get => GetIUnknownValue<IWICBitmapSource>((int)Props.WicBitmapSource);
        }
        public Vector2 Scale
        {
            set => SetValue((int)Props.Scale, value);
            get => GetVector2Value((int)Props.Scale);
        }
        public BitmapSourceInterpolationMode InterpolationMode
        {
            set => SetValue((int)Props.InterpolationMode, value);
            get => GetEnumValue<BitmapSourceInterpolationMode>((int)Props.InterpolationMode);
        }
        public bool EnableDpiCorrection
        {
            set => SetValue((int)Props.EnableDpiCorrection, value);
            get => GetBoolValue((int)Props.EnableDpiCorrection);
        }
        public BitmapSourceAlphaMode AlphaMode
        {
            set => SetValue((int)Props.AlphaMode, value);
            get => GetEnumValue<BitmapSourceAlphaMode>((int)Props.AlphaMode);
        }
        public BitmapSourceOrientation Orientation
        {
            set => SetValue((int)Props.Orientation, value);
            get => GetEnumValue<BitmapSourceOrientation>((int)Props.Orientation);
        }

    }
}
