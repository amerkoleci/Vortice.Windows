// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ElementWiseThresholdOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.ElementWiseThreshold;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_THRESHOLD_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_THRESHOLD_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_THRESHOLD_OPERATOR_DESC::ScaleBias']/*" />
    public ScaleBias? ScaleBias { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ELEMENT_WISE_THRESHOLD_OPERATOR_DESC::Min']/*" />
    public float Minimum { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public IntPtr ScaleBias;
        public float Minimum;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->ScaleBias = (ScaleBias != null) ? new(UnsafeUtilities.AllocWithData(ScaleBias.Value)) : IntPtr.Zero;
        @ref->Minimum = Minimum;

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
}
