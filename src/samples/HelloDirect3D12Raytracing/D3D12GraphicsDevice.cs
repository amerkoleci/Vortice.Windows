// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using System.Runtime.CompilerServices;
using Vortice.Direct3D12;
using Vortice.Direct3D12.Debug;
using Vortice.DXGI;
using Vortice.Direct3D;
using SharpGen.Runtime;
using static Vortice.Direct3D12.D3D12;
using static Vortice.DXGI.DXGI;
using Vortice.Mathematics;
using Vortice.Dxc;
using System.Runtime.InteropServices;
using Vortice;
using ShaderResourceViewDimension = Vortice.Direct3D12.ShaderResourceViewDimension;
using Vortice.DXGI.Debug;

namespace HelloDirect3D12Raytracing;

public sealed class D3D12GraphicsDevice : IGraphicsDevice
{
    private static readonly FeatureLevel[] s_featureLevels = new[]
    {
        FeatureLevel.Level_12_2,
        FeatureLevel.Level_12_1,
        FeatureLevel.Level_12_0,
        FeatureLevel.Level_11_1,
        FeatureLevel.Level_11_0,
    };

    private const int RenderLatency = 2;

    public readonly Window Window;
    public readonly IDXGIFactory4 DXGIFactory;
    private readonly ID3D12Device5? _d3d12Device;
    private readonly ID3D12InfoQueue? _infoQueue;
    private readonly ID3D12Resource[] _renderTargets;
    private readonly ID3D12DescriptorHeap _rtvHeap;
    private readonly int _rtvDescriptorSize;
    private readonly ID3D12CommandAllocator[] _commandAllocators;

    private readonly ID3D12Resource _outputBuffer;

    private readonly ID3D12GraphicsCommandList4 _commandList;

    private readonly ID3D12DescriptorHeap _shaderViewHeap;
    private readonly ID3D12RootSignature _globalRootSignature;
    private readonly ID3D12RootSignature _rayGenRootSignature;
    private readonly ID3D12RootSignature _hitRootSignature;
    private readonly ID3D12RootSignature _missRootSignature;
    private readonly ID3D12StateObject _raytracingStateObject;

    private readonly ID3D12Resource _vertexBuffer;
    private readonly ID3D12Resource _instanceBuffer;

    private readonly ID3D12Resource _bottomLevelAccelerationStructure;
    private readonly ID3D12Resource _topLevelAccelerationStructure;
    private readonly ID3D12StateObjectProperties _raytracingStateObjectProperties;

    private readonly int _shaderBindingTableEntrySize;
    private readonly ID3D12Resource _shaderBindingTableBuffer;

    private readonly ID3D12Fence _frameFence;
    private readonly AutoResetEvent _frameFenceEvent;
    private ulong _frameCount;
    private ulong _frameIndex;
    private int _backbufferIndex;

    public ID3D12Device2 D3D12Device => _d3d12Device!;
    public ID3D12CommandQueue GraphicsQueue { get; }

    public IDXGISwapChain3 SwapChain { get; }

    public static bool IsSupported() => D3D12.IsSupported(FeatureLevel.Level_11_0);

