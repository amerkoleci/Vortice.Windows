// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

/// <include file="Documentation.xml" path="/comments/comment[@id='DML_REDUCE_OPERATOR_DESC']/*" />
public partial struct ReduceOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.Reduce;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_REDUCE_OPERATOR_DESC::Function']/*" />
    public ReduceFunction Function { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_REDUCE_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_REDUCE_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_REDUCE_OPERATOR_DESC::Axes']/*" />
    public int[] Axes { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public ReduceFunction Function;
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public int AxisCount;
        public IntPtr Axes;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->Function = Function;
        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->AxisCount = Axes.Length;
        @ref->Axes = new(UnsafeUtilities.AllocWithData(Axes));

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        UnsafeUtilities.Free(@ref->Axes);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(ReduceOperatorDescription description)
    {
        return new(description);
    }
}
