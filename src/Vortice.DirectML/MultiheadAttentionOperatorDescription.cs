// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct MultiheadAttentionOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.MultiheadAttention;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::QueryTensor']/*" />
    public TensorDescription? QueryTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::KeyTensor']/*" />
    public TensorDescription? KeyTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::ValueTensor']/*" />
    public TensorDescription? ValueTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::StackedQueryKeyTensor']/*" />
    public TensorDescription? StackedQueryKeyTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::StackedKeyValueTensor']/*" />
    public TensorDescription? StackedKeyValueTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::StackedQueryKeyValueTensor']/*" />
    public TensorDescription? StackedQueryKeyValueTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::BiasTensor']/*" />
    public TensorDescription? BiasTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::MaskTensor']/*" />
    public TensorDescription? MaskTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::RelativePositionBiasTensor']/*" />
    public TensorDescription? RelativePositionBiasTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::PastKeyTensor']/*" />
    public TensorDescription? PastKeyTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::PastValueTensor']/*" />
    public TensorDescription? PastValueTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::OutputPresentKeyTensor']/*" />
    public TensorDescription? OutputPresentKeyTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::OutputPresentValueTensor']/*" />
    public TensorDescription? OutputPresentValueTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::Scale']/*" />
    public float Scale { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::MaskFilterValue']/*" />
    public float MaskFilterValue { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::HeadCount']/*" />
    public int HeadCount { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_MULTIHEAD_ATTENTION_OPERATOR_DESC::MaskType']/*" />
    public MultiheadAttentionMaskType MaskType { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"MultiheadAttention: Scale={Scale} MaskFilterValue={MaskFilterValue} HeadCount={HeadCount} MaskType={MaskType}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr QueryTensor;
        public IntPtr KeyTensor;
        public IntPtr ValueTensor;
        public IntPtr StackedQueryKeyTensor;
        public IntPtr StackedKeyValueTensor;
        public IntPtr StackedQueryKeyValueTensor;
        public IntPtr BiasTensor;
        public IntPtr MaskTensor;
        public IntPtr RelativePositionBiasTensor;
        public IntPtr PastKeyTensor;
        public IntPtr PastValueTensor;
        public IntPtr OutputTensor;
        public IntPtr OutputPresentKeyTensor;
        public IntPtr OutputPresentValueTensor;
        public float Scale;
        public float MaskFilterValue;
        public int HeadCount;
        public MultiheadAttentionMaskType MaskType;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->QueryTensor = (QueryTensor != null) ? QueryTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->KeyTensor = (KeyTensor != null) ? KeyTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->ValueTensor = (ValueTensor != null) ? ValueTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->StackedQueryKeyTensor = (StackedQueryKeyTensor != null) ? StackedQueryKeyTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->StackedKeyValueTensor = (StackedKeyValueTensor != null) ? StackedKeyValueTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->StackedQueryKeyValueTensor = (StackedQueryKeyValueTensor != null) ? StackedQueryKeyValueTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->BiasTensor = (BiasTensor != null) ? BiasTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->MaskTensor = (MaskTensor != null) ? MaskTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->RelativePositionBiasTensor = (RelativePositionBiasTensor != null) ? RelativePositionBiasTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->PastKeyTensor = (PastKeyTensor != null) ? PastKeyTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->PastValueTensor = (PastValueTensor != null) ? PastValueTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->OutputPresentKeyTensor = (OutputPresentKeyTensor != null) ? OutputPresentKeyTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputPresentValueTensor = (OutputPresentValueTensor != null) ? OutputPresentValueTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->Scale = Scale;
        @ref->MaskFilterValue = MaskFilterValue;
        @ref->HeadCount = HeadCount;
        @ref->MaskType = MaskType;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        if (QueryTensor != null)
        {
            QueryTensor.Value.__MarshalFree(ref @ref->QueryTensor);
        }

        if (KeyTensor != null)
        {
            KeyTensor.Value.__MarshalFree(ref @ref->KeyTensor);
        }

        if (ValueTensor != null)
        {
            ValueTensor.Value.__MarshalFree(ref @ref->ValueTensor);
        }

        if (StackedQueryKeyTensor != null)
        {
            StackedQueryKeyTensor.Value.__MarshalFree(ref @ref->StackedQueryKeyTensor);
        }

        if (StackedKeyValueTensor != null)
        {
            StackedKeyValueTensor.Value.__MarshalFree(ref @ref->StackedKeyValueTensor);
        }

        if (StackedQueryKeyValueTensor != null)
        {
            StackedQueryKeyValueTensor.Value.__MarshalFree(ref @ref->StackedQueryKeyValueTensor);
        }

        if (BiasTensor != null)
        {
            BiasTensor.Value.__MarshalFree(ref @ref->BiasTensor);
        }

        if (MaskTensor != null)
        {
            MaskTensor.Value.__MarshalFree(ref @ref->MaskTensor);
        }

        if (RelativePositionBiasTensor != null)
        {
            RelativePositionBiasTensor.Value.__MarshalFree(ref @ref->RelativePositionBiasTensor);
        }

        if (PastKeyTensor != null)
        {
            PastKeyTensor.Value.__MarshalFree(ref @ref->PastKeyTensor);
        }

        if (PastValueTensor != null)
        {
            PastValueTensor.Value.__MarshalFree(ref @ref->PastValueTensor);
        }

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        if (OutputPresentKeyTensor != null)
        {
            OutputPresentKeyTensor.Value.__MarshalFree(ref @ref->OutputPresentKeyTensor);
        }

        if (OutputPresentValueTensor != null)
        {
            OutputPresentValueTensor.Value.__MarshalFree(ref @ref->OutputPresentValueTensor);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(MultiheadAttentionOperatorDescription description)
    {
        return new(description);
    }
}
