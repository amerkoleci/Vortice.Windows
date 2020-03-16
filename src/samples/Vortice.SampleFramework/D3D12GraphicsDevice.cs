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

namespace Vortice
{
    public sealed class D3D12GraphicsDevice : IGraphicsDevice
    {
        private const int FrameCount = 2;

        public readonly Window Window;
        public readonly IDXGIFactory4 DXGIFactory;
        public readonly IDXGIAdapter1 DXGIAdapter;
        private readonly ID3D12Device _d3d12Device;
        private readonly ID3D12CommandQueue _d3d12CommandQueue;
        private readonly ID3D12DescriptorHeap _rtvHeap;
        private readonly int _rtvDescriptorSize;
        private readonly ID3D12Resource[] _renderTargets;
        private readonly ID3D12CommandAllocator _commandAllocator;

        private readonly ID3D12RootSignature _rootSignature;
        private readonly ID3D12PipelineState _pipelineState;

        private readonly ID3D12GraphicsCommandList _commandList;

        private readonly ID3D12Resource _vertexBuffer;

        private readonly ID3D12Fence _d3d12Fence;
        private long _fenceValue;
        private readonly AutoResetEvent _fenceEvent;
        private int _frameIndex;

        public ID3D12Device D3D12Device => _d3d12Device;
        public IDXGISwapChain3 SwapChain { get; }

        public static bool IsSupported()
        {
            return D3D12.IsSupported(FeatureLevel.Level_11_0);
        }

        public D3D12GraphicsDevice(bool validation, Window window)
        {
            if (!IsSupported())
            {
                throw new InvalidOperationException("Direct3D12 is not supported on current OS");
            }

            Window = window;

            if (validation
                && D3D12GetDebugInterface<ID3D12Debug>(out var debug).Success)
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

            var adapters = DXGIFactory.Adapters1;
            for (var i = 0; i < adapters.Count; i++)
            {
                var adapter = adapters[i];
                var desc = adapter.Description1;

                // Don't select the Basic Render Driver adapter.
                if ((desc.Flags & AdapterFlags.Software) != AdapterFlags.None)
                {
                    continue;
                }

                if (D3D12CreateDevice(adapter, FeatureLevel.Level_11_0, out var device).Success)
                {
                    DXGIAdapter = adapter;
                    _d3d12Device = device;
                    break;
                }
            }

            // Check raytracing support.
            var featureOptions5 = _d3d12Device.Options5;
            if (featureOptions5.RaytracingTier != RaytracingTier.NotSupported)
            {
                //var d3d12Device5 = _d3d12Device.QueryInterfaceOrNull<ID3D12Device5>();
                //d3d12Device5.CreateStateObject(new StateObjectDescription(StateObjectType.Collection));
            }

            // Create Command queue.
            _d3d12CommandQueue = _d3d12Device.CreateCommandQueue(CommandListType.Direct);

            var swapChainDesc = new SwapChainDescription1
            {
                BufferCount = FrameCount,
                Width = window.Width,
                Height = window.Height,
                Format = Format.R8G8B8A8_UNorm,
                Usage = Usage.RenderTargetOutput,
                SwapEffect = SwapEffect.FlipDiscard,
                SampleDescription = new SampleDescription(1, 0)
            };

            var hwnd = (IntPtr)window.Handle;
            var swapChain = DXGIFactory.CreateSwapChainForHwnd(_d3d12CommandQueue, hwnd, swapChainDesc);
            DXGIFactory.MakeWindowAssociation(hwnd, WindowAssociationFlags.IgnoreAltEnter);

            SwapChain = swapChain.QueryInterface<IDXGISwapChain3>();
            _frameIndex = SwapChain.GetCurrentBackBufferIndex();

            _rtvHeap = _d3d12Device.CreateDescriptorHeap(new DescriptorHeapDescription(DescriptorHeapType.RenderTargetView, FrameCount));
            _rtvDescriptorSize = _d3d12Device.GetDescriptorHandleIncrementSize(DescriptorHeapType.RenderTargetView);

            // Create frame resources.
            {
                var rtvHandle = _rtvHeap.GetCPUDescriptorHandleForHeapStart();

                // Create a RTV for each frame.
                _renderTargets = new ID3D12Resource[FrameCount];
                for (var i = 0; i < FrameCount; i++)
                {
                    _renderTargets[i] = SwapChain.GetBuffer<ID3D12Resource>(i);
                    _d3d12Device.CreateRenderTargetView(_renderTargets[i], null, rtvHandle);
                    rtvHandle += _rtvDescriptorSize;
                }
            }

            _commandAllocator = _d3d12Device.CreateCommandAllocator(CommandListType.Direct);

            //var highestShaderVersion = _d3d12Device.CheckHighestShaderModel(ShaderModel.Model60);
            var highestRootSignatureVersion = _d3d12Device.CheckHighestRootSignatureVersion(RootSignatureVersion.Version11);
            //var opts5 = _d3d12Device.CheckFeatureSupport<FeatureDataD3D12Options5>(SharpDirect3D12.Feature.Options5);

            var rootSignatureDesc = new VersionedRootSignatureDescription(
                new RootSignatureDescription1(RootSignatureFlags.AllowInputAssemblerInputLayout)
                );

            _rootSignature = _d3d12Device.CreateRootSignature(0, rootSignatureDesc);

            const string shaderSource = @"struct PSInput {
                float4 position : SV_POSITION;
                float4 color : COLOR;
            };
            PSInput VSMain(float4 position : POSITION, float4 color : COLOR) {
                PSInput result;
                result.position = position;
                result.color = color;
                return result;
            }
            float4 PSMain(PSInput input) : SV_TARGET {
                return input.color;
            }

