// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ElementWiseClip1OperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.ElementWiseClip1;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_CLIP1_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_CLIP1_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_CLIP1_OPERATOR_DESC::ScaleBias']/*" />
    public ScaleBias? ScaleBias { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_CLIP1_OPERATOR_DESC::MinMaxDataType']/*" />
    public TensorDataType MinMaxDataType { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_CLIP1_OPERATOR_DESC::Min']/*" />
    public ScalarUnion Minimum { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_CLIP1_OPERATOR_DESC::Max']/*" />
    public ScalarUnion Maximum { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"ElementWiseClip1: MinMaxDataType={MinMaxDataType} Minimum={Minimum} Maximum={Maximum}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public IntPtr ScaleBias;
        public TensorDataType MinMaxDataType;
        public ScalarUnion Minimum;
        public ScalarUnion Maximum;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->ScaleBias = (ScaleBias != null) ? new(UnsafeUtilities.AllocWithData(ScaleBias.Value)) : IntPtr.Zero;
        @ref->MinMaxDataType = MinMaxDataType;
        @ref->Minimum = Minimum;
        @ref->Maximum = Maximum;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        if (@ref->ScaleBias != IntPtr.Zero)
        {
            UnsafeUtilities.Free(@ref->ScaleBias);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(ElementWiseClip1OperatorDescription description)
    {
        return new(description);
    }
}
