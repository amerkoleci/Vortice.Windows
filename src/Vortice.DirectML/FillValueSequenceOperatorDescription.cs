// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct FillValueSequenceOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.FillValueSequence;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_FILL_VALUE_SEQUENCE_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_FILL_VALUE_SEQUENCE_OPERATOR_DESC::ValueDataType']/*" />
    public TensorDataType ValueDataType { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_FILL_VALUE_SEQUENCE_OPERATOR_DESC::ValueStart']/*" />
    public ScalarUnion ValueStart { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_FILL_VALUE_SEQUENCE_OPERATOR_DESC::ValueDelta']/*" />
    public ScalarUnion ValueDelta { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"FillValueSequence: ValueDataType={ValueDataType} ValueStart={ValueStart} ValueDelta={ValueDelta}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr OutputTensor;
        public TensorDataType ValueDataType;
        public ScalarUnion ValueStart;
        public ScalarUnion ValueDelta;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->ValueDataType = ValueDataType;
        @ref->ValueStart = ValueStart;
        @ref->ValueDelta = ValueDelta;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(FillValueSequenceOperatorDescription description)
    {
        return new(description);
    }
}
