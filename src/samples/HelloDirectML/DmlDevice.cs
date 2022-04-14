// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vortice.Direct3D12;
using Vortice.Direct3D12.Debug;
using Vortice.DXGI;
using Vortice.DirectML;

namespace HelloDirectML;
public class DmlDevice : IDisposable
{
    public IDXGIFactory4 DXGIFactory;
    public ID3D12Device2 D3D12Device;
    public ID3D12CommandQueue D3D12CommandQueue; 
    public ID3D12CommandAllocator D3D12CommandAllocator;
    public ID3D12GraphicsCommandList4 D3D12CommandList;

    public IDMLDevice DMLDevice;

    public bool IsSupported() => D3D12.IsSupported(Vortice.Direct3D.FeatureLevel.Level_12_0);

    public void InitializeDirect3D12()
    {
        if (!IsSupported())
        {
            throw new InvalidOperationException("Direct3D12 is not supported on current OS");
        }

        var validation = false;

        if (D3D12.D3D12GetDebugInterface(out ID3D12Debug? debug).Success)
        {
            debug!.EnableDebugLayer();
            debug!.Dispose();
            validation = true;
        }

        DXGIFactory = DXGI.CreateDXGIFactory2<IDXGIFactory4>(validation);

        for (int adapterIndex = 0; DXGIFactory.EnumAdapters1(adapterIndex, out IDXGIAdapter1 adapter).Success; adapterIndex++)
        {
            AdapterDescription1 desc = adapter.Description1;

            // Don't select the Basic Render Driver adapter.
            if ((desc.Flags & AdapterFlags.Software) != AdapterFlags.None)
            {
                adapter.Dispose();

                continue;
            }

            if (D3D12.D3D12CreateDevice(adapter, Vortice.Direct3D.FeatureLevel.Level_11_0, out D3D12Device).Success)
            {
                adapter.Dispose();

                break;
            }
        }

        if (D3D12Device == null)
        {
            throw new InvalidOperationException("Direct3D12 device could not be created");
        }

        var commandQueueDesc = new CommandQueueDescription
        {
            Type = CommandListType.Direct,
            Flags = CommandQueueFlags.None,
        };

        var D3D12CommandQueue = D3D12Device.CreateCommandQueue(commandQueueDesc);
        var D3D12CommandAllocator = D3D12Device.CreateCommandAllocator(CommandListType.Direct);
        var D3D12CommandList = D3D12Device.CreateCommandList<ID3D12GraphicsCommandList4>(CommandListType.Direct, D3D12CommandAllocator);
    }

    public void Run()
    {
        InitializeDirect3D12();

        var createFlags = CreateDeviceFlags.None;

#if DEBUG
        createFlags |= CreateDeviceFlags.Debug;
#endif

        DML.DMLCreateDevice(D3D12Device, createFlags, out DMLDevice);

        var tensorSizes = new int[] { 1, 2, 3, 4 };
        var tensorElementCount = tensorSizes.Aggregate((a, b) => a * b);

        var bufferTensorDesc = new BufferTensorDescription
        {
            DataType = TensorDataType.Float32,
            Flags = TensorFlags.None,
            DimensionCount = tensorSizes.Length,
            Sizes = tensorSizes,
            Strides = null,
        };

        bufferTensorDesc.TotalTensorSizeInBytes = CalculateBufferTensorSize(
            bufferTensorDesc.DataType,
            bufferTensorDesc.DimensionCount,
            bufferTensorDesc.Sizes,
            bufferTensorDesc.Strides
            );

    }

    public static long CalculateBufferTensorSize(TensorDataType dataType, int dimensionCount, int[] sizes, int[]? strides)
    {
        return 0;
    }

    public void Dispose()
    {

    }
}
