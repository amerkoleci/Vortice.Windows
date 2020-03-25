using SharpGen.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using C5;
using Vortice;
using Vortice.Animation;
using Vortice.Direct2D1;
using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.DirectComposition;
using Vortice.DXGI;
using Vortice.Win32;

namespace HelloComposition
{
    public sealed partial class CompositionGraphicsDevice : IDisposable
    {
        private static readonly Vortice.Direct3D.FeatureLevel[] s_featureLevels =
        {
            Vortice.Direct3D.FeatureLevel.Level_11_1, Vortice.Direct3D.FeatureLevel.Level_11_0
        };

        private const int FrameCount = 2;

        private readonly IDXGIFactory2 _factory;
        private readonly ID2D1Factory1 _factory2D;
        private readonly IDXGIDevice1 _device;
        private readonly ID3D11Device _device3D;
        private readonly ID2D1Device _device2D;
        
        
        private readonly ID3D11DeviceContext _deviceContext3D;
        private readonly ID2D1DeviceContext _deviceContext2D;
        private readonly IDXGISwapChain _swapChain;
        private readonly IDXGISurface _surface;
        private readonly ID2D1Bitmap _bitmap2D;
        private readonly IDCompositionTarget _compositionTarget;
        private readonly IDCompositionVisual _rootVisual;
        
        private double _lastNextEstimatedFrameTime;

        [NotNull] public Stopwatch DispatcherTimer { get; } = new Stopwatch();
        [NotNull] public IDCompositionDesktopDevice CompositionDevice { get; }
        [NotNull] public IDCompositionSurfaceFactory CompositionSurfaceFactory { get; }
        [NotNull] public IUIAnimationManager2 AnimationManager { get; }
        [NotNull] public IUIAnimationTransitionLibrary2 AnimationTransitionLibrary { get; }
        [NotNull] public IUIAnimationTransitionFactory2 AnimationTransitionFactory { get; }
        [NotNull] public Random Random { get; } = new Random();
        
        [NotNull] private readonly System.Collections.Generic.IList<CompositionVisual> _visuals = new List<CompositionVisual>();
        [NotNull] private readonly Dictionary<string, ComObject> _resources = new Dictionary<string, ComObject>();
        
        [NotNull] private readonly System.Collections.Generic.IList<CompositionVisual> _graphicsInvalidatedVisuals = new List<CompositionVisual>();
        [NotNull] private readonly System.Collections.Generic.IList<CompositionVisual> _compositionInvalidatedVisuals = new List<CompositionVisual>();
        [NotNull] private readonly System.Collections.Generic.IList<CompositionVisual> _positionAnimationInvalidatedVisuals = new List<CompositionVisual>();
        [NotNull] private readonly TreeDictionary<long, Queue<Action>> _scheduleWithTime = new TreeDictionary<long, Queue<Action>>();
        [NotNull] private readonly Queue<Action> _scheduleQueue = new Queue<Action>();
        
        private WNDPROC _wndProc;
        private bool _paused;
        private bool _exitRequested;
        public Window MainWindow { get; private set; }

        private void OnDraw()
        {
            var requiresCommit = false;

            if (_graphicsInvalidatedVisuals.Count != 0)
            {
                requiresCommit = true;

                foreach (var visual in _graphicsInvalidatedVisuals)
                {
                    visual._hasGraphicsInvalidated = false;
                    
                    visual.Draw(_resources);
                }

                _graphicsInvalidatedVisuals.Clear();
            }

            if (_compositionInvalidatedVisuals.Count != 0)
            {
                requiresCommit = true;

                foreach (var visual in _compositionInvalidatedVisuals)
                {
                    visual._hasCompositionInvalidated = false;
                    
                    visual.Composite();
                }

                _compositionInvalidatedVisuals.Clear();
            }
            
            var frameStats = CompositionDevice.FrameStatistics;
 
            var nextEstimatedFrameTime = (double)frameStats.NextEstimatedFrameTime / frameStats.TimeFrequency;

            if (nextEstimatedFrameTime <= _lastNextEstimatedFrameTime)
                nextEstimatedFrameTime = _lastNextEstimatedFrameTime + 2 * double.Epsilon;

            _lastNextEstimatedFrameTime = nextEstimatedFrameTime;
                    
            AnimationManager.Update(nextEstimatedFrameTime);

            if (_positionAnimationInvalidatedVisuals.Count != 0)
            {
                requiresCommit = true;
                
                foreach (var visual in _positionAnimationInvalidatedVisuals)
                {
                    visual._hasPositionAnimationInvalidated = false;
                    
                    visual.Position.AnimationCommit.Commit(nextEstimatedFrameTime);
                    visual.Position.AnimationCommit.Dispose();
                    visual.Position.AnimationCommit = null;
                }

                _positionAnimationInvalidatedVisuals.Clear();
            }

            if (requiresCommit)
            {
                CompositionDevice.Commit();
            }
        }

