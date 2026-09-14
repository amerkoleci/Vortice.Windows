// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;
using NUnit.Framework;
using SharpGen.Runtime;
using Vortice.Direct3D12;
using Vortice.DXGI;

namespace Vortice.D3D12MemoryAllocator.Tests;

[TestFixture]
public class AllocatorTests
{
    [Test]
    public void CreatesResourcesAndAliasesWithDevice8()
    {
        using D3D12TestContext context = new();
        using ID3D12Device8? device = context.Device.QueryInterfaceOrNull<ID3D12Device8>();
        if (device == null)
        {
            Assert.Ignore("ID3D12Device8 is unavailable on this Windows version.");
        }
        ResourceDescription1 description = ResourceDescription1.Buffer(65536);
        using Allocation allocation = context.Allocator.CreateResource2(
            new AllocationDescription(HeapType.Default, AllocationFlags.CanAlias), description,
            ResourceStates.Common, out ID3D12Resource resource);
        using (resource)
        using (ID3D12Resource alias = context.Allocator.CreateAliasingResource1(allocation, 0, description, ResourceStates.Common))
        {
            Assert.That(resource.Description.Width, Is.EqualTo(65536));
            Assert.That(alias.Description.Width, Is.EqualTo(65536));
        }
    }

    [TestCase(false)]
    [TestCase(true)]
    public void CreatesResourcesAndAliasesWithDevice10(bool useCastableFormats)
    {
        using D3D12TestContext context = new();
        using ID3D12Device10? device = context.Device.QueryInterfaceOrNull<ID3D12Device10>();
        if (device == null || !context.Device.Options12.EnhancedBarriersSupported)
        {
            Assert.Ignore("ID3D12Device10 and enhanced barriers are required.");
        }
        if (useCastableFormats && !context.Device.Options12.RelaxedFormatCastingSupported)
        {
            Assert.Ignore("Relaxed format casting is unavailable.");
        }
        ResourceDescription1 description = useCastableFormats
            ? ResourceDescription1.Texture2D(Format.R8G8B8A8_UNorm, 32, 32)
            : ResourceDescription1.Buffer(65536);
        Format[] formats = useCastableFormats ? [Format.R8G8B8A8_UNorm, Format.R8G8B8A8_UNorm_SRgb] : [];
        BarrierLayout layout = useCastableFormats ? BarrierLayout.Common : BarrierLayout.Undefined;
        using Allocation allocation = context.Allocator.CreateResource3(
            new AllocationDescription(HeapType.Default, AllocationFlags.CanAlias), description, layout, formats, out ID3D12Resource resource);
        using (resource)
        using (ID3D12Resource alias = context.Allocator.CreateAliasingResource2(allocation, 0, description, layout, formats))
        {
            Assert.That(resource.Description.Width, Is.EqualTo(description.Width));
            Assert.That(alias.Description.Width, Is.EqualTo(description.Width));
        }
    }

    [Test]
    public void ResourceGettersOwnIndependentReferences()
    {
        using D3D12TestContext context = new();
        using Allocation allocation = context.Allocator.CreateResource(
            new AllocationDescription(HeapType.Upload), ResourceDescription.Buffer(65536),
            ResourceStates.GenericRead, out ID3D12Resource resource);
        using (resource)
        {
            allocation.Name = "buffer λ";
            allocation.PrivateData = 42;
            Assert.That(allocation.Name, Is.EqualTo("buffer λ"));
            Assert.That(allocation.PrivateData, Is.EqualTo((nint)42));
            Assert.That(allocation.Size, Is.GreaterThanOrEqualTo(65536));
            Assert.That(allocation.Alignment, Is.GreaterThan(0));
            using (ID3D12Resource acquired = allocation.Resource!)
            {
                Assert.That(acquired.NativePointer, Is.EqualTo(resource.NativePointer));
            }
            using ID3D12Resource acquiredAgain = allocation.Resource!;
            Assert.That(acquiredAgain.Description.Width, Is.EqualTo(65536));
            allocation.Resource = resource;
            allocation.Name = null;
            Assert.That(allocation.Name, Is.Null);
            resource.SetData<uint>([1, 2, 3, 4]);
        }
        context.Allocator.SetCurrentFrameIndex(1);
        context.Allocator.GetBudget(out Budget local, out Budget nonLocal);
        Assert.That(local.Statistics.AllocationCount + nonLocal.Statistics.AllocationCount, Is.GreaterThanOrEqualTo(1));
        Assert.That(context.Allocator.GetMemoryCapacity(MemorySegmentGroup.Local), Is.GreaterThan(0));
        _ = context.Allocator.IsUMA;
        _ = context.Allocator.IsCacheCoherentUMA;
        _ = context.Allocator.IsGPUUploadHeapSupported;
    }

    [Test]
    public void ReferenceCountsFollowTheOwnershipModel()
    {
        using D3D12TestContext context = new();
        using Allocation allocation = context.Allocator.CreateResource(
            new AllocationDescription(HeapType.Default), ResourceDescription.Buffer(65536),
            ResourceStates.Common, out ID3D12Resource resource);
        using (resource)
        {
            Assert.That(ReferenceCount(allocation), Is.EqualTo(1));
            Assert.That(ReferenceCount(resource), Is.EqualTo(2), "The allocation and the returned wrapper each own one reference.");
            using (ID3D12Resource acquired = allocation.Resource!)
            {
                Assert.That(ReferenceCount(resource), Is.EqualTo(3), "Each getter read acquires its own reference.");
            }
            Assert.That(ReferenceCount(resource), Is.EqualTo(2));
            using ID3D12Heap heap = allocation.Heap!;
            uint heapReferences = ReferenceCount(heap);
            using (ID3D12Heap acquired = allocation.Heap!)
            {
                Assert.That(ReferenceCount(heap), Is.EqualTo(heapReferences + 1));
            }
            Assert.That(ReferenceCount(heap), Is.EqualTo(heapReferences));
        }
    }

    [Test]
    public void CreatesTextureAndAliasingResources()
    {
        using D3D12TestContext context = new();
        ResourceDescription description = ResourceDescription.Texture2D(Format.R8G8B8A8_UNorm, 32, 32);
        using Allocation textureAllocation = context.Allocator.CreateResource(
            new AllocationDescription(HeapType.Default),
            description, ResourceStates.Common, out ID3D12Resource texture);
        texture.Dispose();
        ResourceAllocationInfo info = context.Device.GetResourceAllocationInfo(0, description);
        using Allocation memory = context.Allocator.AllocateMemory(
            new AllocationDescription(HeapType.Default) { Flags = AllocationFlags.CanAlias, ExtraHeapFlags = HeapFlags.AllowOnlyNonRenderTargetDepthStencilTextures }, info);
        using ID3D12Resource alias = context.Allocator.CreateAliasingResource(memory, 0, description, ResourceStates.Common);
        using ID3D12Heap heap = memory.Heap!;
        Assert.That(alias.Description.Width, Is.EqualTo(32));
        Assert.That(heap.NativePointer, Is.Not.EqualTo(nint.Zero));
        Assert.That(memory.Resource, Is.Null);
    }

    private static uint ReferenceCount(ComObject value)
    {
        Marshal.AddRef(value.NativePointer);
        return (uint)Marshal.Release(value.NativePointer);
    }
}
