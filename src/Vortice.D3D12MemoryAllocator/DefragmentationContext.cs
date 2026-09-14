// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

/// <summary>Controls native defragmentation passes. The caller performs resource moves and GPU synchronization.</summary>
public unsafe class DefragmentationContext : ComObject
{
    public DefragmentationContext(nint nativePointer) : base(nativePointer) { }

    /// <summary>Returns S_FALSE when moves are available and S_OK when no moves remain.</summary>
    public Result BeginPass(out DefragmentationPassMoveInfo passInfo)
    {
        DefragmentationPassMoveInfo native = default;
        Result result = D3D12MA.DefragmentationBeginPass(NativePointer, &native);
        GC.KeepAlive(this);
        passInfo = native;
        return result;
    }

    /// <summary>Commits completed moves. All pass data and temporary allocation references expire at this call.</summary>
    public Result EndPass(ref DefragmentationPassMoveInfo passInfo)
    {
        Result result;
        fixed (DefragmentationPassMoveInfo* native = &passInfo)
        {
            result = D3D12MA.DefragmentationEndPass(NativePointer, native);
        }
        GC.KeepAlive(this);
        passInfo = default;
        return result;
    }

    public DefragmentationStatistics Statistics
    {
        get
        {
            DefragmentationStatistics statistics;
            D3D12MA.DefragmentationGetStatistics(NativePointer, &statistics);
            GC.KeepAlive(this);
            return statistics;
        }
    }
}
