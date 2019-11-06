// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

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
        public uint KernelSizeX
        {
            set => SetValue((int)Props.KernelSizeX, value);
            get => GetUintValue((int)Props.KernelSizeX);
        }
        public uint KernelSizeY
        {
            set => SetValue((int)Props.KernelSizeY, value);
            get => GetUintValue((int)Props.KernelSizeY);
        }
        public unsafe float[] KernelMatrix
        {
            set
            {
                var size = (int)(KernelSizeX * KernelSizeY);
                if (value.Length != size)
                    throw new ArgumentException();
                SetValue((int)Props.KernelMatrix, PropertyType.Blob, new IntPtr(Unsafe.AsPointer(ref value[0])), sizeof(float) * size);
            }
            get
            {
                var size = (int)(KernelSizeX * KernelSizeY);
                var value = new float[size];
                GetValue((int)Props.KernelMatrix, PropertyType.Blob, new IntPtr(Unsafe.AsPointer(ref value[0])), sizeof(float) * size);
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
