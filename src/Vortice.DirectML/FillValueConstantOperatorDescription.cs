// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct FillValueConstantOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.FillValueConstant;

    public TensorDescription OutputTensor { get; set; }

    public TensorDataType ValueDataType { get; set; }

    public ScalarUnion Value { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr OutputTensor;
        public TensorDataType ValueDataType;
        public ScalarUnion Value;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->ValueDataType = ValueDataType;
        @ref->Value = Value;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(FillValueConstantOperatorDescription description)
    {
        return new(description);
    }
}
