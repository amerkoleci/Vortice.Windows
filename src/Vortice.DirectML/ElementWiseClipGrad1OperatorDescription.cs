// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ElementWiseClipGrad1OperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.ElementWiseClipGrad1;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_CLIP_GRAD1_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_CLIP_GRAD1_OPERATOR_DESC::InputGradientTensor']/*" />
    public TensorDescription InputGradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_CLIP_GRAD1_OPERATOR_DESC::OutputGradientTensor']/*" />
    public TensorDescription OutputGradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_CLIP_GRAD1_OPERATOR_DESC::MinMaxDataType']/*" />
    public TensorDataType MinMaxDataType { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_CLIP_GRAD1_OPERATOR_DESC::Min']/*" />
    public ScalarUnion Minimum { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_CLIP_GRAD1_OPERATOR_DESC::Max']/*" />
    public ScalarUnion Maximum { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"ElementWiseClipGrad1: MinMaxDataType={MinMaxDataType} Minimum={Minimum} Maximum={Maximum}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr InputGradientTensor;
        public IntPtr OutputGradientTensor;
        public TensorDataType MinMaxDataType;
        public ScalarUnion Minimum;
        public ScalarUnion Maximum;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->InputGradientTensor = InputGradientTensor.__MarshalAlloc();
        @ref->OutputGradientTensor = OutputGradientTensor.__MarshalAlloc();
        @ref->MinMaxDataType = MinMaxDataType;
        @ref->Minimum = Minimum;
        @ref->Maximum = Maximum;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        InputGradientTensor.__MarshalFree(ref @ref->InputGradientTensor);
        OutputGradientTensor.__MarshalFree(ref @ref->OutputGradientTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(ElementWiseClipGrad1OperatorDescription description)
    {
        return new(description);
    }
}
