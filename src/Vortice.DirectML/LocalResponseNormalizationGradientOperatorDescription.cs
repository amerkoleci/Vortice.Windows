// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

/// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_GRAD_OPERATOR_DESC']/*" />
public partial struct LocalResponseNormalizationGradientOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator described.
    /// </summary>
    public OperatorType OperatorType => OperatorType.LocalResponseNormalizationGradient;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_GRAD_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_GRAD_OPERATOR_DESC::InputGradientTensor']/*" />
    public TensorDescription InputGradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_GRAD_OPERATOR_DESC::OutputGradientTensor']/*" />
    public TensorDescription OutputGradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_GRAD_OPERATOR_DESC::CrossChannel']/*" />
    public bool CrossChannel { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_GRAD_OPERATOR_DESC::LocalSize']/*" />
    public int LocalSize { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_GRAD_OPERATOR_DESC::Alpha']/*" />
    public float Alpha { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_GRAD_OPERATOR_DESC::Beta']/*" />
    public float Beta { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_GRAD_OPERATOR_DESC::Bias']/*" />
    public float Bias { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr InputGradientTensor;
        public IntPtr OutputGradientTensor;
        public bool CrossChannel;
        public int LocalSize;
        public float Alpha;
        public float Beta;
        public float Bias;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->InputGradientTensor = InputGradientTensor.__MarshalAlloc();
        @ref->OutputGradientTensor = OutputGradientTensor.__MarshalAlloc();
        @ref->CrossChannel = CrossChannel;
        @ref->LocalSize = LocalSize;
        @ref->Alpha = Alpha;
        @ref->Beta = Beta;
        @ref->Bias = Bias;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        InputGradientTensor.__MarshalFree(ref @ref->InputGradientTensor);
        OutputGradientTensor.__MarshalFree(ref @ref->OutputGradientTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(LocalResponseNormalizationGradientOperatorDescription description)
    {
        return new(description);
    }
}
