// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct Resample1OperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.Resample1;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RESAMPLE1_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RESAMPLE1_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RESAMPLE1_OPERATOR_DESC::InterpolationMode']/*" />
    public InterpolationMode InterpolationMode { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RESAMPLE1_OPERATOR_DESC::Scales']/*" />
    public float[] Scales { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RESAMPLE1_OPERATOR_DESC::InputPixelOffsets']/*" />
    public float[] InputPixelOffsets { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RESAMPLE1_OPERATOR_DESC::OutputPixelOffsets']/*" />
    public float[] OutputPixelOffsets { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public InterpolationMode InterpolationMode;
        public int DimensionCount;
        public IntPtr Scales;
        public IntPtr InputPixelOffsets;
        public IntPtr OutputPixelOffsets;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->InterpolationMode = InterpolationMode;

        var dimensionCount = Scales.Length;
        if (InputPixelOffsets.Length != dimensionCount) { throw new IndexOutOfRangeException("InputPixelOffsets must have the same length as Scales."); }
        if (OutputPixelOffsets.Length != dimensionCount) { throw new IndexOutOfRangeException("OutputPixelOffsets must have the same length as Scales."); }
        @ref->DimensionCount = dimensionCount;

        @ref->Scales = new(UnsafeUtilities.AllocWithData(Scales));
        @ref->InputPixelOffsets = new(UnsafeUtilities.AllocWithData(InputPixelOffsets));
        @ref->OutputPixelOffsets = new(UnsafeUtilities.AllocWithData(OutputPixelOffsets));

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        UnsafeUtilities.Free(@ref->Scales);
        UnsafeUtilities.Free(@ref->InputPixelOffsets);
        UnsafeUtilities.Free(@ref->OutputPixelOffsets);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(Resample1OperatorDescription description)
    {
        return new(description);
    }
}
