// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    public sealed class Crop : ID2D1Effect
    {
        public Crop(ID2D1DeviceContext context)
            : base(context.CreateEffect(EffectGuids.Crop))
        {
        }

        public Crop(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Crop))
        {
        }

        public Vector4 Rectangle
        {
            set => SetValue((int)CropProperties.Rectangle, value);
            get => GetVector4Value((int)CropProperties.Rectangle);
        }

        public BorderMode BorderMode
        {
            set => SetValue((int)CropProperties.BorderMode, value);
            get => GetEnumValue<BorderMode>((int)CropProperties.BorderMode);
        }
    }
}
