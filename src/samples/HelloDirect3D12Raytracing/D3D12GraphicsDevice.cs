// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using Vortice.Direct3D12;
using Vortice.Direct3D12.Debug;
using Vortice.DXGI;
using Vortice.Direct3D;
using SharpGen.Runtime;
using static Vortice.Direct3D12.D3D12;
using static Vortice.DXGI.DXGI;
using Vortice.Mathematics;
using Vortice.Dxc;
using Vortice.Direct3D12.Shader;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Vortice;
using ShaderResourceViewDimension = Vortice.Direct3D12.ShaderResourceViewDimension;
using System.IO;

namespace HelloDirect3D12Raytracing
{
    public sealed class D3D12GraphicsDevice : IGraphicsDevice
    {
        private const int RenderLatency = 2;

        public readonly Window Window;
        public readonly IDXGIFactory4 DXGIFactory;
        private readonly ID3D12Device2 _d3d12Device;
        private readonly ID3D12Device5 _d3d12Device5;
        private readonly ID3D12InfoQueue _infoQueue;
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

        public ID3D12Device2 D3D12Device => _d3d12Device;
        public ID3D12Device2 D3D12Device5 => _d3d12Device5;
        public ID3D12CommandQueue GraphicsQueue { get; }

        public IDXGISwapChain3 SwapChain { get; }

        public static bool IsSupported() => D3D12.IsSupported(FeatureLevel.Level_11_0);

