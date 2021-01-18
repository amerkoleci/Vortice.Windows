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

namespace Vortice
{
    public sealed class D3D12GraphicsDevice : IGraphicsDevice
    {
        private const int RenderLatency = 2;

        public readonly Window Window;
        public readonly IDXGIFactory4 DXGIFactory;
        private readonly ID3D12Device2 _d3d12Device;
        private readonly ID3D12DescriptorHeap _rtvHeap;
        private readonly int _rtvDescriptorSize;
        private readonly ID3D12Resource[] _renderTargets;
        private readonly ID3D12CommandAllocator[] _commandAllocators;

        private readonly ID3D12RootSignature _rootSignature;
        private readonly ID3D12PipelineState _pipelineState;

        private readonly ID3D12GraphicsCommandList _commandList;

        private readonly ID3D12Resource _vertexBuffer;

        private readonly ID3D12Fence _frameFence;
        private readonly AutoResetEvent _frameFenceEvent;
        private ulong _frameCount;
        private ulong _frameIndex;
        private int _backbufferIndex;

        public ID3D12Device2 D3D12Device => _d3d12Device;
        public ID3D12CommandQueue GraphicsQueue { get; }

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
                && D3D12GetDebugInterface(out ID3D12Debug debug).Success)
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

            // Check raytracing support.
            FeatureDataD3D12Options5 featureOptions5 = _d3d12Device.Options5;
            if (featureOptions5.RaytracingTier != RaytracingTier.NotSupported)
            {
                //var d3d12Device5 = _d3d12Device.QueryInterfaceOrNull<ID3D12Device5>();
                //d3d12Device5.CreateStateObject(new StateObjectDescription(StateObjectType.Collection));
            }

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

            _rtvHeap = _d3d12Device.CreateDescriptorHeap<ID3D12DescriptorHeap>(new DescriptorHeapDescription(DescriptorHeapType.RenderTargetView, RenderLatency));
            _rtvDescriptorSize = _d3d12Device.GetDescriptorHandleIncrementSize(DescriptorHeapType.RenderTargetView);