    public unsafe D3D12GraphicsDevice(bool validation, Window window)
    {
        if (!IsSupported())
        {
            throw new InvalidOperationException("Direct3D12 is not supported on current OS");
        }

        Window = window;

        {
            if (validation && D3D12GetDebugInterface(out ID3D12Debug? debug).Success)
            {
                debug!.EnableDebugLayer();
                debug!.Dispose();
            }
            else
            {
                validation = false;
            }

            DXGIFactory = CreateDXGIFactory2<IDXGIFactory4>(validation);

            using (IDXGIFactory6? factory6 = DXGIFactory.QueryInterfaceOrNull<IDXGIFactory6>())
            {
                if (factory6 != null)
                {
                    for (int adapterIndex = 0;
                        _d3d12Device == null && factory6.EnumAdapterByGpuPreference(adapterIndex, GpuPreference.HighPerformance,
                        out IDXGIAdapter1? adapter).Success;
                        adapterIndex++)
                    {
                        AdapterDescription1 desc = adapter!.Description1;

                        // Don't select the Basic Render Driver adapter.
                        if ((desc.Flags & AdapterFlags.Software) != AdapterFlags.None)
                        {
                            adapter.Dispose();

                            continue;
                        }

                        for (int i = 0; i < s_featureLevels.Length; i++)
                        {
                            if (D3D12CreateDevice(adapter, s_featureLevels[i], out _d3d12Device).Success)
                            {
                                adapter.Dispose();

                                Console.WriteLine($"Create Direct3D12 device {s_featureLevels[i]} with adapter ({adapterIndex}): VID:{desc.VendorId:X}, PID:{desc.DeviceId:X} - {desc.Description}");
                                break;
                            }
                        }
                    }
                }
            }

            if (_d3d12Device == null)
            {
                for (int adapterIndex = 0;
                    _d3d12Device == null && DXGIFactory.EnumAdapters1(adapterIndex, out IDXGIAdapter1 adapter).Success;
                    adapterIndex++)
                {
                    AdapterDescription1 desc = adapter.Description1;

                    // Don't select the Basic Render Driver adapter.
                    if ((desc.Flags & AdapterFlags.Software) != AdapterFlags.None)
                    {
                        adapter.Dispose();

                        continue;
                    }

                    for (int i = 0; i < s_featureLevels.Length; i++)
                    {
                        if (D3D12CreateDevice(adapter, s_featureLevels[i], out _d3d12Device).Success)
                        {
                            adapter.Dispose();

                            Console.WriteLine($"Create Direct3D12 device {s_featureLevels[i]} with adapter ({adapterIndex}): VID:{desc.VendorId:X}, PID:{desc.DeviceId:X} - {desc.Description}");
                            break;
                        }
                    }
                }
            }

            if (_d3d12Device == null)
            {
                throw new NotSupportedException("Cannot create Direct3D12 device on current OS");
            }

            // Check raytracing support
            if (_d3d12Device.Options5.RaytracingTier < RaytracingTier.Tier1_0)
            {
                throw new NotSupportedException("Raytracing not supported on current OS");
            }

            if (validation)
            {
                _infoQueue = _d3d12Device.QueryInterfaceOrNull<ID3D12InfoQueue>();
            }

            // Create synchronization objects.
            _frameFence = _d3d12Device.CreateFence(0);
            _frameFenceEvent = new AutoResetEvent(false);
        }

        {
            // Create Command queue.
            GraphicsQueue = _d3d12Device.CreateCommandQueue(CommandListType.Direct);

            SwapChainDescription1 swapChainDesc = new()
            {
                BufferCount = RenderLatency,
                Width = window.ClientSize.Width,
                Height = window.ClientSize.Height,
                Format = Format.R8G8B8A8_UNorm,
                BufferUsage = Usage.RenderTargetOutput,
                SwapEffect = SwapEffect.FlipDiscard,
                SampleDescription = new SampleDescription(1, 0)
            };

            using (IDXGISwapChain1 swapChain = DXGIFactory.CreateSwapChainForHwnd(GraphicsQueue, window.Handle, swapChainDesc))
            {
                DXGIFactory.MakeWindowAssociation(window.Handle, WindowAssociationFlags.IgnoreAltEnter);

                SwapChain = swapChain.QueryInterface<IDXGISwapChain3>();
                _backbufferIndex = SwapChain.CurrentBackBufferIndex;
            }
        }

        // Create frame resources
        {
            _rtvHeap = _d3d12Device.CreateDescriptorHeap<ID3D12DescriptorHeap>(new DescriptorHeapDescription(DescriptorHeapType.RenderTargetView, RenderLatency));
            _rtvDescriptorSize = _d3d12Device.GetDescriptorHandleIncrementSize(DescriptorHeapType.RenderTargetView);

            CpuDescriptorHandle rtvHandle = _rtvHeap.GetCPUDescriptorHandleForHeapStart();

            // Create a RTV for each frame.
            _renderTargets = new ID3D12Resource[RenderLatency];
            for (int i = 0; i < RenderLatency; i++)
            {
                _renderTargets[i] = SwapChain.GetBuffer<ID3D12Resource>(i);
                _d3d12Device.CreateRenderTargetView(_renderTargets[i], null, rtvHandle);
                rtvHandle += _rtvDescriptorSize;
            }
        }

        {
            _commandAllocators = new ID3D12CommandAllocator[RenderLatency];
            for (int i = 0; i < RenderLatency; i++)
            {
                _commandAllocators[i] = _d3d12Device.CreateCommandAllocator(CommandListType.Direct);
            }

            _commandList = _d3d12Device.CreateCommandList<ID3D12GraphicsCommandList4>(0, CommandListType.Direct, _commandAllocators[0]);
            _commandList.Close();
        }

        // Create a root signatures
        {
            var globalRootSignatureDescription = new RootSignatureDescription1(RootSignatureFlags.None);

            _globalRootSignature = _d3d12Device.CreateRootSignature<ID3D12RootSignature>(0, globalRootSignatureDescription);


            var rayGenSignatureDescription = new RootSignatureDescription1(RootSignatureFlags.LocalRootSignature);

            rayGenSignatureDescription.Parameters = new[]{
                    new RootParameter1(
                        new RootDescriptorTable1(
                            new DescriptorRange1(DescriptorRangeType.UnorderedAccessView, 1, 0, 0, 0),
                            new DescriptorRange1(DescriptorRangeType.ShaderResourceView, 1, 0, 0, 1)
                            ), ShaderVisibility.All),
                };

            _rayGenRootSignature = _d3d12Device.CreateRootSignature<ID3D12RootSignature>(0, rayGenSignatureDescription);

            var hitRootSignatureDescription = new RootSignatureDescription1(RootSignatureFlags.LocalRootSignature);

            hitRootSignatureDescription.Parameters = new[]
            {
                    new RootParameter1(RootParameterType.ShaderResourceView, new RootDescriptor1(0, 0), ShaderVisibility.All),
                };

            _hitRootSignature = _d3d12Device.CreateRootSignature<ID3D12RootSignature>(0, hitRootSignatureDescription);

            var missRootSignatureDescription = new RootSignatureDescription1(RootSignatureFlags.LocalRootSignature);

            _missRootSignature = _d3d12Device.CreateRootSignature<ID3D12RootSignature>(0, missRootSignatureDescription);


            // Create the shaders
            var rayGenShaderBlob = CompileLibraryShader(@"Shaders\Raygen.hlsl");
            var hitShaderBlob = CompileLibraryShader(@"Shaders\Hit.hlsl");
            var missShaderBlob = CompileLibraryShader(@"Shaders\Miss.hlsl");

            // Create the pipeline
            var rayGenLibrary = new StateSubObject(new DxilLibraryDescription(new ShaderBytecode(rayGenShaderBlob), new ExportDescription("RayGen")));
            var hitLibrary = new StateSubObject(new DxilLibraryDescription(new ShaderBytecode(hitShaderBlob), new ExportDescription("ClosestHit")));
            var missLibrary = new StateSubObject(new DxilLibraryDescription(new ShaderBytecode(missShaderBlob), new ExportDescription("Miss")));

            var hitGroup = new StateSubObject(new HitGroupDescription("HitGroup", HitGroupType.Triangles, closestHitShaderImport: "ClosestHit"));

            var raytracingShaderConfig = new StateSubObject(new RaytracingShaderConfig(4 * sizeof(float), 2 * sizeof(float)));

            var shaderPayloadAssociation = new StateSubObject(new SubObjectToExportsAssociation(raytracingShaderConfig, "RayGen", "ClosestHit", "Miss"));

            var rayGenRootSignatureStateObject = new StateSubObject(new LocalRootSignature(_rayGenRootSignature));
            var rayGenRootSignatureAssociation = new StateSubObject(new SubObjectToExportsAssociation(rayGenRootSignatureStateObject, "RayGen"));

            var hitRootSignatureStateObject = new StateSubObject(new LocalRootSignature(_hitRootSignature));
            var hitRootSignatureAssociation = new StateSubObject(new SubObjectToExportsAssociation(hitRootSignatureStateObject, "ClosestHit"));

            var missRootSignatureStateObject = new StateSubObject(new LocalRootSignature(_missRootSignature));
            var missRootSignatureAssociation = new StateSubObject(new SubObjectToExportsAssociation(missRootSignatureStateObject, "Miss"));

            var raytracingPipelineConfig = new StateSubObject(new RaytracingPipelineConfig(1));

            var globalRootSignatureStateObject = new StateSubObject(new GlobalRootSignature(_globalRootSignature));

            var stateSubObjects = new StateSubObject[] {
                    rayGenLibrary,
                    hitLibrary,
                    missLibrary,

                    hitGroup,

                    raytracingShaderConfig,
                    shaderPayloadAssociation,

                    rayGenRootSignatureStateObject,
                    rayGenRootSignatureAssociation,
                    hitRootSignatureStateObject,
                    hitRootSignatureAssociation,
                    missRootSignatureStateObject,
                    missRootSignatureAssociation,

                    raytracingPipelineConfig,

                    globalRootSignatureStateObject,
                };

            _raytracingStateObject = _d3d12Device!.CreateStateObject<ID3D12StateObject>(new StateObjectDescription(StateObjectType.RaytracingPipeline, stateSubObjects));
        }

        // Create the vertex buffer
        int vertexStride = Marshal.SizeOf<Vertex>();

        {
            var triangleVertices = new Vertex[]
            {
                    new Vertex(new Vector3(0f, 0.5f, 0.0f), new Color4(1.0f, 0.5f, 0.0f, 1.0f)),
                    new Vertex(new Vector3(0.5f, -0.5f, 0.0f), new Color4(0.0f, 1.0f, 0.5f, 1.0f)),
                    new Vertex(new Vector3(-0.5f, -0.5f, 0.0f), new Color4(0.5f, 0.0f, 1.0f, 1.0f)),
            };

            int vertexBufferSize = 3 * vertexStride;

            _vertexBuffer = _d3d12Device.CreateCommittedResource(new HeapProperties(HeapType.Upload),
                    HeapFlags.None,
                                                                          ResourceDescription.Buffer((ulong)vertexBufferSize),
                                                                          ResourceStates.GenericRead);

            Span<Vertex> bufferData = _vertexBuffer.Map<Vertex>(0, 3);
            ReadOnlySpan<Vertex> src = new(triangleVertices);
            src.CopyTo(bufferData);
            _vertexBuffer.Unmap(0);
        }

        // Build acceleration structures needed for raytracing
        {
            _commandList.Reset(_commandAllocators[_frameIndex]);

            var geometryDescription = new RaytracingGeometryDescription
            {
                Triangles = new RaytracingGeometryTrianglesDescription(new GpuVirtualAddressAndStride(_vertexBuffer.GPUVirtualAddress, (ulong)vertexStride), Format.R32G32B32_Float, 3),
                Flags = RaytracingGeometryFlags.Opaque,
            };


            var bottomLevelInputs = new BuildRaytracingAccelerationStructureInputs
            {
                Type = RaytracingAccelerationStructureType.BottomLevel,
                Flags = RaytracingAccelerationStructureBuildFlags.None,
                Layout = ElementsLayout.Array,
                DescriptorsCount = 1,
                GeometryDescriptions = new[] { geometryDescription },
            };

            var bottomLevelInfo = _d3d12Device.GetRaytracingAccelerationStructurePrebuildInfo(bottomLevelInputs);

            if (bottomLevelInfo.ResultDataMaxSizeInBytes == 0)
                throw new Exception("Failed to create bottom level inputs.");


            var topLevelInputs = new BuildRaytracingAccelerationStructureInputs
            {
                Type = RaytracingAccelerationStructureType.TopLevel,
                Flags = RaytracingAccelerationStructureBuildFlags.None,
                Layout = ElementsLayout.Array,
                DescriptorsCount = 1,
            };

            var topLevelInfo = _d3d12Device.GetRaytracingAccelerationStructurePrebuildInfo(topLevelInputs);

            if (topLevelInfo.ResultDataMaxSizeInBytes == 0)
                throw new Exception("Failed to create top level inputs.");


            var scratchResource = _d3d12Device.CreateCommittedResource<ID3D12Resource>(new HeapProperties(HeapType.Default),
                HeapFlags.None,
                ResourceDescription.Buffer(Math.Max(topLevelInfo.ScratchDataSizeInBytes, bottomLevelInfo.ScratchDataSizeInBytes), ResourceFlags.AllowUnorderedAccess),
                ResourceStates.UnorderedAccess);

            _bottomLevelAccelerationStructure = _d3d12Device.CreateCommittedResource<ID3D12Resource>(new HeapProperties(HeapType.Default),
                HeapFlags.None,
                ResourceDescription.Buffer(Math.Max(topLevelInfo.ScratchDataSizeInBytes, bottomLevelInfo.ResultDataMaxSizeInBytes), ResourceFlags.AllowUnorderedAccess),
                ResourceStates.RaytracingAccelerationStructure);

            _topLevelAccelerationStructure = _d3d12Device.CreateCommittedResource<ID3D12Resource>(new HeapProperties(HeapType.Default),
                HeapFlags.None,
                ResourceDescription.Buffer(Math.Max(topLevelInfo.ScratchDataSizeInBytes, bottomLevelInfo.ResultDataMaxSizeInBytes), ResourceFlags.AllowUnorderedAccess),
                ResourceStates.RaytracingAccelerationStructure);


            // Create the instance buffer
            var instanceDescription = new RaytracingInstanceDescription
            {
                Transform = new Matrix3x4(1, 0, 0, 0,
                                          0, 1, 0, 0,
                                          0, 0, 1, 0),
                InstanceMask = 0xFF,
                InstanceID = (UInt24)0,
                Flags = RaytracingInstanceFlags.None,
                InstanceContributionToHitGroupIndex = (UInt24)0,
                AccelerationStructure = (long)_bottomLevelAccelerationStructure.GPUVirtualAddress,
            };

            _instanceBuffer = _d3d12Device.CreateCommittedResource<ID3D12Resource>(
                new HeapProperties(HeapType.Upload),
                HeapFlags.None,
                ResourceDescription.Buffer((ulong)sizeof(RaytracingInstanceDescription)),
                ResourceStates.GenericRead);

            RaytracingInstanceDescription* instanceBufferDataPointer = _instanceBuffer.Map<RaytracingInstanceDescription>(0);

            unsafe
            {
                *instanceBufferDataPointer = instanceDescription;
            }

            _instanceBuffer.Unmap(0);


            // Build the acceleration structures
            _commandList.BuildRaytracingAccelerationStructure(new BuildRaytracingAccelerationStructureDescription
            {
                Inputs = bottomLevelInputs,
                ScratchAccelerationStructureData = scratchResource.GPUVirtualAddress,
                DestinationAccelerationStructureData = _bottomLevelAccelerationStructure.GPUVirtualAddress,
            });

            _commandList.ResourceBarrier(new ResourceBarrier(new ResourceUnorderedAccessViewBarrier(_bottomLevelAccelerationStructure)));

            topLevelInputs.InstanceDescriptions = (long)_instanceBuffer.GPUVirtualAddress;

            _commandList.BuildRaytracingAccelerationStructure(new BuildRaytracingAccelerationStructureDescription
            {
                Inputs = topLevelInputs,
                ScratchAccelerationStructureData = scratchResource.GPUVirtualAddress,
                DestinationAccelerationStructureData = _topLevelAccelerationStructure.GPUVirtualAddress,
            });

            _commandList.ResourceBarrier(new ResourceBarrier(new ResourceUnorderedAccessViewBarrier(_topLevelAccelerationStructure)));

            _commandList.Close();

            GraphicsQueue.ExecuteCommandList(_commandList);

            WaitIdle();

            scratchResource.Dispose();
        }

        // Create the output buffer
        {
            var outputBufferDescription = new ResourceDescription
            {
                DepthOrArraySize = 1,
                Dimension = ResourceDimension.Texture2D,
                Format = Format.R8G8B8A8_UNorm,
                Flags = ResourceFlags.AllowUnorderedAccess,
                Width = (ulong)window.ClientSize.Width,
                Height = window.ClientSize.Height,
                Layout = TextureLayout.Unknown,
                MipLevels = 1,
                SampleDescription = new SampleDescription(1, 0),
            };

            _outputBuffer = _d3d12Device.CreateCommittedResource<ID3D12Resource>(
                new HeapProperties(HeapType.Default),
                HeapFlags.None,
                outputBufferDescription,
                ResourceStates.CopySource);
        }

        // Create the shader resource heap
        {
            const int ShaderResourceDescriptorCount = 2;

            _shaderViewHeap = _d3d12Device.CreateDescriptorHeap<ID3D12DescriptorHeap>(new DescriptorHeapDescription(DescriptorHeapType.ConstantBufferViewShaderResourceViewUnorderedAccessView, ShaderResourceDescriptorCount, DescriptorHeapFlags.ShaderVisible));

            var shaderViewDescriptorSize = _d3d12Device.GetDescriptorHandleIncrementSize(DescriptorHeapType.ConstantBufferViewShaderResourceViewUnorderedAccessView);

            var shaderViewHandle = _shaderViewHeap.GetCPUDescriptorHandleForHeapStart();


            var outputBufferViewDescription = new UnorderedAccessViewDescription { ViewDimension = UnorderedAccessViewDimension.Texture2D };

            _d3d12Device.CreateUnorderedAccessView(_outputBuffer, null, outputBufferViewDescription, shaderViewHandle);


            var accelerationStructureViewDescription = new ShaderResourceViewDescription
            {
                Format = Format.Unknown,
                ViewDimension = ShaderResourceViewDimension.RaytracingAccelerationStructure,
                Shader4ComponentMapping = ShaderComponentMapping.Default,
                RaytracingAccelerationStructure = new RaytracingAccelerationStructureShaderResourceView { Location = _topLevelAccelerationStructure.GPUVirtualAddress },
            };

            _d3d12Device.CreateShaderResourceView(null, accelerationStructureViewDescription, shaderViewHandle + shaderViewDescriptorSize);
        }

        // Create the shader binding table
        {
            _raytracingStateObjectProperties = _raytracingStateObject.QueryInterface<ID3D12StateObjectProperties>();

            _shaderBindingTableEntrySize = D3D12.ShaderIdentifierSizeInBytes;
            _shaderBindingTableEntrySize += 8; // Ray generator descriptor table
            _shaderBindingTableEntrySize = Align(_shaderBindingTableEntrySize, D3D12.RaytracingShaderRecordByteAlignment);

            var shaderBindingTableSize = _shaderBindingTableEntrySize * 3;

            _shaderBindingTableBuffer = _d3d12Device.CreateCommittedResource<ID3D12Resource>(new HeapProperties(HeapType.Upload),
                                                                               HeapFlags.None,
                                                                               ResourceDescription.Buffer((ulong)shaderBindingTableSize),
                                                                               ResourceStates.GenericRead);

            byte* shaderBindingTableBufferDataPointer;
            _shaderBindingTableBuffer.Map(0, &shaderBindingTableBufferDataPointer).CheckError();


            Unsafe.CopyBlockUnaligned((void*)shaderBindingTableBufferDataPointer, (void*)_raytracingStateObjectProperties.GetShaderIdentifier("RayGen"), D3D12.ShaderIdentifierSizeInBytes);

            unsafe
            {
                *(GpuDescriptorHandle*)(shaderBindingTableBufferDataPointer + _shaderBindingTableEntrySize * 0 + D3D12.ShaderIdentifierSizeInBytes) = _shaderViewHeap.GetGPUDescriptorHandleForHeapStart();
            }

            Unsafe.CopyBlockUnaligned(shaderBindingTableBufferDataPointer + _shaderBindingTableEntrySize * 1, (void*)_raytracingStateObjectProperties.GetShaderIdentifier("HitGroup"), D3D12.ShaderIdentifierSizeInBytes);

            unsafe
            {
                *(ulong*)(shaderBindingTableBufferDataPointer + _shaderBindingTableEntrySize * 1 + D3D12.ShaderIdentifierSizeInBytes) = _vertexBuffer.GPUVirtualAddress;
            }

            Unsafe.CopyBlockUnaligned(shaderBindingTableBufferDataPointer + _shaderBindingTableEntrySize * 2, (void*)_raytracingStateObjectProperties.GetShaderIdentifier("Miss"), D3D12.ShaderIdentifierSizeInBytes);

            _shaderBindingTableBuffer.Unmap(0);
        }

        WaitIdle();
    }

