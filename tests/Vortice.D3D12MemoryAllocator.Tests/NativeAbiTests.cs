// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace Vortice.D3D12MemoryAllocator.Tests;

[TestFixture]
public class NativeAbiTests
{
    // sizeof/offsetof from the C++ headers at native_libs commit
    // 35a88f6dd367982e8790f0df280753fdd1b62bed, shipped in Vortice.D3D12MA.Native 1.0.2.
    [TestCase(typeof(AllocationCallbacks), 24, "Allocate:0 Free:8 PrivateData:16")]
    [TestCase(typeof(AllocatorDescription), 40, "Flags:0 Device:8 PreferredBlockSize:16 AllocationCallbacks:24 Adapter:32")]
    [TestCase(typeof(PoolDescription), 72, "Flags:0 HeapProperties:4 HeapFlags:24 BlockSize:32 MinBlockCount:40 MaxBlockCount:44 MinAllocationAlignment:48 ProtectedSession:56 ResidencyPriority:64")]
    [TestCase(typeof(VirtualBlockDescription), 24, "Flags:0 Size:8 AllocationCallbacks:16")]
    [TestCase(typeof(AllocationDescription), 32, "Flags:0 HeapType:4 ExtraHeapFlags:8 CustomPool:16 PrivateData:24")]
    [TestCase(typeof(VirtualAllocationDescription), 32, "Flags:0 Size:8 Alignment:16 PrivateData:24")]
    [TestCase(typeof(VirtualAllocationInfo), 24, "Offset:0 Size:8 PrivateData:16")]
    [TestCase(typeof(Statistics), 24, "BlockCount:0 AllocationCount:4 BlockBytes:8 AllocationBytes:16")]
    [TestCase(typeof(DetailedStatistics), 64, "Statistics:0 UnusedRangeCount:24 AllocationSizeMin:32 AllocationSizeMax:40 UnusedRangeSizeMin:48 UnusedRangeSizeMax:56")]
    [TestCase(typeof(TotalStatistics), 512, "HeapType0:0 MemorySegment0:320 Total:448")]
    [TestCase(typeof(Budget), 40, "Statistics:0 UsageBytes:24 BudgetBytes:32")]
    [TestCase(typeof(DefragmentationMove), 24, "Operation:0 _sourceAllocation:8 _destinationTemporaryAllocation:16")]
    [TestCase(typeof(DefragmentationPassMoveInfo), 16, "_moveCount:0 _moves:8")]
    [TestCase(typeof(DefragmentationStatistics), 24, "BytesMoved:0 BytesFreed:8 AllocationsMoved:16 HeapsFreed:20")]
    [TestCase(typeof(DefragmentationDescription), 24, "Flags:0 MaxBytesPerPass:8 MaxAllocationsPerPass:16")]
    [TestCase(typeof(VirtualAllocation), 8, "Handle:0")]
    public void LayoutMatchesNativeHeaders([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicNestedTypes)] Type type, int size, string fields)
    {
        Assert.That(IntPtr.Size, Is.EqualTo(8), "The native package supports Windows x64 and ARM64.");
        Type nativeType = type.GetNestedType("__Native", BindingFlags.NonPublic) ?? type;
        Assert.That(Marshal.SizeOf(nativeType), Is.EqualTo(size));
        foreach (string field in fields.Split(' '))
        {
            string[] parts = field.Split(':');
            Assert.That(Marshal.OffsetOf(nativeType, parts[0]).ToInt32(), Is.EqualTo(int.Parse(parts[1])), parts[0]);
        }
    }
}
