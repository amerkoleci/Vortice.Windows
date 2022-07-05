// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

public partial class IUIAnimationVariable2
{
    public double[] PreviousVectorValue
    {
        get
        {
            double[] result = new double[Dimension];
            GetPreviousVectorValue(result, result.Length);
            return result;
        }
    }

    public double[] VectorValue
    {
        get
        {
            double[] result = new double[Dimension];
            GetVectorValue(result, result.Length);
            return result;
        }
    }

    public int[] PreviousIntegerVectorValue
    {
        get
        {
            int[] result = new int[Dimension];
            GetPreviousIntegerVectorValue(result, result.Length);
            return result;
        }
    }

    public int[] FinalIntegerVectorValue
    {
        get
        {
            int[] result = new int[Dimension];
            GetFinalIntegerVectorValue(result, result.Length);
            return result;
        }
    }
}
