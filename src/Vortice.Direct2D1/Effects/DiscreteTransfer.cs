// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace Vortice.Direct2D1.Effects
{
    public sealed class DiscreteTransfer : ID2D1Effect
    {
        private int _redTableSize = 2;
        private int _greenTableSize = 2;
        private int _blueTableSize = 2;
        private int _alphaTableSize = 2;

        public DiscreteTransfer(ID2D1DeviceContext context)
            : base(context.CreateEffect(EffectGuids.DiscreteTransfer))
        {
        }

        public DiscreteTransfer(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.DiscreteTransfer))
        {
        }


        public unsafe float[] RedTable
        {
            get
            {
                var table = new float[_redTableSize];
                GetValue((int)DiscreteTransferProperties.RedTable, PropertyType.Blob, new IntPtr(Unsafe.AsPointer(ref table[0])), sizeof(float) * _redTableSize);
                return table;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("RedTable length cannot be 0");
                }

                _redTableSize = value.Length;
                SetValue((int)DiscreteTransferProperties.RedTable, PropertyType.Blob, new IntPtr(Unsafe.AsPointer(ref value[0])), sizeof(float) * _redTableSize);
            }
        }

        public unsafe float[] GreenTable
        {
            get
            {
                var table = new float[_greenTableSize];
                GetValue((int)DiscreteTransferProperties.GreenTable, PropertyType.Blob, new IntPtr(Unsafe.AsPointer(ref table[0])), sizeof(float) * _greenTableSize);
                return table;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("GreenTable length cannot be 0");
                }

                _greenTableSize = value.Length;
                SetValue((int)DiscreteTransferProperties.GreenTable, PropertyType.Blob, new IntPtr(Unsafe.AsPointer(ref value[0])), sizeof(float) * _greenTableSize);
            }
        }

        public unsafe float[] BlueTable
        {
            get
            {
                var table = new float[_blueTableSize];
                GetValue((int)DiscreteTransferProperties.BlueTable, PropertyType.Blob, new IntPtr(Unsafe.AsPointer(ref table[0])), sizeof(float) * _blueTableSize);
                return table;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("BlueTable length cannot be 0");
                }

                _blueTableSize = value.Length;
                SetValue((int)DiscreteTransferProperties.BlueTable, PropertyType.Blob, new IntPtr(Unsafe.AsPointer(ref value[0])), sizeof(float) * _blueTableSize);
            }
        }

        public unsafe float[] AlphaTable
        {
            get
            {
                var table = new float[_alphaTableSize];
                GetValue((int)DiscreteTransferProperties.AlphaTable, PropertyType.Blob, new IntPtr(Unsafe.AsPointer(ref table[0])), sizeof(float) * _alphaTableSize);
                return table;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("AlphaTable length cannot be 0");
                }

                _alphaTableSize = value.Length;
                SetValue((int)DiscreteTransferProperties.AlphaTable, PropertyType.Blob, new IntPtr(Unsafe.AsPointer(ref value[0])), sizeof(float) * _alphaTableSize);
            }
        }

        public bool RedDisable
        {
            set => SetValue((int)DiscreteTransferProperties.RedDisable, value);
            get => GetBoolValue((int)DiscreteTransferProperties.RedDisable);
        }

        public bool GreenDisable
        {
            set => SetValue((int)DiscreteTransferProperties.GreenDisable, value);
            get => GetBoolValue((int)DiscreteTransferProperties.GreenDisable);
        }

        public bool BlueDisable
        {
            set => SetValue((int)DiscreteTransferProperties.BlueDisable, value);
            get => GetBoolValue((int)DiscreteTransferProperties.BlueDisable);
        }

        public bool AlphaDisable
        {
            set => SetValue((int)DiscreteTransferProperties.AlphaDisable, value);
            get => GetBoolValue((int)DiscreteTransferProperties.AlphaDisable);
        }

        public bool ClampOutput
        {
            set => SetValue((int)DiscreteTransferProperties.ClampOutput, value);
            get => GetBoolValue((int)DiscreteTransferProperties.ClampOutput);
        }
    }
}
