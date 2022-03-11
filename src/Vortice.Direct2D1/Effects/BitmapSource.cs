// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using Vortice.WIC;

namespace Vortice.Direct2D1.Effects;

public sealed class BitmapSource : ID2D1Effect
{
    public BitmapSource(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.BitmapSource))
    {
    }

    public BitmapSource(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.BitmapSource))
    {
    }

    public IWICBitmapSource? WicBitmapSource
    {
        get => GetIUnknownValue<IWICBitmapSource>((int)BitmapSourceProperties.WicBitmapSource);
        set => SetValue((int)BitmapSourceProperties.WicBitmapSource, value);
    }

    public Vector2 Scale
    {
        set => SetValue((int)BitmapSourceProperties.Scale, value);
        get => GetVector2Value((int)BitmapSourceProperties.Scale);
    }

    public BitmapSourceInterpolationMode InterpolationMode
    {
        set => SetValue((int)BitmapSourceProperties.InterpolationMode, value);
        get => GetEnumValue<BitmapSourceInterpolationMode>((int)BitmapSourceProperties.InterpolationMode);
    }

    public bool EnableDpiCorrection
    {
        set => SetValue((int)BitmapSourceProperties.EnableDpiCorrection, value);
        get => GetBoolValue((int)BitmapSourceProperties.EnableDpiCorrection);
    }

    public BitmapSourceAlphaMode AlphaMode
    {
        set => SetValue((int)BitmapSourceProperties.AlphaMode, value);
        get => GetEnumValue<BitmapSourceAlphaMode>((int)BitmapSourceProperties.AlphaMode);
    }

    public BitmapSourceOrientation Orientation
    {
        set => SetValue((int)BitmapSourceProperties.Orientation, value);
        get => GetEnumValue<BitmapSourceOrientation>((int)BitmapSourceProperties.Orientation);
    }
}
