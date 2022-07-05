// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.UIAnimation;

public partial class IUIAnimationVariable2
{
    /// <unmanaged>HRESULT IUIAnimationVariable2::GetFinalIntegerVectorValue([Out, Buffer] int* finalValue, [In] UINT cDimension)</unmanaged>
    /// <unmanaged-short>IUIAnimationVariable2::GetFinalIntegerVectorValue</unmanaged-short>
    public int[] FinalIntegerVectorValue
    {
        get
        {
            int[] result = new int[Dimension];
            GetFinalIntegerVectorValue(result);
            return result;
        }
    }

    /// <unmanaged>HRESULT IUIAnimationVariable2::GetFinalVectorValue([Out, Buffer] double* finalValue, [In] UINT cDimension)</unmanaged>
    /// <unmanaged-short>IUIAnimationVariable2::GetFinalVectorValue</unmanaged-short>
    public double[] FinalVectorValue
    {
        get
        {
            double[] result = new double[Dimension];
            GetFinalVectorValue(result);
            return result;
        }
    }

    /// <unmanaged>HRESULT IUIAnimationVariable2::GetIntegerVectorValue([Out, Buffer] int* value, [In] UINT cDimension)</unmanaged>
    /// <unmanaged-short>IUIAnimationVariable2::GetIntegerVectorValue</unmanaged-short>
    public int[] IntegerVectorValue
    {
        get
        {
            int[] result = new int[Dimension];
            GetIntegerVectorValue(result);
            return result;
        }
    }

    /// <unmanaged>HRESULT IUIAnimationVariable2::GetPreviousIntegerVectorValue([Out, Buffer] int* previousValue, [In] UINT cDimension)</unmanaged>
    /// <unmanaged-short>IUIAnimationVariable2::GetPreviousIntegerVectorValue</unmanaged-short>
    public int[] PreviousIntegerVectorValue
    {
        get
        {
            int[] result = new int[Dimension];
            GetPreviousIntegerVectorValue(result);
            return result;
        }
    }

    /// <unmanaged>HRESULT IUIAnimationVariable2::GetPreviousVectorValue([Out, Buffer] double* previousValue, [In] UINT cDimension)</unmanaged>
    /// <unmanaged-short>IUIAnimationVariable2::GetPreviousVectorValue</unmanaged-short>
    public double[] PreviousVectorValue
    {
        get
        {
            double[] result = new double[Dimension];
            GetPreviousVectorValue(result);
            return result;
        }
    }

    /// <unmanaged>HRESULT IUIAnimationVariable2::GetVectorCurve([In, Buffer] IDCompositionAnimation** animation, [In] UINT cDimension)</unmanaged>
    /// <unmanaged-short>IUIAnimationVariable2::GetVectorCurve</unmanaged-short>
    public IUnknown[] VectorCurve
    {
        get
        {
            IUnknown[] result = new IUnknown[Dimension];
            GetVectorCurve(result);
            return result;
        }
    }

    /// <unmanaged>HRESULT IUIAnimationVariable2::GetVectorValue([Out, Buffer] double* value, [In] UINT cDimension)</unmanaged>
    /// <unmanaged-short>IUIAnimationVariable2::GetVectorValue</unmanaged-short>
    public double[] VectorValue
    {
        get
        {
            double[] result = new double[Dimension];
            GetVectorValue(result);
            return result;
        }
    }
}
