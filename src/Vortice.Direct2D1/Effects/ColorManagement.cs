// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects;

public class ColorManagement : ID2D1Effect
{
    public ColorManagement(ID2D1DeviceContext context)
       : base(context.CreateEffect(EffectGuids.ColorManagement))
    {
    }

    public ColorManagement(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.ColorManagement))
    {
    }

    public ID2D1ColorContext? SourceColorContext
    {
        get => GetColorContextValue((int)ColorManagementProperties.SourceColorContext);
        set => SetValue((int)ColorManagementProperties.SourceColorContext, value);
    }

    public ColorManagementRenderingIntent SourceRenderingIntent
    {
        set => SetValue((int)ColorManagementProperties.SourceRenderingIntent, value);
        get => GetEnumValue<ColorManagementRenderingIntent>((int)ColorManagementProperties.SourceRenderingIntent);
    }

    public ID2D1ColorContext? DestinationColorContext
    {
        get => GetColorContextValue((int)ColorManagementProperties.DestinationColorContext);
        set => SetValue((int)ColorManagementProperties.DestinationColorContext, value);
    }

    public ColorManagementRenderingIntent DestinationRenderingIntent
    {
        set => SetValue((int)ColorManagementProperties.DestinationRenderingIntent, value);
        get => GetEnumValue<ColorManagementRenderingIntent>((int)ColorManagementProperties.DestinationRenderingIntent);
    }

    public ColorManagementAlphaMode AlphaMode
    {
        set => SetValue((int)ColorManagementProperties.AlphaMode, value);
        get => GetEnumValue<ColorManagementAlphaMode>((int)ColorManagementProperties.AlphaMode);
    }

    public ColormanagementQuality Quality
    {
        set => SetValue((int)ColorManagementProperties.Quality, value);
        get => GetEnumValue<ColormanagementQuality>((int)ColorManagementProperties.Quality);
    }
}
