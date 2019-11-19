// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class Border : ID2D1Effect
    {
        public Border(ID2D1DeviceContext context)
           : base(context.CreateEffect_(EffectGuids.Border))
        {
        }

        public Border(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Border))
        {
        }

        public BorderEdgeMode EdgeModeX
        {
            set => SetValue((int)BorderProperties.EdgeModeX, value);
            get => GetEnumValue<BorderEdgeMode>((int)BorderProperties.EdgeModeX);
        }

        public BorderEdgeMode EdgeModeY
        {
            set => SetValue((int)BorderProperties.EdgeModeY, value);
            get => GetEnumValue<BorderEdgeMode>((int)BorderProperties.EdgeModeY);
        }
    }
}
