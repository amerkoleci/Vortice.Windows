// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12CommandQueue
{
    public void ExecuteCommandList(ID3D12CommandList commandList)
    {
        IntPtr ptr = commandList.NativePointer;
        ExecuteCommandLists(1, &ptr);
    }

    public void ExecuteCommandLists(ID3D12CommandList[] commandLists)
    {
        int count = commandLists.Length;
        IntPtr* commandListsPtr = stackalloc IntPtr[count];
        for (int i = 0; i < count; i++)
        {
            commandListsPtr[i] = (commandLists[i] == null) ? IntPtr.Zero : commandLists[i].NativePointer;
        }

        ExecuteCommandLists(count, commandListsPtr);
    }

    public void ExecuteCommandLists(int count, ID3D12CommandList[] commandLists)
    {
        IntPtr* commandListsPtr = stackalloc IntPtr[count];
        for (int i = 0; i < count; i++)
        {
            commandListsPtr[i] = (commandLists[i] == null) ? IntPtr.Zero : commandLists[i].NativePointer;
        }

        ExecuteCommandLists(count, commandListsPtr);
    }

    public void ExecuteCommandLists(ReadOnlySpan<ID3D12CommandList> commandLists)
    {
        IntPtr* commandListsPtr = stackalloc IntPtr[commandLists.Length];
        for (int i = 0; i < commandLists.Length; i++)
        {
            commandListsPtr[i] = (commandLists[i] == null) ? IntPtr.Zero : commandLists[i].NativePointer;
        }

        ExecuteCommandLists(commandLists.Length, commandListsPtr);
    }

    public void BeginEvent(string name)
    {
        int bufferSize = PixHelpers.CalculateNoArgsEventSize(name);
        void* buffer = stackalloc byte[bufferSize];
        PixHelpers.FormatNoArgsEventToBuffer(buffer, PixHelpers.PixEventType.PIXEvent_BeginEvent_NoArgs, 0, name);
        BeginEvent(PixHelpers.WinPIXEventPIX3BlobVersion, new IntPtr(buffer), bufferSize);
    }

    public void SetMarker(string name)
    {
        int bufferSize = PixHelpers.CalculateNoArgsEventSize(name);
        void* buffer = stackalloc byte[bufferSize];
        PixHelpers.FormatNoArgsEventToBuffer(buffer, PixHelpers.PixEventType.PIXEvent_SetMarker_NoArgs, 0, name);
        SetMarker(PixHelpers.WinPIXEventPIX3BlobVersion, new IntPtr(buffer), bufferSize);
    }

    public unsafe void UpdateTileMappings(
        ID3D12Resource resource,
        TiledResourceCoordinate[] resourceRegionStartCoordinates,
        TileRegionSize[] resourceRegionSizes,
        ID3D12Heap heap,
        TileRangeFlags[] rangeFlags,
        int[] heapRangeStartOffsets,
        int[] rangeTileCounts,
        TileMappingFlags flags = TileMappingFlags.None)
    {
        fixed (TiledResourceCoordinate* pResourceRegionStartCoordinates = resourceRegionStartCoordinates)
        fixed (TileRegionSize* pResourceRegionSizes = resourceRegionSizes)
        fixed (TileRangeFlags* pRangeFlags = rangeFlags)
        fixed (int* pHeapRangeStartOffsets = heapRangeStartOffsets)
        fixed (int* pRangeTileCounts = rangeTileCounts)
            UpdateTileMappings(resource,
                resourceRegionStartCoordinates.Length,
                pResourceRegionStartCoordinates,
                pResourceRegionSizes,
                heap,
                rangeFlags.Length,
                pRangeFlags,
                pHeapRangeStartOffsets,
                pRangeTileCounts,
                flags);
    }

    public unsafe void UpdateTileMappings(
        ID3D12Resource resource,
        ReadOnlySpan<TiledResourceCoordinate> resourceRegionStartCoordinates,
        ReadOnlySpan<TileRegionSize> resourceRegionSizes,
        ID3D12Heap heap,
        ReadOnlySpan<TileRangeFlags> rangeFlags,
        ReadOnlySpan<int> heapRangeStartOffsets,
        ReadOnlySpan<int> rangeTileCounts,
        TileMappingFlags flags = TileMappingFlags.None)
    {
        fixed (TiledResourceCoordinate* pResourceRegionStartCoordinates = resourceRegionStartCoordinates)
        fixed (TileRegionSize* pResourceRegionSizes = resourceRegionSizes)
        fixed (TileRangeFlags* pRangeFlags = rangeFlags)
        fixed (int* pHeapRangeStartOffsets = heapRangeStartOffsets)
        fixed (int* pRangeTileCounts = rangeTileCounts)
            UpdateTileMappings(resource,
                resourceRegionStartCoordinates.Length,
                pResourceRegionStartCoordinates,
                pResourceRegionSizes,
                heap,
                rangeFlags.Length,
                pRangeFlags,
                pHeapRangeStartOffsets,
                pRangeTileCounts,
                flags);
    }
}
