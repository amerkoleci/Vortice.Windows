// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct OneHotOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.OneHot;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ONE_HOT_OPERATOR_DESC::IndicesTensor']/*" />
    public TensorDescription IndicesTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ONE_HOT_OPERATOR_DESC::ValuesTensor']/*" />
    public TensorDescription ValuesTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ONE_HOT_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ONE_HOT_OPERATOR_DESC::Axis']/*" />
    public int Axis { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr IndicesTensor;
        public IntPtr ValuesTensor;
        public IntPtr OutputTensor;
        public int Axis;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->IndicesTensor = IndicesTensor.__MarshalAlloc();
        @ref->ValuesTensor = ValuesTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->Axis = Axis;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        IndicesTensor.__MarshalFree(ref @ref->IndicesTensor);
        ValuesTensor.__MarshalFree(ref @ref->ValuesTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion
}
