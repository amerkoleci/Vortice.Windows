// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

/// <include file="Documentation.xml" path="/comments/comment[@id='DML_UPSAMPLE_2D_OPERATOR_DESC']/*" />
public partial struct Upsample2DOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.Upsample2D;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_UPSAMPLE_2D_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_UPSAMPLE_2D_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_UPSAMPLE_2D_OPERATOR_DESC::ScaleSize']/*" />
    public Size2D ScaleSize { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_UPSAMPLE_2D_OPERATOR_DESC::InterpolationMode']/*" />
    public InterpolationMode InterpolationMode { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public Size2D ScaleSize;
        public InterpolationMode InterpolationMode;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->ScaleSize = ScaleSize;
        @ref->InterpolationMode = InterpolationMode;

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

    public static implicit operator OperatorDescription(Upsample2DOperatorDescription description)
    {
        return new(description);
    }
}
