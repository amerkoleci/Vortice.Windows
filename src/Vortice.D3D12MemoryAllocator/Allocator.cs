// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D12;
using Vortice.DXGI;

namespace Vortice.D3D12MemoryAllocator;

/// <summary>Allocates D3D12 resources. Dispose all allocations, pools, and contexts before the allocator.</summary>
public unsafe class Allocator : ComObject
{
    public Allocator(nint nativePointer) : base(nativePointer) { }

    public bool IsUMA
    {
        get
        {
            bool value = D3D12MA.AllocatorIsUMA(NativePointer) != 0;
            GC.KeepAlive(this);
            return value;
        }
    }

    public bool IsCacheCoherentUMA
    {
        get
        {
            bool value = D3D12MA.AllocatorIsCacheCoherentUMA(NativePointer) != 0;
            GC.KeepAlive(this);
            return value;
        }
    }

    public bool IsGPUUploadHeapSupported
    {
        get
        {
            bool value = D3D12MA.AllocatorIsGPUUploadHeapSupported(NativePointer) != 0;
            GC.KeepAlive(this);
            return value;
        }
    }

    public Result CreateResource(in AllocationDescription allocationDescription, in ResourceDescription resourceDescription,
        ResourceStates initialResourceState, ClearValue? optimizedClearValue,
        out Allocation? allocation, out ID3D12Resource? resource)
    {
        return CreateResource<ID3D12Resource>(allocationDescription, resourceDescription, initialResourceState, optimizedClearValue, out allocation, out resource);
    }

    public Allocation CreateResource(in AllocationDescription allocationDescription, in ResourceDescription resourceDescription,
        ResourceStates initialResourceState, out ID3D12Resource resource, ClearValue? optimizedClearValue = null)
    {
        return CreateResource<ID3D12Resource>(allocationDescription, resourceDescription, initialResourceState, out resource, optimizedClearValue);
    }

