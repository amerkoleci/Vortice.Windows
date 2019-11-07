// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Vortice.Direct2D1.Effects
{
    public sealed class ConvolveMatrix : ID2D1Effect
    {
        public ConvolveMatrix(ID2D1DeviceContext context)
            : base(context.CreateEffect(EffectGuids.ConvolveMatrix))
        {
        }

        public ConvolveMatrix(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.ConvolveMatrix))
        {
        }

        public float KernelUnitLehgth
        {
            set => SetValue((int)ConvolveMatrixProperties.KernelUnitLength, value);
            get => GetFloatValue((int)ConvolveMatrixProperties.KernelUnitLength);
        }

        public ConvolveMatrixScaleMode ScaleMode
        {
            set => SetValue((int)ConvolveMatrixProperties.ScaleMode, value);
            get => GetEnumValue<ConvolveMatrixScaleMode>((int)ConvolveMatrixProperties.ScaleMode);
        }

        public int KernelSizeX
        {
            set => SetValue((int)ConvolveMatrixProperties.KernelSizeX, value);
            get => GetIntValue((int)ConvolveMatrixProperties.KernelSizeX);
        }

        public int KernelSizeY
        {
            set => SetValue((int)ConvolveMatrixProperties.KernelSizeY, value);
            get => GetIntValue((int)ConvolveMatrixProperties.KernelSizeY);
        }

        public unsafe float[] KernelMatrix
        {
            get
            {
                var size = KernelSizeX * KernelSizeY;
                var value = new float[size];
                GetValue((int)ConvolveMatrixProperties.KernelMatrix, PropertyType.Blob, new IntPtr(Unsafe.AsPointer(ref value[0])), sizeof(float) * size);
                return value;
            }
            set
            {
                var size = KernelSizeX * KernelSizeY;
                if (value.Length != size)
                {
                    throw new ArgumentException();
                }

                SetValue((int)ConvolveMatrixProperties.KernelMatrix, PropertyType.Blob, new IntPtr(Unsafe.AsPointer(ref value[0])), sizeof(float) * size);
            }
        }

        public float Divisor
        {
            set => SetValue((int)ConvolveMatrixProperties.Divisor, value);
            get => GetFloatValue((int)ConvolveMatrixProperties.Divisor);
        }

        public float Bias
        {
            set => SetValue((int)ConvolveMatrixProperties.Bias, value);
            get => GetFloatValue((int)ConvolveMatrixProperties.Bias);
        }

        public Vector2 KernelOffset
        {
            set => SetValue((int)ConvolveMatrixProperties.KernelOffset, value);
            get => GetVector2Value((int)ConvolveMatrixProperties.KernelOffset);
        }

        public bool PreserveAlpha
        {
            set => SetValue((int)ConvolveMatrixProperties.PreserveAlpha, value);
            get => GetBoolValue((int)ConvolveMatrixProperties.PreserveAlpha);
        }

        public BorderMode BorderMode
        {
            set => SetValue((int)ConvolveMatrixProperties.BorderMode, value);
            get => GetEnumValue<BorderMode>((int)ConvolveMatrixProperties.BorderMode);
        }

        public bool ClampOutput
        {
            set => SetValue((int)ConvolveMatrixProperties.ClampOutput, value);
            get => GetBoolValue((int)ConvolveMatrixProperties.ClampOutput);
        }
    }
}
