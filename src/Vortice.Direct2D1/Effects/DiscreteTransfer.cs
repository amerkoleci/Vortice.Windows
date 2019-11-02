using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = DiscreteTransferProperties;
    public class DiscreteTransfer : ID2D1Effect
    {
        public DiscreteTransfer(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.DiscreteTransfer, this);
        }
        public Vector2 RedTable
        {
            set => SetValue((int)Props.RedTable, value);
            get => GetVector2Value((int)Props.RedTable);
        }
        public Vector2 GreenTable
        {
            set => SetValue((int)Props.GreenTable, value);
            get => GetVector2Value((int)Props.GreenTable);
        }
        public Vector2 BlueTable
        {
            set => SetValue((int)Props.BlueTable, value);
            get => GetVector2Value((int)Props.BlueTable);
        }
        public Vector2 AlphaTable
        {
            set => SetValue((int)Props.AlphaTable, value);
            get => GetVector2Value((int)Props.AlphaTable);
        }
        public bool RedDisable
        {
            set => SetValue((int)Props.RedDisable, value);
            get => GetBoolValue((int)Props.RedDisable);
        }
        public bool GreenDisable
        {
            set => SetValue((int)Props.GreenDisable, value);
            get => GetBoolValue((int)Props.GreenDisable);
        }
        public bool BlueDisable
        {
            set => SetValue((int)Props.BlueDisable, value);
            get => GetBoolValue((int)Props.BlueDisable);
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
