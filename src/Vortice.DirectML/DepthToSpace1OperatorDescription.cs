// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct DepthToSpace1OperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.DepthToSpace1;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_DEPTH_TO_SPACE1_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_DEPTH_TO_SPACE1_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_DEPTH_TO_SPACE1_OPERATOR_DESC::BlockSize']/*" />
    public int BlockSize { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_DEPTH_TO_SPACE1_OPERATOR_DESC::Order']/*" />
    public DepthSpaceOrder Order { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"DepthToSpace1: BlockSize={BlockSize} Order={Order}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public int BlockSize;
        public DepthSpaceOrder Order;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->BlockSize = BlockSize;
        @ref->Order = Order;

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

    public static implicit operator OperatorDescription(DepthToSpace1OperatorDescription description)
    {
        return new(description);
    }
}