        public D3D12GraphicsDevice(bool validation, Window window)
        {
            if (!IsSupported())
            {
                throw new InvalidOperationException("Direct3D12 is not supported on current OS");
            }

            Window = window;

            {
                if (validation && D3D12GetDebugInterface(out ID3D12Debug debug).Success)
                {
                    debug.EnableDebugLayer();
                    debug.Dispose();
                }
                else
                {
                    validation = false;
                }

                if (CreateDXGIFactory2(validation, out DXGIFactory).Failure)
                {
                    throw new InvalidOperationException("Cannot create IDXGIFactory4");
                }

                for (int adapterIndex = 0; DXGIFactory.EnumAdapters1(adapterIndex, out IDXGIAdapter1 adapter).Success; adapterIndex++)
                {
                    AdapterDescription1 desc = adapter.Description1;

                    // Don't select the Basic Render Driver adapter.
                    if ((desc.Flags & AdapterFlags.Software) != AdapterFlags.None)
                    {
                        adapter.Dispose();

                        continue;
                    }

                    if (D3D12CreateDevice(adapter, FeatureLevel.Level_11_0, out _d3d12Device).Success)
                    {
                        adapter.Dispose();

                        break;
                    }
                }

                // Check raytracing support
                if (_d3d12Device.Options5.RaytracingTier < RaytracingTier.Tier1_0)
                    throw new NotSupportedException("Raytracing not supported on _d3d12Device5");

                _d3d12Device5 = _d3d12Device.QueryInterface<ID3D12Device5>();

                if (validation)
                    _infoQueue = _d3d12Device.QueryInterfaceOrNull<ID3D12InfoQueue>();

                // Create synchronization objects.
                _frameFence = _d3d12Device.CreateFence<ID3D12Fence>(0);
                _frameFenceEvent = new AutoResetEvent(false);
            }

            {
                // Create Command queue.
                GraphicsQueue = _d3d12Device.CreateCommandQueue<ID3D12CommandQueue>(CommandListType.Direct);

                SwapChainDescription1 swapChainDesc = new SwapChainDescription1
                {
                    BufferCount = RenderLatency,
                    Width = window.Width,
                    Height = window.Height,
                    Format = Format.R8G8B8A8_UNorm,
                    Usage = Usage.RenderTargetOutput,
                    SwapEffect = SwapEffect.FlipDiscard,
                    SampleDescription = new SampleDescription(1, 0)
                };

                using (IDXGISwapChain1 swapChain = DXGIFactory.CreateSwapChainForHwnd(GraphicsQueue, window.Handle, swapChainDesc))
                {
                    DXGIFactory.MakeWindowAssociation(window.Handle, WindowAssociationFlags.IgnoreAltEnter);

                    SwapChain = swapChain.QueryInterface<IDXGISwapChain3>();
                    _backbufferIndex = SwapChain.GetCurrentBackBufferIndex();
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
                    _commandAllocators[i] = _d3d12Device.CreateCommandAllocator<ID3D12CommandAllocator>(CommandListType.Direct);
                }

                _commandList = _d3d12Device.CreateCommandList<ID3D12GraphicsCommandList4>(0, CommandListType.Direct, _commandAllocators[0]);
                _commandList.Close();
            }

            // Create a root signatures
            {
                var globalRootSignatureDescription = new RootSignatureDescription1(RootSignatureFlags.None);

                _globalRootSignature = _d3d12Device5.CreateRootSignature<ID3D12RootSignature>(0, globalRootSignatureDescription);


                var rayGenSignatureDescription = new RootSignatureDescription1(RootSignatureFlags.LocalRootSignature);

                rayGenSignatureDescription.Parameters = new[]{
                    new RootParameter1(
                        new RootDescriptorTable1(
                            new DescriptorRange1(DescriptorRangeType.UnorderedAccessView, 1, 0, 0, 0),
                            new DescriptorRange1(DescriptorRangeType.ShaderResourceView, 1, 0, 0, 1)
                            ), ShaderVisibility.All),
                };

                _rayGenRootSignature = _d3d12Device5.CreateRootSignature<ID3D12RootSignature>(0, rayGenSignatureDescription);

                var hitRootSignatureDescription = new RootSignatureDescription1(RootSignatureFlags.LocalRootSignature);

                hitRootSignatureDescription.Parameters = new[]
                {
                    new RootParameter1(RootParameterType.ShaderResourceView, new RootDescriptor1(0, 0), ShaderVisibility.All),
                };

                _hitRootSignature = _d3d12Device5.CreateRootSignature<ID3D12RootSignature>(0, hitRootSignatureDescription);

                var missRootSignatureDescription = new RootSignatureDescription1(RootSignatureFlags.LocalRootSignature);

                _missRootSignature = _d3d12Device5.CreateRootSignature<ID3D12RootSignature>(0, missRootSignatureDescription);


                // Create the shaders
                var rayGenShaderBlob = CompileLibraryShader(@"Shaders\Raygen.hlsl");
                var hitShaderBlob = CompileLibraryShader(@"Shaders\Hit.hlsl");
                var missShaderBlob = CompileLibraryShader(@"Shaders\Miss.hlsl");

                // Create the pipeline
                var rayGenLibrary = new StateSubObject(new DxilLibraryDescription(new ShaderBytecode(rayGenShaderBlob), new ExportDescription("RayGen", null)));
                var hitLibrary = new StateSubObject(new DxilLibraryDescription(new ShaderBytecode(hitShaderBlob), new ExportDescription("ClosestHit", null)));
                var missLibrary = new StateSubObject(new DxilLibraryDescription(new ShaderBytecode(missShaderBlob), new ExportDescription("Miss", null)));

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

                _raytracingStateObject = _d3d12Device5.CreateStateObject<ID3D12StateObject>(new StateObjectDescription(StateObjectType.RaytracingPipeline, stateSubObjects));
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

                _vertexBuffer = _d3d12Device5.CreateCommittedResource<ID3D12Resource>(new HeapProperties(HeapType.Upload),
                                                                              HeapFlags.None,
                                                                              ResourceDescription.Buffer((ulong)vertexBufferSize),
                                                                              ResourceStates.GenericRead);

                var vertexBufferData = new ReadOnlySpan<Vertex>(triangleVertices);

                var vertexBufferDataPointer = _vertexBuffer.Map(0);

                MemoryHelpers.CopyMemory(vertexBufferDataPointer, vertexBufferData);

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

                var bottomLevelInfo = _d3d12Device5.GetRaytracingAccelerationStructurePrebuildInfo(bottomLevelInputs);

                if (bottomLevelInfo.ResultDataMaxSizeInBytes == 0)
                    throw new Exception("Failed to create bottom level inputs.");


                var topLevelInputs = new BuildRaytracingAccelerationStructureInputs
                {
                    Type = RaytracingAccelerationStructureType.TopLevel,
                    Flags = RaytracingAccelerationStructureBuildFlags.None,
                    Layout = ElementsLayout.Array,
                    DescriptorsCount = 1,
                };

                var topLevelInfo = _d3d12Device5.GetRaytracingAccelerationStructurePrebuildInfo(topLevelInputs);

                if (topLevelInfo.ResultDataMaxSizeInBytes == 0)
                    throw new Exception("Failed to create top level inputs.");


                var scratchResource = _d3d12Device5.CreateCommittedResource<ID3D12Resource>(new HeapProperties(HeapType.Default),
                    HeapFlags.None,
                    ResourceDescription.Buffer(Math.Max(topLevelInfo.ScratchDataSizeInBytes, bottomLevelInfo.ScratchDataSizeInBytes), ResourceFlags.AllowUnorderedAccess),
                    ResourceStates.UnorderedAccess);

                _bottomLevelAccelerationStructure = _d3d12Device5.CreateCommittedResource<ID3D12Resource>(new HeapProperties(HeapType.Default),
                    HeapFlags.None,
                    ResourceDescription.Buffer(Math.Max(topLevelInfo.ScratchDataSizeInBytes, bottomLevelInfo.ResultDataMaxSizeInBytes), ResourceFlags.AllowUnorderedAccess),
                    ResourceStates.RaytracingAccelerationStructure);

                _topLevelAccelerationStructure = _d3d12Device5.CreateCommittedResource<ID3D12Resource>(new HeapProperties(HeapType.Default),
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
                    Flags = 0,
                    InstanceContributionToHitGroupIndex = (UInt24)0,
                    AccelerationStructure = (long)_bottomLevelAccelerationStructure.GPUVirtualAddress,
                };

                _instanceBuffer = _d3d12Device5.CreateCommittedResource<ID3D12Resource>(new HeapProperties(HeapType.Upload),
                                                                                    HeapFlags.None,
                                                                                    ResourceDescription.Buffer((ulong)Marshal.SizeOf<RaytracingInstanceDescription>()),
                                                                                    ResourceStates.GenericRead);

                var instanceBufferDataPointer = _instanceBuffer.Map(0);

                unsafe
                {
                    *(RaytracingInstanceDescription*)instanceBufferDataPointer = instanceDescription;
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
                    Width = (ulong)window.Width,
                    Height = window.Height,
                    Layout = TextureLayout.Unknown,
                    MipLevels = 1,
                    SampleDescription = new SampleDescription(1, 0),
                };

                _outputBuffer = _d3d12Device5.CreateCommittedResource<ID3D12Resource>(new HeapProperties(HeapType.Default),
                                                                              HeapFlags.None,
                                                                              outputBufferDescription,
                                                                              ResourceStates.CopySource);
            }

            // Create the shader resource heap
            {
                const int ShaderResourceDescriptorCount = 2;

                _shaderViewHeap = _d3d12Device5.CreateDescriptorHeap<ID3D12DescriptorHeap>(new DescriptorHeapDescription(DescriptorHeapType.ConstantBufferViewShaderResourceViewUnorderedAccessView, ShaderResourceDescriptorCount, DescriptorHeapFlags.ShaderVisible));

                var shaderViewDescriptorSize = _d3d12Device5.GetDescriptorHandleIncrementSize(DescriptorHeapType.ConstantBufferViewShaderResourceViewUnorderedAccessView);

                var shaderViewHandle = _shaderViewHeap.GetCPUDescriptorHandleForHeapStart();


                var outputBufferViewDescription = new UnorderedAccessViewDescription { ViewDimension = UnorderedAccessViewDimension.Texture2D };

                _d3d12Device5.CreateUnorderedAccessView(_outputBuffer, null, outputBufferViewDescription, shaderViewHandle);


                var accelerationStructureViewDescription = new ShaderResourceViewDescription
                {
                    Format = Format.Unknown,
                    ViewDimension = ShaderResourceViewDimension.RaytracingAccelerationStructure,
                    Shader4ComponentMapping = D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING,
                    RaytracingAccelerationStructure = new RaytracingAccelerationStructureShaderResourceView { Location = _topLevelAccelerationStructure.GPUVirtualAddress },
                };

                _d3d12Device5.CreateShaderResourceView(null, accelerationStructureViewDescription, shaderViewHandle + shaderViewDescriptorSize);
            }

            // Create the shader binding table
            {
                _raytracingStateObjectProperties = _raytracingStateObject.QueryInterface<ID3D12StateObjectProperties>();

                _shaderBindingTableEntrySize = D3D12.ShaderIdentifierSizeInBytes;
                _shaderBindingTableEntrySize += 8; // Ray generator descriptor table
                _shaderBindingTableEntrySize = Align(_shaderBindingTableEntrySize, D3D12.RaytracingShaderRecordByteAlignment);

                var shaderBindingTableSize = _shaderBindingTableEntrySize * 3;

                _shaderBindingTableBuffer = _d3d12Device5.CreateCommittedResource<ID3D12Resource>(new HeapProperties(HeapType.Upload),
                                                                                   HeapFlags.None,
                                                                                   ResourceDescription.Buffer((ulong)shaderBindingTableSize),
                                                                                   ResourceStates.GenericRead);

                var shaderBindingTableBufferDataPointer = _shaderBindingTableBuffer.Map(0);

                MemoryHelpers.CopyMemory(shaderBindingTableBufferDataPointer + _shaderBindingTableEntrySize * 0, _raytracingStateObjectProperties.GetShaderIdentifier("RayGen"), D3D12.ShaderIdentifierSizeInBytes);

                unsafe
                {
                    *(GpuDescriptorHandle*)(shaderBindingTableBufferDataPointer + _shaderBindingTableEntrySize * 0 + D3D12.ShaderIdentifierSizeInBytes) = _shaderViewHeap.GetGPUDescriptorHandleForHeapStart();
                }

                MemoryHelpers.CopyMemory(shaderBindingTableBufferDataPointer + _shaderBindingTableEntrySize * 1, _raytracingStateObjectProperties.GetShaderIdentifier("HitGroup"), D3D12.ShaderIdentifierSizeInBytes);

                unsafe
                {
                    *(ulong*)(shaderBindingTableBufferDataPointer + _shaderBindingTableEntrySize * 1 + D3D12.ShaderIdentifierSizeInBytes) = _vertexBuffer.GPUVirtualAddress;
                }

                MemoryHelpers.CopyMemory(shaderBindingTableBufferDataPointer + _shaderBindingTableEntrySize * 2, _raytracingStateObjectProperties.GetShaderIdentifier("Miss"), D3D12.ShaderIdentifierSizeInBytes);

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
            _d3d12Device.Dispose();
            _d3d12Device5.Dispose();
            _infoQueue.Dispose();
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

            if (DXGIGetDebugInterface1(out IDXGIDebug1 dxgiDebug).Success)
            {
                dxgiDebug.ReportLiveObjects(All, ReportLiveObjectFlags.Summary | ReportLiveObjectFlags.IgnoreInternal);
                dxgiDebug.Dispose();
            }
        }

        public void WaitIdle()
        {
            GraphicsQueue.Signal(_frameFence, ++_frameCount);
            _frameFence.SetEventOnCompletion(_frameCount, _frameFenceEvent);
            _frameFenceEvent.WaitOne();
        }

        public bool DrawFrame(Action<int, int> draw, [CallerMemberName] string frameName = null)
        {
            WriteDebugMessages(_infoQueue);

            _commandAllocators[_frameIndex].Reset();
            _commandList.Reset(_commandAllocators[_frameIndex]);
            _commandList.BeginEvent("Frame");

            // Call callback.
            draw(Window.Width, Window.Height);

            // Raytracing
            _commandList.ResourceBarrierTransition(_outputBuffer, ResourceStates.CopySource, ResourceStates.UnorderedAccess);

            _commandList.SetDescriptorHeaps(1, new[] { _shaderViewHeap });

            _commandList.SetComputeRootSignature(_globalRootSignature);

            _commandList.SetPipelineState1(_raytracingStateObject);

            _commandList.DispatchRays(new DispatchRaysDescription
            {
                Width = Window.Width,
                Height = Window.Height,
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
            _backbufferIndex = SwapChain.GetCurrentBackBufferIndex();

            return true;
        }

        private static void WriteDebugMessages(ID3D12InfoQueue infoQueue)
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

        private static Span<byte> CompileLibraryShader(string filePath)
        {
            var source = File.ReadAllText(filePath);

            var result = DxcCompiler.Compile(DxcShaderStage.Library, source, "", new DxcCompilerOptions { ShaderModel = DxcShaderModel.Model6_3, EnableDebugInfo = true }, filePath);

            if (result.GetStatus().Failure)
                throw new Exception(result.GetErrors());

            var data = result.GetObjectBytecode().ToArray();

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


        private const int D3D12_SHADER_COMPONENT_MAPPING_MASK = 0x7;
        private const int D3D12_SHADER_COMPONENT_MAPPING_SHIFT = 3;

        private static int D3D12_SHADER_COMPONENT_MAPPING_ALWAYS_SET_BIT_AVOIDING_ZEROMEM_MISTAKES = (1 << (D3D12_SHADER_COMPONENT_MAPPING_SHIFT * 4));

        private static int D3D12_ENCODE_SHADER_4_COMPONENT_MAPPING(int Src0, int Src1, int Src2, int Src3)
        {
            return ((((Src0) & D3D12_SHADER_COMPONENT_MAPPING_MASK) |
                   (((Src1) & D3D12_SHADER_COMPONENT_MAPPING_MASK) << D3D12_SHADER_COMPONENT_MAPPING_SHIFT) |
                   (((Src2) & D3D12_SHADER_COMPONENT_MAPPING_MASK) << (D3D12_SHADER_COMPONENT_MAPPING_SHIFT * 2)) |
                   (((Src3) & D3D12_SHADER_COMPONENT_MAPPING_MASK) << (D3D12_SHADER_COMPONENT_MAPPING_SHIFT * 3)) |
                   D3D12_SHADER_COMPONENT_MAPPING_ALWAYS_SET_BIT_AVOIDING_ZEROMEM_MISTAKES));
        }

        private static int D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING = D3D12_ENCODE_SHADER_4_COMPONENT_MAPPING(0, 1, 2, 3);
    }
}
