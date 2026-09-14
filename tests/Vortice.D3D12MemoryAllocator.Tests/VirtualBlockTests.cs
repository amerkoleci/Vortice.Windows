// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using NUnit.Framework;
using SharpGen.Runtime;
using Vortice.Direct3D12;

namespace Vortice.D3D12MemoryAllocator.Tests;

[TestFixture]
public unsafe class VirtualBlockTests
{
    [Test]
    public void AllocationsAreAlignedAndReusable()
    {
        using VirtualBlock block = D3D12MA.CreateVirtualBlock(new VirtualBlockDescription(4096));
        VirtualAllocation first = block.Allocate(new VirtualAllocationDescription(256, 64), out ulong firstOffset);
        VirtualAllocation second = block.Allocate(new VirtualAllocationDescription(512, 128), out ulong secondOffset);
        Assert.That(firstOffset % 64, Is.Zero);
        Assert.That(secondOffset % 128, Is.Zero);
        Assert.That(firstOffset + 256 <= secondOffset || secondOffset + 512 <= firstOffset, Is.True);
        block.SetAllocationPrivateData(first, 123);
        VirtualAllocationInfo info = block.GetAllocationInfo(first);
        Assert.That(info.Offset, Is.EqualTo(firstOffset));
        Assert.That(info.Size, Is.EqualTo(256));
        Assert.That(info.PrivateData, Is.EqualTo((nint)123));
        Assert.That(block.Statistics.AllocationCount, Is.EqualTo(2));
        Assert.That(block.CalculateStatistics().Statistics.AllocationBytes, Is.EqualTo(768));
        using (JsonDocument json = JsonDocument.Parse(block.BuildStatsString()))
        {
            Assert.That(json.RootElement.ValueKind, Is.EqualTo(JsonValueKind.Object));
        }
        block.FreeAllocation(first);
        block.FreeAllocation(second);
        Assert.That(block.IsEmpty, Is.True);
        VirtualAllocation entire = block.Allocate(new VirtualAllocationDescription(4096), out _);
        Result result = block.Allocate(new VirtualAllocationDescription(1), out VirtualAllocation failed, out _);
        Assert.That(result.Failure, Is.True);
        Assert.That(failed.IsNull, Is.True);
        block.FreeAllocation(entire);
    }

    [Test]
    public void LinearBlockCanBeClearedAndDisposedTwice()
    {
        VirtualBlock block = D3D12MA.CreateVirtualBlock(new VirtualBlockDescription(1024, VirtualBlockFlags.AlgorithmLinear));
        block.Allocate(new VirtualAllocationDescription(32), out _);
        block.Clear();
        Assert.That(block.IsEmpty, Is.True);
        block.QueryInterface(new Guid("00000000-0000-0000-C000-000000000046"), out nint unknown).CheckError();
        using (ComObject reference = new(unknown))
        {
            Assert.That(reference.NativePointer, Is.EqualTo(block.NativePointer));
        }
        block.Dispose();
        block.Dispose();
        Assert.That(block.NativePointer, Is.EqualTo(nint.Zero));
    }

    [TestCase(false)]
    [TestCase(true)]
    public void CallbacksRemainValidUntilNativeDisposal(bool useAllocator)
    {
        Counters counters = default;
        AllocationCallbacks callbacks = new()
        {
            Allocate = &Allocate,
            Free = &Free,
            PrivateData = (nint)(&counters)
        };
        if (useAllocator)
        {
            using D3D12TestContext context = new();
            using Allocator allocator = D3D12MA.CreateAllocator(new AllocatorDescription(context.Device, context.Adapter) { AllocationCallbacks = callbacks });
            using Allocation allocation = allocator.CreateResource(new AllocationDescription(HeapType.Upload),
                ResourceDescription.Buffer(65536), ResourceStates.GenericRead, out ID3D12Resource resource);
            resource.Dispose();
            using JsonDocument json = JsonDocument.Parse(allocator.BuildStatsString(true));
        }
        else
        {
            VirtualBlockDescription description = new(4096) { AllocationCallbacks = callbacks };
            using VirtualBlock block = D3D12MA.CreateVirtualBlock(description);
            block.Allocate(new VirtualAllocationDescription(64), out _);
            using JsonDocument json = JsonDocument.Parse(block.BuildStatsString());
            block.Clear();
        }
        Assert.That(counters.Allocations, Is.GreaterThan(0));
        Assert.That(counters.Frees, Is.EqualTo(counters.Allocations));
        Assert.That(counters.Misaligned, Is.Zero);
    }

    private struct Counters
    {
        public int Allocations;
        public int Frees;
        public int Misaligned;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void* Allocate(nuint size, nuint alignment, void* privateData)
    {
        Counters* counters = (Counters*)privateData;
        void* pointer = NativeMemory.AlignedAlloc(size, alignment);
        counters->Allocations++;
        if ((nuint)pointer % alignment != 0) counters->Misaligned++;
        return pointer;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void Free(void* memory, void* privateData)
    {
        if (memory == null) return;
        ((Counters*)privateData)->Frees++;
        NativeMemory.AlignedFree(memory);
    }
}
