// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct CumulativeSummationOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.CumulativeSummation;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CUMULATIVE_SUMMATION_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CUMULATIVE_SUMMATION_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CUMULATIVE_SUMMATION_OPERATOR_DESC::Axis']/*" />
    public int Axis { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CUMULATIVE_SUMMATION_OPERATOR_DESC::AxisDirection']/*" />
    public AxisDirection AxisDirection { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CUMULATIVE_SUMMATION_OPERATOR_DESC::HasExclusiveSum']/*" />
    public bool HasExclusiveSum { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public int Axis;
        public AxisDirection AxisDirection;
        public bool HasExclusiveSum;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->Axis = Axis;
        @ref->AxisDirection = AxisDirection;
        @ref->HasExclusiveSum = HasExclusiveSum;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(CumulativeSummationOperatorDescription description)
    {
        return new(description);
    }
}
