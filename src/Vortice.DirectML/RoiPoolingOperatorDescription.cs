// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

/// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_POOLING_OPERATOR_DESC']/*" />
public partial struct RoiPoolingOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.RoiPooling;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_POOLING_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_POOLING_OPERATOR_DESC::ROITensor']/*" />
    public TensorDescription RoiTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_POOLING_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_POOLING_OPERATOR_DESC::SpatialScale']/*" />
    public float SpatialScale { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_POOLING_OPERATOR_DESC::PooledSize']/*" />
    public Size2D PooledSize { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr RoiTensor;
        public IntPtr OutputTensor;
        public float SpatialScale;
        public Size2D PooledSize;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->RoiTensor = RoiTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->SpatialScale = SpatialScale;
        @ref->PooledSize = PooledSize;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        RoiTensor.__MarshalFree(ref @ref->RoiTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(RoiPoolingOperatorDescription description)
    {
        return new(description);
    }
}
