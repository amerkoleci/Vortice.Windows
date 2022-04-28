// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

/// <include file="Documentation.xml" path="/comments/comment[@id='DML_TOP_K1_OPERATOR_DESC']/*" />
public partial struct TopK1OperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.TopK1;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_TOP_K1_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_TOP_K1_OPERATOR_DESC::OutputValueTensor']/*" />
    public TensorDescription OutputValueTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_TOP_K1_OPERATOR_DESC::OutputIndexTensor']/*" />
    public TensorDescription OutputIndexTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_TOP_K1_OPERATOR_DESC::Axis']/*" />
    public int Axis { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_TOP_K1_OPERATOR_DESC::K']/*" />
    public int K { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_TOP_K1_OPERATOR_DESC::AxisDirection']/*" />
    public AxisDirection AxisDirection { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputValueTensor;
        public IntPtr OutputIndexTensor;
        public int Axis;
        public int K;
        public AxisDirection AxisDirection;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputValueTensor = OutputValueTensor.__MarshalAlloc();
        @ref->OutputIndexTensor = OutputIndexTensor.__MarshalAlloc();
        @ref->Axis = Axis;
        @ref->K = K;
        @ref->AxisDirection = AxisDirection;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputValueTensor.__MarshalFree(ref @ref->OutputValueTensor);
        OutputIndexTensor.__MarshalFree(ref @ref->OutputIndexTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(TopK1OperatorDescription description)
    {
        return new(description);
    }
}
