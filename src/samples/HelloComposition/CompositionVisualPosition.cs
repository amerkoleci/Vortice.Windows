using System;
using System.Diagnostics.CodeAnalysis;
using Vortice.Animation;
using Vortice.DirectComposition;
using Vortice.Mathematics;

namespace HelloComposition
{
    public sealed partial class CompositionVisualPosition : IDisposable
    {
        internal CompositionVisualPosition([DisallowNull] CompositionGraphicsDevice graphicsDevice,
            [DisallowNull] CompositionVisual visual)
        {
            _graphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            _visual = visual ?? throw new ArgumentNullException(nameof(visual));
            var animationManager = _graphicsDevice.AnimationManager;
            var point = new PointF(20, 20);
            _x = new IUIAnimationVariable2(animationManager, point.X);
            _y = new IUIAnimationVariable2(animationManager, point.Y);
            AnimateToPoint(point);
        }
        [MaybeNull] internal CompositionVisualPositionAnimationCommit AnimationCommit;
        [MaybeNull] private CompositionVisual _visual;
        [MaybeNull] private CompositionGraphicsDevice _graphicsDevice;
        [MaybeNull] private IUIAnimationVariable2 _x;
        [MaybeNull] private IUIAnimationVariable2 _y;
        private PointF _targetPoint;

        public PointF TargetPoint => _targetPoint;
        
        internal sealed class CompositionVisualPositionAnimationCommit : IDisposable
        {
            public IDCompositionAnimation XComposition;
            public IDCompositionAnimation YComposition;
            public IUIAnimationStoryboard2 Storyboard;
            private CompositionVisualPosition _position;

            public CompositionVisualPositionAnimationCommit([DisallowNull] CompositionVisualPosition position)
            {
                _position = position ?? throw new ArgumentNullException(nameof(position));
            }

            public void Commit(double nextEstimatedFrameTime)
            {
                Storyboard.Schedule(nextEstimatedFrameTime);
                
                _position._x.GetCurve(XComposition);
                _position._y.GetCurve(YComposition);
                _position._visual.Visual.SetOffsetX(XComposition);
                _position._visual.Visual.SetOffsetY(YComposition);
            }

            public void Dispose()
            {
                XComposition?.Dispose();
                YComposition?.Dispose();
                Storyboard?.Dispose();
                XComposition = null;
                YComposition = null;
                Storyboard = null;
                _position = null;
            }
        }

        public bool AnimateToPoint(PointF point)
        {
            return TryApplyValue(point);
        }

        public bool AnimateToRelativeOffset(PointF point)
        {
            return TryApplyValue(_targetPoint + point);
        }

        public void Dispose()
        {
            AnimationCommit?.Dispose();
            _x?.Dispose();
            _y?.Dispose();
            AnimationCommit = null;
            _x = null;
            _y = null;
            _visual = null;
            _graphicsDevice = null;
        }
    }
}
