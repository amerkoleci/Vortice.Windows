// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct LocalResponseNormalizationOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.LocalResponseNormalization;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_OPERATOR_DESC::CrossChannel']/*" />
    public bool CrossChannel { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_OPERATOR_DESC::LocalSize']/*" />
    public int LocalSize { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_OPERATOR_DESC::Alpha']/*" />
    public float Alpha { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_OPERATOR_DESC::Beta']/*" />
    public float Beta { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_LOCAL_RESPONSE_NORMALIZATION_OPERATOR_DESC::Bias']/*" />
    public float Bias { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
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
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
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
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion
}