        public CompositionGraphicsDevice(bool validation)
        {
            DXGI.CreateDXGIFactory2(validation, out _factory).CheckError();
            var factory2DOptions = new FactoryOptions
            {
                DebugLevel = validation ? DebugLevel.Information : DebugLevel.None
            };
            D2D1.D2D1CreateFactory(FactoryType.SingleThreaded, factory2DOptions, out _factory2D).CheckError();
            AnimationManager = new IUIAnimationManager2();
            AnimationTransitionLibrary = new IUIAnimationTransitionLibrary2();
            AnimationTransitionFactory = new IUIAnimationTransitionFactory2();

            var creationFlags = DeviceCreationFlags.BgraSupport;
            if (validation)
                creationFlags |= DeviceCreationFlags.Debug;

            if (D3D11.D3D11CreateDevice(
                null,
                DriverType.Hardware,
                creationFlags,
                s_featureLevels,
                out _device3D, out _, out _deviceContext3D).Failure)
            {
                // Remove debug flag not being supported.
                creationFlags &= ~DeviceCreationFlags.Debug;

                D3D11.D3D11CreateDevice(null, DriverType.Hardware,
                    creationFlags, s_featureLevels,
                    out _device3D, out _, out _deviceContext3D).CheckError();
            }

            _device = _device3D.QueryInterface<IDXGIDevice1>();
            _device2D = _factory2D.CreateDevice(_device);
            _deviceContext2D = _device2D.CreateDeviceContext(DeviceContextOptions.None);
            _device.MaximumFrameLatency = 1;

            PlatformConstruct();

            var (width, height) = MainWindow.ClientSize;

            var swapChainDescription = new SwapChainDescription1
            {
                BufferCount = FrameCount,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.FlipSequential,
                Usage = Vortice.DXGI.Usage.RenderTargetOutput,
                Format = Format.B8G8R8A8_UNorm,
                AlphaMode = Vortice.DXGI.AlphaMode.Premultiplied,
                Width = width,
                Height = height
            };

            _swapChain = _factory.CreateSwapChainForComposition(_device3D, swapChainDescription);
            _factory.MakeWindowAssociation(MainWindow.Handle, WindowAssociationFlags.IgnoreAltEnter);

            var dpi = User32.GetDpiForWindow(MainWindow.Handle);
            var bitmapProperties = new BitmapProperties1
            {
                BitmapOptions = BitmapOptions.CannotDraw | BitmapOptions.Target,
                PixelFormat = new PixelFormat(Format.B8G8R8A8_UNorm, Vortice.Direct2D1.AlphaMode.Premultiplied),
                DpiX = dpi,
                DpiY = dpi
            };

            _surface = _swapChain.GetBuffer<IDXGISurface>(0);
            _bitmap2D = _deviceContext2D.CreateBitmapFromDxgiSurface(_surface, bitmapProperties);
            _deviceContext2D.SetTarget(_bitmap2D);

            CompositionDevice = new IDCompositionDesktopDevice(_device);
            _compositionTarget = IDCompositionTarget.FromHwnd(CompositionDevice, MainWindow.Handle, true);
            CompositionSurfaceFactory = new IDCompositionSurfaceFactory(CompositionDevice, _device2D);

            _rootVisual = new IDCompositionVisual2(CompositionDevice);
            _compositionTarget.Root = _rootVisual;
            CompositionDevice.Commit();
            
            _scheduleWithTime.ItemsRemoved += ScheduleWithTimeOnItemsRemoved;
            DispatcherTimer.Start();
        }

