using SharpGen.Runtime;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Vortice.Direct2D1;
using Vortice.DirectComposition;
using Vortice.DXGI;
using Vortice.Mathematics;
using Color = System.Drawing.Color;
using AlphaMode = Vortice.DXGI.AlphaMode;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Serilog;

namespace HelloComposition
{
    public abstract class CompositionVisual : IDisposable
    {
        public int Width { get; }
        public int Height { get; }
        [MaybeNull] public IDCompositionVisual2 Visual { get; private set; }
        [MaybeNull] public CompositionVisualPosition Position { get; private set; }
        [MaybeNull] public IDCompositionSurface SurfaceComposition { get; private set; }
        protected bool ResourcesMissing { get; set; }
        [MaybeNull] protected CompositionGraphicsDevice Device { get; private set; }

        private Color4 _background = new Color4(Color.Transparent);

        private const int SurfaceMargin = 2;

        public Color4 Background
        {
            get
            {
                return _background;
            }

            set
            {
                if (_background == value)
                {
                    return;
                }

                _background = value;
                OnGraphicsInvalidated();
            }
        }

        protected CompositionVisual([DisallowNull] CompositionGraphicsDevice device, int width, int height)
        {
            Device = device ?? throw new ArgumentNullException(nameof(device));
            Width = width;
            Height = height;
            Visual = new IDCompositionVisual2(device.CompositionDevice);
            Position = new CompositionVisualPosition(device, this);
            SurfaceComposition = new IDCompositionSurface(device.CompositionSurfaceFactory, width + 2 * SurfaceMargin,
                height + 2 * SurfaceMargin,
                Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied);
            Visual.Content = SurfaceComposition;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Position?.Dispose();
                SurfaceComposition?.Dispose();
                Visual?.Dispose();
                Position = null;
                SurfaceComposition = null;
                Visual = null;
                Device = null;
            }
        }

        protected abstract void DrawImpl(ID2D1DeviceContext context, IReadOnlyDictionary<string, ComObject> resources,
            Point offset);

        public virtual void Draw(Dictionary<string, ComObject> resources)
        {
            if (SurfaceComposition is null || Visual is null)
                return;

            using var context = SurfaceComposition.BeginDraw<ID2D1DeviceContext>(null, out var offset);
            var transform = context.Transform;
            transform.Translation = new Vector2(2, 2);
            context.Transform = transform;

            if (ResourcesMissing)
            {
                ResourcesMissing = false;
                CreateResources(resources, context);
            }

            if (!ResourcesMissing)
            {
                // We've successfully created resources (if it was needed)

                context.Clear(Background);
                DrawImpl(context, resources, offset);

                if (ResourcesMissing)
                {
                    // Create resources...

                    ResourcesMissing = false;
                    CreateResources(resources, context);

                    if (!ResourcesMissing)
                    {
                        // ... and retry

                        context.Clear(Background);
                        DrawImpl(context, resources, offset);
                    }
                }
            }

            SurfaceComposition.EndDraw();
        }

        public virtual void Composite()
        {
        }

        public virtual void CreateResources(IDictionary<string, ComObject> resources, ID2D1DeviceContext context)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public delegate void VisualInvalidatedEventHandler(CompositionVisual sender);

        public event VisualInvalidatedEventHandler CompositionInvalidated;
        public event VisualInvalidatedEventHandler GraphicsInvalidated;
        public event VisualInvalidatedEventHandler PositionAnimationInvalidated;

        internal bool _hasCompositionInvalidated;
        internal bool _hasGraphicsInvalidated;
        internal bool _hasPositionAnimationInvalidated;

        protected internal void OnCompositionInvalidated([CallerMemberName] string propertyName = null)
        {
            Log.Verbose(
                "[Visual] OnCompositionInvalidated [{Id}][{Source}][{PreviousState}]",
                RuntimeHelpers.GetHashCode(this),
                propertyName,
                _hasCompositionInvalidated
            );
            
            if (_hasCompositionInvalidated) return;

            _hasCompositionInvalidated = true;
            CompositionInvalidated?.Invoke(this);
        }

        protected internal void OnGraphicsInvalidated([CallerMemberName] string propertyName = null)
        {
            Log.Verbose(
                "[Visual] OnGraphicsInvalidated [{Id}][{Source}][{PreviousState}]",
                RuntimeHelpers.GetHashCode(this),
                propertyName,
                _hasGraphicsInvalidated
            );

            if (_hasGraphicsInvalidated) return;

            _hasGraphicsInvalidated = true;
            GraphicsInvalidated?.Invoke(this);
        }

        protected internal void OnPositionAnimationInvalidated([CallerMemberName] string propertyName = null)
        {
            Log.Verbose(
                "[Visual] OnPositionAnimationInvalidated [{Id}][{Source}][{PreviousState}]",
                RuntimeHelpers.GetHashCode(this),
                propertyName,
                _hasPositionAnimationInvalidated
            );

            if (_hasPositionAnimationInvalidated) return;

            _hasPositionAnimationInvalidated = true;
            PositionAnimationInvalidated?.Invoke(this);
        }
    }
}
