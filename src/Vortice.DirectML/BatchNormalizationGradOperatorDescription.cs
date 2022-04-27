// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct BatchNormalizationGradOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.BatchNormalizationGrad;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription InputGradientTensor { get; set; }

    public TensorDescription MeanTensor { get; set; }

    public TensorDescription VarianceTensor { get; set; }

    public TensorDescription ScaleTensor { get; set; }

    public TensorDescription OutputGradientTensor { get; set; }

    public TensorDescription OutputScaleGradientTensor { get; set; }

    public TensorDescription OutputBiasGradientTensor { get; set; }

    public float Epsilon { get; set; }

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
