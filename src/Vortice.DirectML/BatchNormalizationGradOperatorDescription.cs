// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct BatchNormalizationGradOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.BatchNormalizationGrad;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_GRAD_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_GRAD_OPERATOR_DESC::InputGradientTensor']/*" />
    public TensorDescription InputGradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_GRAD_OPERATOR_DESC::MeanTensor']/*" />
    public TensorDescription MeanTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_GRAD_OPERATOR_DESC::VarianceTensor']/*" />
    public TensorDescription VarianceTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_GRAD_OPERATOR_DESC::ScaleTensor']/*" />
    public TensorDescription ScaleTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_GRAD_OPERATOR_DESC::OutputGradientTensor']/*" />
    public TensorDescription OutputGradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_GRAD_OPERATOR_DESC::OutputScaleGradientTensor']/*" />
    public TensorDescription OutputScaleGradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_GRAD_OPERATOR_DESC::OutputBiasGradientTensor']/*" />
    public TensorDescription OutputBiasGradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BATCH_NORMALIZATION_GRAD_OPERATOR_DESC::Epsilon']/*" />
    public float Epsilon { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"BatchNormalizationGrad: Epsilon={Epsilon}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr InputGradientTensor;
        public IntPtr MeanTensor;
        public IntPtr VarianceTensor;
        public IntPtr ScaleTensor;
        public IntPtr OutputGradientTensor;
        public IntPtr OutputScaleGradientTensor;
        public IntPtr OutputBiasGradientTensor;
        public float Epsilon;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->InputGradientTensor = InputGradientTensor.__MarshalAlloc();
        @ref->MeanTensor = MeanTensor.__MarshalAlloc();
        @ref->VarianceTensor = VarianceTensor.__MarshalAlloc();
        @ref->ScaleTensor = ScaleTensor.__MarshalAlloc();
        @ref->OutputGradientTensor = OutputGradientTensor.__MarshalAlloc();
        @ref->OutputScaleGradientTensor = OutputScaleGradientTensor.__MarshalAlloc();
        @ref->OutputBiasGradientTensor = OutputBiasGradientTensor.__MarshalAlloc();
        @ref->Epsilon = Epsilon;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        InputGradientTensor.__MarshalFree(ref @ref->InputGradientTensor);
        MeanTensor.__MarshalFree(ref @ref->MeanTensor);
        VarianceTensor.__MarshalFree(ref @ref->VarianceTensor);
        ScaleTensor.__MarshalFree(ref @ref->ScaleTensor);
        OutputGradientTensor.__MarshalFree(ref @ref->OutputGradientTensor);
        OutputScaleGradientTensor.__MarshalFree(ref @ref->OutputScaleGradientTensor);
        OutputBiasGradientTensor.__MarshalFree(ref @ref->OutputBiasGradientTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(BatchNormalizationGradOperatorDescription description)
    {
        return new(description);
    }
}
