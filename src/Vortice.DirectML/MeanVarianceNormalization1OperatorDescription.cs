// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct MeanVarianceNormalization1OperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.MeanVarianceNormalization1;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription? ScaleTensor { get; set; }

    public TensorDescription? BiasTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    public uint AxisCount { get; set; }

    public uint[] Axes { get; set; }

    public bool NormalizeVariance { get; set; }

    public float Epsilon { get; set; }

    public OperatorDescription? FusedActivation { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr ScaleTensor;
        public IntPtr BiasTensor;
        public IntPtr OutputTensor;
        public uint AxisCount;
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
        @ref->AxisCount = AxisCount;
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
