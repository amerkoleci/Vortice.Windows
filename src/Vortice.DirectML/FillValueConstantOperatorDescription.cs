// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct FillValueConstantOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.FillValueConstant;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_FILL_VALUE_CONSTANT_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_FILL_VALUE_CONSTANT_OPERATOR_DESC::ValueDataType']/*" />
    public TensorDataType ValueDataType { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_FILL_VALUE_CONSTANT_OPERATOR_DESC::Value']/*" />
    public ScalarUnion Value { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"FillValueConstant: ValueDataType={ValueDataType} Value={Value}";

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
