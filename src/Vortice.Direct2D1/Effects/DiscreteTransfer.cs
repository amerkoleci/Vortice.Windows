// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

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
                fixed (void* tablePtr = &table[0])
                {
                    GetValue((int)DiscreteTransferProperties.RedTable, PropertyType.Blob, (IntPtr)tablePtr, sizeof(float) * _redTableSize);
                }
                return table;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("RedTable length cannot be 0");
                }

                _redTableSize = value.Length;
                fixed (void* valuePtr = &value[0])
                {
                    SetValue((int)DiscreteTransferProperties.RedTable, PropertyType.Blob, (IntPtr)valuePtr, sizeof(float) * _redTableSize);
                }
            }
        }

        public unsafe float[] GreenTable
        {
            get
            {
                var table = new float[_greenTableSize];
                fixed (void* tablePtr = &table[0])
                {
                    GetValue((int)DiscreteTransferProperties.GreenTable, PropertyType.Blob, (IntPtr)tablePtr, sizeof(float) * _greenTableSize);
                }
                return table;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("GreenTable length cannot be 0");
                }

                _greenTableSize = value.Length;
                fixed (void* valuePtr = &value[0])
                {
                    SetValue((int)DiscreteTransferProperties.GreenTable, PropertyType.Blob, (IntPtr)valuePtr, sizeof(float) * _greenTableSize);
                }
            }
        }

        public unsafe float[] BlueTable
        {
            get
            {
                var table = new float[_blueTableSize];
                fixed (void* tablePtr = &table[0])
                {
                    GetValue((int)DiscreteTransferProperties.BlueTable, PropertyType.Blob, (IntPtr)tablePtr, sizeof(float) * _blueTableSize);
                }
                return table;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("BlueTable length cannot be 0");
                }

                _blueTableSize = value.Length;
                fixed (void* valuePtr = &value[0])
                {
                    SetValue((int)DiscreteTransferProperties.BlueTable, PropertyType.Blob, (IntPtr)valuePtr, sizeof(float) * _blueTableSize);
                }
            }
        }

        public unsafe float[] AlphaTable
        {
            get
            {
                var table = new float[_alphaTableSize];
                fixed (void* tablePtr = &table[0])
                {
                    GetValue((int)DiscreteTransferProperties.AlphaTable, PropertyType.Blob, (IntPtr)tablePtr, sizeof(float) * _alphaTableSize);
                }
                return table;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("AlphaTable length cannot be 0");
                }

                _alphaTableSize = value.Length;
                fixed (void* valuePtr = &value[0])
                {
                    SetValue((int)DiscreteTransferProperties.AlphaTable, PropertyType.Blob, (IntPtr)valuePtr, sizeof(float) * _alphaTableSize);
                }
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
