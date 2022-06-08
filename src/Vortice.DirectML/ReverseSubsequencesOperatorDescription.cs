// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ReverseSubsequencesOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.ReverseSubsequences;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_REVERSE_SUBSEQUENCES_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_REVERSE_SUBSEQUENCES_OPERATOR_DESC::SequenceLengthsTensor']/*" />
    public TensorDescription SequenceLengthsTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_REVERSE_SUBSEQUENCES_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_REVERSE_SUBSEQUENCES_OPERATOR_DESC::Axis']/*" />
    public int Axis { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"ReverseSubsequences: Axis={Axis}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr SequenceLengthsTensor;
        public IntPtr OutputTensor;
        public int Axis;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->SequenceLengthsTensor = SequenceLengthsTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->Axis = Axis;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        SequenceLengthsTensor.__MarshalFree(ref @ref->SequenceLengthsTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(ReverseSubsequencesOperatorDescription description)
    {
        return new(description);
    }
}
