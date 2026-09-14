# Vortice.D3D12MemoryAllocator

Bindings for [D3D12MemoryAllocator](https://github.com/GPUOpen-LibrariesAndSDKs/D3D12MemoryAllocator), using Vortice.Direct3D12 and Vortice.DXGI objects.

The package targets .NET 8, 9, and 10. Its dependency on Vortice.D3D12MA.Native supplies the Windows x64 and ARM64 native library, which the runtime loads from the package's `runtimes` folder; publishing with a runtime identifier such as `win-x64` only changes the output layout. Applications with a custom native library location can subscribe to `D3D12MA.ResolveLibrary`.

The bindings cover the native allocator, allocations, custom pools, virtual blocks, statistics, budgets, and defragmentation, including all resource creation and aliasing versions. Resource creation versions retain the native Windows/device feature requirements.

## Resource allocation

`CreateResource` returns an allocation and a separately owned resource reference. Both must be disposed:

```csharp
using Vortice.D3D12MemoryAllocator;
using Vortice.Direct3D12;

// device and adapter are existing Vortice objects.
using Allocator allocator = D3D12MA.CreateAllocator(new AllocatorDescription(device, adapter));
using Allocation allocation = allocator.CreateResource(
    new AllocationDescription(HeapType.Upload),
    ResourceDescription.Buffer(65536),
    ResourceStates.GenericRead,
    out ID3D12Resource resource);
using (resource)
{
    resource.SetData<uint>([1, 2, 3, 4]);
}
```

Overloads returning `Result` expose failures without throwing. Generic overloads request a specific `ID3D12Resource` interface. `CreateResource2` accepts `ResourceDescription1`; `CreateResource3` also accepts a barrier layout and a span of castable formats. Aliasing versions follow the same native progression.

Dispose resources before their allocations, a pool's allocations and defragmentation contexts before that pool, and all allocations, pools, and defragmentation contexts before their allocator. The caller must finish GPU work before releasing or reusing the underlying memory.

`Allocation.Resource` and `Allocation.Heap` acquire independent references on each read; dispose each returned wrapper. Assigning `Allocation.Resource` lets the native allocation retain its own resource reference. `Pool.Description.ProtectedSession`, when present, also returns an owned reference that must be disposed.

## Virtual allocation

Virtual blocks allocate offsets without requiring a D3D12 device:

```csharp
using VirtualBlock block = D3D12MA.CreateVirtualBlock(new VirtualBlockDescription(4096));
VirtualAllocation allocation = block.Allocate(new VirtualAllocationDescription(256, 64), out ulong offset);
// Use the offset in an application-managed buffer or other address space.
block.FreeAllocation(allocation);
```

`VirtualAllocation` is a value handle, not a disposable object. Free allocations with their original block or call `Clear` before disposing the block. Handles become invalid after freeing, clearing, or destroying the block.

## Defragmentation

`Allocator.BeginDefragmentation` processes default pools; `Pool.BeginDefragmentation` processes a custom pool. The allocator's native entry point returns no HRESULT, while the pool entry point returns `Result`.

`BeginPass` returns `S_FALSE` (code 1) when `pass.Moves` contains work, or `S_OK` when no moves remain. Only each move's `Operation` may be changed. A pass and every copy of its move span expire at `EndPass`; dispose references acquired through `GetSourceAllocation` and `GetDestinationTemporaryAllocation` before ending the pass.

For a Copy move, create a placed resource at the temporary destination (for example through `CreateAliasingResource`), copy the data, wait for GPU completion, and assign the new resource to the temporary allocation's `Resource`. Dispose acquired resource wrappers before `EndPass`. The original source allocation remains valid and refers to the moved resource afterward; reacquire resources and update application descriptors and addresses as needed.

Ignore leaves the source allocation unchanged. Destroy makes `EndPass` release the application's original source allocation reference. To transfer that reference, set the original owning wrapper's `NativePointer = 0` without calling `Dispose` or `Release` on the transferred pointer. Separately acquired references still need normal disposal before `EndPass`. Do not use a destroyed allocation afterward.

## Callbacks and statistics

`AllocationCallbacks` uses unmanaged Cdecl function pointers. The native library copies the callback structure, but the functions and any `PrivateData` they reference must remain valid until native destruction completes. Callbacks must not throw across the native boundary and must honor the requested size and alignment.

`BuildStatsString` returns a managed copy of the native JSON text and frees the native string. Names are copied by native setters. Private data pointers are passed through without managed ownership.

The native library escapes some non-ASCII characters in names incorrectly in that JSON output (for example, λ). Use ASCII names when a statistics dump must be parsed as JSON; Unicode name setters and getters still work.
