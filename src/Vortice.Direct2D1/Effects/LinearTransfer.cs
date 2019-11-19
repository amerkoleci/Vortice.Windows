// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class LinearTransfer : ID2D1Effect
    {
        public LinearTransfer(ID2D1DeviceContext context)
            : base(context.CreateEffect_(EffectGuids.LinearTransfer))
        {
        }

        public LinearTransfer(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.LinearTransfer))
        {
        }

        public float RedYIntercept
        {
            get => GetFloatValue((int)LinearTransferProperties.RedYIntercept);
            set => SetValue((int)LinearTransferProperties.RedYIntercept, value);
        }

        public float RedSlope
        {
            get => GetFloatValue((int)LinearTransferProperties.RedSlope);
            set => SetValue((int)LinearTransferProperties.RedSlope, value);
        }

        public bool RedDisable
        {
            get => GetBoolValue((int)LinearTransferProperties.RedDisable);
            set => SetValue((int)LinearTransferProperties.RedDisable, value);
        }

        public float GreenYIntercept
        {
            get => GetFloatValue((int)LinearTransferProperties.GreenYIntercept);
            set => SetValue((int)LinearTransferProperties.GreenYIntercept, value);
        }

        public float GreenSlope
        {
            get => GetFloatValue((int)LinearTransferProperties.GreenSlope);
            set => SetValue((int)LinearTransferProperties.GreenSlope, value);
        }

        public bool GreenDisable
        {
            get => GetBoolValue((int)LinearTransferProperties.GreenDisable);
            set => SetValue((int)LinearTransferProperties.GreenDisable, value);
        }

        public float BlueYIntercept
        {
            get => GetFloatValue((int)LinearTransferProperties.BlueYIntercept);
            set => SetValue((int)LinearTransferProperties.BlueYIntercept, value);
        }

        public float BlueSlope
        {
            get => GetFloatValue((int)LinearTransferProperties.BlueSlope);
            set => SetValue((int)LinearTransferProperties.BlueSlope, value);
        }

        public bool BlueDisable
        {
            get => GetBoolValue((int)LinearTransferProperties.BlueDisable);
            set => SetValue((int)LinearTransferProperties.BlueDisable, value);
        }

        public float AlphaYIntercept
        {
            get => GetFloatValue((int)LinearTransferProperties.AlphaYIntercept);
            set => SetValue((int)LinearTransferProperties.AlphaYIntercept, value);
        }

        public float AlphaSlope
        {
            get => GetFloatValue((int)LinearTransferProperties.AlphaSlope);
            set => SetValue((int)LinearTransferProperties.AlphaSlope, value);
        }

        public bool AlphaDisable
        {
            get => GetBoolValue((int)LinearTransferProperties.AlphaDisable);
            set => SetValue((int)LinearTransferProperties.AlphaDisable, value);
        }

        public bool ClampOutput
        {
            get => GetBoolValue((int)LinearTransferProperties.ClampOutput);
            set => SetValue((int)LinearTransferProperties.ClampOutput, value);
        }
    }
}