            // Create frame resources.
            {
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

            _commandAllocators = new ID3D12CommandAllocator[RenderLatency];
            for (int i = 0; i < RenderLatency; i++)
            {
                _commandAllocators[i] = _d3d12Device.CreateCommandAllocator<ID3D12CommandAllocator>(CommandListType.Direct);
            }

            RootSignatureDescription1 rootSignatureDesc = new RootSignatureDescription1(RootSignatureFlags.AllowInputAssemblerInputLayout);

            _rootSignature = _d3d12Device.CreateRootSignature<ID3D12RootSignature>(0, rootSignatureDesc);

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

            InputElementDescription[] inputElementDescs = new[]
            {
                new InputElementDescription("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                new InputElementDescription("COLOR", 0, Format.R32G32B32A32_Float, 12, 0)
            };

            Span<byte> vertexShaderByteCode = CompileBytecodeWithReflection(DxcShaderStage.Vertex,
                shaderSource, "VSMain", out ID3D12ShaderReflection vertexShaderReflection);

            Span<byte> pixelShaderByteCode = CompileBytecodeWithReflection(DxcShaderStage.Pixel,
                shaderSource, "PSMain", out ID3D12ShaderReflection pixelShaderReflection);

            GraphicsPipelineStateDescription psoDesc = new GraphicsPipelineStateDescription()
            {
                RootSignature = _rootSignature,
                VertexShader = vertexShaderByteCode,
                PixelShader = pixelShaderByteCode,
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

            //var test = new TestS();
            //test.RootSignature.Type = PipelineStateSubObjectType.RootSignature;
            //test.RootSignature.RootSignature = _rootSignature.NativePointer;

            //var builder = new PipelineBuilder();
            //builder.Add(_rootSignature);
            //_pipelineState = _d3d12Device.CreatePipelineState<ID3D12PipelineState, TestS>(test);
            //_pipelineState = _d3d12Device.CreatePipelineState<ID3D12PipelineState>(builder.Data);

            _pipelineState = _d3d12Device.CreateGraphicsPipelineState<ID3D12PipelineState>(psoDesc);

            _commandList = _d3d12Device.CreateCommandList<ID3D12GraphicsCommandList>(0, CommandListType.Direct, _commandAllocators[0], _pipelineState);
            _commandList.Close();

            int vertexBufferSize = 3 * Unsafe.SizeOf<Vertex>();

            _vertexBuffer = _d3d12Device.CreateCommittedResource<ID3D12Resource>(
                new HeapProperties(HeapType.Upload),
                HeapFlags.None,
                ResourceDescription.Buffer((ulong)vertexBufferSize),
                ResourceStates.GenericRead);

            Vertex[] triangleVertices = new Vertex[]
            {
                  new Vertex(new Vector3(0f, 0.5f, 0.0f), new Color4(1.0f, 0.0f, 0.0f, 1.0f)),
                  new Vertex(new Vector3(0.5f, -0.5f, 0.0f), new Color4(0.0f, 1.0f, 0.0f, 1.0f)),
                  new Vertex(new Vector3(-0.5f, -0.5f, 0.0f), new Color4(0.0f, 0.0f, 1.0f, 1.0f))
            };

            unsafe
            {
                IntPtr bufferData = _vertexBuffer.Map(0);
                ReadOnlySpan<Vertex> src = new ReadOnlySpan<Vertex>(triangleVertices);
                MemoryHelpers.CopyMemory(bufferData, src);
                _vertexBuffer.Unmap(0);
            }

            // Create synchronization objects.
            _frameFence = _d3d12Device.CreateFence<ID3D12Fence>(0);
            _frameFenceEvent = new AutoResetEvent(false);
        }

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

            _rtvHeap.Dispose();
            _pipelineState.Dispose();
            _rootSignature.Dispose();
            SwapChain.Dispose();
            _frameFence.Dispose();
            GraphicsQueue.Dispose();
            _d3d12Device.Dispose();
            DXGIFactory.Dispose();

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
            _commandAllocators[_frameIndex].Reset();
            _commandList.Reset(_commandAllocators[_frameIndex], _pipelineState);
            _commandList.BeginEvent("Frame");

            // Set necessary state.
            _commandList.SetGraphicsRootSignature(_rootSignature);
            _commandList.RSSetViewport(new Viewport(Window.Width, Window.Height));
            _commandList.RSSetScissorRect(new Rectangle(Window.Width, Window.Height));

            // Indicate that the back buffer will be used as a render target.
            _commandList.ResourceBarrierTransition(_renderTargets[_backbufferIndex], ResourceStates.Present, ResourceStates.RenderTarget);

            // Call callback.
            draw(Window.Width, Window.Height);

            CpuDescriptorHandle rtvHandle = _rtvHeap.GetCPUDescriptorHandleForHeapStart();
            rtvHandle += _backbufferIndex * _rtvDescriptorSize;

            _commandList.OMSetRenderTargets(rtvHandle);

            // Record commands.
            var clearColor = new Color4(0.0f, 0.2f, 0.4f, 1.0f);
            _commandList.ClearRenderTargetView(rtvHandle, clearColor);

            _commandList.IASetPrimitiveTopology(PrimitiveTopology.TriangleList);
            int stride = Unsafe.SizeOf<Vertex>();
            int vertexBufferSize = 3 * stride;
            _commandList.IASetVertexBuffers(0, new VertexBufferView(_vertexBuffer.GPUVirtualAddress, vertexBufferSize, stride));
            _commandList.DrawInstanced(3, 1, 0, 0);

            // Indicate that the back buffer will now be used to present.
            _commandList.ResourceBarrierTransition(_renderTargets[_backbufferIndex], ResourceStates.RenderTarget, ResourceStates.Present);
            _commandList.EndEvent();
            _commandList.Close();

            // Execute the command list.
            GraphicsQueue.ExecuteCommandList(_commandList);

            Result result = SwapChain.Present(1, PresentFlags.None);
            if (result.Failure
                && result.Code == DXGI.ResultCode.DeviceRemoved.Code)
            {
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
            _backbufferIndex = SwapChain.GetCurrentBackBufferIndex();

            return true;
        }

        private static Span<byte> CompileBytecode(DxcShaderStage stage, string shaderSource, string entryPoint)
        {
            IDxcResult results = DxcCompiler.Compile(stage, shaderSource, entryPoint, null);
            if (results.Status.Failure)
            {
                string errors = results.GetErrors();
                Console.WriteLine($"Failed to compile shader: {errors}");
                return null;
            }

            return results.GetObjectBytecode();
        }

        private static Span<byte> CompileBytecodeWithReflection(
            DxcShaderStage stage,
            string shaderSource,
            string entryPoint,
            out ID3D12ShaderReflection reflection)
        {
            IDxcResult results = DxcCompiler.Compile(stage, shaderSource, entryPoint, null);
            if (results.Status.Failure)
            {
                string errors = results.GetErrors();
                Console.WriteLine($"Failed to compile shader: {errors}");

                reflection = default;
                return null;
            }

            using (IDxcBlob reflectionData = results.GetOutput(DxcOutKind.Reflection))
            {
                reflection = DxcCompiler.Utils.CreateReflection<ID3D12ShaderReflection>(reflectionData);
            }

            return results.GetObjectBytecode();
        }

        public interface IPipelineStateStreamSubObject
        {
            PipelineStateSubObjectType Type { get; }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct PipelineStateStreamFlags : IPipelineStateStreamSubObject
        {
            [FieldOffset(0)]
            public PipelineStateSubObjectType Type;

            [FieldOffset(4)]
            public PipelineStateFlags Flags;

            [FieldOffset(0)]
            private IntPtr Ptr;

            PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => Type;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct RootSignatureSubObject : IPipelineStateStreamSubObject
        {
            [FieldOffset(0)]
            public PipelineStateSubObjectType Type;

            [FieldOffset(4)]
            public IntPtr RootSignature;

            [FieldOffset(0)]
            private IntPtr Ptr;

            PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => Type;
        }

        //[StructLayout(LayoutKind.Explicit)]
        //public struct PipelineStateSubObjectTypeInputLayout : IPipelineStateStreamSubObject
        //{
        //    [FieldOffset(0)]
        //    public PipelineStateSubObjectType Type;

        //    [FieldOffset(4)]
        //    public InputLayoutDescription.__Native RootSignature;

        //    [FieldOffset(0)]
        //    private IntPtr Ptr;

        //    PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => Type;
        //}

        struct TestS
        {
            public RootSignatureSubObject RootSignature;
        }

        public class PipelineBuilder
        {
            private readonly List<byte> data = new List<byte>();

            public byte[] Data => data.ToArray();

            public void Add(PipelineStateFlags flags)
            {
                Add(PipelineStateSubObjectType.Flags);
                data.AddRange(BitConverter.GetBytes((int)flags));
            }

            public void Add(ID3D12RootSignature rootSignature)
            {
                Add(PipelineStateSubObjectType.RootSignature);
                Add(rootSignature.NativePointer);
            }

            private void Add(PipelineStateSubObjectType type)
            {
                data.AddRange(BitConverter.GetBytes((int)type));
            }

            private void Add(IntPtr ptr)
            {
                if (IntPtr.Size == 4)
                {
                    data.AddRange(BitConverter.GetBytes((int)ptr));
                }
                else
                {
                    data.AddRange(BitConverter.GetBytes((long)ptr));
                }
            }
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
    }
}
