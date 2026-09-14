// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;
using Vortice.Direct3D12;
using Vortice.DXGI;
using static Vortice.DXGI.DXGI;

namespace Vortice.D3D12MemoryAllocator.Tests;

internal sealed class D3D12TestContext : IDisposable
{
    public readonly IDXGIFactory4 Factory;
    public readonly IDXGIAdapter Adapter;
    public readonly ID3D12Device Device;
    public readonly Allocator Allocator;

    public D3D12TestContext(ulong preferredBlockSize = 0)
    {
        Factory = CreateDXGIFactory2<IDXGIFactory4>(false);
        Adapter = Factory.EnumWarpAdapter<IDXGIAdapter>();
        Device = D3D12.D3D12CreateDevice<ID3D12Device>(Adapter, FeatureLevel.Level_11_0);
        Allocator = D3D12MA.CreateAllocator(new AllocatorDescription(Device, Adapter) { PreferredBlockSize = preferredBlockSize });
    }

    public void Dispose()
    {
        Allocator.Dispose();
        Device.Dispose();
        Adapter.Dispose();
        Factory.Dispose();
    }
}
