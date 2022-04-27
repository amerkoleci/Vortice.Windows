// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct BatchNormalizationOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.BatchNormalization;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription MeanTensor { get; set; }

    public TensorDescription VarianceTensor { get; set; }

    public TensorDescription ScaleTensor { get; set; }

    public TensorDescription BiasTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    public bool Spatial { get; set; }

    public float Epsilon { get; set; }

    public OperatorDescription? FusedActivation { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr MeanTensor;
        public IntPtr VarianceTensor;
        public IntPtr ScaleTensor;
        public IntPtr BiasTensor;
        public IntPtr OutputTensor;
        public bool Spatial;
        public float Epsilon;
        public IntPtr FusedActivation;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->MeanTensor = MeanTensor.__MarshalAlloc();
        @ref->VarianceTensor = VarianceTensor.__MarshalAlloc();
        @ref->ScaleTensor = ScaleTensor.__MarshalAlloc();
        @ref->BiasTensor = BiasTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->Spatial = Spatial;
        @ref->Epsilon = Epsilon;
        @ref->FusedActivation = (FusedActivation != null) ? FusedActivation.__MarshalAlloc() : IntPtr.Zero;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        MeanTensor.__MarshalFree(ref @ref->MeanTensor);
        VarianceTensor.__MarshalFree(ref @ref->VarianceTensor);
        ScaleTensor.__MarshalFree(ref @ref->ScaleTensor);
        BiasTensor.__MarshalFree(ref @ref->BiasTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        if (FusedActivation != null)
        {
            FusedActivation.__MarshalFree(ref @ref->FusedActivation);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(BatchNormalizationOperatorDescription description)
    {
        return new(description);
    }
}
