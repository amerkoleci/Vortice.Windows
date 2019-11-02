using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Vortice.Direct2D1.Effects
{
    using Props = ConvolveMatrixProperties;
    public class ConvolveMatrix : ID2D1Effect
    {
        public ConvolveMatrix(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.ConvolveMatrix, this);
        }
        public float KernelUnitLehgth
        {
            set => SetValue((int)Props.KernelUnitLength, value);
            get => GetFloatValue((int)Props.KernelUnitLength);
        }
        public ConvolveMatrixScaleMode ScaleMode
        {
            set => SetValue((int)Props.ScaleMode, value);
            get => GetEnumValue<ConvolveMatrixScaleMode>((int)Props.ScaleMode);
        }
        public int KernelSizeX
        {
            set => SetValue((int)Props.KernelSizeX, value);
            get => GetIntValue((int)Props.KernelSizeX);
        }
        public int KernelSizeY
        {
            set => SetValue((int)Props.KernelSizeY, value);
            get => GetIntValue((int)Props.KernelSizeY);
        }
        public unsafe float[] KernelMatrix
        {
            set
            {
                if (value.Length != 9)
                    throw new ArgumentException();
                SetValue((int)Props.KernelMatrix, PropertyType.Array, new IntPtr(Unsafe.AsPointer(ref value[0])), sizeof(float) * 9);
            }
            get
            {
                var value = new float[9];
                GetValue((int)Props.KernelMatrix, PropertyType.Array, new IntPtr(Unsafe.AsPointer(ref value[0])), sizeof(float) * 9);
                return value;
            }
        }
        public float Divisor
        {
            set => SetValue((int)Props.Divisor, value);
            get => GetFloatValue((int)Props.Divisor);
        }
        public float Bias
        {
            set => SetValue((int)Props.Bias, value);
            get => GetFloatValue((int)Props.Bias);
        }
        public Vector2 KernelOffset
        {
            set => SetValue((int)Props.KernelOffset, value);
            get => GetVector2Value((int)Props.KernelOffset);
        }
        public bool PreserveAlpha
        {
            set => SetValue((int)Props.PreserveAlpha, value);
            get => GetBoolValue((int)Props.PreserveAlpha);
        }
        public BorderMode BorderMode
        {
            set => SetValue((int)Props.BorderMode, value);
            get => GetEnumValue<BorderMode>((int)Props.BorderMode);
        }
        public bool ClampOutput
        {
            set => SetValue((int)Props.ClampOutput, value);
            get => GetBoolValue((int)Props.ClampOutput);
        }
    }
}
