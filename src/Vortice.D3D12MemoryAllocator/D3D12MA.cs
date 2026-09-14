// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D12;

namespace Vortice.D3D12MemoryAllocator;

public static unsafe partial class D3D12MA
{
    private const string LibraryName = "D3D12MA";

    /// <summary>Allows applications to override native library resolution.</summary>
    public static event DllImportResolver? ResolveLibrary;

    static D3D12MA()
    {
        NativeLibrary.SetDllImportResolver(typeof(D3D12MA).Assembly, (name, assembly, searchPath) =>
        {
            if (ResolveLibrary is { } resolvers)
            {
                foreach (DllImportResolver resolver in resolvers.GetInvocationList())
                {
                    nint library = resolver(name, assembly, searchPath);
                    if (library != 0)
                    {
                        return library;
                    }
                }
            }

            return 0;
        });
    }

    public static Result CreateVirtualBlock(in VirtualBlockDescription description, out VirtualBlock? virtualBlock)
    {
        AllocationCallbacks callbacks = description.AllocationCallbacks.GetValueOrDefault();
        VirtualBlockDescription.__Native native = new()
        {
            Flags = description.Flags,
            Size = description.Size,
            AllocationCallbacks = description.AllocationCallbacks.HasValue ? &callbacks : null
        };
        nint pointer = 0;
        Result result = CreateVirtualBlockNative(&native, &pointer);
        virtualBlock = result.Success ? MarshallingHelpers.FromPointer<VirtualBlock>(pointer) : null;
        return result;
    }

    public static VirtualBlock CreateVirtualBlock(in VirtualBlockDescription description)
    {
        CreateVirtualBlock(description, out VirtualBlock? virtualBlock).CheckError();
        return virtualBlock!;
    }

    public static Result CreateAllocator(in AllocatorDescription description, out Allocator? allocator)
    {
        ArgumentNullException.ThrowIfNull(description.Device);
        ArgumentNullException.ThrowIfNull(description.Adapter);
        AllocationCallbacks callbacks = description.AllocationCallbacks.GetValueOrDefault();
        AllocatorDescription.__Native native = new()
        {
            Flags = description.Flags,
            Device = description.Device.NativePointer,
            PreferredBlockSize = description.PreferredBlockSize,
            AllocationCallbacks = description.AllocationCallbacks.HasValue ? &callbacks : null,
            Adapter = description.Adapter.NativePointer
        };
        nint pointer = 0;
        Result result = CreateAllocatorNative(&native, &pointer);
        GC.KeepAlive(description.Device);
        GC.KeepAlive(description.Adapter);
        allocator = result.Success ? MarshallingHelpers.FromPointer<Allocator>(pointer) : null;
        return result;
    }

    public static Allocator CreateAllocator(in AllocatorDescription description)
    {
        CreateAllocator(description, out Allocator? allocator).CheckError();
        return allocator!;
    }

    /// <summary>Wraps a borrowed native pointer in an independently owned reference.</summary>
    internal static T? GetReference<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(nint pointer) where T : ComObject
    {
        if (pointer == 0)
        {
            return null;
        }
        Marshal.AddRef(pointer);
        return MarshallingHelpers.FromPointer<T>(pointer);
    }

    internal static Result WrapResourceResult<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        Result result, nint allocationPointer, nint resourcePointer, out Allocation? allocation, out T? resource) where T : ID3D12Resource
    {
        if (result.Failure)
        {
            allocation = null;
            resource = null;
            return result;
        }
        allocation = MarshallingHelpers.FromPointer<Allocation>(allocationPointer);
        resource = MarshallingHelpers.FromPointer<T>(resourcePointer);
        return result;
    }
}
