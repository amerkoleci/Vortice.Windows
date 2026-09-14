// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

/// <summary>A move in a native defragmentation pass. Only Operation may be changed.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct DefragmentationMove
{
    public DefragmentationMoveOperation Operation;
    private nint _sourceAllocation;
    private nint _destinationTemporaryAllocation;

    /// <summary>Acquires a reference to the source. Dispose it before ending the pass.</summary>
    public readonly Allocation GetSourceAllocation() => D3D12MA.GetReference<Allocation>(_sourceAllocation)!;

    /// <summary>Acquires a temporary destination reference. Dispose it before ending the pass.</summary>
    public readonly Allocation GetDestinationTemporaryAllocation() => D3D12MA.GetReference<Allocation>(_destinationTemporaryAllocation)!;
}
