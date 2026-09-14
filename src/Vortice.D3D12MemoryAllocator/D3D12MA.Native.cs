// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D12;

namespace Vortice.D3D12MemoryAllocator;

public static unsafe partial class D3D12MA
{
    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_CreateVirtualBlock")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int CreateVirtualBlockNative(VirtualBlockDescription.__Native* description, nint* virtualBlock);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MAVirtualBlock_IsEmpty")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial uint VirtualBlockIsEmpty(nint self);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MAVirtualBlock_Allocate")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int VirtualBlockAllocate(nint self, VirtualAllocationDescription* description, VirtualAllocation* allocation, ulong* offset);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MAVirtualBlock_FreeAllocation")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void VirtualBlockFreeAllocation(nint self, VirtualAllocation allocation);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MAVirtualBlock_Clear")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void VirtualBlockClear(nint self);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MAVirtualBlock_GetAllocationInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void VirtualBlockGetAllocationInfo(nint self, VirtualAllocation allocation, VirtualAllocationInfo* info);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MAVirtualBlock_SetAllocationPrivateData")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void VirtualBlockSetAllocationPrivateData(nint self, VirtualAllocation allocation, nint privateData);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MAVirtualBlock_GetStatistics")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void VirtualBlockGetStatistics(nint self, Statistics* statistics);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MAVirtualBlock_CalculateStatistics")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void VirtualBlockCalculateStatistics(nint self, DetailedStatistics* statistics);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MAVirtualBlock_BuildStatsString")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void VirtualBlockBuildStatsString(nint self, char** text);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MAVirtualBlock_FreeStatsString")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void VirtualBlockFreeStatsString(nint self, char* text);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_CreateAllocator")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int CreateAllocatorNative(AllocatorDescription.__Native* description, nint* allocator);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_IsUMA")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial uint AllocatorIsUMA(nint self);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_IsCacheCoherentUMA")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial uint AllocatorIsCacheCoherentUMA(nint self);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_IsGPUUploadHeapSupported")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial uint AllocatorIsGPUUploadHeapSupported(nint self);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_GetMemoryCapacity")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial ulong AllocatorGetMemoryCapacity(nint self, Vortice.DXGI.MemorySegmentGroup memorySegmentGroup);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_CreateResource")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int AllocatorCreateResource(nint self, AllocationDescription.__Native* allocationDescription, ResourceDescription* resourceDescription, ResourceStates initialState, ClearValue* clearValue, nint* allocation, Guid* resourceId, nint* resource);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_CreateResource2")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int AllocatorCreateResource2(nint self, AllocationDescription.__Native* allocationDescription, ResourceDescription1* resourceDescription, ResourceStates initialState, ClearValue* clearValue, nint* allocation, Guid* resourceId, nint* resource);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_CreateResource3")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int AllocatorCreateResource3(nint self, AllocationDescription.__Native* allocationDescription, ResourceDescription1* resourceDescription, BarrierLayout initialLayout, ClearValue* clearValue, uint formatCount, Vortice.DXGI.Format* formats, nint* allocation, Guid* resourceId, nint* resource);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_AllocateMemory")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int AllocatorAllocateMemory(nint self, AllocationDescription.__Native* description, ResourceAllocationInfo* info, nint* allocation);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_CreateAliasingResource")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int AllocatorCreateAliasingResource(nint self, nint allocation, ulong offset, ResourceDescription* description, ResourceStates initialState, ClearValue* clearValue, Guid* resourceId, nint* resource);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_CreateAliasingResource1")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int AllocatorCreateAliasingResource1(nint self, nint allocation, ulong offset, ResourceDescription1* description, ResourceStates initialState, ClearValue* clearValue, Guid* resourceId, nint* resource);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_CreateAliasingResource2")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int AllocatorCreateAliasingResource2(nint self, nint allocation, ulong offset, ResourceDescription1* description, BarrierLayout initialLayout, ClearValue* clearValue, uint formatCount, Vortice.DXGI.Format* formats, Guid* resourceId, nint* resource);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_CreatePool")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int AllocatorCreatePool(nint self, PoolDescription.__Native* description, nint* pool);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_SetCurrentFrameIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void AllocatorSetCurrentFrameIndex(nint self, uint frameIndex);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_GetBudget")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void AllocatorGetBudget(nint self, Budget* local, Budget* nonLocal);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_CalculateStatistics")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void AllocatorCalculateStatistics(nint self, TotalStatistics.__Native* statistics);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_BuildStatsString")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void AllocatorBuildStatsString(nint self, char** text, uint detailedMap);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_FreeStatsString")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void AllocatorFreeStatsString(nint self, char* text);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocator_BeginDefragmentation")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void AllocatorBeginDefragmentation(nint self, DefragmentationDescription* description, nint* context);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocation_GetOffset")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial ulong AllocationGetOffset(nint self);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocation_GetAlignment")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial ulong AllocationGetAlignment(nint self);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocation_GetSize")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial ulong AllocationGetSize(nint self);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocation_GetResource")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial nint AllocationGetResource(nint self);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocation_GetHeap")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial nint AllocationGetHeap(nint self);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocation_GetPrivateData")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial nint AllocationGetPrivateData(nint self);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocation_GetName")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial nint AllocationGetName(nint self);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocation_SetResource")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void AllocationSetResource(nint self, nint value);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocation_SetPrivateData")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void AllocationSetPrivateData(nint self, nint value);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Allocation_SetName")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void AllocationSetName(nint self, char* value);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Pool_GetDesc")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void PoolGetDescription(nint self, PoolDescription.__Native* description);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Pool_GetStatistics")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void PoolGetStatistics(nint self, Statistics* statistics);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Pool_CalculateStatistics")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void PoolCalculateStatistics(nint self, DetailedStatistics* statistics);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Pool_GetName")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial nint PoolGetName(nint self);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Pool_SetName")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void PoolSetName(nint self, char* name);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_Pool_BeginDefragmentation")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int PoolBeginDefragmentation(nint self, DefragmentationDescription* description, nint* context);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_DefragmentationContext_BeginPass")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int DefragmentationBeginPass(nint self, DefragmentationPassMoveInfo* passInfo);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_DefragmentationContext_EndPass")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial int DefragmentationEndPass(nint self, DefragmentationPassMoveInfo* passInfo);

    [LibraryImport(LibraryName, EntryPoint = "D3D12MA_DefragmentationContext_GetStats")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    internal static partial void DefragmentationGetStatistics(nint self, DefragmentationStatistics* statistics);
}
