// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

/// <include file="Documentation.xml" path="/comments/comment[@id='DML_GATHER_ND_OPERATOR_DESC']/*" />
public partial struct GatherNdOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator described.
    /// </summary>
    public OperatorType OperatorType => OperatorType.GatherNd;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GATHER_ND_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GATHER_ND_OPERATOR_DESC::IndicesTensor']/*" />
    public TensorDescription IndicesTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GATHER_ND_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GATHER_ND_OPERATOR_DESC::InputDimensionCount']/*" />
    public int InputDimensionCount { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GATHER_ND_OPERATOR_DESC::IndicesDimensionCount']/*" />
    public int IndicesDimensionCount { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr IndicesTensor;
        public IntPtr OutputTensor;
        public int InputDimensionCount;
        public int IndicesDimensionCount;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->IndicesTensor = IndicesTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->InputDimensionCount = InputDimensionCount;
        @ref->IndicesDimensionCount = IndicesDimensionCount;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        IndicesTensor.__MarshalFree(ref @ref->IndicesTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(GatherNdOperatorDescription description)
    {
        return new(description);
    }
}
