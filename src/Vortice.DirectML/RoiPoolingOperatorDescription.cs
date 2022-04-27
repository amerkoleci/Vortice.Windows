// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct RoiPoolingOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.RoiPooling;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription ROITensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    public float SpatialScale { get; set; }

    public Size2D PooledSize { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr ROITensor;
        public IntPtr OutputTensor;
        public float SpatialScale;
        public Size2D PooledSize;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->ROITensor = ROITensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->SpatialScale = SpatialScale;
        @ref->PooledSize = PooledSize;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        ROITensor.__MarshalFree(ref @ref->ROITensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(RoiPoolingOperatorDescription description)
    {
        return new(description);
    }
}
