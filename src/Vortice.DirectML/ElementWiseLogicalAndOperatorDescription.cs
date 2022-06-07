// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ElementWiseLogicalAndOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.ElementWiseLogicalAnd;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_LOGICAL_AND_OPERATOR_DESC::ATensor']/*" />
    public TensorDescription ATensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_LOGICAL_AND_OPERATOR_DESC::BTensor']/*" />
    public TensorDescription BTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_LOGICAL_AND_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr ATensor;
        public IntPtr BTensor;
        public IntPtr OutputTensor;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->ATensor = ATensor.__MarshalAlloc();
        @ref->BTensor = BTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        ATensor.__MarshalFree(ref @ref->ATensor);
        BTensor.__MarshalFree(ref @ref->BTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion
}
