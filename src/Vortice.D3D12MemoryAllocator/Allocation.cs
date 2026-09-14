// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D12;

namespace Vortice.D3D12MemoryAllocator;

/// <summary>A native allocation. Keep its allocator and custom pool alive until disposal.</summary>
public unsafe class Allocation : ComObject
{
    public Allocation(nint nativePointer) : base(nativePointer) { }

    public ulong Offset
    {
        get
        {
            ulong value = D3D12MA.AllocationGetOffset(NativePointer);
            GC.KeepAlive(this);
            return value;
        }
    }

    public ulong Alignment
    {
        get
        {
            ulong value = D3D12MA.AllocationGetAlignment(NativePointer);
            GC.KeepAlive(this);
            return value;
        }
    }

    public ulong Size
    {
        get
        {
            ulong value = D3D12MA.AllocationGetSize(NativePointer);
            GC.KeepAlive(this);
            return value;
        }
    }

    /// <summary>Gets an independently owned resource reference, or assigns the native allocation's resource.</summary>
    public ID3D12Resource? Resource
    {
        get
        {
            ID3D12Resource? resource = D3D12MA.GetReference<ID3D12Resource>(D3D12MA.AllocationGetResource(NativePointer));
            GC.KeepAlive(this);
            return resource;
        }
        set
        {
            D3D12MA.AllocationSetResource(NativePointer, value?.NativePointer ?? 0);
            GC.KeepAlive(value);
            GC.KeepAlive(this);
        }
    }

    /// <summary>Gets an independently owned heap reference, or null for an implicit committed heap.</summary>
    public ID3D12Heap? Heap
    {
        get
        {
            ID3D12Heap? heap = D3D12MA.GetReference<ID3D12Heap>(D3D12MA.AllocationGetHeap(NativePointer));
            GC.KeepAlive(this);
            return heap;
        }
    }

    public nint PrivateData
    {
        get
        {
            nint value = D3D12MA.AllocationGetPrivateData(NativePointer);
            GC.KeepAlive(this);
            return value;
        }
        set
        {
            D3D12MA.AllocationSetPrivateData(NativePointer, value);
            GC.KeepAlive(this);
        }
    }

    public string? Name
    {
        get
        {
            string? value = Marshal.PtrToStringUni(D3D12MA.AllocationGetName(NativePointer));
            GC.KeepAlive(this);
            return value;
        }
        set
        {
            fixed (char* name = value)
            {
                D3D12MA.AllocationSetName(NativePointer, name);
            }
            GC.KeepAlive(this);
        }
    }
}
