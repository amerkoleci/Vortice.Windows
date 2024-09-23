// Copyright (c) Amer Koleci and Contributors.
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
using Vortice;
using Vortice.DXGI.Debug;
using System.Runtime.InteropServices;

namespace HelloDirect3D12;

public sealed unsafe partial class D3D12GraphicsDevice : IGraphicsDevice
{
    private const int RenderLatency = 2;

    public readonly Window Window;
    public readonly IDXGIFactory4 DXGIFactory;
    private readonly ID3D12Device2 Device;
    private readonly ID3D12InfoQueue1? _infoQueue1;
    private readonly ID3D12DescriptorHeap _rtvDescriptorHeap;
    private readonly uint _rtvDescriptorSize;
    private readonly ID3D12Resource[] _renderTargets;

    private readonly Format _depthStencilFormat;
    private readonly ID3D12Resource? _depthStencilTexture;
    private readonly ID3D12DescriptorHeap? _dsvDescriptorHeap;

    private readonly ID3D12CommandAllocator[] _commandAllocators;

    private readonly ID3D12RootSignature _rootSignature;
    private readonly ID3D12PipelineState _pipelineState;

    private readonly ID3D12GraphicsCommandList4 _commandList;

    private readonly ID3D12Resource _vertexBuffer;

    private readonly ID3D12Fence _frameFence;
    private readonly AutoResetEvent _frameFenceEvent;
    private ulong _frameCount;
    private ulong _frameIndex;
    private int _backbufferIndex;

    public bool UseRenderPass { get; set; } = true;

    public static bool IsSupported() => D3D12.IsSupported(FeatureLevel.Level_12_0);

