// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

/// <summary>Allocates virtual ranges. Free allocations or call Clear before disposing the block.</summary>
public unsafe class VirtualBlock : ComObject
{
    public VirtualBlock(nint nativePointer) : base(nativePointer) { }

    public bool IsEmpty
    {
        get
        {
            bool result = D3D12MA.VirtualBlockIsEmpty(NativePointer) != 0;
            GC.KeepAlive(this);
            return result;
        }
    }

    public Statistics Statistics
    {
        get
        {
            Statistics statistics;
            D3D12MA.VirtualBlockGetStatistics(NativePointer, &statistics);
            GC.KeepAlive(this);
            return statistics;
        }
    }

    public Result Allocate(in VirtualAllocationDescription description, out VirtualAllocation allocation, out ulong offset)
    {
        VirtualAllocation nativeAllocation = default;
        ulong nativeOffset = 0;
        Result result;
        fixed (VirtualAllocationDescription* nativeDescription = &description)
        {
            result = D3D12MA.VirtualBlockAllocate(NativePointer, nativeDescription, &nativeAllocation, &nativeOffset);
        }
        GC.KeepAlive(this);
        allocation = result.Success ? nativeAllocation : default;
        offset = nativeOffset;
        return result;
    }

    public VirtualAllocation Allocate(in VirtualAllocationDescription description, out ulong offset)
    {
        Allocate(description, out VirtualAllocation allocation, out offset).CheckError();
        return allocation;
    }

    public void FreeAllocation(VirtualAllocation allocation)
    {
        D3D12MA.VirtualBlockFreeAllocation(NativePointer, allocation);
        GC.KeepAlive(this);
    }

    public void Clear()
    {
        D3D12MA.VirtualBlockClear(NativePointer);
        GC.KeepAlive(this);
    }

    public VirtualAllocationInfo GetAllocationInfo(VirtualAllocation allocation)
    {
        VirtualAllocationInfo info;
        D3D12MA.VirtualBlockGetAllocationInfo(NativePointer, allocation, &info);
        GC.KeepAlive(this);
        return info;
    }

    public void SetAllocationPrivateData(VirtualAllocation allocation, nint privateData)
    {
        D3D12MA.VirtualBlockSetAllocationPrivateData(NativePointer, allocation, privateData);
        GC.KeepAlive(this);
    }

    public DetailedStatistics CalculateStatistics()
    {
        DetailedStatistics statistics;
        D3D12MA.VirtualBlockCalculateStatistics(NativePointer, &statistics);
        GC.KeepAlive(this);
        return statistics;
    }

    public string BuildStatsString()
    {
        char* text = null;
        try
        {
            D3D12MA.VirtualBlockBuildStatsString(NativePointer, &text);
            return new string(text);
        }
        finally
        {
            D3D12MA.VirtualBlockFreeStatsString(NativePointer, text);
            GC.KeepAlive(this);
        }
    }
}
