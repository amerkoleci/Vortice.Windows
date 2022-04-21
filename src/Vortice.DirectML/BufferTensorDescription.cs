// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;
public partial class BufferTensorDescription : TensorDescription
{
    public TensorDataType DataType { get; set; }

    public TensorFlags Flags { get; set; }

    public int DimensionCount => Sizes.Length;

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
    internal struct __BufferTensorDescriptionNative
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

    internal unsafe override void __MarshalFree(ref __Native @ref)
    {
        var desc = (__BufferTensorDescriptionNative*)@ref.Description;
        (*desc).__MarshalFree();

        UnsafeUtilities.Free(desc);
    }

    internal unsafe override void __MarshalTo(ref __Native @ref)
    {
        var desc = UnsafeUtilities.Alloc<__BufferTensorDescriptionNative>();
        (*desc).DataType = DataType;
        (*desc).Flags = Flags;
        (*desc).DimensionCount = (uint)Sizes.Length;
        (*desc).PSizes = UnsafeUtilities.AllocToPointer(Sizes);
        (*desc).PStrides = Strides != null ? UnsafeUtilities.AllocToPointer(Strides) : IntPtr.Zero;
        (*desc).TotalTensorSizeInBytes = (ulong)TotalTensorSizeInBytes;
        (*desc).GuaranteedBaseOffsetAlignment = (uint)GuaranteedBaseOffsetAlignment;

        @ref.Type = TensorType.Buffer;
        @ref.Description = new IntPtr(desc);
    }
    #endregion
}