    public Allocation CreateResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in AllocationDescription allocationDescription, in ResourceDescription resourceDescription,
        ResourceStates initialResourceState, out T resource, ClearValue? optimizedClearValue = null) where T : ID3D12Resource
    {
        CreateResource(allocationDescription, resourceDescription, initialResourceState, optimizedClearValue, out Allocation? allocation, out T? value).CheckError();
        resource = value!;
        return allocation!;
    }

    public Result CreateResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in AllocationDescription allocationDescription, in ResourceDescription resourceDescription,
        ResourceStates initialResourceState, ClearValue? optimizedClearValue,
        out Allocation? allocation, out T? resource) where T : ID3D12Resource
    {
        AllocationDescription.__Native native = default;
        allocationDescription.__MarshalTo(ref native);
        ClearValue clearValue = optimizedClearValue.GetValueOrDefault();
        Guid resourceId = typeof(T).GUID;
        nint allocationPointer = 0;
        nint resourcePointer = 0;
        Result result;
        fixed (ResourceDescription* description = &resourceDescription)
        {
            result = D3D12MA.AllocatorCreateResource(NativePointer, &native, description, initialResourceState,
                optimizedClearValue.HasValue ? &clearValue : null, &allocationPointer, &resourceId, &resourcePointer);
        }
        GC.KeepAlive(allocationDescription.CustomPool);
        GC.KeepAlive(this);
        return D3D12MA.WrapResourceResult(result, allocationPointer, resourcePointer, out allocation, out resource);
    }

    public Result CreateResource2(in AllocationDescription allocationDescription, in ResourceDescription1 resourceDescription,
        ResourceStates initialResourceState, ClearValue? optimizedClearValue,
        out Allocation? allocation, out ID3D12Resource? resource)
    {
        return CreateResource2<ID3D12Resource>(allocationDescription, resourceDescription, initialResourceState, optimizedClearValue, out allocation, out resource);
    }

    public Allocation CreateResource2(in AllocationDescription allocationDescription, in ResourceDescription1 resourceDescription,
        ResourceStates initialResourceState, out ID3D12Resource resource, ClearValue? optimizedClearValue = null)
    {
        return CreateResource2<ID3D12Resource>(allocationDescription, resourceDescription, initialResourceState, out resource, optimizedClearValue);
    }

    public Allocation CreateResource2<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in AllocationDescription allocationDescription, in ResourceDescription1 resourceDescription,
        ResourceStates initialResourceState, out T resource, ClearValue? optimizedClearValue = null) where T : ID3D12Resource
    {
        CreateResource2(allocationDescription, resourceDescription, initialResourceState, optimizedClearValue, out Allocation? allocation, out T? value).CheckError();
        resource = value!;
        return allocation!;
    }

    public Result CreateResource2<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in AllocationDescription allocationDescription, in ResourceDescription1 resourceDescription,
        ResourceStates initialResourceState, ClearValue? optimizedClearValue,
        out Allocation? allocation, out T? resource) where T : ID3D12Resource
    {
        AllocationDescription.__Native native = default;
        allocationDescription.__MarshalTo(ref native);
        ClearValue clearValue = optimizedClearValue.GetValueOrDefault();
        Guid resourceId = typeof(T).GUID;
        nint allocationPointer = 0;
        nint resourcePointer = 0;
        Result result;
        fixed (ResourceDescription1* description = &resourceDescription)
        {
            result = D3D12MA.AllocatorCreateResource2(NativePointer, &native, description, initialResourceState,
                optimizedClearValue.HasValue ? &clearValue : null, &allocationPointer, &resourceId, &resourcePointer);
        }
        GC.KeepAlive(allocationDescription.CustomPool);
        GC.KeepAlive(this);
        return D3D12MA.WrapResourceResult(result, allocationPointer, resourcePointer, out allocation, out resource);
    }

    public Result CreateResource3(in AllocationDescription allocationDescription, in ResourceDescription1 resourceDescription,
        BarrierLayout initialLayout, ReadOnlySpan<Format> castableFormats, ClearValue? optimizedClearValue,
        out Allocation? allocation, out ID3D12Resource? resource)
    {
        return CreateResource3<ID3D12Resource>(allocationDescription, resourceDescription, initialLayout, castableFormats, optimizedClearValue, out allocation, out resource);
    }

    public Allocation CreateResource3(in AllocationDescription allocationDescription, in ResourceDescription1 resourceDescription,
        BarrierLayout initialLayout, ReadOnlySpan<Format> castableFormats, out ID3D12Resource resource, ClearValue? optimizedClearValue = null)
    {
        return CreateResource3<ID3D12Resource>(allocationDescription, resourceDescription, initialLayout, castableFormats, out resource, optimizedClearValue);
    }

    public Allocation CreateResource3<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in AllocationDescription allocationDescription, in ResourceDescription1 resourceDescription,
        BarrierLayout initialLayout, ReadOnlySpan<Format> castableFormats, out T resource, ClearValue? optimizedClearValue = null) where T : ID3D12Resource
    {
        CreateResource3(allocationDescription, resourceDescription, initialLayout, castableFormats, optimizedClearValue, out Allocation? allocation, out T? value).CheckError();
        resource = value!;
        return allocation!;
    }

    public Result CreateResource3<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in AllocationDescription allocationDescription, in ResourceDescription1 resourceDescription,
        BarrierLayout initialLayout, ReadOnlySpan<Format> castableFormats, ClearValue? optimizedClearValue,
        out Allocation? allocation, out T? resource) where T : ID3D12Resource
    {
        AllocationDescription.__Native native = default;
        allocationDescription.__MarshalTo(ref native);
        ClearValue clearValue = optimizedClearValue.GetValueOrDefault();
        Guid resourceId = typeof(T).GUID;
        nint allocationPointer = 0;
        nint resourcePointer = 0;
        Result result;
        fixed (ResourceDescription1* description = &resourceDescription)
        fixed (Format* formats = castableFormats)
        {
            result = D3D12MA.AllocatorCreateResource3(NativePointer, &native, description, initialLayout,
                optimizedClearValue.HasValue ? &clearValue : null, (uint)castableFormats.Length, formats, &allocationPointer, &resourceId, &resourcePointer);
        }
        GC.KeepAlive(allocationDescription.CustomPool);
        GC.KeepAlive(this);
        return D3D12MA.WrapResourceResult(result, allocationPointer, resourcePointer, out allocation, out resource);
    }

    public Result AllocateMemory(in AllocationDescription description, in ResourceAllocationInfo allocationInfo, out Allocation? allocation)
    {
        AllocationDescription.__Native native = default;
        description.__MarshalTo(ref native);
        nint pointer = 0;
        Result result;
        fixed (ResourceAllocationInfo* info = &allocationInfo)
        {
            result = D3D12MA.AllocatorAllocateMemory(NativePointer, &native, info, &pointer);
        }
        GC.KeepAlive(description.CustomPool);
        GC.KeepAlive(this);
        allocation = result.Success ? MarshallingHelpers.FromPointer<Allocation>(pointer) : null;
        return result;
    }

    public Allocation AllocateMemory(in AllocationDescription description, in ResourceAllocationInfo allocationInfo)
    {
        AllocateMemory(description, allocationInfo, out Allocation? allocation).CheckError();
        return allocation!;
    }

    public Result CreateAliasingResource(Allocation allocation, ulong allocationLocalOffset, in ResourceDescription resourceDescription,
        ResourceStates initialResourceState, ClearValue? optimizedClearValue, out ID3D12Resource? resource)
    {
        return CreateAliasingResource<ID3D12Resource>(allocation, allocationLocalOffset, resourceDescription, initialResourceState, optimizedClearValue, out resource);
    }

    public ID3D12Resource CreateAliasingResource(Allocation allocation, ulong allocationLocalOffset, in ResourceDescription resourceDescription,
        ResourceStates initialResourceState, ClearValue? optimizedClearValue = null)
    {
        return CreateAliasingResource<ID3D12Resource>(allocation, allocationLocalOffset, resourceDescription, initialResourceState, optimizedClearValue);
    }

    public T CreateAliasingResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Allocation allocation, ulong allocationLocalOffset, in ResourceDescription resourceDescription,
        ResourceStates initialResourceState, ClearValue? optimizedClearValue = null) where T : ID3D12Resource
    {
        CreateAliasingResource(allocation, allocationLocalOffset, resourceDescription, initialResourceState, optimizedClearValue, out T? resource).CheckError();
        return resource!;
    }

    public Result CreateAliasingResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Allocation allocation, ulong allocationLocalOffset, in ResourceDescription resourceDescription,
        ResourceStates initialResourceState, ClearValue? optimizedClearValue, out T? resource) where T : ID3D12Resource
    {
        ArgumentNullException.ThrowIfNull(allocation);
        ClearValue clearValue = optimizedClearValue.GetValueOrDefault();
        Guid resourceId = typeof(T).GUID;
        nint pointer = 0;
        Result result;
        fixed (ResourceDescription* description = &resourceDescription)
        {
            result = D3D12MA.AllocatorCreateAliasingResource(NativePointer, allocation.NativePointer, allocationLocalOffset, description, initialResourceState,
                optimizedClearValue.HasValue ? &clearValue : null, &resourceId, &pointer);
        }
        GC.KeepAlive(allocation);
        GC.KeepAlive(this);
        resource = result.Success ? MarshallingHelpers.FromPointer<T>(pointer) : null;
        return result;
    }

    public Result CreateAliasingResource1(Allocation allocation, ulong allocationLocalOffset, in ResourceDescription1 resourceDescription,
        ResourceStates initialResourceState, ClearValue? optimizedClearValue, out ID3D12Resource? resource)
    {
        return CreateAliasingResource1<ID3D12Resource>(allocation, allocationLocalOffset, resourceDescription, initialResourceState, optimizedClearValue, out resource);
    }

    public ID3D12Resource CreateAliasingResource1(Allocation allocation, ulong allocationLocalOffset, in ResourceDescription1 resourceDescription,
        ResourceStates initialResourceState, ClearValue? optimizedClearValue = null)
    {
        return CreateAliasingResource1<ID3D12Resource>(allocation, allocationLocalOffset, resourceDescription, initialResourceState, optimizedClearValue);
    }

    public T CreateAliasingResource1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Allocation allocation, ulong allocationLocalOffset, in ResourceDescription1 resourceDescription,
        ResourceStates initialResourceState, ClearValue? optimizedClearValue = null) where T : ID3D12Resource
    {
        CreateAliasingResource1(allocation, allocationLocalOffset, resourceDescription, initialResourceState, optimizedClearValue, out T? resource).CheckError();
        return resource!;
    }

    public Result CreateAliasingResource1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Allocation allocation, ulong allocationLocalOffset, in ResourceDescription1 resourceDescription,
        ResourceStates initialResourceState, ClearValue? optimizedClearValue, out T? resource) where T : ID3D12Resource
    {
        ArgumentNullException.ThrowIfNull(allocation);
        ClearValue clearValue = optimizedClearValue.GetValueOrDefault();
        Guid resourceId = typeof(T).GUID;
        nint pointer = 0;
        Result result;
        fixed (ResourceDescription1* description = &resourceDescription)
        {
            result = D3D12MA.AllocatorCreateAliasingResource1(NativePointer, allocation.NativePointer, allocationLocalOffset, description, initialResourceState,
                optimizedClearValue.HasValue ? &clearValue : null, &resourceId, &pointer);
        }
        GC.KeepAlive(allocation);
        GC.KeepAlive(this);
        resource = result.Success ? MarshallingHelpers.FromPointer<T>(pointer) : null;
        return result;
    }

    public Result CreateAliasingResource2(Allocation allocation, ulong allocationLocalOffset, in ResourceDescription1 resourceDescription,
        BarrierLayout initialLayout, ReadOnlySpan<Format> castableFormats, ClearValue? optimizedClearValue, out ID3D12Resource? resource)
    {
        return CreateAliasingResource2<ID3D12Resource>(allocation, allocationLocalOffset, resourceDescription, initialLayout, castableFormats, optimizedClearValue, out resource);
    }

    public ID3D12Resource CreateAliasingResource2(Allocation allocation, ulong allocationLocalOffset, in ResourceDescription1 resourceDescription,
        BarrierLayout initialLayout, ReadOnlySpan<Format> castableFormats, ClearValue? optimizedClearValue = null)
    {
        return CreateAliasingResource2<ID3D12Resource>(allocation, allocationLocalOffset, resourceDescription, initialLayout, castableFormats, optimizedClearValue);
    }

    public T CreateAliasingResource2<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Allocation allocation, ulong allocationLocalOffset, in ResourceDescription1 resourceDescription,
        BarrierLayout initialLayout, ReadOnlySpan<Format> castableFormats, ClearValue? optimizedClearValue = null) where T : ID3D12Resource
    {
        CreateAliasingResource2(allocation, allocationLocalOffset, resourceDescription, initialLayout, castableFormats, optimizedClearValue, out T? resource).CheckError();
        return resource!;
    }

    public Result CreateAliasingResource2<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Allocation allocation, ulong allocationLocalOffset, in ResourceDescription1 resourceDescription,
        BarrierLayout initialLayout, ReadOnlySpan<Format> castableFormats, ClearValue? optimizedClearValue, out T? resource) where T : ID3D12Resource
    {
        ArgumentNullException.ThrowIfNull(allocation);
        ClearValue clearValue = optimizedClearValue.GetValueOrDefault();
        Guid resourceId = typeof(T).GUID;
        nint pointer = 0;
        Result result;
        fixed (ResourceDescription1* description = &resourceDescription)
        fixed (Format* formats = castableFormats)
        {
            result = D3D12MA.AllocatorCreateAliasingResource2(NativePointer, allocation.NativePointer, allocationLocalOffset, description, initialLayout,
                optimizedClearValue.HasValue ? &clearValue : null, (uint)castableFormats.Length, formats, &resourceId, &pointer);
        }
        GC.KeepAlive(allocation);
        GC.KeepAlive(this);
        resource = result.Success ? MarshallingHelpers.FromPointer<T>(pointer) : null;
        return result;
    }

    public Result CreatePool(in PoolDescription description, out Pool? pool)
    {
        PoolDescription.__Native native = default;
        description.__MarshalTo(ref native);
        nint pointer = 0;
        Result result = D3D12MA.AllocatorCreatePool(NativePointer, &native, &pointer);
        GC.KeepAlive(description.ProtectedSession);
        GC.KeepAlive(this);
        pool = result.Success ? MarshallingHelpers.FromPointer<Pool>(pointer) : null;
        return result;
    }

    public Pool CreatePool(in PoolDescription description)
    {
        CreatePool(description, out Pool? pool).CheckError();
        return pool!;
    }

    public ulong GetMemoryCapacity(MemorySegmentGroup memorySegmentGroup)
    {
        ulong capacity = D3D12MA.AllocatorGetMemoryCapacity(NativePointer, memorySegmentGroup);
        GC.KeepAlive(this);
        return capacity;
    }

    public void SetCurrentFrameIndex(uint frameIndex)
    {
        D3D12MA.AllocatorSetCurrentFrameIndex(NativePointer, frameIndex);
        GC.KeepAlive(this);
    }

    public void GetBudget(out Budget localBudget, out Budget nonLocalBudget)
    {
        Budget local;
        Budget nonLocal;
        D3D12MA.AllocatorGetBudget(NativePointer, &local, &nonLocal);
        GC.KeepAlive(this);
        localBudget = local;
        nonLocalBudget = nonLocal;
    }

    public TotalStatistics CalculateStatistics()
    {
        TotalStatistics.__Native native;
        D3D12MA.AllocatorCalculateStatistics(NativePointer, &native);
        GC.KeepAlive(this);
        TotalStatistics statistics = default;
        statistics.__MarshalFrom(ref native);
        return statistics;
    }

    public string BuildStatsString(bool detailedMap = false)
    {
        char* text = null;
        try
        {
            D3D12MA.AllocatorBuildStatsString(NativePointer, &text, detailedMap ? 1u : 0u);
            // The native allocator prefixes its JSON with a UTF-16 byte-order mark.
            return new string(text != null && *text == (char)0xFEFF ? text + 1 : text);
        }
        finally
        {
            D3D12MA.AllocatorFreeStatsString(NativePointer, text);
            GC.KeepAlive(this);
        }
    }

    /// <summary>Begins defragmentation of default pools. The native API does not return an HRESULT.</summary>
    public void BeginDefragmentation(in DefragmentationDescription description, out DefragmentationContext? context)
    {
        nint pointer = 0;
        fixed (DefragmentationDescription* native = &description)
        {
            D3D12MA.AllocatorBeginDefragmentation(NativePointer, native, &pointer);
        }
        GC.KeepAlive(this);
        context = MarshallingHelpers.FromPointer<DefragmentationContext>(pointer);
    }

    public DefragmentationContext BeginDefragmentation(in DefragmentationDescription description)
    {
        BeginDefragmentation(description, out DefragmentationContext? context);
        return context ?? throw new InvalidOperationException("The native allocator returned no defragmentation context.");
    }
}
