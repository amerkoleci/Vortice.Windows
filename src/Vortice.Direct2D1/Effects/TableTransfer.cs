// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    public sealed class TableTransfer : ID2D1Effect
    {
        private int _redTableSize = 2;
        private int _greenTableSize = 2;
        private int _blueTableSize = 2;
        private int _alphaTableSize = 2;

        public TableTransfer(ID2D1DeviceContext context)
             : base(context.CreateEffect(EffectGuids.TableTransfer))
        {
        }

        public TableTransfer(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.TableTransfer))
        {
        }

        
        public unsafe float[] RedTable
        {
            get
            {
                var table = new float[_redTableSize];
                fixed (void* tablePtr = &table[0])
                {
                    GetValue((int)TableTransferProperties.RedTable, PropertyType.Blob, (IntPtr)tablePtr, sizeof(float) * _redTableSize);
                }
                return table;
            }
            set
            {
                if (value.Length == 0)
                    throw new ArgumentException($"{nameof(RedTable)} length must be greather than 0");
                _redTableSize = value.Length;
                fixed (void* valuePtr = &value[0])
                {
                    SetValue((int)TableTransferProperties.RedTable, PropertyType.Blob, (IntPtr)valuePtr, sizeof(float) * _redTableSize);
                }
            }
        }

        public bool RedDisable
        {
            get => GetBoolValue((int)TableTransferProperties.RedDisable);
            set => SetValue((int)TableTransferProperties.RedDisable, value);
        }

        public unsafe float[] GreenTable
        {
            get
            {
                var table = new float[_greenTableSize];
                fixed (void* tablePtr = &table[0])
                {
                    GetValue((int)TableTransferProperties.GreenTable, PropertyType.Blob, (IntPtr)tablePtr, sizeof(float) * _greenTableSize);
                }
                return table;
            }
            set
            {
                if (value.Length == 0)
                    throw new ArgumentException($"{nameof(GreenTable)} length must be greather than 0");
                _greenTableSize = value.Length;
                fixed (void* valuePtr = &value[0])
                {
                    SetValue((int)TableTransferProperties.GreenTable, PropertyType.Blob, (IntPtr)valuePtr, sizeof(float) * _greenTableSize);
                }
            }
        }

        public bool GreenDisable
        {
            get => GetBoolValue((int)TableTransferProperties.GreenDisable);
            set => SetValue((int)TableTransferProperties.GreenDisable, value);
        }

        public unsafe float[] BlueTable
        {
            get
            {
                var table = new float[_blueTableSize];
                fixed (void* tablePtr = &table[0])
                {
                    GetValue((int)TableTransferProperties.BlueTable, PropertyType.Blob, (IntPtr)tablePtr, sizeof(float) * _blueTableSize);
                }
                return table;
            }
            set
            {
                if (value.Length == 0)
                    throw new ArgumentException($"{nameof(BlueTable)} length must be greather than 0");
                _blueTableSize = value.Length;
                fixed (void* valuePtr = &value[0])
                {
                    SetValue((int)TableTransferProperties.BlueTable, PropertyType.Blob, (IntPtr)valuePtr, sizeof(float) * _blueTableSize);
                }
            }
        }

        public bool BlueDisable
        {
            get => GetBoolValue((int)TableTransferProperties.BlueDisable);
            set => SetValue((int)TableTransferProperties.BlueDisable, value);
        }

        public unsafe float[] AlphaTable
        {
            get
            {
                var table = new float[_alphaTableSize];
                fixed (void* tablePtr = &table[0])
                {
                    GetValue((int)TableTransferProperties.AlphaTable, PropertyType.Blob, (IntPtr)tablePtr, sizeof(float) * _alphaTableSize);
                }
                return table;
            }
            set
            {
                if (value.Length == 0)
                    throw new ArgumentException($"{nameof(AlphaTable)} length must be greather than 0");
                _alphaTableSize = value.Length;
                fixed (void* valuePtr = &value[0])
                {
                    SetValue((int)TableTransferProperties.AlphaTable, PropertyType.Blob, (IntPtr)valuePtr, sizeof(float) * _alphaTableSize);
                }
            }
        }

        public bool AlphaDisable
        {
            get => GetBoolValue((int)TableTransferProperties.AlphaDisable);
            set => SetValue((int)TableTransferProperties.AlphaDisable, value);
        }

        public bool ClampOutput
        {
            get => GetBoolValue((int)TableTransferProperties.ClampOutput);
            set => SetValue((int)TableTransferProperties.ClampOutput, value);
        }
    }
}
