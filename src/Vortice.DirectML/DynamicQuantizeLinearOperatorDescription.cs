// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct DynamicQuantizeLinearOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.DynamicQuantizeLinear;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    public TensorDescription OutputScaleTensor { get; set; }

    public TensorDescription OutputZeroPointTensor { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public IntPtr OutputScaleTensor;
        public IntPtr OutputZeroPointTensor;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->OutputScaleTensor = OutputScaleTensor.__MarshalAlloc();
        @ref->OutputZeroPointTensor = OutputZeroPointTensor.__MarshalAlloc();

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        OutputScaleTensor.__MarshalFree(ref @ref->OutputScaleTensor);
        OutputZeroPointTensor.__MarshalFree(ref @ref->OutputZeroPointTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(DynamicQuantizeLinearOperatorDescription description)
    {
        return new(description);
    }
}
