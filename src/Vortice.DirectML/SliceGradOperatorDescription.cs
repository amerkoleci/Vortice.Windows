// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct SliceGradOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.SliceGrad;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SLICE_GRAD_OPERATOR_DESC::InputGradientTensor']/*" />
    public TensorDescription InputGradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SLICE_GRAD_OPERATOR_DESC::OutputGradientTensor']/*" />
    public TensorDescription OutputGradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SLICE_GRAD_OPERATOR_DESC::InputWindowOffsets']/*" />
    public int[] InputWindowOffsets { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SLICE_GRAD_OPERATOR_DESC::InputWindowSizes']/*" />
    public int[] InputWindowSizes { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SLICE_GRAD_OPERATOR_DESC::InputWindowStrides']/*" />
    public int[] InputWindowStrides { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputGradientTensor;
        public IntPtr OutputGradientTensor;
        public int DimensionCount;
        public IntPtr InputWindowOffsets;
        public IntPtr InputWindowSizes;
        public IntPtr InputWindowStrides;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputGradientTensor = InputGradientTensor.__MarshalAlloc();
        @ref->OutputGradientTensor = OutputGradientTensor.__MarshalAlloc();

        var dimensionCount = InputWindowOffsets.Length;
        if (InputWindowSizes.Length != dimensionCount) { throw new IndexOutOfRangeException("InputWindowSizes must have the same length as InputWindowOffsets."); }
        if (InputWindowStrides.Length != dimensionCount) { throw new IndexOutOfRangeException("InputWindowStrides must have the same length as InputWindowOffsets."); }
        @ref->DimensionCount = dimensionCount;

        @ref->InputWindowOffsets = new(UnsafeUtilities.AllocWithData(InputWindowOffsets));
        @ref->InputWindowSizes = new(UnsafeUtilities.AllocWithData(InputWindowSizes));
        @ref->InputWindowStrides = new(UnsafeUtilities.AllocWithData(InputWindowStrides));

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputGradientTensor.__MarshalFree(ref @ref->InputGradientTensor);
        OutputGradientTensor.__MarshalFree(ref @ref->OutputGradientTensor);
        UnsafeUtilities.Free(@ref->InputWindowOffsets);
        UnsafeUtilities.Free(@ref->InputWindowSizes);
        UnsafeUtilities.Free(@ref->InputWindowStrides);

        UnsafeUtilities.Free(@ref);
    }
    #endregion
}