    public void Dispose()
    {
        WaitIdle();

        for (int i = 0; i < RenderLatency; i++)
        {
            _commandAllocators[i].Dispose();
            _renderTargets[i].Dispose();
        }
        _commandList.Dispose();

        _rtvHeap.Dispose();
        SwapChain.Dispose();
        _frameFence.Dispose();
        GraphicsQueue.Dispose();
        _d3d12Device!.Dispose();
        _infoQueue?.Dispose();
        DXGIFactory.Dispose();

        _shaderViewHeap.Dispose();
        _globalRootSignature.Dispose();
        _rayGenRootSignature.Dispose();
        _hitRootSignature.Dispose();
        _missRootSignature.Dispose();
        _raytracingStateObject.Dispose();
        _shaderBindingTableBuffer.Dispose();

        _bottomLevelAccelerationStructure.Dispose();
        _topLevelAccelerationStructure.Dispose();
        _raytracingStateObjectProperties.Dispose();

        _instanceBuffer.Dispose();
        _vertexBuffer.Dispose();

        if (DXGIGetDebugInterface1(out IDXGIDebug1? dxgiDebug).Success)
        {
            dxgiDebug!.ReportLiveObjects(DebugAll, ReportLiveObjectFlags.Summary | ReportLiveObjectFlags.IgnoreInternal);
            dxgiDebug!.Dispose();
        }
    }

