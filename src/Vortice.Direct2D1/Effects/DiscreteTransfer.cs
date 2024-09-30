// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class DiscreteTransfer : ID2D1Effect
{
    private uint _redTableSize = 2;
    private uint _greenTableSize = 2;
    private uint _blueTableSize = 2;
    private uint _alphaTableSize = 2;

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
            fixed (void* tablePtr = table)
            {
                GetValue((int)DiscreteTransferProperties.RedTable, PropertyType.Blob, tablePtr, sizeof(float) * _redTableSize);
            }
            return table;
        }
        set
        {
            if (value.Length == 0)
            {
                throw new ArgumentException("RedTable length cannot be 0");
            }

            _redTableSize = (uint)value.Length;
            fixed (void* valuePtr = &value[0])
            {
                SetValue((int)DiscreteTransferProperties.RedTable, PropertyType.Blob, valuePtr, sizeof(float) * _redTableSize);
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
                GetValue((int)DiscreteTransferProperties.GreenTable, PropertyType.Blob, tablePtr, sizeof(float) * _greenTableSize);
            }
            return table;
        }
        set
        {
            if (value.Length == 0)
            {
                throw new ArgumentException("GreenTable length cannot be 0");
            }

            _greenTableSize = (uint)value.Length;
            fixed (void* valuePtr = value)
            {
                SetValue((int)DiscreteTransferProperties.GreenTable, PropertyType.Blob, valuePtr, sizeof(float) * _greenTableSize);
            }
        }
    }

    public unsafe float[] BlueTable
    {
        get
        {
            var table = new float[_blueTableSize];
            fixed (void* tablePtr = table)
            {
                GetValue((int)DiscreteTransferProperties.BlueTable, PropertyType.Blob, tablePtr, sizeof(float) * (uint)_blueTableSize);
            }
            return table;
        }
        set
        {
            if (value.Length == 0)
            {
                throw new ArgumentException("BlueTable length cannot be 0");
            }

            _blueTableSize = (uint)value.Length;
            fixed (void* valuePtr = value)
            {
                SetValue((int)DiscreteTransferProperties.BlueTable, PropertyType.Blob, valuePtr, sizeof(float) * _blueTableSize);
            }
        }
    }

    public unsafe float[] AlphaTable
    {
        get
        {
            var table = new float[_alphaTableSize];
            fixed (void* tablePtr = table)
            {
                GetValue((int)DiscreteTransferProperties.AlphaTable, PropertyType.Blob, tablePtr, sizeof(float) * (uint)_alphaTableSize);
            }
            return table;
        }
        set
        {
            if (value.Length == 0)
            {
                throw new ArgumentException("AlphaTable length cannot be 0");
            }

            _alphaTableSize = (uint)value.Length;
            fixed (void* valuePtr = value)
            {
                SetValue((int)DiscreteTransferProperties.AlphaTable, PropertyType.Blob, valuePtr, sizeof(float) * _alphaTableSize);
            }
        }
    }

    public bool RedDisable
    {
        get => GetBoolValue((int)DiscreteTransferProperties.RedDisable);
        set => SetValue((int)DiscreteTransferProperties.RedDisable, value);
    }

    public bool GreenDisable
    {
        get => GetBoolValue((int)DiscreteTransferProperties.GreenDisable);
        set => SetValue((int)DiscreteTransferProperties.GreenDisable, value);
    }

    public bool BlueDisable
    {
        get => GetBoolValue((int)DiscreteTransferProperties.BlueDisable);
        set => SetValue((int)DiscreteTransferProperties.BlueDisable, value);
    }

    public bool AlphaDisable
    {
        get => GetBoolValue((int)DiscreteTransferProperties.AlphaDisable);
        set => SetValue((int)DiscreteTransferProperties.AlphaDisable, value);
    }

    public bool ClampOutput
    {
        get => GetBoolValue((int)DiscreteTransferProperties.ClampOutput);
        set => SetValue((int)DiscreteTransferProperties.ClampOutput, value);
    }
}
