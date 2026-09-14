// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using NUnit.Framework;
using Vortice.Direct3D12;

namespace Vortice.D3D12MemoryAllocator.Tests;

[TestFixture]
public class AllocationWorkloadTests
{
    private const int ResourceCount = 128;
    private const int ResourceBytes = 65536;
    private const int ReplacementRounds = 4;
    private const int BlockCount = 4;
    private const ulong TotalBytes = ResourceCount * ResourceBytes;

    [Test]
    public void PooledResourcesReuseHeapsAndPreserveData()
    {
        using D3D12TestContext context = new();
        Statistics committed = RunWorkload(context, committed: true);
        Statistics pooled = RunWorkload(context, committed: false);
        Assert.That(committed.BlockCount, Is.EqualTo(ResourceCount));
        Assert.That(pooled.BlockCount, Is.EqualTo(BlockCount), "Suballocation must reduce the workload to four heaps.");
        Assert.That(pooled.AllocationBytes, Is.EqualTo(committed.AllocationBytes));
        Assert.That(pooled.BlockBytes, Is.EqualTo(committed.BlockBytes));
        TestContext.Out.WriteLine($"Same {TotalBytes / 1048576} MiB workload: {committed.BlockCount} committed heaps -> {pooled.BlockCount} shared heaps.");
    }

    private static Statistics RunWorkload(D3D12TestContext context, bool committed)
    {
        using Pool? pool = committed ? null : context.Allocator.CreatePool(new PoolDescription(HeapType.Default)
        {
            HeapFlags = HeapFlags.AllowOnlyBuffers,
            BlockSize = TotalBytes / BlockCount,
            MinBlockCount = BlockCount,
            MaxBlockCount = BlockCount
        });
        // Staging resources are outside the allocator so its statistics measure only the workload.
        using ID3D12Resource upload = context.Device.CreateCommittedResource(HeapType.Upload,
            ResourceDescription.Buffer(TotalBytes), ResourceStates.GenericRead);
        using ID3D12Resource readback = context.Device.CreateCommittedResource(HeapType.Readback,
            ResourceDescription.Buffer(TotalBytes), ResourceStates.CopyDest);
        Allocation?[] allocations = new Allocation[ResourceCount];
        ID3D12Resource?[] resources = new ID3D12Resource[ResourceCount];
        uint[] expected = new uint[TotalBytes / sizeof(uint)];
        uint[] actual = new uint[expected.Length];
        HashSet<nint> originalHeaps = [];
        Statistics snapshot = default;
        try
        {
            for (int round = 0; round <= ReplacementRounds; round++)
            {
                HashSet<(nint Heap, ulong Offset)> freedLocations = [];
                if (round != 0)
                {
                    for (int i = round % 2; i < ResourceCount; i += 2)
                    {
                        if (!committed)
                        {
                            freedLocations.Add(GetLocation(allocations[i]!));
                        }
                        resources[i]!.Dispose();
                        resources[i] = null;
                        allocations[i]!.Dispose();
                        allocations[i] = null;
                    }
                    Assert.That(context.Allocator.CalculateStatistics().Total.Statistics.AllocationCount, Is.EqualTo(ResourceCount / 2));
                }

                for (int i = 0; i < ResourceCount; i++)
                {
                    if (resources[i] != null)
                    {
                        continue;
                    }
                    AllocationFlags flags = committed ? AllocationFlags.Committed
                        : round == 0 ? AllocationFlags.None : AllocationFlags.NeverAllocate;
                    allocations[i] = context.Allocator.CreateResource(
                        new AllocationDescription(HeapType.Default, flags) { CustomPool = pool },
                        ResourceDescription.Buffer(ResourceBytes), ResourceStates.CopyDest, out ID3D12Resource resource);
                    resources[i] = resource;
                    if (!committed && round != 0)
                    {
                        Assert.That(freedLocations.Remove(GetLocation(allocations[i]!)), Is.True,
                            "A replacement must reuse a location freed in this round.");
                    }
                    for (int word = 0; word < ResourceBytes / sizeof(uint); word++)
                    {
                        expected[i * (ResourceBytes / sizeof(uint)) + word] = ((uint)(round + 1) << 24) | ((uint)i << 16) | (uint)word;
                    }
                }

                Statistics statistics = context.Allocator.CalculateStatistics().Total.Statistics;
                Assert.That(statistics.AllocationCount, Is.EqualTo(ResourceCount));
                Assert.That(statistics.AllocationBytes, Is.EqualTo(TotalBytes));
                Assert.That(statistics.BlockCount, Is.EqualTo(committed ? ResourceCount : BlockCount));
                Assert.That(statistics.BlockBytes, Is.EqualTo(TotalBytes));
                if (!committed)
                {
                    HashSet<nint> heaps = [];
                    foreach (Allocation? allocation in allocations)
                    {
                        heaps.Add(GetLocation(allocation!).Heap);
                    }
                    Assert.That(heaps.Count, Is.EqualTo(BlockCount), "Verify actual heap identities as well as allocator statistics.");
                    if (round == 0)
                    {
                        originalHeaps = heaps;
                    }
                    else
                    {
                        Assert.That(heaps.SetEquals(originalHeaps), Is.True, "Replacement must not allocate different heaps.");
                        Assert.That(freedLocations, Is.Empty);
                    }
                }
                CopyAndVerify(context.Device, upload, readback, resources, expected, actual, round);
                snapshot = statistics;
            }
        }
        finally
        {
            for (int i = 0; i < ResourceCount; i++)
            {
                resources[i]?.Dispose();
                allocations[i]?.Dispose();
            }
        }
        Assert.That(context.Allocator.CalculateStatistics().Total.Statistics.AllocationCount, Is.Zero);
        TestContext.Out.WriteLine($"{(committed ? "Committed" : "Pooled")}: {snapshot.BlockCount} heaps, " +
            $"{ReplacementRounds * ResourceCount / 2} replacements, {(ReplacementRounds + 1) * TotalBytes / 1048576} MiB verified by GPU readback, zero live allocations after cleanup.");
        return snapshot;
    }

