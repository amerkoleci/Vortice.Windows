// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

/// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_MATRIX_MULTIPLY_OPERATOR_DESC']/*" />
public partial struct QuantizedLinearMatrixMultiplyOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator described.
    /// </summary>
    public OperatorType OperatorType => OperatorType.QuantizedLinearMatrixMultiply;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_MATRIX_MULTIPLY_OPERATOR_DESC::ATensor']/*" />
    public TensorDescription ATensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_MATRIX_MULTIPLY_OPERATOR_DESC::AScaleTensor']/*" />
    public TensorDescription AScaleTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_MATRIX_MULTIPLY_OPERATOR_DESC::AZeroPointTensor']/*" />
    public TensorDescription? AZeroPointTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_MATRIX_MULTIPLY_OPERATOR_DESC::BTensor']/*" />
    public TensorDescription BTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_MATRIX_MULTIPLY_OPERATOR_DESC::BScaleTensor']/*" />
    public TensorDescription BScaleTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_MATRIX_MULTIPLY_OPERATOR_DESC::BZeroPointTensor']/*" />
    public TensorDescription? BZeroPointTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_MATRIX_MULTIPLY_OPERATOR_DESC::OutputScaleTensor']/*" />
    public TensorDescription OutputScaleTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_MATRIX_MULTIPLY_OPERATOR_DESC::OutputZeroPointTensor']/*" />
    public TensorDescription? OutputZeroPointTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_MATRIX_MULTIPLY_OPERATOR_DESC::OutputTensor']/*" />
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

    public static implicit operator OperatorDescription(QuantizedLinearMatrixMultiplyOperatorDescription description)
    {
        return new(description);
    }
}
