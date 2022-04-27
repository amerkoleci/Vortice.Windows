// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ElementWiseQuantizedLinearAddOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.ElementWiseQuantizedLinearAdd;

    public TensorDescription ATensor { get; set; }

    public TensorDescription AScaleTensor { get; set; }

    public TensorDescription? AZeroPointTensor { get; set; }

    public TensorDescription BTensor { get; set; }

    public TensorDescription BScaleTensor { get; set; }

    public TensorDescription? BZeroPointTensor { get; set; }

    public TensorDescription OutputScaleTensor { get; set; }

    public TensorDescription? OutputZeroPointTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr ATensor;
        public IntPtr AScaleTensor;
        public IntPtr AZeroPointTensor;
        public IntPtr BTensor;
        public IntPtr BScaleTensor;
        public IntPtr BZeroPointTensor;
        public IntPtr OutputScaleTensor;
        public IntPtr OutputZeroPointTensor;
        public IntPtr OutputTensor;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->ATensor = ATensor.__MarshalAlloc();
        @ref->AScaleTensor = AScaleTensor.__MarshalAlloc();
        @ref->AZeroPointTensor = (AZeroPointTensor != null) ? AZeroPointTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->BTensor = BTensor.__MarshalAlloc();
        @ref->BScaleTensor = BScaleTensor.__MarshalAlloc();
        @ref->BZeroPointTensor = (BZeroPointTensor != null) ? BZeroPointTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputScaleTensor = OutputScaleTensor.__MarshalAlloc();
        @ref->OutputZeroPointTensor = (OutputZeroPointTensor != null) ? OutputZeroPointTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        ATensor.__MarshalFree(ref @ref->ATensor);
        AScaleTensor.__MarshalFree(ref @ref->AScaleTensor);

        if (AZeroPointTensor != null)
        {
            AZeroPointTensor.Value.__MarshalFree(ref @ref->AZeroPointTensor);
        }

        BTensor.__MarshalFree(ref @ref->BTensor);
        BScaleTensor.__MarshalFree(ref @ref->BScaleTensor);

        if (BZeroPointTensor != null)
        {
            BZeroPointTensor.Value.__MarshalFree(ref @ref->BZeroPointTensor);
        }

        OutputScaleTensor.__MarshalFree(ref @ref->OutputScaleTensor);

        if (OutputZeroPointTensor != null)
        {
            OutputZeroPointTensor.Value.__MarshalFree(ref @ref->OutputZeroPointTensor);
        }

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(ElementWiseQuantizedLinearAddOperatorDescription description)
    {
        return new(description);
    }
}
