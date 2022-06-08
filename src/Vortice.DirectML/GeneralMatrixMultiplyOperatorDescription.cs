// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct GeneralMatrixMultiplyOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.GeneralMatrixMultiply;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GEMM_OPERATOR_DESC::ATensor']/*" />
    public TensorDescription ATensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GEMM_OPERATOR_DESC::BTensor']/*" />
    public TensorDescription BTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GEMM_OPERATOR_DESC::CTensor']/*" />
    public TensorDescription? CTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GEMM_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GEMM_OPERATOR_DESC::TransA']/*" />
    public MatrixTransform TransformA { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GEMM_OPERATOR_DESC::TransB']/*" />
    public MatrixTransform TransformB { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GEMM_OPERATOR_DESC::Alpha']/*" />
    public float Alpha { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GEMM_OPERATOR_DESC::Beta']/*" />
    public float Beta { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GEMM_OPERATOR_DESC::FusedActivation']/*" />
    public OperatorDescription? FusedActivation { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"GeneralMatrixMultiply: TransformA={TransformA} TransformB={TransformB} Alpha={Alpha} Beta={Beta}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr ATensor;
        public IntPtr BTensor;
        public IntPtr CTensor;
        public IntPtr OutputTensor;
        public MatrixTransform TransformA;
        public MatrixTransform TransformB;
        public float Alpha;
        public float Beta;
        public IntPtr FusedActivation;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->ATensor = ATensor.__MarshalAlloc();
        @ref->BTensor = BTensor.__MarshalAlloc();
        @ref->CTensor = (CTensor != null) ? CTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->TransformA = TransformA;
        @ref->TransformB = TransformB;
        @ref->Alpha = Alpha;
        @ref->Beta = Beta;
        @ref->FusedActivation = (FusedActivation != null) ? FusedActivation.Value.__MarshalAlloc() : IntPtr.Zero;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        ATensor.__MarshalFree(ref @ref->ATensor);
        BTensor.__MarshalFree(ref @ref->BTensor);

        if (CTensor != null)
        {
            CTensor.Value.__MarshalFree(ref @ref->CTensor);
        }

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        if (FusedActivation != null)
        {
            FusedActivation.Value.__MarshalFree(ref @ref->FusedActivation);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(GeneralMatrixMultiplyOperatorDescription description)
    {
        return new(description);
    }
}
