using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = ColorManagementProperties;
    public class ColorManagement : ID2D1Effect
    {
        public ColorManagement(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.ColorManagement, this);
        }

        public ID2D1ColorContext SourceColorContext
        {
            set => SetValue((int)Props.SourceColorContext, value);
            get => GetIUnknownValue<ID2D1ColorContext>((int)Props.SourceColorContext);
        }
        public ColorManagementRenderingIntent SourceRenderingIntent
        {
            set => SetValue((int)Props.SourceRenderingIntent, value);
            get => GetEnumValue<ColorManagementRenderingIntent>((int)Props.SourceRenderingIntent);
        }
        public ID2D1ColorContext DestinationColorContext
        {
            set => SetValue((int)Props.DestinationInationColorContext, value);
            get => GetIUnknownValue<ID2D1ColorContext>((int)Props.DestinationInationColorContext);
        }
        public ColorManagementRenderingIntent DestinationRenderingIntent
        {
            set => SetValue((int)Props.DestinationInationRenderingIntent, value);
            get => GetEnumValue<ColorManagementRenderingIntent>((int)Props.DestinationInationRenderingIntent);
        }
        public ColorManagementAlphaMode AlphaMode
        {
            set => SetValue((int)Props.AlphaMode, value);
            get => GetEnumValue<ColorManagementAlphaMode>((int)Props.AlphaMode);
        }
        public ColormanagementQuality Quality
        {
            set => SetValue((int)Props.Quality, value);
            get => GetEnumValue<ColormanagementQuality>((int)Props.Quality);
        }

    }
}
