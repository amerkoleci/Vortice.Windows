// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct AveragePoolingGradOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.AveragePoolingGrad;

    public TensorDescription InputGradientTensor { get; set; }

    public TensorDescription OutputGradientTensor { get; set; }

    public uint DimensionCount { get; set; }

    public uint[] Strides { get; set; }

    public uint[] WindowSize { get; set; }

    public uint[] StartPadding { get; set; }

    public uint[] EndPadding { get; set; }

    public bool IncludePadding { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputGradientTensor;
        public IntPtr OutputGradientTensor;
        public uint DimensionCount;
        public IntPtr Strides;
        public IntPtr WindowSize;
        public IntPtr StartPadding;
        public IntPtr EndPadding;
        public bool IncludePadding;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputGradientTensor = InputGradientTensor.__MarshalAlloc();
        @ref->OutputGradientTensor = OutputGradientTensor.__MarshalAlloc();
        @ref->DimensionCount = DimensionCount;
        @ref->Strides = new(UnsafeUtilities.AllocWithData(Strides));
        @ref->WindowSize = new(UnsafeUtilities.AllocWithData(WindowSize));
        @ref->StartPadding = new(UnsafeUtilities.AllocWithData(StartPadding));
        @ref->EndPadding = new(UnsafeUtilities.AllocWithData(EndPadding));
        @ref->IncludePadding = IncludePadding;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputGradientTensor.__MarshalFree(ref @ref->InputGradientTensor);
        OutputGradientTensor.__MarshalFree(ref @ref->OutputGradientTensor);
           UnsafeUtilities.Free(@ref->Strides);
           UnsafeUtilities.Free(@ref->WindowSize);
           UnsafeUtilities.Free(@ref->StartPadding);
           UnsafeUtilities.Free(@ref->EndPadding);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(AveragePoolingGradOperatorDescription description)
    {
        return new(description);
    }
}
