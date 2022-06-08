// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ResampleOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.Resample;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RESAMPLE_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RESAMPLE_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RESAMPLE_OPERATOR_DESC::InterpolationMode']/*" />
    public InterpolationMode InterpolationMode { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RESAMPLE_OPERATOR_DESC::Scales']/*" />
    public float[] Scales { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"Resample: InterpolationMode={InterpolationMode}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public InterpolationMode InterpolationMode;
        public int ScaleCount;
        public IntPtr Scales;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->InterpolationMode = InterpolationMode;
        @ref->ScaleCount = Scales.Length;
        @ref->Scales = new(UnsafeUtilities.AllocWithData(Scales));

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        UnsafeUtilities.Free(@ref->Scales);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(ResampleOperatorDescription description)
    {
        return new(description);
    }
}