    public D3D12GraphicsDevice(bool validation, Window window, Format depthStencilFormat = Format.D32_Float)
    {
        if (!IsSupported())
        {
            throw new InvalidOperationException("Direct3D12 is not supported on current OS");
        }

        Window = window;
        _depthStencilFormat = depthStencilFormat;

        if (validation
            && D3D12GetDebugInterface(out ID3D12Debug? debug).Success)
        {
            debug!.EnableDebugLayer();
            debug!.Dispose();
        }
        else
        {
            validation = false;
        }

        if (D3D12GetDebugInterface(out ID3D12DeviceRemovedExtendedDataSettings1? dredSettings).Success)
        {
            // Turn on auto-breadcrumbs and page fault reporting.
            dredSettings.SetAutoBreadcrumbsEnablement(DredEnablement.ForcedOn);
            dredSettings.SetPageFaultEnablement(DredEnablement.ForcedOn);
            dredSettings.SetBreadcrumbContextEnablement(DredEnablement.ForcedOn);

            dredSettings.Dispose();
        }

        DXGIFactory = CreateDXGIFactory2<IDXGIFactory4>(validation);

        ID3D12Device2? d3d12Device = default;
        for (int adapterIndex = 0;
            DXGIFactory.EnumAdapters1(adapterIndex, out IDXGIAdapter1? adapter).Success;
            adapterIndex++)
        {
            AdapterDescription1 desc = adapter.Description1;

            // Don't select the Basic Render Driver adapter.
            if ((desc.Flags & AdapterFlags.Software) != AdapterFlags.None)
            {
                adapter.Dispose();
                continue;
            }

            if (D3D12CreateDevice(adapter, FeatureLevel.Level_11_0, out d3d12Device).Success)
            {
                adapter.Dispose();
                break;
            }
        }

        using (IDXGIFactory5? factory5 = DXGIFactory.QueryInterfaceOrNull<IDXGIFactory5>())
        {
            if (factory5 != null)
            {
                IsTearingSupported = factory5.PresentAllowTearing;
            }
        }

        if (d3d12Device == null)
        {
            throw new PlatformNotSupportedException("Cannot create ID3D12Device");
        }

        Device = d3d12Device!;

        _infoQueue1 = Device.QueryInterfaceOrNull<ID3D12InfoQueue1>();

        // RenderDoc makes the query fail for whatever reason.
        if (_infoQueue1 != null)
        {
            _infoQueue1.RegisterMessageCallback(DebugCallback, MessageCallbackFlags.None);
        }

        // Create Command queue.
        GraphicsQueue = Device.CreateCommandQueue(CommandListType.Direct);
        GraphicsQueue.Name = "Graphics Queue";

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

        _rtvDescriptorHeap = Device.CreateDescriptorHeap(new DescriptorHeapDescription(DescriptorHeapType.RenderTargetView, RenderLatency));
        _rtvDescriptorSize = Device.GetDescriptorHandleIncrementSize(DescriptorHeapType.RenderTargetView);


        if (_depthStencilFormat != Format.Unknown)
        {
            ResourceDescription depthStencilDesc = ResourceDescription.Texture2D(
                _depthStencilFormat,
                (uint)swapChainDesc.Width,
                (uint)swapChainDesc.Height,
                flags: ResourceFlags.AllowDepthStencil);

            ClearValue depthOptimizedClearValue = new ClearValue(_depthStencilFormat, 1.0f, 0);

            _depthStencilTexture = Device.CreateCommittedResource(
                HeapType.Default,
                depthStencilDesc,
                ResourceStates.DepthWrite,
                depthOptimizedClearValue);
            _depthStencilTexture.Name = "DepthStencil Texture";

            DepthStencilViewDescription dsViewDesc = new()
            {
                Format = _depthStencilFormat,
                ViewDimension = DepthStencilViewDimension.Texture2D
            };

            _dsvDescriptorHeap = Device.CreateDescriptorHeap(new DescriptorHeapDescription(DescriptorHeapType.DepthStencilView, 1));
            Device.CreateDepthStencilView(_depthStencilTexture, dsViewDesc, _dsvDescriptorHeap.GetCPUDescriptorHandleForHeapStart());
        }

        // Create frame resources.
        {
            CpuDescriptorHandle rtvHandle = _rtvDescriptorHeap.GetCPUDescriptorHandleForHeapStart();

            // Create a RTV for each frame.
            _renderTargets = new ID3D12Resource[RenderLatency];
            for (int i = 0; i < RenderLatency; i++)
            {
                _renderTargets[i] = SwapChain.GetBuffer<ID3D12Resource>(i);
                Device.CreateRenderTargetView(_renderTargets[i], null, rtvHandle);
                rtvHandle += (int)_rtvDescriptorSize;
            }
        }

        _commandAllocators = new ID3D12CommandAllocator[RenderLatency];
        for (int i = 0; i < RenderLatency; i++)
        {
            _commandAllocators[i] = Device.CreateCommandAllocator(CommandListType.Direct);
        }

        RootSignatureFlags rootSignatureFlags = RootSignatureFlags.AllowInputAssemblerInputLayout;
        rootSignatureFlags |= RootSignatureFlags.DenyHullShaderRootAccess;
        rootSignatureFlags |= RootSignatureFlags.DenyDomainShaderRootAccess;
        rootSignatureFlags |= RootSignatureFlags.DenyGeometryShaderRootAccess;
        rootSignatureFlags |= RootSignatureFlags.DenyAmplificationShaderRootAccess;
        rootSignatureFlags |= RootSignatureFlags.DenyMeshShaderRootAccess;
        RootSignatureDescription1 rootSignatureDesc = new(rootSignatureFlags);

        _rootSignature = Device.CreateRootSignature(rootSignatureDesc);
        InputElementDescription[] inputElementDescs = new[]
        {
            new InputElementDescription("POSITION", 0, Format.R32G32B32_Float, 0, 0),
            new InputElementDescription("COLOR", 0, Format.R32G32B32A32_Float, 12, 0)
        };

        ReadOnlyMemory<byte> vertexShaderByteCode = CompileBytecode(DxcShaderStage.Vertex, "Triangle.hlsl", "VSMain");
        ReadOnlyMemory<byte> pixelShaderByteCode = CompileBytecode(DxcShaderStage.Pixel, "Triangle.hlsl", "PSMain");

        bool usePSOStream = true;
        if (usePSOStream)
        {
            PipelineStateStream pipelineStateStream = new()
            {
                RootSignature = _rootSignature,
                VertexShader = vertexShaderByteCode.Span,
                PixelShader = pixelShaderByteCode.Span,
                InputLayout = new InputLayoutDescription(inputElementDescs),
                SampleMask = uint.MaxValue,
                PrimitiveTopology = PrimitiveTopologyType.Triangle,
                RasterizerState = RasterizerDescription.CullCounterClockwise,
                BlendState = BlendDescription.Opaque,
                DepthStencilState = DepthStencilDescription.Default,
                RenderTargetFormats = new[] { Format.R8G8B8A8_UNorm },
                DepthStencilFormat = _depthStencilFormat,
                SampleDescription = SampleDescription.Default
            };

            _pipelineState = Device.CreatePipelineState(pipelineStateStream);
        }
        else
        {

            GraphicsPipelineStateDescription psoDesc = new()
            {
                RootSignature = _rootSignature,
                VertexShader = vertexShaderByteCode,
                PixelShader = pixelShaderByteCode,
                InputLayout = new InputLayoutDescription(inputElementDescs),
                SampleMask = uint.MaxValue,
                PrimitiveTopologyType = PrimitiveTopologyType.Triangle,
                RasterizerState = RasterizerDescription.CullCounterClockwise,
                BlendState = BlendDescription.Opaque,
                DepthStencilState = DepthStencilDescription.Default,
                RenderTargetFormats = new[] { Format.R8G8B8A8_UNorm },
                DepthStencilFormat = _depthStencilFormat,
                SampleDescription = SampleDescription.Default
            };

            _pipelineState = Device.CreateGraphicsPipelineState(psoDesc);
        }

        _commandList = Device.CreateCommandList<ID3D12GraphicsCommandList4>(CommandListType.Direct, _commandAllocators[0], _pipelineState);
        _commandList.Close();

        ulong vertexBufferSize = 3 * (ulong)sizeof(VertexPositionColor);

        _vertexBuffer = Device.CreateCommittedResource(
            HeapType.Upload,
            ResourceDescription.Buffer(vertexBufferSize),
            ResourceStates.GenericRead);

        ReadOnlySpan<VertexPositionColor> triangleVertices =
        [
            new VertexPositionColor(new Vector3(0f, 0.5f, 0.0f), new Color4(1.0f, 0.0f, 0.0f, 1.0f)),
            new VertexPositionColor(new Vector3(0.5f, -0.5f, 0.0f), new Color4(0.0f, 1.0f, 0.0f, 1.0f)),
            new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.0f), new Color4(0.0f, 0.0f, 1.0f, 1.0f))
        ];

        _vertexBuffer.SetData(triangleVertices);

        // Create synchronization objects.
        _frameFence = Device.CreateFence(0);
        _frameFenceEvent = new AutoResetEvent(false);
    }

    public bool IsTearingSupported { get; }

    public ID3D12CommandQueue GraphicsQueue { get; }

    public IDXGISwapChain3 SwapChain { get; }

    public void Dispose()
    {
        WaitIdle();

        _vertexBuffer.Dispose();

        for (int i = 0; i < RenderLatency; i++)
        {
            _commandAllocators[i].Dispose();
            _renderTargets[i].Dispose();
        }
        _commandList.Dispose();

        _depthStencilTexture?.Dispose();
        _dsvDescriptorHeap?.Dispose();
        _rtvDescriptorHeap.Dispose();
        _pipelineState.Dispose();
        _rootSignature.Dispose();
        SwapChain.Dispose();
        _frameFence.Dispose();
        GraphicsQueue.Dispose();

        if (_infoQueue1 != null)
        {
            _infoQueue1.Dispose();
        }

#if DEBUG
        uint refCount = Device.Release();
        if (refCount > 0)
        {
            System.Diagnostics.Debug.WriteLine($"Direct3D11: There are {refCount} unreleased references left on the device");

            ID3D12DebugDevice? d3d12DebugDevice = Device.QueryInterfaceOrNull<ID3D12DebugDevice>();
            if (d3d12DebugDevice != null)
            {
                d3d12DebugDevice.ReportLiveDeviceObjects(ReportLiveDeviceObjectFlags.Detail | ReportLiveDeviceObjectFlags.IgnoreInternal);
                d3d12DebugDevice.Dispose();
            }
        }
#else
        Device.Dispose();
#endif

        DXGIFactory.Dispose();

#if DEBUG
        if (DXGIGetDebugInterface1(out IDXGIDebug1? dxgiDebug).Success)
        {
            dxgiDebug!.ReportLiveObjects(DebugAll, ReportLiveObjectFlags.Summary | ReportLiveObjectFlags.IgnoreInternal);
            dxgiDebug!.Dispose();
        }
#endif
    }

    public void WaitIdle()
    {
        GraphicsQueue.Signal(_frameFence, ++_frameCount);
        _frameFence.SetEventOnCompletion(_frameCount, _frameFenceEvent);
        _frameFenceEvent.WaitOne();
    }

    public bool DrawFrame(Action<int, int> draw, [CallerMemberName] string? frameName = null)
    {
        _commandAllocators[_frameIndex].Reset();
        _commandList.Reset(_commandAllocators[_frameIndex], _pipelineState);
        _commandList.BeginEvent("Frame");

        // Set necessary state.
        _commandList.SetGraphicsRootSignature(_rootSignature);

        // Indicate that the back buffer will be used as a render target.
        _commandList.ResourceBarrierTransition(_renderTargets[_backbufferIndex], ResourceStates.Present, ResourceStates.RenderTarget);

        CpuDescriptorHandle rtvDescriptor = new(_rtvDescriptorHeap.GetCPUDescriptorHandleForHeapStart(), _backbufferIndex, _rtvDescriptorSize);
        CpuDescriptorHandle? dsvDescriptor = _dsvDescriptorHeap != null ? _dsvDescriptorHeap.GetCPUDescriptorHandleForHeapStart() : null;

        Color4 clearColor = Colors.CornflowerBlue;

        if (UseRenderPass)
        {
            var renderPassDesc = new RenderPassRenderTargetDescription(rtvDescriptor,
                new RenderPassBeginningAccess(new ClearValue(Format.R8G8B8A8_UNorm, clearColor)),
                new RenderPassEndingAccess(RenderPassEndingAccessType.Preserve)
                );

            RenderPassDepthStencilDescription? depthStencil = default;
            if (_dsvDescriptorHeap != null)
            {
                depthStencil = new RenderPassDepthStencilDescription(
                    _dsvDescriptorHeap.GetCPUDescriptorHandleForHeapStart(),
                    new RenderPassBeginningAccess(new ClearValue(_depthStencilFormat, 1.0f, 0)),
                    new RenderPassEndingAccess(RenderPassEndingAccessType.Discard)
                    );
            }

            _commandList.BeginRenderPass(renderPassDesc, depthStencil);
        }
        else
        {
            _commandList.OMSetRenderTargets(rtvDescriptor, dsvDescriptor);
            _commandList.ClearRenderTargetView(rtvDescriptor, clearColor);

            if (dsvDescriptor.HasValue)
            {
                _commandList.ClearDepthStencilView(dsvDescriptor.Value, ClearFlags.Depth, 1.0f, 0);
            }
        }

        _commandList.RSSetViewport(new Viewport(Window.ClientSize.Width, Window.ClientSize.Height));
        _commandList.RSSetScissorRect(Window.ClientSize.Width, Window.ClientSize.Height);

        // Call callback.
        draw(Window.ClientSize.Width, Window.ClientSize.Height);

        _commandList.IASetPrimitiveTopology(PrimitiveTopology.TriangleList);
        uint stride = (uint)sizeof(VertexPositionColor);
        uint vertexBufferSize = 3 * stride;
        _commandList.IASetVertexBuffers(0, new VertexBufferView(_vertexBuffer.GPUVirtualAddress, vertexBufferSize, stride));
        _commandList.DrawInstanced(3, 1, 0, 0);

        if (UseRenderPass)
        {
            _commandList.EndRenderPass();
        }

        // Indicate that the back buffer will now be used to present.
        _commandList.ResourceBarrierTransition(_renderTargets[_backbufferIndex], ResourceStates.RenderTarget, ResourceStates.Present);
        _commandList.EndEvent();
        _commandList.Close();

        // Execute the command list.
        GraphicsQueue.ExecuteCommandList(_commandList);

        Result result = SwapChain.Present(1, PresentFlags.None);
        if (result.Failure
            && (result.Code == Vortice.DXGI.ResultCode.DeviceRemoved.Code || result.Code == Vortice.DXGI.ResultCode.DeviceReset.Code))
        {
            HandleDeviceLost();

            return false;
        }

        GraphicsQueue.Signal(_frameFence, ++_frameCount);

        ulong GPUFrameCount = _frameFence.CompletedValue;

        if ((_frameCount - GPUFrameCount) >= RenderLatency)
        {
            _frameFence.SetEventOnCompletion(GPUFrameCount + 1, _frameFenceEvent);
            _frameFenceEvent.WaitOne();
        }

        _frameIndex = _frameCount % RenderLatency;
        _backbufferIndex = SwapChain.CurrentBackBufferIndex;

        return true;
    }

    private void HandleDeviceLost()
    {
        Result removedReason = Device.DeviceRemovedReason;

        ID3D12DeviceRemovedExtendedData1? dred = Device.QueryInterfaceOrNull<ID3D12DeviceRemovedExtendedData1>();

        if (dred != null)
        {
            if (dred.GetAutoBreadcrumbsOutput1(out DredAutoBreadcrumbsOutput1? dredAutoBreadcrumbsOutput).Success)
            {
                AutoBreadcrumbNode1? currentNode = dredAutoBreadcrumbsOutput.HeadAutoBreadcrumbNode;
                int index = 0;
                while (currentNode != null)
                {
                    string? cmdListName = currentNode.CommandListDebugName;
                    string? cmdQueueName = currentNode.CommandQueueDebugName;
                    int expected = currentNode.BreadcrumbCount;
                    int actual = currentNode.LastBreadcrumbValue.GetValueOrDefault();

                    bool errorOccurred = actual > 0 && actual < expected;

                    if (actual == 0)
                    {
                        // Don't bother logging nodes that don't submit anything
                        currentNode = currentNode.Next;
                        ++index;
                        continue;
                    }

                    currentNode = currentNode.Next;
                    ++index;
                }
            }

            if (dred.GetPageFaultAllocationOutput1(out DredPageFaultOutput1? pageFaultOutput).Success)
            {

            }

            dred.Dispose();
        }
    }

    private static void DebugCallback(MessageCategory category, MessageSeverity severity, MessageId id, string description)
    {
    }

    private static ReadOnlyMemory<byte> CompileBytecode(DxcShaderStage stage, string shaderName, string entryPoint)
    {
        string assetsPath = Path.Combine(AppContext.BaseDirectory, "Assets");
        string shaderSource = File.ReadAllText(Path.Combine(assetsPath, shaderName));

        using (ShaderIncludeHandler includeHandler = new(assetsPath))
        {
            using IDxcResult results = DxcCompiler.Compile(stage, shaderSource, entryPoint, includeHandler: includeHandler);
            if (results.GetStatus().Failure)
            {
                throw new Exception(results.GetErrors());
            }

            return results.GetObjectBytecodeMemory();
        }
    }
}