            [numthreads(1, 1, 1)]
            void CSMain(uint3 DTid : SV_DispatchThreadID )
            {
            }
";

            var inputElementDescs = new[]
            {
                new InputElementDescription("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                new InputElementDescription("COLOR", 0, Format.R32G32B32A32_Float, 12, 0)
            };

            var psoDesc = new GraphicsPipelineStateDescription()
            {
                RootSignature = _rootSignature,
                VertexShader = ShaderCompiler.Compile(shaderSource, ShaderStage.Vertex),
                PixelShader = ShaderCompiler.Compile(shaderSource, ShaderStage.Pixel),
                InputLayout = new InputLayoutDescription(inputElementDescs),
                SampleMask = uint.MaxValue,
                PrimitiveTopologyType = PrimitiveTopologyType.Triangle,
                RasterizerState = RasterizerDescription.CullCounterClockwise,
                BlendState = BlendDescription.Opaque,
                DepthStencilState = DepthStencilDescription.None,
                RenderTargetFormats = new[] { Format.R8G8B8A8_UNorm },
                DepthStencilFormat = Format.Unknown,
                SampleDescription = new SampleDescription(1, 0)
            };

            _pipelineState = _d3d12Device.CreateGraphicsPipelineState(psoDesc);

            _commandList = _d3d12Device.CreateCommandList(CommandListType.Direct, _commandAllocator, _pipelineState);
            _commandList.Close();

            var vertexBufferSize = 3 * Unsafe.SizeOf<Vertex>();

            _vertexBuffer = _d3d12Device.CreateCommittedResource(
                new HeapProperties(HeapType.Upload),
                HeapFlags.None,
                ResourceDescription.Buffer(vertexBufferSize),
                ResourceStates.GenericRead);

            var triangleVertices = new Vertex[]
            {
                  new Vertex(new Vector3(0f, 0.5f, 0.0f), new Color4(1.0f, 0.0f, 0.0f, 1.0f)),
                  new Vertex(new Vector3(0.5f, -0.5f, 0.0f), new Color4(0.0f, 1.0f, 0.0f, 1.0f)),
                  new Vertex(new Vector3(-0.5f, -0.5f, 0.0f), new Color4(0.0f, 0.0f, 1.0f, 1.0f))
            };

            unsafe
            {
                var bufferData = _vertexBuffer.Map(0);
                var src = new ReadOnlySpan<Vertex>(triangleVertices);
                MemoryHelpers.CopyMemory(bufferData, src);
                _vertexBuffer.Unmap(0);
            }

            // Create synchronization objects.
            _d3d12Fence = _d3d12Device.CreateFence(0);
            _fenceValue = 1;
            _fenceEvent = new AutoResetEvent(false);
            WaitForPreviousFrame();
        }

