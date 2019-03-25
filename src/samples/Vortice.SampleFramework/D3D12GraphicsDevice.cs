// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using SharpDirect3D12;
using SharpDirect3D12.Debug;
using SharpDXGI;
using SharpDXGI.Direct3D;
using static SharpDirect3D12.D3D12;
using static SharpDXGI.DXGI;

namespace Vortice
{
    public sealed class D3D12GraphicsDevice : IGraphicsDevice
    {
        private const int FrameCount = 2;

        public readonly Window Window;
        public readonly IDXGIFactory4 DXGIFactory;
        private readonly ID3D12Device _d3d12Device;
        private readonly ID3D12CommandQueue _d3d12CommandQueue;
        private readonly ID3D12DescriptorHeap _rtvHeap;
        private readonly int _rtvDescriptorSize;
        private readonly ID3D12Resource[] _renderTargets;
        private readonly ID3D12CommandAllocator _commandAllocator;
        private readonly ID3D12GraphicsCommandList _commandList;
        private readonly ID3D12Fence _d3d12Fence;
        private ulong _fenceValue;
        private readonly AutoResetEvent _fenceEvent;
        private int _frameIndex;

        public ID3D12Device D3D12Device => _d3d12Device;
        public IDXGISwapChain3 SwapChain { get; }

        public static bool IsSupported()
        {
            return ID3D12Device.IsSupported(null, FeatureLevel.Level_11_0);
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
            }
            else
            {
                validation = false;
            }

            if (CreateDXGIFactory2(validation, out DXGIFactory).Failure)
            {
                throw new InvalidOperationException("Cannot create IDXGIFactory4");
            }

            D3D12CreateDevice(null, FeatureLevel.Level_11_0, out _d3d12Device).CheckError();
            _d3d12CommandQueue = _d3d12Device.CreateCommandQueue(new CommandQueueDescription(CommandListType.Direct, CommandQueuePriority.Normal));

            var swapChainDesc = new SwapChainDescription1
            {
                BufferCount = FrameCount,
                Width = window.Width,
                Height = window.Height,
                Format = Format.B8G8R8A8_UNorm,
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
            _commandList = _d3d12Device.CreateCommandList(CommandListType.Direct, _commandAllocator);
            _commandList.Close();

            // Create synchronization objects.
            _d3d12Fence = _d3d12Device.CreateFence(0);
            _fenceValue = 1;
            _fenceEvent = new AutoResetEvent(false);
        }

        public void Dispose()
        {
            _d3d12Device.Dispose();
            DXGIFactory.Dispose();
        }

        public bool DrawFrame(Action<int, int> draw, [CallerMemberName]string frameName = null)
        {
            _commandAllocator.Reset();
            _commandList.Reset(_commandAllocator, null);

            // Indicate that the back buffer will be used as a render target.
            _commandList.ResourceBarrierTransition(_renderTargets[_frameIndex], ResourceStates.Present, ResourceStates.RenderTarget);

            // Call callback.
            draw(Window.Width, Window.Height);

            var rtvHandle = _rtvHeap.GetCPUDescriptorHandleForHeapStart();
            rtvHandle += _frameIndex * _rtvDescriptorSize;

            // Record commands.
            var clearColor = new Vector4(0.0f, 0.2f, 0.4f, 1.0f);
            _commandList.ClearRenderTargetView(rtvHandle, clearColor);

            // Indicate that the back buffer will now be used to present.
            _commandList.ResourceBarrierTransition(_renderTargets[_frameIndex], ResourceStates.RenderTarget, ResourceStates.Present);
            _commandList.Close();

            // Execute the command list.
            _d3d12CommandQueue.ExecuteCommandList(_commandList);

            var result = SwapChain.Present(1, PresentFlags.None);
            if (result.Failure
                && result.Code == SharpDXGI.ResultCode.DeviceRemoved.Code)
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
    }
}
