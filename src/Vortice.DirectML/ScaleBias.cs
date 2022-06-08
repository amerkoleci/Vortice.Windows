// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ScaleBias
{
    /// <summary>
    /// Construts a <see cref="ScaleBias"/> with the given scale and bias.
    /// </summary>
    /// <param name="scale"></param>
    /// <param name="bias"></param>
    public ScaleBias(float scale = 1.0f, float bias = 0.0f)
    {
        Scale = scale;
        Bias = bias;
    }

    ///<inheritdoc></inheritdoc>
    public override string ToString() => $"Scale={Scale} Bias={Bias}";
}
