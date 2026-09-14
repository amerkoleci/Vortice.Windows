// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Text.Json;
using NUnit.Framework;
using SharpGen.Runtime;
using Vortice.Direct3D12;

namespace Vortice.D3D12MemoryAllocator.Tests;

[TestFixture]
public class PoolTests
{
    [Test]
    public void PoolSuballocatesAndReportsExhaustion()
    {
        using D3D12TestContext context = new();
        PoolDescription description = new(HeapType.Upload)
        {
            HeapFlags = HeapFlags.AllowOnlyBuffers,
            BlockSize = 262144,
            MinBlockCount = 1,
            MaxBlockCount = 1
        };
        using Pool pool = context.Allocator.CreatePool(description);
        pool.Name = "pool";
        Assert.That(pool.Name, Is.EqualTo("pool"));
        Assert.That(pool.Description.BlockSize, Is.EqualTo(description.BlockSize));
        AllocationDescription allocationDescription = new(HeapType.Upload) { CustomPool = pool };
        using (Allocation first = context.Allocator.CreateResource(allocationDescription,
            ResourceDescription.Buffer(65536), ResourceStates.GenericRead, out ID3D12Resource firstResource))
        using (firstResource)
        using (Allocation second = context.Allocator.CreateResource(allocationDescription,
            ResourceDescription.Buffer(65536), ResourceStates.GenericRead, out ID3D12Resource secondResource))
        using (secondResource)
        {
            using ID3D12Heap firstHeap = first.Heap!;
            using ID3D12Heap secondHeap = second.Heap!;
            Assert.That(firstHeap.NativePointer, Is.EqualTo(secondHeap.NativePointer));
            Assert.That(first.Offset + first.Size <= second.Offset || second.Offset + second.Size <= first.Offset, Is.True);
            Assert.That(pool.Statistics.AllocationCount, Is.EqualTo(2));
            Assert.That(pool.CalculateStatistics().Statistics.AllocationCount, Is.EqualTo(2));
            allocationDescription.Flags = AllocationFlags.NeverAllocate;
            Result result = context.Allocator.CreateResource(allocationDescription, ResourceDescription.Buffer(524288),
                ResourceStates.GenericRead, null, out Allocation? failedAllocation, out ID3D12Resource? failedResource);
            Assert.That(result.Failure, Is.True);
            Assert.That(failedAllocation, Is.Null);
            Assert.That(failedResource, Is.Null);
            using JsonDocument json = JsonDocument.Parse(context.Allocator.BuildStatsString(true));
            Assert.That(json.RootElement.ValueKind, Is.EqualTo(JsonValueKind.Object));
            TotalStatistics statistics = context.Allocator.CalculateStatistics();
            Assert.That(statistics.HeapType.Length, Is.EqualTo(5));
            Assert.That(statistics.MemorySegmentGroup.Length, Is.EqualTo(2));
            Assert.That(statistics.Total.Statistics.AllocationCount, Is.EqualTo(2));
        }
        Assert.That(pool.Statistics.AllocationCount, Is.Zero);
    }
}
