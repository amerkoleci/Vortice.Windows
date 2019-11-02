using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = LinearTransferProperties;
    public class LinearTransfer : ID2D1Effect
    {
        public LinearTransfer(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.LinearTransfer, this);
        }

        public float RedYIntercept
        {
            set => SetValue((int)Props.RedYIntercept, value);
            get => GetFloatValue((int)Props.RedYIntercept);
        }
        public float RedSlope
        {
            set => SetValue((int)Props.RedSlope, value);
            get => GetFloatValue((int)Props.RedSlope);
        }
        public bool RedDisable
        {
            set => SetValue((int)Props.RedDisable, value);
            get => GetBoolValue((int)Props.RedDisable);
        }
        public float GreenYIntercept
        {
            set => SetValue((int)Props.GreenYIntercept, value);
            get => GetFloatValue((int)Props.GreenYIntercept);
        }
        public float GreenSlope
        {
            set => SetValue((int)Props.GreenSlope, value);
            get => GetFloatValue((int)Props.GreenSlope);
        }
        public bool GreenDisable
        {
            set => SetValue((int)Props.GreenDisable, value);
            get => GetBoolValue((int)Props.GreenDisable);
        }
        public float BlueYIntercept
        {
            set => SetValue((int)Props.BlueYIntercept, value);
            get => GetFloatValue((int)Props.BlueYIntercept);
        }
        public float BlueSlope
        {
            set => SetValue((int)Props.BlueSlope, value);
            get => GetFloatValue((int)Props.BlueSlope);
        }
        public bool BlueDisable
        {
            set => SetValue((int)Props.BlueDisable, value);
            get => GetBoolValue((int)Props.BlueDisable);
        }
        public float AlphaYIntercept
        {
            set => SetValue((int)Props.AlphaYIntercept, value);
            get => GetFloatValue((int)Props.AlphaYIntercept);
        }
        public float AlphaSlope
        {
            set => SetValue((int)Props.AlphaSlope, value);
            get => GetFloatValue((int)Props.AlphaSlope);
        }
        public bool AlphaDisable
        {
            set => SetValue((int)Props.AlphaDisable, value);
            get => GetBoolValue((int)Props.AlphaDisable);
        }
        public bool ClampOutput
        {
            set => SetValue((int)Props.ClampOutput, value);
            get => GetBoolValue((int)Props.ClampOutput);
        }
    }
}
