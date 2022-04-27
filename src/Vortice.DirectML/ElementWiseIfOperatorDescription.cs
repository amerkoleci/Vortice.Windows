// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ElementWiseIfOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.ElementWiseIf;

    public TensorDescription ConditionTensor { get; set; }

    public TensorDescription ATensor { get; set; }

    public TensorDescription BTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr ConditionTensor;
        public IntPtr ATensor;
        public IntPtr BTensor;
        public IntPtr OutputTensor;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->ConditionTensor = ConditionTensor.__MarshalAlloc();
        @ref->ATensor = ATensor.__MarshalAlloc();
        @ref->BTensor = BTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        ConditionTensor.__MarshalFree(ref @ref->ConditionTensor);
        ATensor.__MarshalFree(ref @ref->ATensor);
        BTensor.__MarshalFree(ref @ref->BTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(ElementWiseIfOperatorDescription description)
    {
        return new(description);
    }
}
