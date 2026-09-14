// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

/// <summary>Native pass data. Moves and all copies of this value expire at EndPass.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct DefragmentationPassMoveInfo
{
    private uint _moveCount;
    private DefragmentationMove* _moves;
    public readonly uint MoveCount => _moveCount;
    public readonly Span<DefragmentationMove> Moves => new(_moves, checked((int)_moveCount));
}
