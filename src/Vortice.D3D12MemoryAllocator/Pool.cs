// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

/// <summary>A custom memory pool. Dispose its allocations and contexts before the pool.</summary>
public unsafe class Pool : ComObject
{
    public Pool(nint nativePointer) : base(nativePointer) { }

    /// <summary>Gets the pool description. A returned ProtectedSession reference must be disposed by the caller.</summary>
    public PoolDescription Description
    {
        get
        {
            PoolDescription.__Native native;
            D3D12MA.PoolGetDescription(NativePointer, &native);
            PoolDescription description = default;
            description.__MarshalFrom(ref native);
            GC.KeepAlive(this);
            return description;
        }
    }

    public Statistics Statistics
    {
        get
        {
            Statistics statistics;
            D3D12MA.PoolGetStatistics(NativePointer, &statistics);
            GC.KeepAlive(this);
            return statistics;
        }
    }

    public DetailedStatistics CalculateStatistics()
    {
        DetailedStatistics statistics;
        D3D12MA.PoolCalculateStatistics(NativePointer, &statistics);
        GC.KeepAlive(this);
        return statistics;
    }

    public string? Name
    {
        get
        {
            string? name = Marshal.PtrToStringUni(D3D12MA.PoolGetName(NativePointer));
            GC.KeepAlive(this);
            return name;
        }
        set
        {
            fixed (char* name = value)
            {
                D3D12MA.PoolSetName(NativePointer, name);
            }
            GC.KeepAlive(this);
        }
    }

    public Result BeginDefragmentation(in DefragmentationDescription description, out DefragmentationContext? context)
    {
        nint pointer = 0;
        Result result;
        fixed (DefragmentationDescription* native = &description)
        {
            result = D3D12MA.PoolBeginDefragmentation(NativePointer, native, &pointer);
        }
        GC.KeepAlive(this);
        context = result.Success ? MarshallingHelpers.FromPointer<DefragmentationContext>(pointer) : null;
        return result;
    }

    public DefragmentationContext BeginDefragmentation(in DefragmentationDescription description)
    {
        BeginDefragmentation(description, out DefragmentationContext? context).CheckError();
        return context!;
    }
}
