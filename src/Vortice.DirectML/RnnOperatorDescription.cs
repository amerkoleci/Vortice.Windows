// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct RnnOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.Rnn;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription WeightTensor { get; set; }

    public TensorDescription RecurrenceTensor { get; set; }

    public TensorDescription? BiasTensor { get; set; }

    public TensorDescription? HiddenInitTensor { get; set; }

    public TensorDescription? SequenceLengthsTensor { get; set; }

    public TensorDescription? OutputSequenceTensor { get; set; }

    public TensorDescription? OutputSingleTensor { get; set; }

    public uint ActivationDescCount { get; set; }

    public OperatorDescription ActivationDescs { get; set; }

    public RecurrentNetworkDirection Direction { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr WeightTensor;
        public IntPtr RecurrenceTensor;
        public IntPtr BiasTensor;
        public IntPtr HiddenInitTensor;
        public IntPtr SequenceLengthsTensor;
        public IntPtr OutputSequenceTensor;
        public IntPtr OutputSingleTensor;
        public uint ActivationDescCount;
        public IntPtr ActivationDescs;
        public RecurrentNetworkDirection Direction;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->WeightTensor = WeightTensor.__MarshalAlloc();
        @ref->RecurrenceTensor = RecurrenceTensor.__MarshalAlloc();
        @ref->BiasTensor = (BiasTensor != null) ? BiasTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->HiddenInitTensor = (HiddenInitTensor != null) ? HiddenInitTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->SequenceLengthsTensor = (SequenceLengthsTensor != null) ? SequenceLengthsTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputSequenceTensor = (OutputSequenceTensor != null) ? OutputSequenceTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputSingleTensor = (OutputSingleTensor != null) ? OutputSingleTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->ActivationDescCount = ActivationDescCount;
        @ref->ActivationDescs = ActivationDescs.__MarshalAlloc();
        @ref->Direction = Direction;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        WeightTensor.__MarshalFree(ref @ref->WeightTensor);
        RecurrenceTensor.__MarshalFree(ref @ref->RecurrenceTensor);

        if (BiasTensor != null)
        {
            BiasTensor.Value.__MarshalFree(ref @ref->BiasTensor);
        }


        if (HiddenInitTensor != null)
        {
            HiddenInitTensor.Value.__MarshalFree(ref @ref->HiddenInitTensor);
        }


        if (SequenceLengthsTensor != null)
        {
            SequenceLengthsTensor.Value.__MarshalFree(ref @ref->SequenceLengthsTensor);
        }


        if (OutputSequenceTensor != null)
        {
            OutputSequenceTensor.Value.__MarshalFree(ref @ref->OutputSequenceTensor);
        }


        if (OutputSingleTensor != null)
        {
            OutputSingleTensor.Value.__MarshalFree(ref @ref->OutputSingleTensor);
        }

        ActivationDescs.__MarshalFree(ref @ref->ActivationDescs);
        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(RnnOperatorDescription description)
    {
        return new(description);
    }
}
