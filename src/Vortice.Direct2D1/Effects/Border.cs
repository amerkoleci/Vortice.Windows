using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = BorderProperties;
    public class Border : ID2D1Effect
    {
        public Border(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Border, this);
        }

        public BorderEdgeMode EdgeModeX
        {
            set => SetValue((int)Props.EdgeModeX, value);
            get => GetEnumValue<BorderEdgeMode>((int)Props.EdgeModeX);
        }
        public BorderEdgeMode EdgeModeY
        {
            set => SetValue((int)Props.EdgeModeY, value);
            get => GetEnumValue<BorderEdgeMode>((int)Props.EdgeModeY);
        }
    }
}
