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
            fixed (char* chars = name)
            {
                BeginEvent(D3D12.PIX_EVENT_UNICODE_VERSION, new IntPtr(chars), (name.Length + 1) * 2);
            }
        }

        public unsafe void SetMarker(string name)
        {
            fixed (char* chars = name)
            {
                SetMarker(D3D12.PIX_EVENT_UNICODE_VERSION, new IntPtr(chars), (name.Length + 1) * 2);
            }
        }

        public void UpdateTileMappings(
            ID3D12Resource resource,
            TiledResourceCoordinate[] resourceRegionStartCoordinates,
            TileRegionSize[] resourceRegionSizes,
            ID3D12Heap heap,
            TileRangeFlags[] rangeFlags,
            int[] heapRangeStartOffsets,
            int[] rangeTileCounts,
            TileMappingFlags flags = TileMappingFlags.None)
        {
            UpdateTileMappings(resource,
                resourceRegionStartCoordinates.Length,
                resourceRegionStartCoordinates,
                resourceRegionSizes,
                heap,
                rangeFlags.Length,
                rangeFlags,
                heapRangeStartOffsets,
                rangeTileCounts,
                flags);
        }
    }
}
