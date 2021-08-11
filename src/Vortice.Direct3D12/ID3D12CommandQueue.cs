// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    public partial class ID3D12CommandQueue
    {
        public void ExecuteCommandList(ID3D12CommandList commandList)
        {
            unsafe
            {
                IntPtr ptr = commandList.NativePointer;
                ExecuteCommandLists(1, new IntPtr(&ptr));
            }
        }

        public unsafe void ExecuteCommandLists(ID3D12CommandList[] commandLists)
        {
            int count = commandLists.Length;
            IntPtr* commandListsPtr = stackalloc IntPtr[count];
            for (int i = 0; i < count; i++)
            {
                commandListsPtr[i] = (commandLists[i] == null) ? IntPtr.Zero : commandLists[i].NativePointer;
            }

            ExecuteCommandLists(count, new IntPtr(commandListsPtr));
        }

        public unsafe void ExecuteCommandLists(int count, ID3D12CommandList[] commandLists)
        {
            IntPtr* commandListsPtr = stackalloc IntPtr[count];
            for (int i = 0; i < count; i++)
            {
                commandListsPtr[i] = (commandLists[i] == null) ? IntPtr.Zero : commandLists[i].NativePointer;
            }

            ExecuteCommandLists(count, new IntPtr(commandListsPtr));
        }

        public unsafe void ExecuteCommandLists(ReadOnlySpan<ID3D12CommandList> commandLists)
        {
            IntPtr* commandListsPtr = stackalloc IntPtr[commandLists.Length];
            for (int i = 0; i < commandLists.Length; i++)
            {
                commandListsPtr[i] = (commandLists[i] == null) ? IntPtr.Zero : commandLists[i].NativePointer;
            }

            ExecuteCommandLists(commandLists.Length, new IntPtr(commandListsPtr));
        }

        public unsafe void BeginEvent(string name)
        {
            int sizeInQWords = PixHelpers.CalculateNoArgsEventSizeInQWords(name);
            ulong* buffer = stackalloc ulong[sizeInQWords];
            PixHelpers.FormatNoArgsEventToBuffer(buffer, PixHelpers.PixEventType.PIXEvent_BeginEvent_NoArgs, 0, name);
            BeginEvent(PixHelpers.WinPIXEventPIX3BlobVersion, new IntPtr(buffer), sizeInQWords * 8);
        }

        public unsafe void SetMarker(string name)
        {
            int sizeInQWords = PixHelpers.CalculateNoArgsEventSizeInQWords(name);
            ulong* buffer = stackalloc ulong[sizeInQWords];
            PixHelpers.FormatNoArgsEventToBuffer(buffer, PixHelpers.PixEventType.PIXEvent_SetMarker_NoArgs, 0, name);
            SetMarker(PixHelpers.WinPIXEventPIX3BlobVersion, new IntPtr(buffer), sizeInQWords * 8);
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
}
