// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct BatchNormalizationTrainingOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.BatchNormalizationTraining;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_TRAINING_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_TRAINING_OPERATOR_DESC::ScaleTensor']/*" />
    public TensorDescription ScaleTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_TRAINING_OPERATOR_DESC::BiasTensor']/*" />
    public TensorDescription BiasTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_TRAINING_OPERATOR_DESC::FusedAddTensor']/*" />
    public TensorDescription? FusedAddTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_TRAINING_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_TRAINING_OPERATOR_DESC::OutputMeanTensor']/*" />
    public TensorDescription OutputMeanTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_TRAINING_OPERATOR_DESC::OutputVarianceTensor']/*" />
    public TensorDescription OutputVarianceTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_TRAINING_OPERATOR_DESC::Epsilon']/*" />
    public float Epsilon { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_TRAINING_OPERATOR_DESC::FusedActivation']/*" />
    public OperatorDescription? FusedActivation { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"BatchNormalizationTraining: Epsilon={Epsilon}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr ScaleTensor;
        public IntPtr BiasTensor;
        public IntPtr FusedAddTensor;
        public IntPtr OutputTensor;
        public IntPtr OutputMeanTensor;
        public IntPtr OutputVarianceTensor;
        public float Epsilon;
        public IntPtr FusedActivation;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->ScaleTensor = ScaleTensor.__MarshalAlloc();
        @ref->BiasTensor = BiasTensor.__MarshalAlloc();
        @ref->FusedAddTensor = (FusedAddTensor != null) ? FusedAddTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->OutputMeanTensor = OutputMeanTensor.__MarshalAlloc();
        @ref->OutputVarianceTensor = OutputVarianceTensor.__MarshalAlloc();
        @ref->Epsilon = Epsilon;
        @ref->FusedActivation = (FusedActivation != null) ? FusedActivation.Value.__MarshalAlloc() : IntPtr.Zero;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        ScaleTensor.__MarshalFree(ref @ref->ScaleTensor);
        BiasTensor.__MarshalFree(ref @ref->BiasTensor);

        if (FusedAddTensor != null)
        {
            FusedAddTensor.Value.__MarshalFree(ref @ref->FusedAddTensor);
        }

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        OutputMeanTensor.__MarshalFree(ref @ref->OutputMeanTensor);
        OutputVarianceTensor.__MarshalFree(ref @ref->OutputVarianceTensor);

        if (FusedActivation != null)
        {
            FusedActivation.Value.__MarshalFree(ref @ref->FusedActivation);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(BatchNormalizationTrainingOperatorDescription description)
    {
        return new(description);
    }
}