    private static (nint Heap, ulong Offset) GetLocation(Allocation allocation)
    {
        using ID3D12Heap? heap = allocation.Heap;
        Assert.That(heap, Is.Not.Null, "A pooled resource must have an explicit shared heap.");
        return (heap!.NativePointer, allocation.Offset);
    }

    private static void CopyAndVerify(ID3D12Device device, ID3D12Resource upload, ID3D12Resource readback,
        ID3D12Resource?[] resources, uint[] expected, uint[] actual, int round)
    {
        upload.SetData<uint>(expected);
        using ID3D12CommandQueue queue = device.CreateCommandQueue(CommandListType.Direct);
        using ID3D12CommandAllocator commandAllocator = device.CreateCommandAllocator(CommandListType.Direct);
        using ID3D12GraphicsCommandList commands = device.CreateCommandList<ID3D12GraphicsCommandList>(CommandListType.Direct, commandAllocator);
        using ID3D12Fence fence = device.CreateFence();
        for (int i = 0; i < ResourceCount; i++)
        {
            ID3D12Resource resource = resources[i]!;
            ulong offset = (ulong)i * ResourceBytes;
            if (round == 0 || i % 2 == round % 2)
            {
                // Leave survivors untouched so readback detects corruption caused by replacements.
                commands.CopyBufferRegion(resource, 0, upload, offset, ResourceBytes);
                commands.ResourceBarrierTransition(resource, ResourceStates.CopyDest, ResourceStates.CopySource);
            }
            commands.CopyBufferRegion(readback, offset, resource, 0, ResourceBytes);
        }
        commands.Close();
        queue.ExecuteCommandList(commands);
        queue.Signal(fence, 1).CheckError();
        fence.SetEventOnCompletion(1).CheckError();
        readback.GetData<uint>(actual.AsSpan());
        Assert.That(actual.AsSpan().SequenceEqual(expected), Is.True, $"GPU data must match in round {round}, including surviving resources.");
    }
}
