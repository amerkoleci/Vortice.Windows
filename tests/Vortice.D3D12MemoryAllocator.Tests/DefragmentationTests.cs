// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using NUnit.Framework;
using Vortice.Direct3D12;

namespace Vortice.D3D12MemoryAllocator.Tests;

[TestFixture]
public class DefragmentationTests
{
    [Test]
    public void EmptyAllocatorFinishesWithoutMoves()
    {
        using D3D12TestContext context = new();
        using DefragmentationContext defragmentation = context.Allocator.BeginDefragmentation(default);
        Assert.That(defragmentation.BeginPass(out DefragmentationPassMoveInfo pass).Code, Is.Zero);
        Assert.That(pass.MoveCount, Is.Zero);
        Assert.That(defragmentation.Statistics.AllocationsMoved, Is.Zero);
    }

    [Test]
    public void LinearPoolRejectsDefragmentation()
    {
        using D3D12TestContext context = new();
        using Pool pool = context.Allocator.CreatePool(new PoolDescription(HeapType.Default) { Flags = PoolFlags.AlgorithmLinear });
        Assert.That(pool.BeginDefragmentation(default, out DefragmentationContext? defragmentation).Failure, Is.True);
        Assert.That(defragmentation, Is.Null);
    }

    [TestCase(DefragmentationMoveOperation.Copy, true)]
    [TestCase(DefragmentationMoveOperation.Ignore, true)]
    [TestCase(DefragmentationMoveOperation.Destroy, true)]
    [TestCase(DefragmentationMoveOperation.Copy, false)]
    [TestCase(DefragmentationMoveOperation.Ignore, false)]
    [TestCase(DefragmentationMoveOperation.Destroy, false)]
    public void FragmentedBlocksProcessRealMoves(DefragmentationMoveOperation operation, bool customPool)
    {
        // Default pools take their block size from the allocator; the custom pool sets its own.
        using D3D12TestContext context = new(preferredBlockSize: customPool ? 0UL : 262144UL);
        using Pool? pool = customPool ? context.Allocator.CreatePool(new PoolDescription(HeapType.Default)
        {
            HeapFlags = HeapFlags.AllowOnlyBuffers,
            BlockSize = 262144,
            MaxBlockCount = 3
        }) : null;
        // Only default-heap allocations are counted, so the staging buffers below do not affect the expectations.
        uint AllocationCount() => pool?.Statistics.AllocationCount
            ?? context.Allocator.CalculateStatistics().HeapType[0].Statistics.AllocationCount;
        DefragmentationContext BeginDefragmentation(DefragmentationDescription description)
            => pool?.BeginDefragmentation(description) ?? context.Allocator.BeginDefragmentation(description);

        AllocationDescription description = new(HeapType.Default) { CustomPool = pool };
        ResourceDescription resourceDescription = ResourceDescription.Buffer(65536);
        List<Allocation> allocations = [];
        try
        {
            using Allocation uploadAllocation = context.Allocator.CreateResource(new AllocationDescription(HeapType.Upload),
                resourceDescription, ResourceStates.GenericRead, out ID3D12Resource upload);
            using (upload)
            {
                uint[] expected = Enumerable.Range(0, 16384).Select(i => (uint)i * 17 + 3).ToArray();
                upload.SetData<uint>(expected);
                for (int i = 0; i < 12; i++)
                {
                    Allocation allocation = context.Allocator.CreateResource(description, resourceDescription,
                        ResourceStates.CopyDest, out ID3D12Resource resource);
                    allocations.Add(allocation);
                    using (resource)
                    {
                        Submit(context.Device, commands =>
                        {
                            commands.CopyResource(resource, upload);
                            commands.ResourceBarrierTransition(resource, ResourceStates.CopyDest, ResourceStates.CopySource);
                        });
                    }
                }

                // Free scattered slots so that at least one allocation can be compacted into an earlier block.
                foreach (int index in new[] { 0, 1, 4, 5 })
                {
                    allocations[index].Dispose();
                }
                Assert.That(AllocationCount(), Is.EqualTo(8));
                using DefragmentationContext defragmentation = BeginDefragmentation(new DefragmentationDescription
                {
                    Flags = DefragmentationFlags.AlgorithmFull,
                    MaxAllocationsPerPass = 1
                });
                Assert.That(defragmentation.BeginPass(out DefragmentationPassMoveInfo pass).Code, Is.EqualTo(1), "S_FALSE must expose pending moves.");
                Assert.That(pass.MoveCount, Is.EqualTo(1), "The fixture must actually exercise a move.");
                ref DefragmentationMove move = ref pass.Moves[0];
                move.Operation = operation;
                Allocation original;
                using (Allocation source = move.GetSourceAllocation())
                {
                    original = allocations.Single(allocation => allocation.NativePointer == source.NativePointer);
                    if (operation == DefragmentationMoveOperation.Copy)
                    {
                        using Allocation destination = move.GetDestinationTemporaryAllocation();
                        using ID3D12Resource sourceResource = source.Resource!;
                        using ID3D12Resource destinationResource = context.Allocator.CreateAliasingResource(destination, 0,
                            resourceDescription, ResourceStates.CopyDest);
                        Submit(context.Device, commands =>
                        {
                            commands.CopyResource(destinationResource, sourceResource);
                            commands.ResourceBarrierTransition(destinationResource, ResourceStates.CopyDest, ResourceStates.CopySource);
                        });
                        destination.Resource = destinationResource;
                    }
                    else if (operation == DefragmentationMoveOperation.Destroy)
                    {
                        // EndPass consumes the application's original allocation reference.
                        original.NativePointer = 0;
                    }
                }
                defragmentation.EndPass(ref pass).CheckError();
                Assert.That(pass.MoveCount, Is.Zero);
                Assert.That(AllocationCount(), Is.EqualTo(operation == DefragmentationMoveOperation.Destroy ? 7 : 8));
                Assert.That(defragmentation.Statistics.AllocationsMoved, Is.EqualTo(operation == DefragmentationMoveOperation.Copy ? 1 : 0));

                if (operation == DefragmentationMoveOperation.Copy)
                {
                    using ID3D12Resource movedResource = original.Resource!;
                    using Allocation readbackAllocation = context.Allocator.CreateResource(new AllocationDescription(HeapType.Readback),
                        resourceDescription, ResourceStates.CopyDest, out ID3D12Resource readback);
                    using (readback)
                    {
                        Submit(context.Device, commands => commands.CopyResource(readback, movedResource));
                        uint[] actual = new uint[expected.Length];
                        readback.GetData<uint>(actual.AsSpan());
                        Assert.That(actual, Is.EqualTo(expected), "Copy must preserve data after EndPass replaces the source resource.");
                    }
                }
            }
        }
        finally
        {
            foreach (Allocation allocation in allocations)
            {
                allocation.Dispose();
            }
        }
        Assert.That(AllocationCount(), Is.Zero);
    }

    private static void Submit(ID3D12Device device, Action<ID3D12GraphicsCommandList> record)
    {
        using ID3D12CommandQueue queue = device.CreateCommandQueue(CommandListType.Direct);
        using ID3D12CommandAllocator allocator = device.CreateCommandAllocator(CommandListType.Direct);
        using ID3D12GraphicsCommandList commands = device.CreateCommandList<ID3D12GraphicsCommandList>(CommandListType.Direct, allocator);
        using ID3D12Fence fence = device.CreateFence();
        record(commands);
        commands.Close();
        queue.ExecuteCommandList(commands);
        queue.Signal(fence, 1).CheckError();
        fence.SetEventOnCompletion(1).CheckError();
    }
}
