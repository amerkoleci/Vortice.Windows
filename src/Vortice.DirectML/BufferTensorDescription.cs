// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct BufferTensorDescription : ITensorDescription, ITensorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of tensor description.
    /// </summary>
    public TensorType TensorType => TensorType.Buffer;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BUFFER_TENSOR_DESC::DataType']/*" />
    public TensorDataType DataType { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BUFFER_TENSOR_DESC::Flags']/*" />
    public TensorFlags Flags { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BUFFER_TENSOR_DESC::Sizes']/*" />
    public int[] Sizes { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BUFFER_TENSOR_DESC::Strides']/*" />
    public int[]? Strides { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BUFFER_TENSOR_DESC::TotalTensorSizeInBytes']/*" />
    public long TotalTensorSizeInBytes { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BUFFER_TENSOR_DESC::GuaranteedBaseOffsetAlignment']/*" />
    public int GuaranteedBaseOffsetAlignment { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public TensorDataType DataType;
        public TensorFlags Flags;
        public int DimensionCount;
        public IntPtr PSizes;
        public IntPtr PStrides;
        public long TotalTensorSizeInBytes;
        public int GuaranteedBaseOffsetAlignment;
    }

    unsafe IntPtr ITensorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->DataType = DataType;
        @ref->Flags = Flags;
        @ref->DimensionCount = Sizes.Length;
        @ref->PSizes = UnsafeUtilities.AllocToPointer(Sizes);
        @ref->PStrides = IntPtr.Zero;
        if (Strides != null) {
            if (Strides.Length != Sizes.Length) { throw new IndexOutOfRangeException("Strides must have the same length as Sizes."); }
            @ref->PStrides = UnsafeUtilities.AllocToPointer(Strides);
        }
        @ref->TotalTensorSizeInBytes = TotalTensorSizeInBytes;
        @ref->GuaranteedBaseOffsetAlignment = GuaranteedBaseOffsetAlignment;

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
