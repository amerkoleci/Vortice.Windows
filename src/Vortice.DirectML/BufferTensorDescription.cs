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

    /// <summary>
    /// Calculates the minimum implied tensor size in bytes given the data type, sizes, and
    /// strides for this <see cref="BufferTensorDescription"/>.
    /// </summary>
    /// <returns></returns>
    public long CalculateMinimumImpliedSize() => CalculateMinimumImpliedSize(DataType, Sizes, Strides);

    /// <summary>
    /// Calculates the minimum implied tensor size in bytes given the data type, sizes, and
    /// strides.
    /// </summary>
    /// <param name="dataType"></param>
    /// <param name="sizes"></param>
    /// <param name="strides"></param>
    /// <returns></returns>
    /// <remarks>
    /// Based on the DirectMLX.h DMLCalcBufferTensorSize function. See
    /// <see href="https://github.com/microsoft/DirectML/blob/master/Libraries/DirectMLX.h"/>.
    /// </remarks>
    public static long CalculateMinimumImpliedSize(
        TensorDataType dataType,
        int[] sizes,
        int[]? strides)
    {
        var elementSizeInBytes = dataType switch
        {
            TensorDataType.Float64 or TensorDataType.Uint64 or TensorDataType.Int64 => 8,
            TensorDataType.Float32 or TensorDataType.Uint32 or TensorDataType.Int32 => 4,
            TensorDataType.Float16 or TensorDataType.Uint16 or TensorDataType.Int16 => 2,
            TensorDataType.Uint8 or TensorDataType.Int8 => 1,
            _ => 0
        };

        long minimumImpliedSizeInBytes;
        if (strides == null)
        {
            minimumImpliedSizeInBytes = 1;
            for (var i = 0; i < sizes.Length; i++)
            {
                minimumImpliedSizeInBytes *= sizes[i];
            }
            minimumImpliedSizeInBytes *= elementSizeInBytes;
        }
        else
        {
            var indexOfLastElement = 0;
            for (var i = 0; i < sizes.Length; i++)
            {
                indexOfLastElement += (sizes[i] - 1) * strides[i];
            }
            minimumImpliedSizeInBytes = (indexOfLastElement + 1) * elementSizeInBytes;
        }

        // Round up to the nearest 4 bytes.
        return minimumImpliedSizeInBytes + 3 & ~3L;
    }

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
