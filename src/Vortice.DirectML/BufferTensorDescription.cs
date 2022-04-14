// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;
public partial class BufferTensorDescription
{
    public TensorDataType DataType { get; set; }

    public TensorFlags Flags { get; set; }

    public int[] Sizes { get; set; }

    public int[]? Strides { get; set; }

    public long TotalTensorSizeInBytes { get; set; }

    public int GuaranteedBaseOffsetAlignment { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BufferTensorDescription"/> class.
    /// </summary>
    public BufferTensorDescription(int[] sizes)
    {
        Sizes = sizes;
    }

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

        internal void __MarshalFree()
        {
            if (PSizes != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(PSizes);
            }

            if (PStrides != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(PStrides);
            }
        }
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        Sizes = new int[@ref.DimensionCount];
        if (@ref.DimensionCount > 0)
        {
            UnsafeUtilities.Read(@ref.PSizes, Sizes);
        }

        if (@ref.DimensionCount > 0 && @ref.PStrides != IntPtr.Zero)
        {
            Strides = new int[@ref.DimensionCount];
            UnsafeUtilities.Read(@ref.PStrides, Strides);
        }

        DataType = @ref.DataType;
        Flags = @ref.Flags;
        TotalTensorSizeInBytes = (long)@ref.TotalTensorSizeInBytes;
        GuaranteedBaseOffsetAlignment = (int)@ref.GuaranteedBaseOffsetAlignment;
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.DataType = DataType;
        @ref.Flags = Flags;
        @ref.DimensionCount = (uint)Sizes.Length;
        @ref.PSizes = UnsafeUtilities.AllocToPointer(Sizes);
        @ref.PStrides = Strides != null ? UnsafeUtilities.AllocToPointer(Strides) : IntPtr.Zero;
        @ref.TotalTensorSizeInBytes = (ulong)TotalTensorSizeInBytes;
        @ref.GuaranteedBaseOffsetAlignment = (uint)GuaranteedBaseOffsetAlignment;
    }
    #endregion
}