        public void Dispose()
        {
            // Stop the Dispatcher
            DispatcherTimer.Stop();
            _scheduleQueue.Clear();
            _scheduleWithTime.ItemsRemoved -= ScheduleWithTimeOnItemsRemoved; 
            _scheduleWithTime.Clear();
            
            
            // Drop the visual tree
            _graphicsInvalidatedVisuals.Clear();
            _compositionInvalidatedVisuals.Clear();
            foreach (var resource in _resources.Values)
                resource.Dispose();
            foreach (var visual in _visuals)
                visual.Dispose();
            _resources.Clear();
            _compositionTarget.Root = null;
            _rootVisual.Dispose();
            
            // Drop the rest of the graphics stack
            CompositionSurfaceFactory.Dispose();
            _compositionTarget.Dispose();
            CompositionDevice.Dispose();
            _bitmap2D.Dispose();
            _surface.Dispose();
            _deviceContext2D.Dispose();
            _device2D.Dispose();
            _deviceContext3D.ClearState();
            _deviceContext3D.Flush();
            _deviceContext3D.Dispose();
            _device3D.Dispose();
            _device.Dispose();
            _swapChain.Dispose();
            AnimationTransitionLibrary.Dispose();
            AnimationManager.Dispose();
            _factory2D.Dispose();
            _factory.Dispose();
        }

        private void ScheduleWithTimeOnItemsRemoved(object sender, ItemCountEventArgs<C5.KeyValuePair<long, Queue<Action>>> eventargs)
        {
            while (eventargs.Item.Value.TryDequeue(out var action)) action();
        }

        public bool DrawFrame()
        {
            _deviceContext2D.BeginDraw();

            OnDraw();

            _deviceContext2D.EndDraw().CheckError();

            var result = _swapChain.Present(1, PresentFlags.None);
            return !result.Failure || result.Code != Vortice.DXGI.ResultCode.DeviceRemoved.Code;
        }

        public void AddVisual(CompositionVisual visual)
        {
            _visuals.Add(visual);
            _rootVisual.AddVisual(visual.Visual, true, null);
            visual.CompositionInvalidated += VisualOnCompositionInvalidated;
            visual.GraphicsInvalidated += VisualOnGraphicsInvalidated;
            visual.PositionAnimationInvalidated += VisualOnPositionAnimationInvalidated;
            visual._hasCompositionInvalidated = false;
            visual._hasGraphicsInvalidated = false;
            visual._hasPositionAnimationInvalidated = false;
            visual.OnCompositionInvalidated();
            visual.OnGraphicsInvalidated();
            visual.OnPositionAnimationInvalidated();
        }

        private void VisualOnPositionAnimationInvalidated(CompositionVisual sender)
        {
            _positionAnimationInvalidatedVisuals.Add(sender);
        }

        private void VisualOnGraphicsInvalidated(CompositionVisual sender)
        {
            _graphicsInvalidatedVisuals.Add(sender);
        }

        private void VisualOnCompositionInvalidated(CompositionVisual sender)
        {
            _compositionInvalidatedVisuals.Add(sender);
        }

        public void Schedule(TimeSpan delay, Action action)
        {
            if (_exitRequested)
                return;

            var key = (long) Math.Ceiling(DispatcherTimer.ElapsedMilliseconds + delay.TotalMilliseconds);
            var queue = new Queue<Action>();
            _scheduleWithTime.FindOrAdd(key, ref queue);
            queue.Enqueue(action);
        }

        public void Schedule(Action action)
        {
            if (_exitRequested)
                return;

            _scheduleQueue.Enqueue(action);
        }

        public void ProcessDispatcherQueue()
        {
            if (_exitRequested)
                return;
            
            while (_scheduleQueue.TryDequeue(out var action))
                action();

            _scheduleWithTime.RemoveRangeTo(DispatcherTimer.ElapsedMilliseconds);
        }
    }
}