    public void WaitIdle()
    {
        GraphicsQueue.Signal(_frameFence, ++_frameCount);
        _frameFence.SetEventOnCompletion(_frameCount, _frameFenceEvent);
        _frameFenceEvent.WaitOne();
    }

    public bool DrawFrame(Action<int, int> draw, [CallerMemberName] string? frameName = null)
    {
        WriteDebugMessages(_infoQueue);

        _commandAllocators[_frameIndex].Reset();
        _commandList.Reset(_commandAllocators[_frameIndex]);
        _commandList.BeginEvent("Frame");

        // Call callback.
        draw(Window.ClientSize.Width, Window.ClientSize.Height);

        // Raytracing
        _commandList.ResourceBarrierTransition(_outputBuffer, ResourceStates.CopySource, ResourceStates.UnorderedAccess);

        _commandList.SetDescriptorHeaps(1, new[] { _shaderViewHeap });

        _commandList.SetComputeRootSignature(_globalRootSignature);

        _commandList.SetPipelineState1(_raytracingStateObject);

        _commandList.DispatchRays(new DispatchRaysDescription
        {
            Width = Window.ClientSize.Width,
            Height = Window.ClientSize.Height,
            Depth = 1,

            RayGenerationShaderRecord = new GpuVirtualAddressRange
            {
                StartAddress = _shaderBindingTableBuffer.GPUVirtualAddress + (ulong)_shaderBindingTableEntrySize * 0,
                SizeInBytes = (ulong)_shaderBindingTableEntrySize,
            },

            HitGroupTable = new GpuVirtualAddressRangeAndStride
            {
                StartAddress = _shaderBindingTableBuffer.GPUVirtualAddress + (ulong)_shaderBindingTableEntrySize * 1,
                SizeInBytes = (ulong)_shaderBindingTableEntrySize,
                StrideInBytes = (ulong)_shaderBindingTableEntrySize,
            },

            MissShaderTable = new GpuVirtualAddressRangeAndStride
            {
                StartAddress = _shaderBindingTableBuffer.GPUVirtualAddress + (ulong)_shaderBindingTableEntrySize * 2,
                SizeInBytes = (ulong)_shaderBindingTableEntrySize,
                StrideInBytes = (ulong)_shaderBindingTableEntrySize,
            },
        });

        // Copy the output buffer to the back buffer
        _commandList.ResourceBarrierTransition(_outputBuffer, ResourceStates.UnorderedAccess, ResourceStates.CopySource);
        _commandList.ResourceBarrierTransition(_renderTargets[_backbufferIndex], ResourceStates.Present, ResourceStates.CopyDestination);
        _commandList.CopyResource(_renderTargets[_backbufferIndex], _outputBuffer);

        // Indicate that the back buffer will now be used to present.
        _commandList.ResourceBarrierTransition(_renderTargets[_backbufferIndex], ResourceStates.CopyDestination, ResourceStates.Present);
        _commandList.EndEvent();
        _commandList.Close();

        // Execute the command list.
        GraphicsQueue.ExecuteCommandList(_commandList);

        Result result = SwapChain.Present(1, PresentFlags.None);
        if (result.Failure
            && result.Code == Vortice.DXGI.ResultCode.DeviceRemoved.Code)
        {
            return false;
        }

        WaitIdle();

        _frameIndex = _frameCount % RenderLatency;
        _backbufferIndex = SwapChain.CurrentBackBufferIndex;

        return true;
    }

