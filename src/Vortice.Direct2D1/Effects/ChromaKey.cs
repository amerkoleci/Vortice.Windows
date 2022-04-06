// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class ChromaKey : ID2D1Effect
{
    public ChromaKey(ID2D1DeviceContext context)
       : base(context.CreateEffect(EffectGuids.ChromaKey))
    {
    }

    public ChromaKey(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.ChromaKey))
    {
    }

    public Vector3 Color
    {
        set => SetValue((int)ChromakeyProperties.Color, value);
        get => GetVector3Value((int)ChromakeyProperties.Color);
    }

    public float Tolerance
    {
        set => SetValue((int)ChromakeyProperties.Tolerance, value);
        get => GetFloatValue((int)ChromakeyProperties.Tolerance);
    }

    public bool InvertAlpha
    {
        set => SetValue((int)ChromakeyProperties.InverseErtAlpha, value);
        get => GetBoolValue((int)ChromakeyProperties.InverseErtAlpha);
    }

    public bool Feather
    {
        set => SetValue((int)ChromakeyProperties.Feather, value);
        get => GetBoolValue((int)ChromakeyProperties.Feather);
    }
}
