// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct GruOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.Gru;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRU_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRU_OPERATOR_DESC::WeightTensor']/*" />
    public TensorDescription WeightTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRU_OPERATOR_DESC::RecurrenceTensor']/*" />
    public TensorDescription RecurrenceTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRU_OPERATOR_DESC::BiasTensor']/*" />
    public TensorDescription? BiasTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRU_OPERATOR_DESC::HiddenInitTensor']/*" />
    public TensorDescription? HiddenInitializerTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRU_OPERATOR_DESC::SequenceLengthsTensor']/*" />
    public TensorDescription? SequenceLengthsTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRU_OPERATOR_DESC::OutputSequenceTensor']/*" />
    public TensorDescription? OutputSequenceTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRU_OPERATOR_DESC::OutputSingleTensor']/*" />
    public TensorDescription? OutputSingleTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRU_OPERATOR_DESC::ActivationDescs']/*" />
    public OperatorDescription[] Activations { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRU_OPERATOR_DESC::Direction']/*" />
    public RecurrentNetworkDirection Direction { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRU_OPERATOR_DESC::LinearBeforeReset']/*" />
    public bool LinearBeforeReset { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr WeightTensor;
        public IntPtr RecurrenceTensor;
        public IntPtr BiasTensor;
        public IntPtr HiddenInitializerTensor;
        public IntPtr SequenceLengthsTensor;
        public IntPtr OutputSequenceTensor;
        public IntPtr OutputSingleTensor;
        public int ActivationDescCount;
        public IntPtr Activations;
        public RecurrentNetworkDirection Direction;
        public bool LinearBeforeReset;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->WeightTensor = WeightTensor.__MarshalAlloc();
        @ref->RecurrenceTensor = RecurrenceTensor.__MarshalAlloc();
        @ref->BiasTensor = (BiasTensor != null) ? BiasTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->HiddenInitializerTensor = (HiddenInitializerTensor != null) ? HiddenInitializerTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->SequenceLengthsTensor = (SequenceLengthsTensor != null) ? SequenceLengthsTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputSequenceTensor = (OutputSequenceTensor != null) ? OutputSequenceTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputSingleTensor = (OutputSingleTensor != null) ? OutputSingleTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->ActivationDescCount = Activations.Length;

        @ref->Activations = IntPtr.Zero;
        if (Activations.Length != 0)
        {
            var activationDescsPtr = UnsafeUtilities.Alloc<OperatorDescription.__Native>(Activations.Length);
            for (int i = 0; i < Activations.Length; i++)
            {
                Activations[i].__MarshalTo(ref activationDescsPtr[i]);
            }
            @ref->Activations = new(activationDescsPtr);
        }

        @ref->Direction = Direction;
        @ref->LinearBeforeReset = LinearBeforeReset;

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

        if (HiddenInitializerTensor != null)
        {
            HiddenInitializerTensor.Value.__MarshalFree(ref @ref->HiddenInitializerTensor);
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


        if (@ref->Activations != IntPtr.Zero)
        {
            var activationDescsPtr = (OperatorDescription.__Native*)@ref->Activations;
            for (int i = 0; i < Activations.Length; i++)
            {
                Activations[i].__MarshalFree(ref activationDescsPtr[i]);
            }
            UnsafeUtilities.Free(@ref->Activations);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(GruOperatorDescription description)
    {
        return new(description);
    }
}
