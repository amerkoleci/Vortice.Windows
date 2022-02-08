// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes the root signature 1.0 layout of a descriptor table as a collection of descriptor ranges that appear one after the other in a descriptor heap.
/// </summary>
public partial class RootDescriptorTable
{
    /// <summary>
    /// Gets or sets the descriptor ranges.
    /// </summary>
    public DescriptorRange[] Ranges { get; set; }

    public RootDescriptorTable(params DescriptorRange[] ranges)
    {
        Ranges = ranges;
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public int NumDescriptorRanges;
        public IntPtr PDescriptorRanges;

        internal void __MarshalFree()
        {
            if (PDescriptorRanges != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(PDescriptorRanges);
            }
        }
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    internal unsafe void __MarshalFrom(in __Native @ref)
    {
        Ranges = new DescriptorRange[@ref.NumDescriptorRanges];
        if (@ref.NumDescriptorRanges > 0)
        {
            UnsafeUtilities.Read(@ref.PDescriptorRanges, Ranges);
        }
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.NumDescriptorRanges = Ranges?.Length ?? 0;
        @ref.PDescriptorRanges = UnsafeUtilities.AllocToPointer(Ranges);
    }
    #endregion
}
