// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct BufferTensorDescription : ITensorDescription, ITensorDescriptionMarshal
{
    public TensorDataType DataType { get; set; }

    public TensorFlags Flags { get; set; }

    public int DimensionCount => Sizes.Length;

    public int[] Sizes { get; set; }

    public int[]? Strides { get; set; }

    public long TotalTensorSizeInBytes { get; set; }

    public int GuaranteedBaseOffsetAlignment { get; set; }

    public TensorType TensorType => TensorType.Buffer;

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public TensorDataType DataType;
        public TensorFlags Flags;
        public uint DimensionCount;
        public IntPtr PSizes;
        public IntPtr PStrides;
        public ulong TotalTensorSizeInBytes;
        public uint GuaranteedBaseOffsetAlignment;
    }

    unsafe IntPtr ITensorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->DataType = DataType;
        @ref->Flags = Flags;
        @ref->DimensionCount = (uint)Sizes.Length;
        @ref->PSizes = UnsafeUtilities.AllocToPointer(Sizes);
        @ref->PStrides = Strides != null ? UnsafeUtilities.AllocToPointer(Strides) : IntPtr.Zero;
        @ref->TotalTensorSizeInBytes = (ulong)TotalTensorSizeInBytes;
        @ref->GuaranteedBaseOffsetAlignment = (uint)GuaranteedBaseOffsetAlignment;

        return new(@ref);
    }

    unsafe void ITensorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        if (@ref->PSizes != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(@ref->PSizes);
        }

        if (@ref->PStrides != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(@ref->PStrides);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator TensorDescription(BufferTensorDescription description)
    {
        return new(description);
    }
}