    private static void WriteDebugMessages(ID3D12InfoQueue? infoQueue)
    {
        if (infoQueue == null)
            return;

        for (ulong i = 0; i < infoQueue.NumStoredMessages; i++)
        {
            var message = infoQueue.GetMessage(i);

            Console.WriteLine($"[{message.Severity}] {message.Category} {message.Id}: {message.Description}");
            Console.WriteLine("");
        }

        infoQueue.ClearStoredMessages();
    }

    private static byte[] CompileLibraryShader(string filePath)
    {
        string? source = File.ReadAllText(filePath);

        IDxcResult? result = DxcCompiler.Compile(DxcShaderStage.Library, source, "", new DxcCompilerOptions { ShaderModel = DxcShaderModel.Model6_3, EnableDebugInfo = true }, filePath);

        if (result == null || result.GetStatus().Failure)
        {
            throw new Exception(result!.GetErrors());
        }

        byte[] data = result.GetObjectBytecodeArray();

        result.Dispose();
        return data;
    }

    private readonly struct Vertex
    {
        public readonly Vector3 Position;
        public readonly Color4 Color;

        public Vertex(in Vector3 position, in Color4 color)
        {
            Position = position;
            Color = color;
        }
    }

    private static int Align(int value, int alignment)
    {
        return ((value + alignment - 1) / alignment) * alignment;
    }
}