        public void Dispose()
        {
            _d3d12Device.Dispose();
            DXGIFactory.Dispose();
        }

        public bool DrawFrame(Action<int, int> draw, [CallerMemberName]string frameName = null)
        {
            _commandAllocator.Reset();
            _commandList.Reset(_commandAllocator, _pipelineState);

            // Set necessary state.
            _commandList.SetGraphicsRootSignature(_rootSignature);
            _commandList.RSSetViewport(new Viewport(Window.Width, Window.Height));
            _commandList.RSSetScissorRect(Rect.Create(Window.Width, Window.Height));

            // Indicate that the back buffer will be used as a render target.
            _commandList.ResourceBarrierTransition(_renderTargets[_frameIndex], ResourceStates.Present, ResourceStates.RenderTarget);

            // Call callback.
            draw(Window.Width, Window.Height);

            var rtvHandle = _rtvHeap.GetCPUDescriptorHandleForHeapStart();
            rtvHandle += _frameIndex * _rtvDescriptorSize;

            _commandList.OMSetRenderTargets(rtvHandle);

            // Record commands.
            var clearColor = new Color4(0.0f, 0.2f, 0.4f, 1.0f);
            _commandList.ClearRenderTargetView(rtvHandle, clearColor);

            _commandList.IASetPrimitiveTopology(PrimitiveTopology.TriangleList);
            var stride = Unsafe.SizeOf<Vertex>();
            var vertexBufferSize = 3 * stride;
            _commandList.IASetVertexBuffers(new VertexBufferView(_vertexBuffer.GPUVirtualAddress, vertexBufferSize, stride));
            _commandList.DrawInstanced(3, 1, 0, 0);

            // Indicate that the back buffer will now be used to present.
            _commandList.ResourceBarrierTransition(_renderTargets[_frameIndex], ResourceStates.RenderTarget, ResourceStates.Present);
            _commandList.Close();

            // Execute the command list.
            _d3d12CommandQueue.ExecuteCommandList(_commandList);

            var result = SwapChain.Present(1, PresentFlags.None);
            if (result.Failure
                && result.Code == DXGI.ResultCode.DeviceRemoved.Code)
            {
                return false;
            }

            WaitForPreviousFrame();

            return true;
        }

        private void WaitForPreviousFrame()
        {
            // WAITING FOR THE FRAME TO COMPLETE BEFORE CONTINUING IS NOT BEST PRACTICE.
            // This is code implemented as such for simplicity. The D3D12HelloFrameBuffering
            // sample illustrates how to use fences for efficient resource usage and to
            // maximize GPU utilization.

            // Signal and increment the fence value.
            var fenceValueToSignal = _fenceValue;
            _d3d12CommandQueue.Signal(_d3d12Fence, fenceValueToSignal);
            _fenceValue++;

            // Wait until the previous frame is finished.
            if (_d3d12Fence.CompletedValue < fenceValueToSignal)
            {
                _d3d12Fence.SetEventOnCompletion(fenceValueToSignal, _fenceEvent);
                _fenceEvent.WaitOne();
            }

            _frameIndex = SwapChain.GetCurrentBackBufferIndex();
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
        };

    }
}
