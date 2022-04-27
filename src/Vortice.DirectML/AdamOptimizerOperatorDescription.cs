// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct AdamOptimizerOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.AdamOptimizer;

    public TensorDescription InputParametersTensor { get; set; }

    public TensorDescription InputFirstMomentTensor { get; set; }

    public TensorDescription InputSecondMomentTensor { get; set; }

    public TensorDescription GradientTensor { get; set; }

    public TensorDescription TrainingStepTensor { get; set; }

    public TensorDescription OutputParametersTensor { get; set; }

    public TensorDescription OutputFirstMomentTensor { get; set; }

    public TensorDescription OutputSecondMomentTensor { get; set; }

    public float LearningRate { get; set; }

    public float Beta1 { get; set; }

    public float Beta2 { get; set; }

    public float Epsilon { get; set; }

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
