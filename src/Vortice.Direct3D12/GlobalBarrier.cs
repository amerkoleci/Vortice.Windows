// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes a resource memory access barrier.
/// Used by GLOBAL, TEXTURE, and BUFFER barriers to indicate when resource memory must be made visible for a specific access type.
/// </summary>
public partial struct GlobalBarrier
{
    /// <summary>
    /// Initializes a new transition instance of <see cref="GlobalBarrier"/> struct.
    /// </summary>
    /// <param name="syncBefore">Synchronization scope of all preceding GPU work that must be completed before executing the barrier.</param>
    /// <param name="syncAfter">Synchronization scope of all subsequent GPU work that must wait until the barrier execution is finished.</param>
    /// <param name="accessBefore">Write accesses that must be flushed and finished before the barrier is executed.</param>
    /// <param name="accessAfter">Accesses that must be available for data written via AccessBefore after the barrier is executed.</param>
    public GlobalBarrier(BarrierSync syncBefore, BarrierSync syncAfter, BarrierAccess accessBefore, BarrierAccess accessAfter)
    {
        SyncBefore = syncBefore;
        SyncAfter = syncAfter;
        AccessBefore = accessBefore;
        AccessAfter = accessAfter;  
    }
}
