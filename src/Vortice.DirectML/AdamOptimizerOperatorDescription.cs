// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct AdamOptimizerOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.AdamOptimizer;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ADAM_OPTIMIZER_OPERATOR_DESC::InputParametersTensor']/*" />
    public TensorDescription InputParametersTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ADAM_OPTIMIZER_OPERATOR_DESC::InputFirstMomentTensor']/*" />
    public TensorDescription InputFirstMomentTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ADAM_OPTIMIZER_OPERATOR_DESC::InputSecondMomentTensor']/*" />
    public TensorDescription InputSecondMomentTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ADAM_OPTIMIZER_OPERATOR_DESC::GradientTensor']/*" />
    public TensorDescription GradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ADAM_OPTIMIZER_OPERATOR_DESC::TrainingStepTensor']/*" />
    public TensorDescription TrainingStepTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ADAM_OPTIMIZER_OPERATOR_DESC::OutputParametersTensor']/*" />
    public TensorDescription OutputParametersTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ADAM_OPTIMIZER_OPERATOR_DESC::OutputFirstMomentTensor']/*" />
    public TensorDescription OutputFirstMomentTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ADAM_OPTIMIZER_OPERATOR_DESC::OutputSecondMomentTensor']/*" />
    public TensorDescription OutputSecondMomentTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ADAM_OPTIMIZER_OPERATOR_DESC::LearningRate']/*" />
    public float LearningRate { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ADAM_OPTIMIZER_OPERATOR_DESC::Beta1']/*" />
    public float Beta1 { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ADAM_OPTIMIZER_OPERATOR_DESC::Beta2']/*" />
    public float Beta2 { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ADAM_OPTIMIZER_OPERATOR_DESC::Epsilon']/*" />
    public float Epsilon { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"AdamOptimizer: LearningRate={LearningRate} Beta1={Beta1} Beta2={Beta2} Epsilon={Epsilon}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputParametersTensor;
        public IntPtr InputFirstMomentTensor;
        public IntPtr InputSecondMomentTensor;
        public IntPtr GradientTensor;
        public IntPtr TrainingStepTensor;
        public IntPtr OutputParametersTensor;
        public IntPtr OutputFirstMomentTensor;
        public IntPtr OutputSecondMomentTensor;
        public float LearningRate;
        public float Beta1;
        public float Beta2;
        public float Epsilon;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputParametersTensor = InputParametersTensor.__MarshalAlloc();
        @ref->InputFirstMomentTensor = InputFirstMomentTensor.__MarshalAlloc();
        @ref->InputSecondMomentTensor = InputSecondMomentTensor.__MarshalAlloc();
        @ref->GradientTensor = GradientTensor.__MarshalAlloc();
        @ref->TrainingStepTensor = TrainingStepTensor.__MarshalAlloc();
        @ref->OutputParametersTensor = OutputParametersTensor.__MarshalAlloc();
        @ref->OutputFirstMomentTensor = OutputFirstMomentTensor.__MarshalAlloc();
        @ref->OutputSecondMomentTensor = OutputSecondMomentTensor.__MarshalAlloc();
        @ref->LearningRate = LearningRate;
        @ref->Beta1 = Beta1;
        @ref->Beta2 = Beta2;
        @ref->Epsilon = Epsilon;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputParametersTensor.__MarshalFree(ref @ref->InputParametersTensor);
        InputFirstMomentTensor.__MarshalFree(ref @ref->InputFirstMomentTensor);
        InputSecondMomentTensor.__MarshalFree(ref @ref->InputSecondMomentTensor);
        GradientTensor.__MarshalFree(ref @ref->GradientTensor);
        TrainingStepTensor.__MarshalFree(ref @ref->TrainingStepTensor);
        OutputParametersTensor.__MarshalFree(ref @ref->OutputParametersTensor);
        OutputFirstMomentTensor.__MarshalFree(ref @ref->OutputFirstMomentTensor);
        OutputSecondMomentTensor.__MarshalFree(ref @ref->OutputSecondMomentTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(AdamOptimizerOperatorDescription description)
    {
        return new(description);
    }
}
