// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class GammaTransfer : ID2D1Effect
{
    public GammaTransfer(ID2D1DeviceContext context)
       : base(context.CreateEffect(EffectGuids.GammaTransfer))
    {
    }

    public GammaTransfer(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.GammaTransfer))
    {
    }

    public float RedAmplitude
    {
        set => SetValue((int)GammaTransferProperties.RedAmplitude, value);
        get => GetFloatValue((int)GammaTransferProperties.RedAmplitude);
    }
    public float RedExponent
    {
        set => SetValue((int)GammaTransferProperties.RedExponent, value);
        get => GetFloatValue((int)GammaTransferProperties.RedExponent);
    }
    public float RedOffset
    {
        set => SetValue((int)GammaTransferProperties.RedOffset, value);
        get => GetFloatValue((int)GammaTransferProperties.RedOffset);
    }
    public bool RedDisable
    {
        set => SetValue((int)GammaTransferProperties.RedDisable, value);
        get => GetBoolValue((int)GammaTransferProperties.RedDisable);
    }

    public float GreenAmplitude
    {
        set => SetValue((int)GammaTransferProperties.GreenAmplitude, value);
        get => GetFloatValue((int)GammaTransferProperties.GreenAmplitude);
    }
    public float GreenExponent
    {
        set => SetValue((int)GammaTransferProperties.GreenExponent, value);
        get => GetFloatValue((int)GammaTransferProperties.GreenExponent);
    }
    public float GreenOffset
    {
        set => SetValue((int)GammaTransferProperties.GreenOffset, value);
        get => GetFloatValue((int)GammaTransferProperties.GreenOffset);
    }
    public bool GreenDisable
    {
        set => SetValue((int)GammaTransferProperties.GreenDisable, value);
        get => GetBoolValue((int)GammaTransferProperties.GreenDisable);
    }

    public float BlueAmplitude
    {
        set => SetValue((int)GammaTransferProperties.BlueAmplitude, value);
        get => GetFloatValue((int)GammaTransferProperties.BlueAmplitude);
    }
    public float BlueExponent
    {
        set => SetValue((int)GammaTransferProperties.BlueExponent, value);
        get => GetFloatValue((int)GammaTransferProperties.BlueExponent);
    }
    public float BlueOffset
    {
        set => SetValue((int)GammaTransferProperties.BlueOffset, value);
        get => GetFloatValue((int)GammaTransferProperties.BlueOffset);
    }
    public bool BlueDisable
    {
        set => SetValue((int)GammaTransferProperties.BlueDisable, value);
        get => GetBoolValue((int)GammaTransferProperties.BlueDisable);
    }

    public float AlphaAmplitude
    {
        set => SetValue((int)GammaTransferProperties.AlphaAmplitude, value);
        get => GetFloatValue((int)GammaTransferProperties.AlphaAmplitude);
    }
    public float AlphaExponent
    {
        set => SetValue((int)GammaTransferProperties.AlphaExponent, value);
        get => GetFloatValue((int)GammaTransferProperties.AlphaExponent);
    }
    public float AlphaOffset
    {
        set => SetValue((int)GammaTransferProperties.AlphaOffset, value);
        get => GetFloatValue((int)GammaTransferProperties.AlphaOffset);
    }
    public bool AlphaDisable
    {
        set => SetValue((int)GammaTransferProperties.AlphaDisable, value);
        get => GetBoolValue((int)GammaTransferProperties.AlphaDisable);
    }

    public bool ClampOutput
    {
        set => SetValue((int)GammaTransferProperties.ClampOutput, value);
        get => GetBoolValue((int)GammaTransferProperties.ClampOutput);
    }
}
