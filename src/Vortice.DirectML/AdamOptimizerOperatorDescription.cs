// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct AdamOptimizerOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public TensorDescription InputParametersTensor { get; }

    public TensorDescription InputFirstMomentTensor { get; }

    public TensorDescription InputSecondMomentTensor { get; }

    public TensorDescription GradientTensor { get; }

    public TensorDescription TrainingStepTensor { get; }

    public TensorDescription OutputParametersTensor { get; }

    public TensorDescription OutputFirstMomentTensor { get; }

    public TensorDescription OutputSecondMomentTensor { get; }

    public float LearningRate { get; }

    public float Beta1 { get; }

    public float Beta2 { get; }

    public float Epsilon { get; }

    public OperatorType OperatorType => OperatorType.AdamOptimizer;

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
