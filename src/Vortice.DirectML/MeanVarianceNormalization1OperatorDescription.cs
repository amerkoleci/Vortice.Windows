// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

/// <include file="Documentation.xml" path="/comments/comment[@id='DML_MEAN_VARIANCE_NORMALIZATION1_OPERATOR_DESC']/*" />
public partial struct MeanVarianceNormalization1OperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.MeanVarianceNormalization1;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MEAN_VARIANCE_NORMALIZATION1_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MEAN_VARIANCE_NORMALIZATION1_OPERATOR_DESC::ScaleTensor']/*" />
    public TensorDescription? ScaleTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MEAN_VARIANCE_NORMALIZATION1_OPERATOR_DESC::BiasTensor']/*" />
    public TensorDescription? BiasTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MEAN_VARIANCE_NORMALIZATION1_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MEAN_VARIANCE_NORMALIZATION1_OPERATOR_DESC::Axes']/*" />
    public int[] Axes { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MEAN_VARIANCE_NORMALIZATION1_OPERATOR_DESC::NormalizeVariance']/*" />
    public bool NormalizeVariance { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MEAN_VARIANCE_NORMALIZATION1_OPERATOR_DESC::Epsilon']/*" />
    public float Epsilon { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MEAN_VARIANCE_NORMALIZATION1_OPERATOR_DESC::FusedActivation']/*" />
    public OperatorDescription? FusedActivation { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr ScaleTensor;
        public IntPtr BiasTensor;
        public IntPtr OutputTensor;
        public int AxisCount;
        public IntPtr Axes;
        public bool NormalizeVariance;
        public float Epsilon;
        public IntPtr FusedActivation;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->ScaleTensor = (ScaleTensor != null) ? ScaleTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->BiasTensor = (BiasTensor != null) ? BiasTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->AxisCount = Axes.Length;
        @ref->Axes = new(UnsafeUtilities.AllocWithData(Axes));
        @ref->NormalizeVariance = NormalizeVariance;
        @ref->Epsilon = Epsilon;
        @ref->FusedActivation = (FusedActivation != null) ? FusedActivation.Value.__MarshalAlloc() : IntPtr.Zero;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);

        if (ScaleTensor != null)
        {
            ScaleTensor.Value.__MarshalFree(ref @ref->ScaleTensor);
        }

        if (BiasTensor != null)
        {
            BiasTensor.Value.__MarshalFree(ref @ref->BiasTensor);
        }

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        UnsafeUtilities.Free(@ref->Axes);

        if (FusedActivation != null)
        {
            FusedActivation.Value.__MarshalFree(ref @ref->FusedActivation);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(MeanVarianceNormalization1OperatorDescription description)
    {
        return new(description);
    }
}
